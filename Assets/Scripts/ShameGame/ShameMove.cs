using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShameMove : MonoBehaviour {
	public bool move = true;
	public Transform lTarget, rTarget, lEye, rEye;
	public float rate = 1f;
	public float rotSpeed = 0.1f;
	public Transform dirIndicator;
	public Vector3 dirOffset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (move){
			Vector3 lDir = Vector3.Normalize(lTarget.position - lEye.position);
			Vector3 rDir = Vector3.Normalize(rTarget.position - rEye.position);
			Vector3 avgDir = Vector3.Normalize(lDir + rDir);
			Debug.Log("avgDir = " + avgDir);
			dirIndicator.rotation = Quaternion.LookRotation(avgDir);
			//transform.position += new Vector3(avgDir.x, 0f, avgDir.z) * rate;


      		float step = rotSpeed * Time.deltaTime;
	        Vector3 newDir = Vector3.RotateTowards(transform.forward, avgDir, step, 0.0f);
	        newDir.y = transform.forward.y;
	        Debug.DrawRay(transform.position, newDir, Color.red, 100f);
	        // Move our position a step closer to the target.
	        transform.rotation = Quaternion.LookRotation(newDir);
	        transform.position += transform.forward * rate;
		}
	}
}
