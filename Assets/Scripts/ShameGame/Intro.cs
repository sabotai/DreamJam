﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

	public GameObject bag;
	public GameObject insideLight;
	public RawImage canvasImage;
	int state = 0;
	public float pct = 1f;
	AudioSource audSrc;
	public AudioClip clipSwitch, clipBag, clipDissolve, clipChime;
	public Material sky;
	public Color origSky;
	public Light dirLight;
	public GameObject[] textObjects, textObjects2;
	public GameObject room, laundromat;
	public Camera origCam, newCam;
	int finalState = 14;
	// Use this for initialization
	void Start () {
		audSrc = GetComponent< AudioSource>();
		sky.color = new Color(245f/255f, 78f/255f, 78f/255f, 1f);
		origSky = sky.color;
		dirLight = GameObject.Find("Directional Light 2").GetComponent<Light>();

		state -= textObjects.Length;
		//origCam = Camera.main;
		//newCam = GameObject.Find("LaundryCam").GetComponent<Camera>();
		laundromat.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (state > -textObjects.Length && state < 1){
			if (state == -textObjects.Length + 1){
				textObjects[textObjects.Length + state - 1].SetActive(true);
			} else {
				textObjects[0].SetActive(false);
				textObjects[textObjects.Length + state - 1].SetActive(true);
			}
		} else if (state == 1){
			for (int i = 0; i < textObjects.Length; i++){
				textObjects[i].SetActive(false);
			}
		} else if(state == 2){
			bag.SetActive(false);
			if (audSrc.clip != clipBag){
				audSrc.clip = clipBag;
				audSrc.Play();
			}
		} else if (state == 3){
			insideLight.SetActive(false);
			if (audSrc.clip != clipSwitch){
				audSrc.clip = clipSwitch;
				audSrc.Play();
			}
		} else if (state == 4){
				updateRes(origCam, 2, 1, 1024, 576, pct);
				//sky.SetColor("_MainColor", Color.Lerp(Color.red, sky.GetColor("_MainColor"), pct));
				pct -= (Time.deltaTime / 2f);
				Debug.Log("pct = " + pct);
		} else if (state == 5){
				updateRes(newCam, 2, 1, 1024, 576, pct);
				//sky.SetColor("_MainColor", Color.Lerp(Color.red, sky.GetColor("_MainColor"), pct));
				//sky.color = Color.Lerp(Color.red, origSky, pct);
				//dirLight.color = sky.color;
				if (pct < 1f) {
					pct += (Time.deltaTime / 2f);
					} else {state++;}
				Debug.Log("pct = " + pct);
		} else if (state == 6){

			textObjects2[0].SetActive(true);
		} else if (state == 7){

			textObjects2[1].SetActive(true);
		} else if (state == 8){
			textObjects2[2].SetActive(true);
		} else if (state == 9){
			textObjects2[3].SetActive(true);
		} else if (state == 10){

			textObjects2[0].SetActive(false);
			textObjects2[1].SetActive(false);
			textObjects2[2].SetActive(false);
			textObjects2[3].SetActive(false);
			textObjects2[4].SetActive(true);
		} else if (state == 11){
			textObjects2[5].SetActive(true);
		} else if (state == 12){
			textObjects2[6].SetActive(true);
		} else if (state == 13){
			for (int i = 0; i < textObjects2.Length; i++){
				textObjects2[i].SetActive(false);
			}
			if (audSrc.clip != clipChime){
				audSrc.clip = clipChime;
				audSrc.Play();
			}
		} else if (state >= finalState){
			if (audSrc.clip != clipDissolve){
				//pct = 1f;
				audSrc.clip = clipDissolve;
				audSrc.Play();
			}
				updateRes(newCam, 2, 1, 1024, 576, pct);
				//sky.SetColor("_MainColor", Color.Lerp(Color.red, sky.GetColor("_MainColor"), pct));
				sky.color = Color.Lerp(Color.red, origSky, pct);
				dirLight.color = sky.color;
				pct -= (Time.deltaTime / 2f);
				Debug.Log("pct = " + pct);
		}
		if (Input.GetKeyDown(KeyCode.Space)){
			state++;
		}

		if (pct < 0f) {

			if (state < finalState - 1){
			
				if (origCam.enabled){
					room.SetActive(false); 
					origCam.enabled = false;
					laundromat.SetActive(true);
					state++;
				}
			} else {

			sky.color = origSky;
			dirLight.color = sky.color;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}
		}
	}

		void updateRes(Camera cam, float minWidth, float minHeight, float maxWidth, float maxHeight, float pct){

		float newW = Mathf.Lerp(minWidth, maxWidth, pct);
		float newH = Mathf.Lerp(minHeight, maxHeight, pct);
		
		//update resolution of render textures
		RenderTexture newRT = new RenderTexture( (int)newW, (int)newH, 16, RenderTextureFormat.ARGBFloat );
		newRT.filterMode = FilterMode.Point;
		cam.targetTexture = newRT;
		canvasImage.texture = newRT;

	}
}
