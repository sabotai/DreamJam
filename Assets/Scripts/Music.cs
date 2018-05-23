using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {
	public Score score;
	public AudioClip[] tracks;
	AudioSource aud;
	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	}
	public void newMusic(){
		int newClip = Random.Range(0, tracks.Length);
		//if (aud.clip != tracks[score.currentRound]){
			aud.clip = tracks[newClip];
			aud.Play();
		//}
		if (GameObject.Find("Platform-Conspiracy") != null) aud.Stop();
	}
}
