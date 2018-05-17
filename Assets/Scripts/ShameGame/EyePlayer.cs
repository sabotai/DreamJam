using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EyePlayer : MonoBehaviour {

	Rigidbody rb;
	public float forceAmt = 3f;
	public KeyCode fwdKey, backKey, rKey, lKey, blink;
	Vector3 orig;
	public float maxStray = 10f;
	AudioSource audSrc;
	public AudioClip clip_GazeMake;
	Transform lCam, rCam;
	public ShameMove moveScript;
	float gazeEscalation = 5f;
	public bool momentumBased = false;
	public DryEyes eyeMan;
	Vector3 origPosL, origPosR;
	GameObject playersObj;
	public float dist = 250f;
	public float phoneDist = 50f;

	// Use this for initialization
	void Start () {
		dist = 250f;
		phoneDist = 100f;
		rb = GetComponent<Rigidbody>();
		orig = transform.localPosition;
		audSrc = GetComponent<AudioSource>();
		lCam = GameObject.Find("LCam").transform;
		rCam = GameObject.Find("RCam").transform;
		origPosL = lCam.localPosition;
		origPosR = rCam.localPosition;
		playersObj = GameObject.Find("Players");
		if (eyeMan == null) eyeMan = GameObject.Find("Players").GetComponent<DryEyes>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//transform.forward = Vector3.Normalize(transform.localPosition - lCam.localPosition);
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, orig.z);
		if (Vector3.Distance(orig, transform.localPosition) <= maxStray){
			if (momentumBased){
				if (Input.GetKey(fwdKey)){
					rb.AddForce(transform.up * forceAmt * rb.mass);
				}
				if (Input.GetKey(lKey)){
					rb.AddForce(-transform.right * forceAmt * rb.mass);
				}
				if (Input.GetKey(backKey)){
					rb.AddForce(-transform.up * forceAmt * rb.mass);
				}
				if (Input.GetKey(rKey)){
					rb.AddForce(transform.right * forceAmt * rb.mass);
				}
			} else {
				Vector3 velo = Vector3.zero;	
				if (Input.GetKey(fwdKey)){

					velo += transform.up;
				}
				if (Input.GetKey(lKey)){
					velo += -transform.right;
				}
				if (Input.GetKey(backKey)){
					velo += -transform.up;
				}
				if (Input.GetKey(rKey)){
					velo += transform.right;
				} 
				rb.velocity = velo * forceAmt * rb.mass;
			}
		} else {
			Debug.Log("slow down " + gameObject.name + "!");
			//transform.localPosition -= rb.velocity;
			rb.velocity *= -2f;
			//keep it inside the range
			transform.localPosition = orig + Vector3.Normalize(transform.localPosition - orig) * maxStray;
			//rb.velocity = Vector3.Normalize(orig - transform.localPosition) * 2f;

		}
		rb.velocity =  Vector3.ClampMagnitude(rb.velocity, 4f);

		if (moveScript.enabled && CheckGaze()) {
			if (gameObject.name == "LTarget"){
				if (lCam.localPosition == origPosL)	StartCoroutine (ScreenShake.Shake (lCam, 0.05f, 0.1f));
				//if (!eyeMan.distBlur) {
					eyeMan.xResL *= 1f - (Time.deltaTime);
					eyeMan.yResL *= 1f - (Time.deltaTime);
					eyeMan.updateRes(0, eyeMan.xResL, eyeMan.yResL);

				//}
			} else if (gameObject.name == "RTarget"){
				if (rCam.localPosition == origPosR)	StartCoroutine (ScreenShake.Shake (rCam, 0.05f, 0.1f));
				//if (!eyeMan.distBlur) {
					eyeMan.xResR *= 1f - (Time.deltaTime);
					eyeMan.yResR *= 1f - (Time.deltaTime);
					eyeMan.updateRes(1, eyeMan.xResR, eyeMan.yResR);

				//}
			}

			//SetLaserA.alpha = 1f - DryEyes.lifeAmt;
			float pct = (gazeEscalation * Time.deltaTime) / moveScript.rate;
			moveScript.rate += gazeEscalation * Time.deltaTime;
			moveScript.rotSpeed *= (1f + pct); //Mathf.Clamp(moveScript.rate / 600f, moveScript.rotSpeedRange.x, moveScript.rotSpeedRange.y);
			if (!audSrc.isPlaying) audSrc.PlayOneShot(clip_GazeMake);

		}
	}

	bool CheckGaze(){
		Vector3 origin = Vector3.zero;
		if (gameObject.name == "LTarget"){
			origin = lCam.position;
		} else if (gameObject.name == "RTarget"){
			origin = rCam.position;
		}
		Ray beam = new Ray(transform.position, Vector3.Normalize(transform.position - origin));

		float currentDist = dist;
		if (DryEyes.blinking) currentDist = 0f;
		if (RaisePhone.phoneRaised) currentDist = phoneDist;
		Debug.DrawRay (beam.origin, beam.direction * currentDist);

		RaycastHit beamHit = new RaycastHit ();

		if (Physics.Raycast(beam, out beamHit, currentDist)){
			if (beamHit.transform.CompareTag("Stranger")){
				LookAtTarget[] look = beamHit.transform.gameObject.GetComponentsInChildren<LookAtTarget>();
				if (look != null && (look[0].gazing || look[1].gazing)){
					Debug.Log("gazed at ... " + beamHit.transform.name);
					DirectedAgent agent = beamHit.transform.gameObject.GetComponent<DirectedAgent>();
					if (agent != null){
						beamHit.transform.gameObject.GetComponent<DirectedAgent>().target = transform;
					}
					return true;
				}
			}
		}
		return false;
	}

}
