using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	public float[] playerScore;
	public bool debugScore = false;
	public Text[] pScore;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (debugScore) Debug.Log("P1 = " + playerScore[0] + ", P2 = " + playerScore[1]);

		for (int i = 0; i < playerScore.Length; i++){
			pScore[i].text = playerScore[i].ToString();
		}
	}
}
