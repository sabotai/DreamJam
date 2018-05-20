using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtendedPulse : MonoBehaviour {

	public Color cOn, cOff;
	public int dir;
	float current;
	public float speed = 1f;
	float initSpeed;
	float maxSpeed = 8f;
	public bool ui;
	public float sleepTime = 10f;
	public float awakeTime = 5f;
	float startTime;
	float timerAmt;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		current = 0f;
		if (dir == 0) dir = 1;
		else if (dir == -1) current = 1f;
		initSpeed = speed;
		timerAmt = awakeTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (ui){
				GetComponent<Text>().color = Color.Lerp(cOn, cOff, current);
			} else {
				GetComponent<Renderer>().material.color = Color.Lerp(cOn, cOff, current);
			}
			current += (speed * dir * Time.deltaTime);


			if (startTime + timerAmt < Time.time){
				if (current > 1f || current < 0f) {
					startTime += timerAmt;
					dir *= -1;

					if (current > 1f) timerAmt = awakeTime;
					if (current < 0f) timerAmt = sleepTime;
				} 
				
			}

			current = Mathf.Clamp(current, 0f, 1f);
		}
	
}
