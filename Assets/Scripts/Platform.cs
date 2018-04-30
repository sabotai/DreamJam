using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	public Vector3 rotDir;
	public float rotSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Rotate(rotDir, rotSpeed);

		if (Input.GetKeyDown(KeyCode.Space)) Reverse();
	}

	void Rotate(Vector3 dir, float speed){
		// Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(dir * Time.deltaTime * speed);

	}

	void Reverse(){
		rotDir *= -1.5f;
	}

}
