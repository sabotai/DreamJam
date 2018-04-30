using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float spawnTime = 5f;
	float startTime = 0f;
	public GameObject spawnObject;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startTime + spawnTime){
			Instantiate(spawnObject, transform.position, Quaternion.identity);
			startTime = Time.time;
		}
	}
}
