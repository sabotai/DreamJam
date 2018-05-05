using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixelIntroOutro : MonoBehaviour {

	public RawImage[] canvasImages;
	public Camera[] cams;
	public float pct = 1f;
	public bool intro = true;
	public float minWidth = 16;
	public float minHeight = 9;
	public float maxWidth = 512;
	public float maxHeight = 288;
	public float speed = 0.5f;
	// Use this for initialization
	void Start () {
		if (intro) pct = 0f;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (pct < 1f){
			updateRes(minWidth, minHeight, maxWidth, maxHeight, pct);
			pct += Time.deltaTime * speed;
		} else {
			enabled = false;
		}
	}


	void updateRes(float _minWidth, float _minHeight, float _maxWidth, float _maxHeight, float _pct){

		float newW = Mathf.Lerp(_minWidth, _maxWidth, _pct);
		float newH = Mathf.Lerp(_minHeight, _maxHeight, _pct);
		
		//update resolution of render textures
		RenderTexture newRT = new RenderTexture( (int)newW, (int)newH, 16, RenderTextureFormat.ARGBFloat );
		newRT.filterMode = FilterMode.Point;

		for (int i = 0; i < cams.Length; i++){
			cams[i].targetTexture = newRT;
			canvasImages[i].texture = newRT;
		}

	}
}
