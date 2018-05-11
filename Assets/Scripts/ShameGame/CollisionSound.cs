using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionSound : MonoBehaviour {
	public AudioClip clip_collision, clip_restart;
	AudioSource audSrc;
	public AudioSource audSrc2;
	public bool restart = true;
	public float timerAmt = 2f;
	float startTime;
	// Use this for initialization
	void Start () {
		audSrc = GetComponent<AudioSource>();
		startTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		if (startTime != 0f){
			/*
			if (Time.time > startTime + timerAmt - clip_restart.length){
				if (audSrc2.clip != clip_restart){
					audSrc2.Stop();
				}
		        if (!audSrc2.isPlaying)
		        {
				Debug.Log("play restart sound");
		            audSrc2.clip = clip_restart;
		            audSrc2.Play();
		        }
			}
			*/
			if (Time.time > startTime + timerAmt) {
				GetComponent<PixelIntroOutro>().enabled = true;
			}
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.collider.CompareTag("Obstacle") || col.collider.CompareTag("Stranger") || col.collider.CompareTag("Hazard") ){
			audSrc.PlayOneShot(clip_collision);

			//if (col.collider.CompareTag("Stranger") || col.collider.CompareTag("Hazard") ){
			restartStuff();
		  //  }
		}
	}
	public void restartStuff(){
				GetComponent<ShameMove>().enabled = false;
				startTime = Time.time;
			        if (!audSrc2.isPlaying)      {
						Debug.Log("play restart sound");
			            audSrc2.clip = clip_restart;
			            audSrc2.Play();
			        }
	}


}
