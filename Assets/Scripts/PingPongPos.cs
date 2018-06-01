using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongPos : MonoBehaviour {

	public float range;
	public Vector3 dir;
	Vector3 origin;
	public float duration = 2f;
	float startTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		origin = transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = origin + (Mathf.PingPong(Time.time, range) * dir);

        float t = (Time.time - startTime) / duration;
		transform.position = origin + (Mathf.SmoothStep(-range, range, t) * dir);
		if (t > 1f) {
			startTime = Time.time;
			range *= -1f;
		}
		
	}
}
