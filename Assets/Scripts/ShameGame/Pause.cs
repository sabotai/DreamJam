using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	public bool paused = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Cancel") && !paused){
			paused = true;
		} else {
			if (paused){
				transform.GetChild(0).gameObject.SetActive(true);
				Time.timeScale = 0f;
				if (Input.GetButtonDown("Cancel")) {
					Time.timeScale = 1f;
					transform.GetChild(0).gameObject.SetActive(false);
					paused = false;
				}
				if (Input.GetButtonDown("Shared")) Application.Quit();
			}
		}


	}
}