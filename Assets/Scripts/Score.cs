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
	public float scoreCap = 100f;
	public GameObject[] roundScore;
	int[] roundsWon;
	public int numRounds = 1;
	public float maxFont = 260;
	public static bool nextRound = false;
	public int currentRound = 0;
	public Platform plat;
	public Text announce, announceShadow, announceShadow2;
	public static bool gameOver = false;
	public static int recentWinner = -1;
	public Color drawColor;
	public static string p1Name, p2Name;
	float origBounceMag, origBounceSpeed;
	Vector3[] scoreOrigPos = new Vector3[2];
	public static AudioSource[] scoreAudSrc = new AudioSource[2];
	public AudioSource winPlayer;
	public AudioClip winSound;
	public Music music;


	// Use this for initialization
	void Start () {
		scoreAudSrc[0] = GameObject.Find("P1 Score").GetComponent<AudioSource>();
		scoreAudSrc[1] = GameObject.Find("P2 Score").GetComponent<AudioSource>();
		startSize = pScore[0].fontSize;
		roundsWon = new int[playerScore.Length];
		for (int i = 0; i < roundsWon.Length; i++){
			roundsWon[i] = 0;
		}
		plat = GameObject.Find("PlatParent").GetComponent<Platform>();
		//Platform.waves = false;
		//Platform.randomize = false;
		announce.text = "";
		announceShadow.text = "";
		announceShadow2.text = "";
		gameOver = false;
		nextRound = false;
		origBounceMag = pScore[0].gameObject.GetComponent<BounceTitle>().mag;
		origBounceSpeed = pScore[0].gameObject.GetComponent<BounceTitle>().speed;
		for (int i = 0; i < scoreOrigPos.Length; i++){
			scoreOrigPos[i] = pScore[i].transform.position;

		}
		music.newMusic();
	}
	
	void OnEnable () {
		//Platform.waves = false;
		//Platform.randomize = false;
		announce.text = "";
		announceShadow.text = "";
		announceShadow2.text = "";
	}
	// Update is called once per frame
	void Update () {
		//if (debugScore) Debug.Log("P1 = " + playerScore[0] + ", P2 = " + playerScore[1]);

		if (!gameOver){
			if (nextRound){
				if (Input.GetButtonDown("Shared")){
					music.newMusic();
					 advanceRound();

					for (int i = 0; i < playerScore.Length; i++){
						pScore[i].enabled = true;
						pScore[i].transform.position = scoreOrigPos[i];
					}
					}
			} else {
				for (int i = 0; i < playerScore.Length; i++){
					
					pScore[i].text = playerScore[i].ToString();
					pScore[i].fontSize = startSize + (int)( (playerScore[i] / scoreCap) * (maxFont - startSize));
					//if (playerScore[i] > scoreCap * 0.9f) {
						pScore[i].gameObject.GetComponent<BounceTitle>().mag = origBounceMag * (playerScore[i] / scoreCap) * 1f;
						//pScore[i].gameObject.GetComponent<BounceTitle>().speed = origBounceSpeed * (playerScore[i] / scoreCap);
						//} else {

						//}
					if (playerScore[i] >= scoreCap) roundWin(i); 
				}
			}
		} else {
			if (Input.GetButtonDown("Shared")) reset();
		}

	}

	void roundWin(int player){

		for (int i = 0; i < playerScore.Length; i++){
			pScore[i].enabled = false;
		}
		roundScore[player].transform.GetChild(roundsWon[player]).gameObject.SetActive(true);
		roundScore[player].transform.GetChild(roundsWon[player] + numRounds).gameObject.SetActive(false);

		roundsWon[player]++;
		nextRound = true;
		recentWinner = player;

		if (roundsWon[player] >= numRounds){
			playerWin(player);
		} else {
			int pWin = player + 1;
			string winnerName = "";
			if (pWin == 1) winnerName = p1Name; else if (pWin == 2) winnerName = p2Name;
			string announceMe = winnerName + " WINS\nTHE ROUND";
			announce.text = announceMe.Replace("\\n", "\n");
			announce.color = pScore[player].GetComponent<Text>().color;
			announceShadow.text = announce.text;
			announceShadow2.text = announce.text;
			//announceShadow = announce.color;
		}
	}

	public void timerRoundWin(){
		int roundWinner = -1;
		int highestScore = 0;
		for (int i = 0; i < playerScore.Length; i++){
			if ((int)(playerScore[i]) > highestScore) roundWinner = i;
		}
		if (roundWinner != -1)	{
			roundWin(roundWinner); 
		} else {
			//advanceRound();
			recentWinner = -1;
			string announceMe = "ROUND DRAW";
			announce.text = announceMe;
			announce.color = drawColor;//Color.Lerp(pScore[0].GetComponent<Text>().color, pScore[1].GetComponent<Text>().color, 0.5f);
			announceShadow.text = announce.text;
			announceShadow2.text = announce.text;
			nextRound = true;
		}
	}

	public void advanceRound(){
		announce.text = "";
		announceShadow.text = "";
		announceShadow2.text = "";
		Timer.startTime += Timer.roundTime;
		GameObject[] pieces = GameObject.FindGameObjectsWithTag("Pieces");
		foreach (GameObject piece in pieces)
        {
            Destroy(piece);
        }
		GameObject[] players = GameObject.FindGameObjectsWithTag("Players");
		foreach (GameObject player in players)
        {
        	Debug.Log("respawning player ... " + player.name);
            player.GetComponent<Player>().Respawn();
        }

		for (int i = 0; i < playerScore.Length; i++){
			playerScore[i] = 0f;
		}

		switch (currentRound){
             		case 0:
		             	//Platform.waves = true;
             		break;
             		case 1:
		             	//Platform.waves = false;
		             	//Platform.randomize = true;
             		break;
             		case 2:
		             	//Platform.randomize = false;
		             	//plat.ResetPos();
             			int winner = -1;
		             	for (int i = 0; i < roundsWon.Length; i++){
		             		if (roundsWon[i] >= numRounds) {
		             			winner = i;
		             		}
		             	}
		             	if (winner != -1) {
		             		playerWin(winner);
		             	} else {
		             		currentRound = -1;
		             	}
             		break;

		}
		plat.rotSpeed = plat.origSpeed;
		currentRound++;
		nextRound = false;
	}

	void playerWin(int winner){
		winPlayer.clip = winSound;
		winPlayer.Play();
		announce.color = pScore[winner].GetComponent<Text>().color;
		winner++;
		string winnerName = "";
		if (winner == 1) winnerName = p1Name; else if (winner == 2) winnerName = p2Name;
		string announceMe = winnerName + " WINS!";
		announce.text = announceMe.Replace("\\n", "\n");
		announceShadow.text = announce.text;
		announceShadow2.text = announce.text;
		gameOver = true;
	}

	void reset(){
		for (int i = 0; i < playerScore.Length; i++){
			roundScore[i].transform.GetChild(0).gameObject.SetActive(false);
			roundScore[i].transform.GetChild(1).gameObject.SetActive(false);
			roundScore[i].transform.GetChild(2).gameObject.SetActive(false);

			roundScore[i].transform.GetChild(3).gameObject.SetActive(true);
			roundScore[i].transform.GetChild(4).gameObject.SetActive(true);
			roundScore[i].transform.GetChild(5).gameObject.SetActive(true);
		}
		//Platform.waves = false;
		//Platform.randomize = false;
		p1Name = "";
		p2Name = "";
		DemoMode.menuReset = true;	
		
	}
}
