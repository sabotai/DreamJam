using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFwd : MonoBehaviour {

	float speed;
	Vector3 origin;
	float resetTime, startTime;
	// Use this for initialization
	void Start () {
		speed = Random.Range(0.35f, 0.7f) * -100f;
		origin = transform.position;
		resetTime = 10f;
		startTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += -transform.forward * speed;
		if (Time.time > resetTime + startTime){
			transform.position = origin;
			startTime += resetTime;
		}
	}
}
