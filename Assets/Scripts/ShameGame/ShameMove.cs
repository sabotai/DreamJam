﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShameMove : MonoBehaviour {
	public bool move = true;
	public Transform lTarget, rTarget, lEye, rEye;
	public float rate = 1f;
	public float maxRot = 20f;
	public float rotSpeed = 0.1f;
	public Transform dirIndicator;
	public Vector3 dirOffset;


	float walkSpeed, normalSpeed, slowSpeed;
	Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		normalSpeed = rate;
		walkSpeed = rate / 3.5f;
		slowSpeed = rate / 10f;
		

		Quaternion lRot = lEye.rotation;
		Quaternion rRot = rEye.rotation;

		Quaternion avgRot = Quaternion.Slerp(lRot, rRot, 0.5f);// Quaternion.Slerp(lRot, rRot, 0.5f);
		//Debug.Log(avgRot);


        //float step = rotSpeed * Time.deltaTime;
		Vector3 avgEuler = avgRot.eulerAngles;
		Vector3 myEuler = transform.rotation.eulerAngles;
        Quaternion target = Quaternion.Euler(myEuler.x,avgEuler.y,myEuler.z);//Quaternion.RotateTowards(transform.rotation, avgRot, step);
		
		dirIndicator.rotation = target;
      	//float step = rotSpeed *  Time.deltaTime;
      	//transform.localRotation = target;
      	transform.forward = Vector3.Slerp(transform.forward, dirIndicator.forward, rotSpeed * Time.deltaTime);

	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetButtonDown("Cancel")) {
			Application.Quit();
		}
		*/
		rotSpeed = Mathf.Min(rotSpeed, maxRot);
		if (Cheat.enableDebug && Input.GetKey(KeyCode.E)) GetComponent<BoxCollider>().enabled = false;
		if (Cheat.enableDebug && Input.GetKey(KeyCode.R)) GetComponent<BoxCollider>().enabled = true;

		Quaternion lRot = lEye.rotation;
		Quaternion rRot = rEye.rotation;

		Quaternion avgRot = Quaternion.Slerp(lRot, rRot, 0.5f);
		//Debug.Log(avgRot);


		Vector3 avgEuler = avgRot.eulerAngles;
		Vector3 myEuler = transform.rotation.eulerAngles;
        Quaternion target = Quaternion.Euler(myEuler.x,avgEuler.y,myEuler.z);
		
		dirIndicator.rotation = target;

      	transform.forward = Vector3.Slerp(transform.forward, dirIndicator.forward, rotSpeed * Time.deltaTime);
      	
		//doing this will directly control the rotation
      	//transform.forward = dirIndicator.forward;


		if (move){

			if (DryEyes.blinking) rate = slowSpeed;
			 else if (RaisePhone.phoneRaised) rate = walkSpeed;
			 else rate = normalSpeed;
			 //GetComponent<Rigidbody>().velocity = (transform.forward * rate * Time.deltaTime * 6f);
	        transform.position += transform.forward * rate * Time.deltaTime;
		}
	}

	public void backstep(){
		transform.position -= transform.forward * rate * Time.deltaTime * 1.5f;
	}


}