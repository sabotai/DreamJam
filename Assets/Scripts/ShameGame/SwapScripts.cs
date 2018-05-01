using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapScripts : MonoBehaviour {
	public EyePlayer script1, script2;
	public KeyCode actionKey;
	KeyCode fwdKey, lKey, rKey, backKey;
	public bool defaultSwap = true;
	// Use this for initialization
	void Start () {

		fwdKey = script2.fwdKey;
		lKey = script2.lKey;
		rKey = script2.rKey;
		backKey = script2.backKey;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (defaultSwap){
			if (Input.GetKey(actionKey)){
				script2.fwdKey = fwdKey;
				script2.lKey = lKey;
				script2.rKey = rKey;
				script2.backKey = backKey;
			} else {
				script2.fwdKey = script1.fwdKey;
				script2.lKey = script1.lKey;
				script2.rKey = script1.rKey;
				script2.backKey = script1.backKey;

			}

			} else {
		if (Input.GetKey(actionKey)){
			script2.fwdKey = script1.fwdKey;
			script2.lKey = script1.lKey;
			script2.rKey = script1.rKey;
			script2.backKey = script1.backKey;

		} else {
			script2.fwdKey = fwdKey;
			script2.lKey = lKey;
			script2.rKey = rKey;
			script2.backKey = backKey;
		}
			}

	}
}
