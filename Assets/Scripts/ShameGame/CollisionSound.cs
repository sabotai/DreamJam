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
	public Rigidbody phone;
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
			if (Time.timeSinceLevelLoad > 10f) {
				Settings.instructions(false);
			} else {
				if (Settings.timesPlayed() < 3)	Settings.instructions(true);
			}
			Settings.timesPlayed(Settings.timesPlayed() + 1);
			
			Debug.Log("timesPlayer = " + Settings.timesPlayed());
			audSrc.PlayOneShot(clip_collision);
			phone.gameObject.GetComponent<AudioSource>().PlayOneShot(clip_collision);

			//if (col.collider.CompareTag("Stranger") || col.collider.CompareTag("Hazard") ){
			restartStuff();
		  //  }
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.name == "destination"){
			
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}

	}
	public void restartStuff(){
				GetComponent<ShameMove>().enabled = false;
				phone.isKinematic = false;
				RaisePhone.dir = -1;
				startTime = Time.time;
			        if (!audSrc2.isPlaying)      {
						Debug.Log("play restart sound");
			            audSrc2.clip = clip_restart;
			            audSrc2.pitch = 1.15f;
			            audSrc2.Play();
			        }
	}


}
