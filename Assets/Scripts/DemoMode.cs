using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMode : MonoBehaviour {

	public GameObject[] enableThese;
	public GameObject[] disableThese;
	public static bool menuReset = false;
	public static bool demoMode = true;
	public Platform plat;
	// Use this for initialization
	void Start () {
		float rando = Random.value;
		if (rando > 0.5f) plat.waves = true; else plat.waves = false;

		
	}
	
	// Update is called once per frame
	void Update () {
		if (demoMode){
			if (Input.GetKey(KeyCode.Space)){//} || Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.V) || Input.GetKey(KeyCode.N) || Input.GetKey(KeyCode.M)) {
				for (int i = 0; i < enableThese.Length; i++){
					enableThese[i].SetActive(true);
				}
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
			for (int i = 0; i < disableThese.Length; i++){
				disableThese[i].SetActive(true);
			}
			menuReset = false;
			demoMode = true;

		}
	}


}
