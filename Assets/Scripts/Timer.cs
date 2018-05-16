using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	public float roundTime = 60f;
	public static float startTime = 0f;
	public Score scoreMan;
	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}
	void OnEnable(){
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		int timeRemaining = (int)(startTime + roundTime - Time.time);
		text.text = " " + timeRemaining;
		if (startTime + roundTime < Time.time){
			scoreMan.advanceRound();
			//startTime += Time.time;

		}
		
	}
}
