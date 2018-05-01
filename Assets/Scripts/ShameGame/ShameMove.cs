using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShameMove : MonoBehaviour {
	public bool move = true;
	public Transform lTarget, rTarget, lEye, rEye;
	public float rate = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (move){
			Vector3 lDir = Vector3.Normalize(lTarget.position - lEye.position);
			Vector3 rDir = Vector3.Normalize(rTarget.position - rEye.position);
			Vector3 avgDir = Vector3.Normalize(lDir + rDir);
			transform.position += new Vector3(avgDir.x, 0f, avgDir.z) * rate;
		}
	}
}
