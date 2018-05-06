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
	float gazeEscalation = 0.5f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		orig = transform.localPosition;
		audSrc = GetComponent<AudioSource>();
		lCam = GameObject.Find("LCam").transform;
		rCam = GameObject.Find("RCam").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, orig.z);
		if (Vector3.Distance(orig, transform.localPosition) < maxStray){
			if (Input.GetKey(fwdKey)){
				rb.AddForce(Vector3.up * forceAmt * rb.mass);
			}
			if (Input.GetKey(lKey)){
				rb.AddForce(Vector3.left * forceAmt * rb.mass);
			}
			if (Input.GetKey(backKey)){
				rb.AddForce(Vector3.down * forceAmt * rb.mass);
			}
			if (Input.GetKey(rKey)){
				rb.AddForce(Vector3.right * forceAmt * rb.mass);
			}
		} else {
			//Debug.Log("slow down!");
			//transform.localPosition -= rb.velocity;
			//rb.velocity *= -1f;
			//keep it inside the range
			transform.localPosition = orig + Vector3.Normalize(transform.localPosition - orig) * maxStray;
			rb.velocity = Vector3.Normalize(orig - transform.localPosition) * 2f;

		}
		rb.velocity =  Vector3.ClampMagnitude(rb.velocity, 4f);

		if (CheckGaze()) {
			if (gameObject.name == "LTarget"){
				StartCoroutine (ScreenShake.Shake (lCam, 0.05f, 0.1f));
			} else if (gameObject.name == "RTarget"){
				StartCoroutine (ScreenShake.Shake (rCam, 0.05f, 0.1f));
			}
			moveScript.rate += gazeEscalation * Time.deltaTime;
			moveScript.rotSpeed = moveScript.rate / 10f;
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

		float dist = 500f;
		if (DryEyes.blinking) dist = 50f;
		Debug.DrawRay (beam.origin, beam.direction * dist);

		RaycastHit beamHit = new RaycastHit ();

		if (Physics.Raycast(beam, out beamHit, dist)){
			if (beamHit.transform.CompareTag("Stranger")){
				return true;
			}
		}
		return false;
	}

}
