using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DryEyes : MonoBehaviour {
	Transform lTarget, rTarget;
	Material rBlinderMat, lBlinderMat;
	Transform rBlinder, lBlinder;
	float initialDist, eyeDist, pEyeDist;
	public float speed = 1f;
	Vector3 lOrigPos, rOrigPos;
	float newA;
	public AudioClip blinkSound;
	public AudioSource audSrc, audSrcL, audSrcR;
	RenderTexture lTexture, rTexture;
	float maxW, maxH;
	Camera lCam, rCam;
	RawImage lScreen, rScreen;
	public bool autoDry = false;
	public bool blinkAvgPos = false;
	public float blinkZScale = 0.021f;
	float origBlinkZ;
	// Use this for initialization
	void Start () {
		lTarget = GameObject.Find("LTarget").transform;
		rTarget = GameObject.Find("RTarget").transform;
		rBlinder = GameObject.Find("RBlinder").transform;
		lBlinder = GameObject.Find("LBlinder").transform;
		rBlinderMat = GameObject.Find("RBlinder").GetComponent<Renderer>().material;
		lBlinderMat = GameObject.Find("LBlinder").GetComponent<Renderer>().material;
		origBlinkZ = GameObject.Find("RBlinder").transform.localScale.z;
		initialDist = Vector3.Distance(rTarget.position, lTarget.position);
		lOrigPos = lTarget.localPosition;
		rOrigPos = rTarget.localPosition;
		newA = 0f;
		lCam = GameObject.Find("LCam").GetComponent<Camera>();
		rCam = GameObject.Find("RCam").GetComponent<Camera>();
		lScreen = GameObject.Find("LScreen").GetComponent<RawImage>();
		rScreen = GameObject.Find("RScreen").GetComponent<RawImage>();
		lTexture = lCam.targetTexture;
		rTexture = rCam.targetTexture;
		maxW = lTexture.width;
		maxH = lTexture.height;

	}
	
	// Update is called once per frame
	void Update () {
		eyeDist = Vector3.Distance(rTarget.position, lTarget.position) - initialDist;
		//Debug.Log("eyeDist = " + eyeDist);
		if (eyeDist > 5f){
			newA = rBlinderMat.color.a + speed * Time.deltaTime;

		} else {
			//if (rBlinder.color.a > 0f)	newA = rBlinder.color.a - speed * Time.deltaTime;
			//else newA = 0f;
			//degrade automatically to reward blink
			if (autoDry) newA += (speed / 30f) * Time.deltaTime;
		}
		audSrcR.volume = newA * 0.5f;
		audSrcL.volume = newA * 0.5f;

		//rBlinderMat.color = new Color(rBlinderMat.color.r, rBlinderMat.color.g, rBlinderMat.color.b, newA);
		//lBlinderMat.color = new Color(rBlinderMat.color.r, rBlinderMat.color.g, rBlinderMat.color.b, newA);
		if (Input.GetKeyDown(KeyCode.Space)) newA = blink();
		if (Input.GetKeyUp(KeyCode.Space)) 		StartCoroutine (BlinkUp (0.3f, origBlinkZ, blinkZScale));


		if (Mathf.Abs(eyeDist - pEyeDist) > 0.1f && !GetComponent<PixelIntroOutro>().enabled) updateRes();
	}
	void updateRes(){

		float newW = maxW / Mathf.Clamp((1f + (7f * (eyeDist / 5f))), 1f, 8f); //eye distance maxing out at 5
		float newH = maxH /  Mathf.Clamp((1f + (7f * (eyeDist / 5f))), 1f, 8f);
		//Debug.Log("(" + newW + ", " + newH + ")");
		//update resolution of render textures
		RenderTexture newLeft = new RenderTexture( (int)newW, (int)newH, 16, RenderTextureFormat.ARGBFloat );
		RenderTexture newRight = new RenderTexture( (int)newW, (int)newH, 16, RenderTextureFormat.ARGBFloat );
		newLeft.filterMode = FilterMode.Point;
		newRight.filterMode = FilterMode.Point;
		lCam.targetTexture = newLeft;
		lScreen.texture = newLeft;
		rCam.targetTexture = newRight;
		rScreen.texture = newRight;

		eyeDist = pEyeDist;

	}
	float blink(){
		//reset to original position
		//lTarget.localPosition = lOrigPos;
		//rTarget.localPosition = rOrigPos;
		audSrc.PlayOneShot(blinkSound);

		if (blinkAvgPos){
			//reset to average position
			Vector3 avgOffset = ((lTarget.localPosition - lOrigPos) + (rTarget.localPosition - rOrigPos));
			//Debug.Log("avgOffset = " + avgOffset);
			lTarget.localPosition = lOrigPos + avgOffset;
			rTarget.localPosition = rOrigPos + avgOffset;
		}
		lTarget.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		rTarget.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		//rBlinderMat.color = new Color(rBlinderMat.color.r, rBlinderMat.color.g, rBlinderMat.color.b, 1f);
		//rBlinderMat.color = new Color(rBlinderMat.color.r, rBlinderMat.color.g, rBlinderMat.color.b, 1f);
		StartCoroutine (BlinkDown (0.3f, origBlinkZ, blinkZScale));
		return 0f;
	}

	public static IEnumerator BlinkUp(float duration, float origScale, float targetScale) {

		float elapsed = 0.0f;
		Transform rBlinder = GameObject.Find("RBlinder").transform;
		Transform lBlinder = GameObject.Find("LBlinder").transform;


		while (elapsed < duration) {

			elapsed += Time.deltaTime;   
 
			if (elapsed < duration){

				rBlinder.localScale = Vector3.Slerp(new Vector3 (rBlinder.transform.localScale.x, rBlinder.transform.localScale.y, targetScale), new Vector3 (rBlinder.transform.localScale.x, rBlinder.transform.localScale.y, origScale), (elapsed) / duration );
				lBlinder.localScale = Vector3.Slerp(new Vector3 (rBlinder.transform.localScale.x, rBlinder.transform.localScale.y, targetScale), new Vector3 (rBlinder.transform.localScale.x, rBlinder.transform.localScale.y, origScale), (elapsed) / duration );
	
			}
			yield return null;
		}
	}
	public static IEnumerator BlinkDown(float duration, float origScale, float targetScale) {

		float elapsed = 0.0f;
		Transform rBlinder = GameObject.Find("RBlinder").transform;
		Transform lBlinder = GameObject.Find("LBlinder").transform;


		while (elapsed < duration) {

			elapsed += Time.deltaTime;   
 
			if (elapsed < duration){
				rBlinder.localScale = Vector3.Slerp(new Vector3 (rBlinder.transform.localScale.x, rBlinder.transform.localScale.y, origScale), new Vector3 (rBlinder.transform.localScale.x, rBlinder.transform.localScale.y, targetScale), (elapsed) / duration );
				lBlinder.localScale = Vector3.Slerp(new Vector3 (rBlinder.transform.localScale.x, rBlinder.transform.localScale.y, origScale), new Vector3 (rBlinder.transform.localScale.x, rBlinder.transform.localScale.y, targetScale), (elapsed) / duration );
			}

			yield return null;
		}
	}
}
