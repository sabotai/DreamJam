using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnim : MonoBehaviour {
	Animator myAnim;
	public int anim = -1;

	// Use this for initialization
	void Start () {
		myAnim = GetComponent<Animator>();
		if (anim == -1) anim = (int)(Random.Range(0f, 5f));

		
	}
	
	// Update is called once per frame
	void Update () {
		myAnim.SetInteger("random", anim);
	}
}
