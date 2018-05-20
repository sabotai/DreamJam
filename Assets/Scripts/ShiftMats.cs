using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShiftMats : MonoBehaviour {

	public Material[] playerMats;
	public GameObject[] players;
	public Text[] pScore;

	// Use this for initialization
	void Start () {
		//if (!players[0].activeSelf) players[0] = GameObject.Find("P1");
		//if (!players[1].activeSelf) players[1] = GameObject.Find("P2");
		players[0] = GameObject.Find("P1");
		players[1] = GameObject.Find("P2");

	}
	void OnEnable(){
		players[0] = GameObject.Find("P1");
		players[1] = GameObject.Find("P2");
		/*
		for (int i = 0; i < playerMats.Length; i++){
			playerMats[i] = players[i].GetComponent<Renderer>().material;
		}
		*/	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ShiftTheMats(){
			Material firstMat = playerMats[0];
			for (int i = 0; i < playerMats.Length; i++){
				if (i < playerMats.Length - 1)	{
					playerMats[i] = playerMats[i+1];
					} else {
						playerMats[i] = firstMat;
					}
				pScore[i].color = playerMats[i].color;
				players[i].GetComponent<Renderer>().material = playerMats[i];
				for (int j = 0; j < players[i].transform.childCount; j++){
					if (players[i].transform.GetChild(j).gameObject.tag != "Label"){
						players[i].transform.GetChild(j).gameObject.GetComponent<Renderer>().material = playerMats[i];
					}
				}
			}
}
}
