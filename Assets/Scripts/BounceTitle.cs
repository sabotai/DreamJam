using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTitle : MonoBehaviour {
	public float speed = 5f;
	public float mag = 1.3f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float offset = Mathf.Sin(Time.time * Time.deltaTime * speed) * mag;

		transform.Translate(offset, offset, 0);
	}
}
