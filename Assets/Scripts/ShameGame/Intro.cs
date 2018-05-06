using System.Collections;
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
	public AudioClip clipSwitch, clipBag, clipDissolve;
	public Material sky;
	public Color origSky;
	public Light dirLight;
	public GameObject[] textObjects;
	// Use this for initialization
	void Start () {
		audSrc = GetComponent< AudioSource>();
		sky.color = new Color(245f/255f, 78f/255f, 78f/255f, 1f);
		origSky = sky.color;
		dirLight = GameObject.Find("Directional Light").GetComponent<Light>();

		state -= textObjects.Length;
	}
	
	// Update is called once per frame
	void Update () {
		if (state > -textObjects.Length && state < 1){
			textObjects[textObjects.Length + state - 1].SetActive(true);
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
		} else if (state >= 4){
			if (audSrc.clip != clipDissolve){
				audSrc.clip = clipDissolve;
				audSrc.Play();
			}
			updateRes(2, 1, 1024, 576, pct);
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
			sky.color = origSky;
			dirLight.color = sky.color;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

		}
	}

		void updateRes(float minWidth, float minHeight, float maxWidth, float maxHeight, float pct){

		float newW = Mathf.Lerp(minWidth, maxWidth, pct);
		float newH = Mathf.Lerp(minHeight, maxHeight, pct);
		
		//update resolution of render textures
		RenderTexture newRT = new RenderTexture( (int)newW, (int)newH, 16, RenderTextureFormat.ARGBFloat );
		newRT.filterMode = FilterMode.Point;
		Camera.main.targetTexture = newRT;
		canvasImage.texture = newRT;

	}
}
