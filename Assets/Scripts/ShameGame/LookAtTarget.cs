using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {
	public Transform target;
	float distThresh = 100f;
	public bool useDistThresh = true;
	// Use this for initialization
	void Start () {
		distThresh = 300f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(target.position, transform.position) < distThresh || !useDistThresh)
			transform.LookAt(target);
			
	}
}
