using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
	public int[] playerScore;
	public bool debugScore = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (debugScore) Debug.Log("P1 = " + playerScore[0] + ", P2 = " + playerScore[1]);
	}
}
