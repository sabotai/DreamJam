using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnim : MonoBehaviour {
	Animator myAnim;
	// Use this for initialization
	void Start () {
		myAnim = GetComponent<Animator>();
		myAnim.SetFloat("randomize", Random.value);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
