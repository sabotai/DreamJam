using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour {
	public Vector3 dir;
	public float speed = 1f;
	public bool paused = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetButtonDown("Shared")){
			paused = !paused;
		} else {
			if (!paused){
				transform.position += (dir * speed);
			}
		}
	}
}
