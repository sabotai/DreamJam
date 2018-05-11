using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PixelIntroOutro : MonoBehaviour {

	public RawImage[] canvasImages;
	public Camera[] cams;
	public float pct = 1f;
	public bool intro = true;
	public float minWidth = 16;
	public float minHeight = 9;
	public float speed = 0.5f;
	RenderTexture lTexture, rTexture;
	float maxW, maxH;
	// Use this for initialization
	void Start () {
		if (intro) pct = 0f; else pct = 1f;
		lTexture = cams[0].targetTexture;
		rTexture = cams[1].targetTexture;
		
		maxW = lTexture.width;
		maxH = lTexture.height;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (intro){
			if (pct < 1f){
				updateRes(minWidth, minHeight, maxW, maxH, pct);
				pct += Time.deltaTime * speed;
			} else {
				GetComponent<ShameMove>().enabled = true;
				intro = false;
				enabled = false;
			}
		} else {
			if (pct > 0f){
				updateRes(minWidth, minHeight, maxW, maxH, pct);
				updateRes(minWidth, minHeight, maxW, maxH, pct);
				pct -= Time.deltaTime * speed;
			} else {
				
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}


	void updateRes(float _minWidth, float _minHeight, float _maxWidth, float _maxHeight, float _pct){

		float newW = Mathf.Lerp(_minWidth, _maxWidth, _pct);
		float newH = Mathf.Lerp(_minHeight, _maxHeight, _pct);
		

		for (int i = 0; i < cams.Length; i++){
		//update resolution of render textures

			//prevent it from restarting cycle at high res
			if (cams[i].targetTexture.width >= newW || intro){// && cams[i].targetTexture.height >= newH){
				RenderTexture newRT = new RenderTexture( (int)newW, (int)newH, 16, RenderTextureFormat.ARGBFloat );
				newRT.filterMode = FilterMode.Point;
				cams[i].targetTexture = newRT;
				canvasImages[i].texture = newRT;
			}
		}

	}

}
