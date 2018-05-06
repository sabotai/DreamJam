using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFwd : MonoBehaviour {

	float speed;
	Vector3 origin;
	public float resetTime = 10f;
	float startTime;
	public float speedMultiplier = -100f;
	// Use this for initialization
	void Start () {
		speed = Random.Range(0.35f, 0.7f) * speedMultiplier;
		origin = transform.position;
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
