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

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		orig = transform.localPosition;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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
			Debug.Log("slow down!");
			
			rb.velocity *= -1f;
			//rb.velocity *= 0.85f;
		}
	}

}
