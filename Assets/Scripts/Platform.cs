using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	public Vector3 rotDir;
	public float rotSpeed;
	public bool autoSlow = true;
	float oldDir;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Button.direction != oldDir){
			Reverse();
		}
		//if (Input.GetKeyDown(KeyCode.Space)) Reverse();
		Rotate(rotDir, rotSpeed);

		if (autoSlow && Mathf.Abs(rotDir.y) > 1f) rotDir *= 0.9975f;

		oldDir = Button.direction;
	}

	void Rotate(Vector3 dir, float speed){
		// Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(dir * Time.deltaTime * speed);

	}

	public void Reverse(){
		rotDir *= -2f;
	}


}
