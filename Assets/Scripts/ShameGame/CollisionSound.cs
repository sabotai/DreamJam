using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour {
	public AudioClip clip_collision;
	AudioSource audSrc;
	// Use this for initialization
	void Start () {
		audSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision col){
		if (col.collider.CompareTag("Obstacle") || col.collider.CompareTag("Stranger")){
			audSrc.PlayOneShot(clip_collision);
		}
	}
}
