using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAssistant : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < Input.GetJoystickNames().Length; i++)
		Debug.Log(i + " " + Input.GetJoystickNames()[i]);
	}
}
