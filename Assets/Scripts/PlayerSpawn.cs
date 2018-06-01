using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {
	AudioSource aud;
	public AudioClip respawnSound;
	// Use this for initialization
	void Start () {
		GetComponent<Player>().enabled = false;

		aud = GetComponent<AudioSource>();

        aud.pitch = 1f;
    	aud.PlayOneShot(respawnSound, 0.69f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		if (GetComponent<Player>().enabled == false) 
		GetComponent<Player>().enabled = true;
	}
}
