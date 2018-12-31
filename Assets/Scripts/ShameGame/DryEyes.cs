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
	public Camera lCam, rCam;
	RawImage lScreen, rScreen;
	public bool autoDry = false;
	public bool blinkAvgPos = false;
	public float blinkZScale = 0.021f;
	public float blinkDarkness = 0.2f;
	float origBlinkZ;
	public static bool blinking = false;
	public bool distBlur = false;
	public float xResL = 256f;
	public float yResL = 288f;
	public float xResR = 256f;
	public float yResR = 288f;
	public float minRes = 20f;
	public float maxResMulti = 1f;
	public float eyeDistThresh = 0.5f;
	public static float lifeAmt = 1f;
	public static float minLife = 0.03f;
	public bool autoRecovery = true;
	public float autoRecoveryRate = 0.1f;
	bool blinkDown = false;
	float blinkStart = 0f;
	float pct = 0f;
	public float blinkSpeed = 30f;
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
		if (lCam == null) lCam = GameObject.Find("LCam").GetComponent<Camera>();
		if (rCam == null) rCam = GameObject.Find("RCam").GetComponent<Camera>();
		lScreen = GameObject.Find("LScreen").GetComponent<RawImage>();
		rScreen = GameObject.Find("RScreen").GetComponent<RawImage>();
		lTexture = lCam.targetTexture;
		rTexture = rCam.targetTexture;
		maxW = lTexture.width * maxResMulti;
		maxH = lTexture.height * maxResMulti;

	}
	
	// Update is called once per frame
	void Update () {
		maxW = lTexture.width * maxResMulti;
		maxH = lTexture.height * maxResMulti;
		
		float lifeL =  ((xResL / (maxW - minRes)) + (yResL / (maxH - minRes))) / 2f;
		float lifeR = ((xResR / (maxW - minRes)) + (yResR / (maxH - minRes))) / 2f;
		lifeAmt = (Mathf.Min(lifeL, lifeR) * 2f) - minLife;
		//Debug.Log("lifeAmt = " + lifeAmt + " LLife = " + lifeL + " RLife = " + lifeR);
		if (lifeAmt < 0f) GetComponent<CollisionSound>().restartStuff();
		if (autoRecovery) {
			xResL += (maxW - xResL) * autoRecoveryRate * Time.deltaTime;
			yResL += (maxH - yResL) * autoRecoveryRate * Time.deltaTime;
			xResR += (maxW - xResR) * autoRecoveryRate * Time.deltaTime;
			yResR += (maxH - yResR) * autoRecoveryRate * Time.deltaTime;
			xResL = Mathf.Min(xResL, maxW);
			yResL = Mathf.Min(yResL, maxH);
			xResR = Mathf.Min(xResR, maxW);
			yResR = Mathf.Min(yResR, maxH);
			if (Time.frameCount % 10 == 0) { //update ~6 times a second
				updateRes(0, xResL, yResL);
				updateRes(1, xResR, yResR);
			}
		}

		eyeDist = Vector3.Distance(rTarget.position, lTarget.position) - initialDist;
		//Debug.Log("eyeDist = " + eyeDist);
		if (eyeDist > eyeDistThresh){
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

            //StopAllCoroutines();
			//rLid.localScale = new Vector3 (rLid.transform.localScale.x, rLid.transform.localScale.y, origBlinkZ);
			//lLid.localScale = new Vector3 (lLid.transform.localScale.x, lLid.transform.localScale.y, origBlinkZ);
			newA = blink();
			//GetComponent<ShameMove>().move = false;
		}
		if (Input.GetKeyUp(KeyCode.Space)) {

            //StopAllCoroutines();
			//StartCoroutine (BlinkUp (rLid, lLid, 0.3f, origBlinkZ, blinkZScale, rBlinderMatBlack, lBlinderMatBlack, blinkDarkness));
			blinking = false;
			blinkDown = false;
			//GetComponent<ShameMove>().move = true;
		}


		if (blinkDown) {
			if (pct < 1f) pct += Time.deltaTime * blinkSpeed;
		} else {
			if (pct > 0f) pct -= Time.deltaTime * blinkSpeed;
		}


		newBlink(); 
	

		if (distBlur){
			if (Mathf.Abs(eyeDist - pEyeDist) > 0.1f && !GetComponent<PixelIntroOutro>().enabled) {
				float newW = xResL / Mathf.Clamp((1f + (7f * (eyeDist / eyeDistThresh))), 1f, 8f); //eye distance maxing out at 5
				float newH = yResL /  Mathf.Clamp((1f + (7f * (eyeDist / eyeDistThresh))), 1f, 8f);
				//Debug.Log("DistBlur LEFT (" + newW + ", " + newH + ")");
				updateRes(0, newW, newH);

				newW = xResR / Mathf.Clamp((1f + (7f * (eyeDist / eyeDistThresh))), 1f, 8f); //eye distance maxing out at 5
				newH = yResR /  Mathf.Clamp((1f + (7f * (eyeDist / eyeDistThresh))), 1f, 8f);
				//Debug.Log("DistBlur RIGHT (" + newW + ", " + newH + ")");
				updateRes(1, newW, newH);
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

		//if (xResL < minRes || xResR < minRes || yResL < minRes || yResR < minRes) {
			//if the resolution gets really bad, player loses
		//	GetComponent<CollisionSound>().restartStuff();
		//} else {

		//ScalableBufferManager.ResizeBuffers(w / maxW, h / maxH);
		

		//RenderTexture newRend = new RenderTexture( (int)w, (int)h, 16, RenderTextureFormat.ARGBFloat );
		RenderTexture newRend = new RenderTexture( (int)w, (int)h, 16, RenderTextureFormat.ARGB32 );
		newRend.filterMode = FilterMode.Point;
		if (eyeIndex == 0){
				lCam.targetTexture = newRend;
				lScreen.texture = newRend;
			} else if (eyeIndex == 1){
				rCam.targetTexture = newRend;
				rScreen.texture = newRend;
			}
		
		

	}
	float blink(){
		//reset to original position
		//lTarget.localPosition = lOrigPos;
		//rTarget.localPosition = rOrigPos;
		audSrc.PlayOneShot(blinkSound);
		blinking = true;
		//if (!distBlur){
			xResL = (xResL + xResR) / 2f;
			xResR = xResL;
			yResL = (yResL + yResR) / 2f;
			yResR = yResL;

			updateRes(0, xResL, yResL);
			updateRes(1, xResR, yResR);
		//} 


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
		//rLid.localScale = new Vector3 (rLid.transform.localScale.x, rLid.transform.localScale.y, origBlinkZ);
		//lLid.localScale = new Vector3 (lLid.transform.localScale.x, lLid.transform.localScale.y, origBlinkZ);
		blinkStart = Time.time;
		blinkDown = true;
		//StartCoroutine (BlinkDown (rLid, lLid, 0.3f, origBlinkZ, blinkZScale, rBlinderMatBlack, lBlinderMatBlack, blinkDarkness));
		return 0f;
	}

	public static IEnumerator BlinkUp (Transform rBlinder, Transform lBlinder, float duration, float origScale, float targetScale, Material left, Material right, float blinkDarkness) {

		float elapsed = 0.0f;
		//Transform rBlinder = GameObject.Find("RLid").transform;
		//Transform lBlinder = GameObject.Find("LLid").transform;

		rBlinder.localScale = new Vector3 (rBlinder.transform.localScale.x, rBlinder.transform.localScale.y, targetScale);
		lBlinder.localScale = new Vector3 (lBlinder.transform.localScale.x, lBlinder.transform.localScale.y, targetScale);

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

	void newBlink(){
  
 				rLid.localScale = Vector3.Lerp(new Vector3 (rLid.transform.localScale.x, rLid.transform.localScale.y, origBlinkZ), new Vector3 (rLid.transform.localScale.x, rLid.transform.localScale.y, blinkZScale), pct);
				lLid.localScale = Vector3.Lerp(new Vector3 (lLid.transform.localScale.x, lLid.transform.localScale.y, origBlinkZ), new Vector3 (lLid.transform.localScale.x, lLid.transform.localScale.y, blinkZScale), pct);
				rBlinderMatBlack.color = new Color(rBlinderMatBlack.color.r, rBlinderMatBlack.color.g, rBlinderMatBlack.color.b, pct - blinkDarkness);	
				lBlinderMatBlack.color = new Color(lBlinderMatBlack.color.r, lBlinderMatBlack.color.g, lBlinderMatBlack.color.b, pct - blinkDarkness);	

	}
	public static IEnumerator BlinkDown(Transform rBlinder, Transform lBlinder, float duration, float origScale, float targetScale, Material left, Material right, float blinkDarkness) {

		float elapsed = 0.0f;
		//Transform rBlinder = GameObject.Find("RBlinder").transform;
		//Transform lBlinder = GameObject.Find("LBlinder").transform;

		//rBlinder.localScale = new Vector3 (rBlinder.transform.localScale.x, rBlinder.transform.localScale.y, origScale);
		//lBlinder.localScale = new Vector3 (lBlinder.transform.localScale.x, lBlinder.transform.localScale.y, origScale);

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
