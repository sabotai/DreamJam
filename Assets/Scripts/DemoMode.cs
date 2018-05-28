using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DemoMode : MonoBehaviour {

	public GameObject[] enableThese;
	public GameObject[] disableThese;
	public GameObject[] enableOneOfThese;
	GameObject enableThisOne;
	public static bool menuReset = false;
	public static bool demoMode = true;
	Platform demoPlat;
	Spawner[] spawners;
	public Score scoreMan;
	public Text instructions;
	public Text roundText;
	//public Platform plat;
	// Use this for initialization
	void Start () {
		demoPlat = GameObject.Find("PlatParent").GetComponent<Platform>();
		demoMode = true;
		float rando = Random.value;
		if (rando > 0.5f) {
			demoPlat.waves = true; 
			} else {
				demoPlat.waves = false;
			}
		enableThisOne = enableOneOfThese[Random.Range(0, enableOneOfThese.Length)];
        if (enableThisOne.name == "Platform-Conspiracy") RenderSettings.fog = true; else RenderSettings.fog = false;

		spawners = GetComponents<Spawner>();
	}
	
	// Update is called once per frame
	void Update () {
		if (demoMode){
			if (Input.GetButton("Shared") || Input.GetButton("Primary_P1") || Input.GetButton("Primary_P2")){//} || Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.V) || Input.GetKey(KeyCode.N) || Input.GetKey(KeyCode.M)) {
				for (int i = 0; i < enableThese.Length; i++){
					enableThese[i].SetActive(true);
					}
				enableThisOne.SetActive(true);
				for (int i = 0; i < disableThese.Length; i++){
					disableThese[i].SetActive(false);
				}
				demoMode = false;
			}	
			/*
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
				demoPlat.rotSpeed -= 0.5f;
			}
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
				demoPlat.rotSpeed += 0.5f;
			}
			*/
			if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
				if (scoreMan.scoreCap > 20f) scoreMan.scoreCap-= 10;
				updateInstructions(scoreMan.scoreCap + " TO WIN");
			}
			if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
				if (scoreMan.scoreCap < 200f) scoreMan.scoreCap+= 10;
				updateInstructions(scoreMan.scoreCap + " TO WIN");
			}
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
				if (scoreMan.numRounds < 3) scoreMan.numRounds++;
				
				updateInstructions(scoreMan.numRounds + " ROUNDS");				/*
				foreach (Spawner spawn in spawners){
					spawn.spawnTime -= 0.1f;
				}
				*/
			}
			if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
				if (scoreMan.numRounds > 1) scoreMan.numRounds--;
				if (scoreMan.numRounds == 1)  updateInstructions(scoreMan.numRounds + " ROUND");
				else updateInstructions(scoreMan.numRounds + " ROUNDS");	
							/*
				foreach (Spawner spawn in spawners){
					spawn.spawnTime += 0.1f;
				}
				*/
			}
			if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.N)){
				demoPlat.Reverse();
			}
			if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.M)){
				demoPlat.waves = !demoPlat.waves;
				demoPlat.rotSpeed = demoPlat.origSpeed;
				demoPlat.ResetPos();
			}
		}

		if (menuReset && !demoMode){
			Start();
			for (int i = 0; i < enableThese.Length; i++){
				enableThese[i].SetActive(false);
			}
				enableThisOne.SetActive(false);
			for (int i = 0; i < disableThese.Length; i++){
				disableThese[i].SetActive(true);
			}
			menuReset = false;
			demoMode = true;
			SceneManager.LoadScene(0);

		}
	}

	void updateInstructions(string text){
		roundText.text = text;
		roundText.GetComponent<UIFadeOut>().pct = 0f;
		if (scoreMan.numRounds == 1)		instructions.text = "The first player to score " + scoreMan.scoreCap + " wins!";
		else instructions.text = "The first player to score " + scoreMan.scoreCap + " takes the round. the first player to take " + scoreMan.numRounds + " rounds wins!";

	}

}
