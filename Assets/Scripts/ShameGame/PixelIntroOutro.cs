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
	public GameObject instructions;
	public Color startColor, endColor;
	public AudioClip musicClip;
	public GameObject streetScene;
	public AudioSource musicPlayer;

	// Use this for initialization
	void Start () {
       		//AudioListener.volume = 1f;
			AudioListener.pause = false;
					AudioListener.volume = 0.25f;
		if (Settings.timesPlayed() != 0) {
       		//musicPlayer.clip = musicClip;
       	}  
		streetScene.SetActive(false);
		if (!Settings.instructions()) {
			clearInstructions();
		}
			//musicPlayer.Play();
		if (intro) pct = 0f; else pct = 1f;
		lTexture = cams[0].targetTexture;
		rTexture = cams[1].targetTexture;
		
		maxW = lTexture.width;
		maxH = lTexture.height;
		
		updateRes(minWidth, minHeight, maxW, maxH, 0.0f);
		//AudioListener.pause = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (intro){
			if (pct < 1f){
				if (Input.GetKeyDown(KeyCode.Space)){
					clearInstructions();
				} else if (!instructions.activeSelf){
					updateRes(minWidth, minHeight, maxW, maxH, pct);
					pct += Time.deltaTime * speed;
				}
			} else {
				GetComponent<DryEyes>().enabled = true;
				GetComponent<ShameMove>().enabled = true;
				GetComponent<ShameMove>().move = true;
				intro = false;
				enabled = false;
			}
		} else {
			if (pct > 0f){
				updateRes(minWidth, minHeight, maxW, maxH, pct);
				updateRes(minWidth, minHeight, maxW, maxH, pct);
				pct -= Time.deltaTime * speed;
				if (pct < 0.05f && Settings.instructions()) {

					instructions.transform.parent.gameObject.GetComponent<Image>().color = Color.Lerp(startColor, endColor, pct * 20f);
       				AudioListener.volume = pct * 30f;
       			} else {
       				instructions.transform.parent.gameObject.GetComponent<Image>().color = endColor;
       				AudioListener.volume = 1f;
       			}
			} else {
				
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

	void clearInstructions(){
		streetScene.SetActive(true);
       		musicPlayer.clip = musicClip;
					instructions.SetActive(false);
					instructions.transform.parent.gameObject.GetComponent<Image>().color = endColor;
					AudioListener.pause = false;
					musicPlayer.Play();
       				AudioListener.volume = 1f;
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
