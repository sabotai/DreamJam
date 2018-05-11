using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisePhone : MonoBehaviour {

	float pct = 1f;
	public float speed = 1f;
	float dir = 1;
	public Transform up, down;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.N)) dir = 1f;
		if (Input.GetKey(KeyCode.V) || Input.GetKey(KeyCode.M)) dir = -1f;
		pct += dir * speed * Time.deltaTime;
		pct = Mathf.Clamp(pct, 0f, 1f);
		transform.rotation = Quaternion.Slerp(down.rotation, up.rotation, pct);
	}
}
