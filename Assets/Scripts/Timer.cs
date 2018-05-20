using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	public static float roundTime = 60f;
	public static float startTime = 0f;
	public Score scoreMan;
	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		startTime = Time.time;
	}
	void OnEnable(){
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Score.nextRound && !Score.gameOver){
			int timeRemaining = (int)(startTime + roundTime - Time.time);
			text.text = timeRemaining.ToString();
			if (startTime + roundTime < Time.time){
				scoreMan.timerRoundWin();
				//scoreMan.advanceRound();
				//startTime += Time.time;

			}
		} else {
			text.text = "";
		}
		
	}
}
