using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Outro : MonoBehaviour {

	
	public GameObject insideLight;
	public RawImage canvasImage;
	int state = 0;
	public float pct = 0f;
	AudioSource audSrc;
	public AudioClip clipSwitch, clipDissolve, clipCrying, clipMenu;
	public Material sky, origSky, redSky;
	public Cubemap skyCube;
	Color skyColor;
	public GameObject dirLight;
	public Transform dirDay, dirLate;
	public GameObject[] textObjects;
	public Camera origCam;
	int finalState = 14;
	public float skyPct = 0f;
	public float openingSpeed = 0.1f;
	float fastSpeed;
	public GameObject caption;
	public Text helperText;
	// Use this for initialization
	void Start () {
		audSrc = GetComponent< AudioSource>();
		skyColor = new Color(0f, 0.5f, 1f);//245f/255f, 78f/255f, 78f/255f, 1f);
		//sky = redSky;
		RenderSettings.skybox.Lerp(redSky, origSky, 0f);
		DynamicGI.UpdateEnvironment();
		//origSky = sky.color;
		state = -1;
		fastSpeed = openingSpeed * 15f;
		AudioListener.volume = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && state != -1 && state != 2) {
			state++;
			audSrc.PlayOneShot(clipMenu, 0.15f);
		}
		if (state == -1){

			if (pct < 1f){
				pct += (Time.deltaTime * openingSpeed);
				if (pct > 0.08875f) openingSpeed = fastSpeed;
				updateRes(origCam, 2, 1f, 1600f, 900f, pct);//1024f, 576f, pct);
			} else {
				if (Input.GetKeyDown(KeyCode.Space)) state++;
			}

			helperText.enabled = true;
		} else if (state == 0){ 
				if (audSrc.clip != clipSwitch){
					audSrc.clip = clipSwitch;
					audSrc.Play();
				}
				insideLight.SetActive(true);
			
			
		} else if (state == 1){
			textObjects[0].SetActive(true);
			
		} else if (state == 2){
			helperText.enabled = false;
			textObjects[0].SetActive(false);
			if (audSrc.clip != clipCrying){
				audSrc.clip = clipCrying;
				audSrc.loop = true;
				audSrc.Play();
				caption.SetActive(true);
			}
			if (skyPct < 1f){
				Color newColor = Color.Lerp(Color.red, skyColor, skyPct);
				//RenderSettings.skybox = sky;
                //RenderSettings.customReflection = skyCube;
				//sky.SetColor ("_TintColor", newColor);
				RenderSettings.skybox.Lerp(redSky, origSky, skyPct);
				DynamicGI.UpdateEnvironment();
				dirLight.transform.rotation = Quaternion.Slerp(dirDay.rotation, dirLate.rotation, skyPct);
				//dirLight.GetComponent<Light>().color = newColor;
				skyPct += (Time.deltaTime / 120f);

			}  else {
				caption.SetActive(false);
				state++;
			}
		} else if (state == 3){
			helperText.enabled = true;
				audSrc.Stop();
			textObjects[1].SetActive(true);
		} else if (state == 4){
			textObjects[2].SetActive(true);
		} else if (state == 5){
			textObjects[3].SetActive(true);
		} else if (state == 6){
			textObjects[4].SetActive(true);
		} else if (state == 7){
			textObjects[5].SetActive(true);
		}  else {
			for (int i = 0; i < 5; i++){
				textObjects[i].SetActive(false);
			}
			helperText.enabled = false;
			textObjects[6].SetActive(true);
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
