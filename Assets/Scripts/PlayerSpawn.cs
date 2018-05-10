using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Player>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		if (GetComponent<Player>().enabled == false) 
		GetComponent<Player>().enabled = true;
	}
}
