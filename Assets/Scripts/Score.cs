using System.Collections;
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
	int numRounds = 3;
	public float maxFont = 260;
	public static bool nextRound = false;
	public int currentRound = 0;
	public Platform plat;
	public Text announce, announceShadow;
	public static bool gameOver = false;
	public static int recentWinner = -1;
	public Color drawColor;

	// Use this for initialization
	void Start () {
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
		gameOver = false;
		nextRound = false;
	}
	
	void OnEnable () {
		//Platform.waves = false;
		//Platform.randomize = false;
		announce.text = "";
		announceShadow.text = "";
	}
	// Update is called once per frame
	void Update () {
		//if (debugScore) Debug.Log("P1 = " + playerScore[0] + ", P2 = " + playerScore[1]);

		if (!gameOver){
			if (nextRound){
				if (Input.GetKeyDown(KeyCode.Space)) advanceRound();
			} else {
				for (int i = 0; i < playerScore.Length; i++){
					
					pScore[i].text = playerScore[i].ToString();
					pScore[i].fontSize = startSize + (int)( (playerScore[i] / scoreCap) * (maxFont - startSize));

					if (playerScore[i] > scoreCap) roundWin(i); 
				}
			}
		} else {
			if (Input.GetKeyDown(KeyCode.Space)) reset();
		}

	}

	void roundWin(int player){
		roundScore[player].transform.GetChild(roundsWon[player]).gameObject.SetActive(true);
		roundScore[player].transform.GetChild(roundsWon[player] + numRounds).gameObject.SetActive(false);

		roundsWon[player]++;
		nextRound = true;
		recentWinner = player;

		if (roundsWon[player] >= numRounds){
			playerWin(player);
		} else {
			int pWin = player + 1;
			string announceMe = "PLAYER " + pWin + " WINS\nTHE ROUND";
			announce.text = announceMe.Replace("\\n", "\n");
			announce.color = pScore[player].GetComponent<Text>().color;
			announceShadow.text = announce.text;
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
			nextRound = true;
		}
	}

	public void advanceRound(){
		announce.text = "";
		announceShadow.text = "";
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
		announce.color = pScore[winner].GetComponent<Text>().color;
		winner++;
		string announceMe = "PLAYER " + winner + " WINS!";
		announce.text = announceMe.Replace("\\n", "\n");
		announceShadow.text = announce.text;
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
		DemoMode.menuReset = true;	
		
	}
}
