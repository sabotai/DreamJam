using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeOut : MonoBehaviour {

	Text myText;
	public Color startColor;
	public float speed = 1f;
	public float pct = 0f;
	public float timerAmt = 2f;
	float startTime;
	public bool startFaded = true;
	// Use this for initialization
	void Start () {
		myText = GetComponent<Text>();
		startColor = myText.color;
		startTime = 0f;
		if (startFaded) pct = 1f;
		myText.color = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 0f), pct);
	}
	
	// Update is called once per frame
	void Update () {

		if (pct < 1f && Time.time > startTime + timerAmt) {

			if (pct == 0f) startTime = Time.time;
			pct += Time.deltaTime * speed;
			myText.color = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 0f), pct);
		}
	}
}
