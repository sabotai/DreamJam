using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeY : MonoBehaviour {
	public float scale = 0f;
	// Use this for initialization
	void Start () {
		float offset = Random.Range(-1f, 1f) * scale;
		transform.position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.O)) Start();	
	}
}
