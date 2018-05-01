using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EyePlayer : MonoBehaviour {

	Rigidbody rb;
	public float forceAmt = 3f;
	public KeyCode fwdKey, backKey, rKey, lKey, blink;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

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

	}

}
