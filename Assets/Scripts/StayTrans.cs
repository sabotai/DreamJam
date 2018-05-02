using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayTrans : MonoBehaviour {
	public Material transMat;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Color oldColor = GetComponent<Renderer>().material.color;
		GetComponent<Renderer>().material = transMat;
		GetComponent<Renderer>().material.color = new Color(oldColor.r, oldColor.g, oldColor.b, transMat.color.a);
	}
}
