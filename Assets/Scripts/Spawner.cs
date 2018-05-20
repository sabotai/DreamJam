using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public float spawnTime = 5f;
	float origSpawnTime;
	float startTime = 0f;
	public GameObject spawnObject;
	public GameObject man;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		man = GameObject.Find("Level7");
		origSpawnTime = spawnTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Score.nextRound){
			if (Time.time > startTime + spawnTime){
				Instantiate(spawnObject, transform.position, Quaternion.identity);
				startTime = Time.time;
			}
			spawnTime = origSpawnTime / Mathf.Abs(man.GetComponent<Platform>().rotDir.y);
		}
	}
}
