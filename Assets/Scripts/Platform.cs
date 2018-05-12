﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	public Vector3 rotDir;
	public float rotSpeed;
	public float origSpeed;
	public bool autoSlow = true;
	float oldDir;
	float maxSpeed = 1000f;
	public bool waves = false;
	public float scale = 1f;
	public bool randomize = false;


	// Use this for initialization
	void Start () {
		origSpeed = rotSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Button.direction != oldDir){
			Reverse();
		}
		//if (Input.GetKeyDown(KeyCode.Space)) Reverse();
		Rotate(rotDir, rotSpeed);

		if (autoSlow && Mathf.Abs(rotSpeed) > 1f) rotSpeed *= 0.999f;

		oldDir = Button.direction;

		if (waves) makeWaves(); 
		if (randomize) randomizePlats();
	}

	void Rotate(Vector3 dir, float speed){
		// Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(dir * Time.deltaTime * speed);

	}

	public void Reverse(){
		rotDir *= -1f;
		rotSpeed *= 1.5f;
		rotSpeed = Mathf.Clamp(rotSpeed, -maxSpeed, maxSpeed);
		Debug.Log("rotSpeed = " + rotSpeed);
	}

	public void makeWaves(){
		for (int i = 0; i < transform.childCount; i++){
			float offset = Mathf.Sin(i + (Time.frameCount * (.001f * rotSpeed))) * scale;
			transform.GetChild(i).position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
		}
	}
	public void randomizePlats(){
		for (int i = 0; i < transform.childCount; i++){
			float offset = Random.Range(-2f, 2f) * scale;
			transform.GetChild(i).position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
		}
		randomize = false;

	}


}
