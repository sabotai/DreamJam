using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DryEyes : MonoBehaviour {
	Transform lTarget, rTarget;
	Material rBlinder, lBlinder;
	float initialDist, eyeDist, pEyeDist;
	public float speed = 1f;
	Vector3 lOrigPos, rOrigPos;
	float newA;
	public AudioClip blinkSound;
	public AudioSource audSrc, audSrcL, audSrcR;
	public RenderTexture lTexture, rTexture;
	Camera lCam, rCam;
	RawImage lScreen, rScreen;
	public bool autoDry = false;
	public bool blinkAvgPos = false;
	// Use this for initialization
	void Start () {
		lTarget = GameObject.Find("LTarget").transform;
		rTarget = GameObject.Find("RTarget").transform;
		rBlinder = GameObject.Find("RBlinder").GetComponent<Renderer>().material;
		lBlinder = GameObject.Find("LBlinder").GetComponent<Renderer>().material;
		initialDist = Vector3.Distance(rTarget.position, lTarget.position);
		lOrigPos = lTarget.localPosition;
		rOrigPos = rTarget.localPosition;
		newA = 0f;
		lCam = GameObject.Find("LCam").GetComponent<Camera>();
		rCam = GameObject.Find("RCam").GetComponent<Camera>();
		lScreen = GameObject.Find("LScreen").GetComponent<RawImage>();
		rScreen = GameObject.Find("RScreen").GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {
		eyeDist = Vector3.Distance(rTarget.position, lTarget.position) - initialDist;
		//Debug.Log("eyeDist = " + eyeDist);
		if (eyeDist > 5f){
			newA = rBlinder.color.a + speed * Time.deltaTime;

		} else {
			//if (rBlinder.color.a > 0f)	newA = rBlinder.color.a - speed * Time.deltaTime;
			//else newA = 0f;
			//degrade automatically to reward blink
			if (autoDry) newA += (speed / 30f) * Time.deltaTime;
		}
		audSrcR.volume = newA * 0.5f;
		audSrcL.volume = newA * 0.5f;

		rBlinder.color = new Color(rBlinder.color.r, rBlinder.color.g, rBlinder.color.b, newA);
		lBlinder.color = new Color(rBlinder.color.r, rBlinder.color.g, rBlinder.color.b, newA);
		if (Input.GetKeyDown(KeyCode.Space)) newA = blink();


		if (Mathf.Abs(eyeDist - pEyeDist) > 0.1f) updateRes();
	}
	void updateRes(){

		float newW = 512f / Mathf.Clamp((1f + (4f * (eyeDist / 5f))), 1f, 4f);
		float newH = 288f /  Mathf.Clamp((1f + (4f * (eyeDist / 5f))), 1f, 4f);
		
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
		rBlinder.color = new Color(rBlinder.color.r, rBlinder.color.g, rBlinder.color.b, 1f);
		lBlinder.color = new Color(rBlinder.color.r, rBlinder.color.g, rBlinder.color.b, 1f);
		return 0f;
	}
}
