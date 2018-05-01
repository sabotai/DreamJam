using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftMats : MonoBehaviour {

	public Material[] playerMats;
	public GameObject[] players;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < playerMats.Length; i++){
			playerMats[i] = players[i].GetComponent<Renderer>().material;
				
			}	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){
			Material firstMat = playerMats[0];
			for (int i = 0; i < playerMats.Length; i++){
				if (i < playerMats.Length - 1)	{
					playerMats[i] = playerMats[i+1];
					} else {
						playerMats[i] = firstMat;
					}
				players[i].GetComponent<Renderer>().material = playerMats[i];
				for (int j = 0; j < players[i].transform.childCount; j++){
					players[i].transform.GetChild(j).gameObject.GetComponent<Renderer>().material = playerMats[i];
				}
			}

		}
		
	}
}
