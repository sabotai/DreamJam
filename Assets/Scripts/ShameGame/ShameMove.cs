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
	public Vector2 rotSpeedRange = new Vector2(0.2f, 0.5f);
	public float minRotThresh = 0.1f;
	Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.E)) GetComponent<BoxCollider>().enabled = false;
		if (Input.GetKey(KeyCode.R)) GetComponent<BoxCollider>().enabled = true;

		Quaternion lRot = lEye.localRotation;
		Quaternion rRot = rEye.localRotation;

		Quaternion avgRot = Quaternion.Slerp(lRot, rRot, 0.5f);// Quaternion.Slerp(lRot, rRot, 0.5f);
		//Debug.Log(avgRot);


        //float step = rotSpeed * Time.deltaTime;
		Vector3 avgEuler = avgRot.eulerAngles;
		Vector3 myEuler = transform.rotation.eulerAngles;
        Quaternion target = Quaternion.Euler(myEuler.x,avgEuler.y,myEuler.z);//Quaternion.RotateTowards(transform.rotation, avgRot, step);
		
		dirIndicator.localRotation = target;
      	float step = rotSpeed *  Time.deltaTime;
      	transform.localRotation = target;

        //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, target, step);
        //transform.Rotate(target.eulerAngles * Time.deltaTime);
		//transform.rotation = Quaternion.LookRotation(newDir);
		//if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.N))	move = false; else move = true;
		/*

			Vector3 avgDir = Vector3.Normalize(lDir + rDir);
			//Debug.Log("avgDir = " + avgDir + " ; middleDir = " + dirIndicator.eulerAngles);

			//dirIndicator.localRotation = Quaternion.LookRotation(new Vector3 (0f, avgDir.y, 0f));
			dirIndicator.rotation = Quaternion.LookRotation(avgDir);
			dirIndicator.eulerAngles = new Vector3(0f, dirIndicator.eulerAngles.y, 0f);
*/
		if (move){
			/*
      		float step = rotSpeed * Time.deltaTime;
      		Debug.Log("step= " + step);
	        Vector3 newDir = Vector3.RotateTowards(transform.forward, avgDir, step, 0.0f);
	        newDir.y = transform.forward.y;
	        Debug.DrawRay(transform.position, newDir, Color.red, 100f);
	        // Move our position a step closer to the target.
	        transform.rotation = Quaternion.LookRotation(newDir);
	    //}
*/

	        //rb.AddForce(transform.forward * rate * Time.deltaTime);
	        //rb.velocity = transform.forward * rate * Time.deltaTime;
			//rb.velocity = Vector3.ClampMagnitude(rb.velocity, rate);
	        transform.position += transform.forward * rate * Time.deltaTime;
		}
	}


}
