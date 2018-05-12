﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
	public float[] playerScore;
	public bool debugScore = false;
	public Text[] pScore;
	int startSize;
	public int scoreCap = 100;
	public GameObject[] roundScore;
	int[] roundsWon;
	int numRounds = 3;
	bool nextRound = false;
	public int currentRound = 0;
	public Platform plat;

	// Use this for initialization
	void Start () {
		startSize = pScore[0].fontSize;
		roundsWon = new int[playerScore.Length];
	}
	
	// Update is called once per frame
	void Update () {
		//if (debugScore) Debug.Log("P1 = " + playerScore[0] + ", P2 = " + playerScore[1]);


		for (int i = 0; i < playerScore.Length; i++){
			
			pScore[i].text = playerScore[i].ToString();
			pScore[i].fontSize = (int)playerScore[i] * 4 + startSize;

			if (playerScore[i] > scoreCap) roundWin(i); 
		}

		if (nextRound){
			advanceRound();
		}
	}

	void roundWin(int player){
		roundScore[player].transform.GetChild(roundsWon[player]).gameObject.SetActive(true);
		roundScore[player].transform.GetChild(roundsWon[player] + numRounds).gameObject.SetActive(false);
		
		nextRound = true;
		roundsWon[player]++;
	}

	void advanceRound(){

		for (int i = 0; i < playerScore.Length; i++){
			playerScore[i] = 0f;
		}

		switch (currentRound){
             		case 0:
		             	plat.waves = true;
             			break;
             		case 1:
		             	plat.waves = false;
		             	plat.randomize = true;
             			break;
             		case 2:
		             	SceneManager.LoadScene(0);	
             			break;
             		case 3:
		             	
             			break;

		}
		plat.rotSpeed = plat.origSpeed;
		currentRound++;
		nextRound = false;
	}
}
