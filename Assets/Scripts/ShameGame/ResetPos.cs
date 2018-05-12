using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPos : MonoBehaviour {
	Vector3 origPos;
	public float timerAmt = 10f;
	float startTime = 0f;
	// Use this for initialization
	void Start () {
		origPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startTime + timerAmt) {
			transform.position = origPos;
			startTime = Time.time;
		}
	}
}
