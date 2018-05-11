using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DryEyes : MonoBehaviour {
	Transform lTarget, rTarget;
	Material rBlinderMat, lBlinderMat, rBlinderMatBlack, lBlinderMatBlack;
	Transform rLid, lLid;
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
	public float blinkDarkness = 0.2f;
	float origBlinkZ;
	public bool blinking = false;
	public bool distBlur = false;
	public float xResL = 256f;
	public float yResL = 288f;
	public float xResR = 256f;
	public float yResR = 288f;
	public float minRes = 20f;
	// Use this for initialization
	void Start () {
		lTarget = GameObject.Find("LTarget").transform;
		rTarget = GameObject.Find("RTarget").transform;
		rLid = GameObject.Find("RLid").transform;
		lLid = GameObject.Find("LLid").transform;
		rBlinderMat = GameObject.Find("RLid").GetComponent<Renderer>().material;
		lBlinderMat = GameObject.Find("LLid").GetComponent<Renderer>().material;
		rBlinderMatBlack = GameObject.Find("RBlinderBlack").GetComponent<Renderer>().material;
		lBlinderMatBlack = GameObject.Find("LBlinderBlack").GetComponent<Renderer>().material;
		origBlinkZ = GameObject.Find("RLid").transform.localScale.z;
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

		//rBlinderMatBlack.color = new Color(rBlinderMat.color.r, rBlinderMat.color.g, rBlinderMat.color.b, newA);
		//lBlinderMatBlack.color = new Color(rBlinderMat.color.r, rBlinderMat.color.g, rBlinderMat.color.b, newA);
		if (Input.GetKeyDown(KeyCode.Space)) {
			newA = blink();
			//GetComponent<ShameMove>().move = false;
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			StartCoroutine (BlinkUp (rLid, lLid, 0.3f, origBlinkZ, blinkZScale, rBlinderMatBlack, lBlinderMatBlack, blinkDarkness));
			blinking = false;
			//GetComponent<ShameMove>().move = true;
		}

		if (distBlur){
			if (Mathf.Abs(eyeDist - pEyeDist) > 0.1f && !GetComponent<PixelIntroOutro>().enabled) {
				float newW = maxW / Mathf.Clamp((1f + (7f * (eyeDist / 5f))), 1f, 8f); //eye distance maxing out at 5
				float newH = maxH /  Mathf.Clamp((1f + (7f * (eyeDist / 5f))), 1f, 8f);
				Debug.Log("(" + newW + ", " + newH + ")");
				updateRes(newW, newH);
			}
		}
	}
	public void updateRes(float w, float h){

		//Debug.Log("(" + newW + ", " + newH + ")");
		//update resolution of render textures
		RenderTexture newLeft = new RenderTexture( (int)w, (int)h, 16, RenderTextureFormat.ARGBFloat );
		RenderTexture newRight = new RenderTexture( (int)w, (int)h, 16, RenderTextureFormat.ARGBFloat );
		newLeft.filterMode = FilterMode.Point;
		newRight.filterMode = FilterMode.Point;
		lCam.targetTexture = newLeft;
		lScreen.texture = newLeft;
		rCam.targetTexture = newRight;
		rScreen.texture = newRight;

		eyeDist = pEyeDist;

	}
	public void updateRes(int eyeIndex, float w, float h){

		if (xResL < minRes || xResR < minRes || yResL < minRes || yResR < minRes) {
			GetComponent<CollisionSound>().restartStuff();
		} else {


		RenderTexture newRend = new RenderTexture( (int)w, (int)h, 16, RenderTextureFormat.ARGBFloat );
		newRend.filterMode = FilterMode.Point;
		if (eyeIndex == 0){
				lCam.targetTexture = newRend;
				lScreen.texture = newRend;
			} else if (eyeIndex == 1){
				rCam.targetTexture = newRend;
				rScreen.texture = newRend;
			}
		}
	}
	float blink(){
		//reset to original position
		//lTarget.localPosition = lOrigPos;
		//rTarget.localPosition = rOrigPos;
		audSrc.PlayOneShot(blinkSound);
		blinking = true;
		if (!distBlur){
			xResL = (xResL + xResR) / 2f;
			xResR = xResL;
			yResL = (yResL + yResR) / 2f;
			yResR = yResL;

			updateRes(0, xResL, yResL);
			updateRes(1, xResR, yResR);
		} 


		if (blinkAvgPos){
			//reset to average position
			Vector3 avgOffset = ((lTarget.localPosition - lOrigPos) + (rTarget.localPosition - rOrigPos));
			//Debug.Log("avgOffset = " + avgOffset);
			lTarget.localPosition = lOrigPos + avgOffset;
			rTarget.localPosition = rOrigPos + avgOffset;
		}
		lTarget.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		rTarget.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		//resets to visible?
		StartCoroutine (BlinkDown (rLid, lLid, 0.3f, origBlinkZ, blinkZScale, rBlinderMatBlack, lBlinderMatBlack, blinkDarkness));
		return 0f;
	}

	public static IEnumerator BlinkUp (Transform rBlinder, Transform lBlinder, float duration, float origScale, float targetScale, Material left, Material right, float blinkDarkness) {

		float elapsed = 0.0f;
		//Transform rBlinder = GameObject.Find("RLid").transform;
		//Transform lBlinder = GameObject.Find("LLid").transform;


		while (elapsed < duration) {

			elapsed += Time.deltaTime;   
 
			if (elapsed < duration){

				rBlinder.localScale = Vector3.Lerp(new Vector3 (rBlinder.transform.localScale.x, rBlinder.transform.localScale.y, targetScale), new Vector3 (rBlinder.transform.localScale.x, rBlinder.transform.localScale.y, origScale), (elapsed) / duration );
				lBlinder.localScale = Vector3.Lerp(new Vector3 (lBlinder.transform.localScale.x, lBlinder.transform.localScale.y, targetScale), new Vector3 (lBlinder.transform.localScale.x, lBlinder.transform.localScale.y, origScale), (elapsed) / duration );
				right.color = new Color(right.color.r, right.color.g, right.color.b, 1f - (elapsed) / duration - blinkDarkness);	//0.1 to keep it a little bit visible
				left.color = new Color(left.color.r, left.color.g, left.color.b, 1f - (elapsed) / duration - blinkDarkness);	
			}
			yield return null;
		}
	}
	public static IEnumerator BlinkDown(Transform rBlinder, Transform lBlinder, float duration, float origScale, float targetScale, Material left, Material right, float blinkDarkness) {

		float elapsed = 0.0f;
		//Transform rBlinder = GameObject.Find("RBlinder").transform;
		//Transform lBlinder = GameObject.Find("LBlinder").transform;


		while (elapsed < duration) {

			elapsed += Time.deltaTime;   
 
			if (elapsed < duration){
				rBlinder.localScale = Vector3.Lerp(new Vector3 (rBlinder.transform.localScale.x, rBlinder.transform.localScale.y, origScale), new Vector3 (rBlinder.transform.localScale.x, rBlinder.transform.localScale.y, targetScale), (elapsed) / duration );
				lBlinder.localScale = Vector3.Lerp(new Vector3 (lBlinder.transform.localScale.x, lBlinder.transform.localScale.y, origScale), new Vector3 (lBlinder.transform.localScale.x, lBlinder.transform.localScale.y, targetScale), (elapsed) / duration );
				right.color = new Color(right.color.r, right.color.g, right.color.b,(elapsed) / duration - blinkDarkness);	
				left.color = new Color(left.color.r, left.color.g, left.color.b,  (elapsed) / duration - blinkDarkness);	
			}

			yield return null;
		}
	}
}
