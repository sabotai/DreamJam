using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {
	public Transform target;
	float distThresh = 100f;
	public bool useDistThresh = true;
	public bool limitForward = false;
	Quaternion origRot;
	public float limit = 85f;
	public bool gazing = false;
	// Use this for initialization
	void Start () {
		origRot = transform.localRotation;
		distThresh = 300f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(target.position, transform.position) < distThresh || !useDistThresh){
			if (limitForward){
				Vector3 relativePos = target.position - transform.parent.position;
       			Quaternion rotation = Quaternion.LookRotation(relativePos);
				float angle = Quaternion.Angle(transform.parent.rotation, rotation);
					if (angle < limit) {
						Debug.Log("angle met ... " + angle);
						gazing = true;
					} else {
						Debug.Log("angle NOT met ... " + angle);
						gazing = false;
						transform.localRotation = origRot;
					}
			} else {
				gazing = true;
			}
		}

		if (gazing) transform.LookAt(target);
		
			
	}
}
