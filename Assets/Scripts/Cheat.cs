using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Alpha1))
			SceneManager.LoadScene(0);
		if (Input.GetKey(KeyCode.Alpha2))
			SceneManager.LoadScene(1);
		if (Input.GetKey(KeyCode.Alpha3))
			SceneManager.LoadScene(2);
		if (Input.GetKey(KeyCode.Alpha4))
			SceneManager.LoadScene(3);
		if (Input.GetKey(KeyCode.Alpha5))
			SceneManager.LoadScene(4);
	}
}