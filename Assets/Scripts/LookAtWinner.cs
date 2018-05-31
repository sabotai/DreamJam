using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtWinner : MonoBehaviour {

	Quaternion origRot;
	Transform[] players = new Transform[2];
	int closeFOV = 12;
	int defaultFOV = 47;
	// Use this for initialization
	void Start () {
		origRot = transform.rotation;
		players[0] = GameObject.Find("P1").transform;
		players[1] = GameObject.Find("P2").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Score.gameOver || Score.nextRound && Score.recentWinner != -1){

			transform.LookAt(players[Score.recentWinner].position + Vector3.up * 0.2f);
			if (GetComponent<Camera>().fieldOfView != closeFOV) {
				GetComponent<Camera>().fieldOfView = closeFOV;
				players[Score.recentWinner].gameObject.GetComponent<Player>().Respawn();
			}
		} else {
			transform.rotation = origRot;
			GetComponent<Camera>().fieldOfView = defaultFOV;
		}
	}
}
