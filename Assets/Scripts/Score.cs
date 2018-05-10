using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	public float[] playerScore;
	public bool debugScore = false;
	public Text[] pScore;
	int startSize;
	public int scoreCap = 100;

	// Use this for initialization
	void Start () {
		startSize = pScore[0].fontSize;
	}
	
	// Update is called once per frame
	void Update () {
		//if (debugScore) Debug.Log("P1 = " + playerScore[0] + ", P2 = " + playerScore[1]);


		for (int i = 0; i < playerScore.Length; i++){
			
			pScore[i].text = playerScore[i].ToString();
			pScore[i].fontSize = (int)playerScore[i] * 4 + startSize;

			if (playerScore[i] > scoreCap) roundWin(i); 
		}
	}

	void roundWin(int player){

	}
}
