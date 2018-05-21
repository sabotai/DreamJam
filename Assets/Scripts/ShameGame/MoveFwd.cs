using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFwd : MonoBehaviour {
	//public bool rotate = false;
	public Vector3 rotate = Vector3.zero;
	float speed;
	Vector3 origin;
	public float resetTime = 10f;
	float startTime;
	public float speedMultiplier = -100f;
	public bool speedMultIsSpeed = false;
	public bool triggerReset = false;
	// Use this for initialization
	void Start () {
		speed = Random.Range(0.35f, 0.7f) * speedMultiplier;
		if (speedMultIsSpeed) speed = speedMultiplier;
		origin = transform.position;
		startTime = 0f;

		if (rotate != Vector3.zero) rotate = Vector3.Lerp(-rotate, rotate, Random.value);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += -transform.forward * speed * (Time.deltaTime * 60f);

		if (!triggerReset){
			if (Time.time > resetTime + startTime){
				transform.position = origin;
				startTime += resetTime;
			}
		}
		transform.Rotate(rotate * speed * (Time.deltaTime * 60f));
	}

	void OnTriggerEnter(Collider other) {
				//Debug.Log("RESET POS");
		if (triggerReset){
			if (other.gameObject.tag == "Boundary"){
				transform.position = origin;
			}
		}
	}
}
