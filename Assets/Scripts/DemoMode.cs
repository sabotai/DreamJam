using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoMode : MonoBehaviour {

	public GameObject[] enableThese;
	public GameObject[] disableThese;
	public GameObject[] enableOneOfThese;
	GameObject enableThisOne;
	public static bool menuReset = false;
	public static bool demoMode = true;
	Platform demoPlat;
	Spawner[] spawners;
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

		spawners = GetComponents<Spawner>();
	}
	
	// Update is called once per frame
	void Update () {
		if (demoMode){
			if (Input.GetKey(KeyCode.Space)){//} || Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.V) || Input.GetKey(KeyCode.N) || Input.GetKey(KeyCode.M)) {
				for (int i = 0; i < enableThese.Length; i++){
					enableThese[i].SetActive(true);
					}
				enableThisOne.SetActive(true);
				for (int i = 0; i < disableThese.Length; i++){
					disableThese[i].SetActive(false);
				}
				demoMode = false;
			}	
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
				demoPlat.rotSpeed -= 0.5f;
			}
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
				demoPlat.rotSpeed += 0.5f;
			}
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
				foreach (Spawner spawn in spawners){
					spawn.spawnTime -= 0.1f;
				}
			}
			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
				foreach (Spawner spawn in spawners){
					spawn.spawnTime += 0.1f;
				}
				
			}
			if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.N)){
				demoPlat.Reverse();
			}
			if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.M)){
				demoPlat.waves = !demoPlat.waves;
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


}
