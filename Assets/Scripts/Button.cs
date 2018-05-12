using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
	bool buttonDown, buttonUp;
	public float targetY = -0.1f;
	float origY;
	public float origAvailableY;
	public Transform button;
	public float pushSpeed = 1f;
	public static float direction = 1f;
	float startTime = 0f;
	public float refreshTime = 5f;
	public static bool buttonAvailable;
	public ShiftMats matShifter;
	public ColorPulse pulser, pulser2;
	public Score score;
	public Platform plat;

	// Use this for initialization
	void Start () {
		buttonAvailable = true;
		buttonDown = false;
		buttonUp = false;
		origY = button.localPosition.y;
		
		if (button == null)	button = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (buttonAvailable){
			if (Input.GetKeyDown(KeyCode.Space)) {
				direction *= -1f;
				buttonDown = true;
				buttonUp = false;
				buttonAvailable = false;
				matShifter.ShiftTheMats();
				if (score.currentRound == 2) plat.randomizePlats();

			} else if (!buttonDown){
				buttonPress(false, origAvailableY);
			}
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			buttonUp = true;
			buttonDown = false;
		}

		if (buttonDown) buttonPress(true, targetY); else if (buttonUp) buttonPress(false, origY);

		if (Time.time > startTime + refreshTime){
			buttonAvailable = true;
			startTime = Time.time;
		}
		
	}

	void buttonPress(bool down, float target){
		if (down){
				pulser.dir = -1;
				pulser2.dir = 1;
			if (button.localPosition.y > target){
				button.localPosition = new Vector3(button.localPosition.x, button.localPosition.y - (1.5f * pushSpeed * Time.deltaTime), button.localPosition.z);
			} else {
				if (!Input.GetKey(KeyCode.Space))	{
				button.localPosition = new Vector3(button.localPosition.x, target, button.localPosition.z);
					buttonDown = false;
				}
			}
			} else if (!down){
				if (buttonAvailable) {
					pulser.dir = 1;
					pulser2.dir = -1;
				}
					if (button.localPosition.y < target){
						button.localPosition = new Vector3(button.localPosition.x, button.localPosition.y + (pushSpeed * Time.deltaTime), button.localPosition.z);
					} else {
						button.localPosition = new Vector3(button.localPosition.x, target, button.localPosition.z);
						buttonUp = false;
					}
			}
	}
}
