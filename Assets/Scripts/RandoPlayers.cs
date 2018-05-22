﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandoPlayers : MonoBehaviour {

	public string[] names;

	// Use this for initialization
	void Start () {
		string name = names[Random.Range(0, names.Length)];
		GetComponent<TextMesh>().text = name;
		if (transform.parent.name == "P1") Score.p1Name = name;
		if (transform.parent.name == "P2") Score.p2Name = name;
		/*
		GameObject[] labels = GameObject.FindGameObjectsWithTag("Label");
		foreach (GameObject label in labels)
        {
            label.GetComponent<Text>().text = name;
        }
        string name2 = names[Random.Range(0, names.Length)];
        if (name == name2) name2 = names[Random.Range(0, names.Length)];

		labels = GameObject.FindGameObjectsWithTag("Label2");
		foreach (GameObject label in labels)
        {
            label.GetComponent<Text>().text = name2;
        }
        */
		
	}
	void OnEnable(){
		Start();
	}
	
	// Update is called once per frame
	void Update () {
	}

}
