using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {
	public Transform target;
	float distThresh = 100f;
	// Use this for initialization
	void Start () {
		distThresh = 100f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(target.position, transform.position) < distThresh)	transform.LookAt(target);
	}
}
