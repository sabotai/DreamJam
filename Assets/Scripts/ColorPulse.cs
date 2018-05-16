using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPulse : MonoBehaviour {

	public Color c1, c2;
	public int dir;
	float current;
	public float speed = 1f;
	float initSpeed;
	public bool pulse = true;
	public bool speedBind = true;
	public Platform plat;
	float maxSpeed = 8f;
	public bool ui;
	// Use this for initialization
	void Start () {
		current = 0f;
		if (dir == 0) dir = 1;
		else if (dir == -1) current = 1f;
		initSpeed = speed;
		if (!plat) plat = GameObject.Find("PlatParent").GetComponent<Platform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (ui){
				GetComponent<Text>().material.color = Color.Lerp(c1, c2, current);
			} else {
				GetComponent<Renderer>().material.color = Color.Lerp(c1, c2, current);
			}
		current += (speed * dir * Time.deltaTime);
		if ((current > 1f || current < 0f) && pulse) dir *= -1;

		if (pulse && speedBind) speed = 0.1f * (plat.rotSpeed / initSpeed);
		speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);
		current = Mathf.Clamp(current, 0f, 1f);
	}
}
