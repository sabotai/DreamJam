using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMode : MonoBehaviour {

	public GameObject[] enableThese;
	public GameObject[] disableThese;
	public GameObject[] enableOneOfThese;
	GameObject enableThisOne;
	public static bool menuReset = false;
	public static bool demoMode = true;
	//public Platform plat;
	// Use this for initialization
	void Start () {
		demoMode = true;
		float rando = Random.value;
		if (rando > 0.5f) {
			Platform.waves = true; 
			} else {
				Platform.waves = false;
			}
		enableThisOne = enableOneOfThese[Random.Range(0, enableOneOfThese.Length)];

		
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

		}
	}


}
