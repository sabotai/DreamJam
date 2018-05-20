using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStageSetup : MonoBehaviour {
	Camera myCam;
	public Platform myPlat;
	bool flash = false;
	public Color c, c1, c2;
	public bool randomizeColor = true;
	public bool randomizePlats = true;
	public bool randomizeTextures = false;
	public bool lerpColors = false;
	public Texture[] texts;
	public Renderer[] randoRends;

	// Use this for initialization
	void Start () {
		float rando = Random.value;
		if (randomizeColor){
			if (c1 == null) c1 = Color.red;
			if (c2 == null) c2 = Color.blue;

			myCam = GetComponent<Camera>();
			c = Color.Lerp(c1, c2, rando);
			myCam.backgroundColor = c;

			rando = Random.value;
			if (rando > 0.69f) flash = true;

			if (flash) {
				rando = Random.value;
				c1 = Color.Lerp(c1, c2, rando);
				rando = Random.value;
				c2 = Color.Lerp(c1, c2, rando);
			}
		}
		if (randomizePlats){
			rando = Random.value;
			if (rando >= 0.25f){
				if (rando >= 0.75f) myPlat.randomize = true; else myPlat.waves = true;
			}
		}

		if (randomizeTextures){
			for (int i = 0; i < randoRends.Length; i++){
				randoRends[i].material.mainTexture = texts[Random.Range(0, texts.Length)];

			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (flash || lerpColors){
			float pct = Mathf.PingPong(Time.time / 2f, 1);
			c = Color.Lerp(c1, c2, pct);
			myCam.backgroundColor = c;
		}
	}
}
