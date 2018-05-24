using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimeOut : MonoBehaviour {

	public float timerAmt = 30f;
	public float lastPress = 0f;
	public float countdown = 10f;
	string text = "still there?";
	public Text[] announcements;
	public Text[] countdowns;

	// Use this for initialization
	void Start () {
		lastPress = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {

			if (Time.time > lastPress + timerAmt){
				foreach (Text announcement in announcements){
					announcement.text = "";
				}
				foreach (Text countdownObj in countdowns){
					countdownObj.text = "";
				}
			}
			lastPress = Time.time;

		
		} else {
			if (Time.time > lastPress + timerAmt){
				foreach (Text announcement in announcements){
					announcement.text = text;
				}
				foreach (Text countdownObj in countdowns){
					int timeLeft = (int)(lastPress + timerAmt + countdown - Time.time);
					countdownObj.text = "" + timeLeft;
				}
				if (Time.time > lastPress + timerAmt + countdown) SceneManager.LoadScene(0);
			}
		}
	}
}
