using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cheat : MonoBehaviour {

	public bool showFPS = false;
	Text fpsObj;
	public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames = 0;
    private float fps;
    public static bool enableDebug = false;
    public bool c, pC, pV, pN, pM, v, n, m;
	public int buttonCount = 0;

	// Use this for initialization
	void Start () {
		buttonCount = 0;
		fpsObj = GameObject.Find("FPS").GetComponent<Text>();
		lastInterval = Time.realtimeSinceStartup;
        frames = 0;
		c = false;
		v = false;
		n = false;
		m = false;
		pC = false;
		pV = false;
		pN = false;
		pM = false;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (buttonCount >= 4) {
			SceneManager.LoadScene(0);
			} else {
				buttonCount = 0;
			}

		//if (Time.frameCount % 2 == 0) buttonCount = 0;
		if (Input.GetKey(KeyCode.C)) buttonCount++;
		if (Input.GetKey(KeyCode.V)) buttonCount++;
		if (Input.GetKey(KeyCode.N)) buttonCount++;
		if (Input.GetKey(KeyCode.M)) buttonCount++;
		if (Input.GetKeyUp(KeyCode.C)) buttonCount = 0;
		if (Input.GetKeyUp(KeyCode.V)) buttonCount = 0;
		if (Input.GetKeyUp(KeyCode.N)) buttonCount = 0;
		if (Input.GetKeyUp(KeyCode.M)) buttonCount = 0;
		*/
		
		c = Input.GetKey(KeyCode.C);
		v = Input.GetKey(KeyCode.V);
		n = Input.GetKey(KeyCode.N);
		m = Input.GetKey(KeyCode.M);
		if (pC && !c) c = true;
		if (pV && !v) v = true;
		if (pN && !n) n = true;
		if (pM && !m) m = true;
		if (c && v && n && m) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyUp(KeyCode.C)) c = false;
		if (Input.GetKeyUp(KeyCode.V)) v = false;
		if (Input.GetKeyUp(KeyCode.N)) n = false;
		if (Input.GetKeyUp(KeyCode.M)) m = false;
		pC = c;
		pV = v;
		pN = n;
		pM = m;
		/*
		if ((c || pC) && (v || pV) && (n || pN) && (m || pM)) {
			SceneManager.LoadScene(0);
			} else {
				if (Time.frameCount % 2 == 0){
					pC = c;
					pV = v;
					pN = n;
					pM = m;
				}
			}
		
		/*
		if (Input.GetKeyUp(KeyCode.C)) c = false;
		if (Input.GetKeyUp(KeyCode.V)) v = false;
		if (Input.GetKeyUp(KeyCode.N)) n = false;
		if (Input.GetKeyUp(KeyCode.M)) m = false;
		if (Input.GetKey(KeyCode.C)) c = true;
		if (Input.GetKey(KeyCode.V)) v = true;
		if (Input.GetKey(KeyCode.N)) n = true;
		if (Input.GetKey(KeyCode.M)) m = true;

		if (c && v && n && m) SceneManager.LoadScene(0);
		*/

		if (Input.GetKeyDown(KeyCode.Asterisk) || Input.GetKeyDown(KeyCode.Backspace)) enableDebug = !enableDebug;

		if (enableDebug){
			if (Input.GetKey(KeyCode.Alpha1))
				SceneManager.LoadScene(0);
			if (Input.GetKey(KeyCode.Alpha2))
				SceneManager.LoadScene(1);
			if (Input.GetKey(KeyCode.Alpha3))
				SceneManager.LoadScene(2);
			if (Input.GetKey(KeyCode.Alpha4))
				SceneManager.LoadScene(3);
			if (Input.GetKey(KeyCode.Alpha5))
				SceneManager.LoadScene(4);
			if (Input.GetKey(KeyCode.Alpha6))
				SceneManager.LoadScene(5);
			if (Input.GetKey(KeyCode.Alpha7))
				SceneManager.LoadScene(6);
			if (Input.GetKey(KeyCode.Alpha8))
				SceneManager.LoadScene(7);
			if (Input.GetKeyDown(KeyCode.T))
				Time.timeScale *= 2f;

			if (Input.GetKeyDown(KeyCode.Y))
				Time.timeScale = 1f;

			if (Input.GetKeyDown(KeyCode.F)){

				showFPS = !showFPS;
			}

			frames++;
			if (showFPS){
				fpsObj.enabled = true;
		        float timeNow = Time.realtimeSinceStartup;
		        if (timeNow > lastInterval + updateInterval)
		        {
		            fps = (float)(frames / (timeNow - lastInterval));
		            frames = 0;
		            lastInterval = timeNow;
		        }
		        int iFps = (int)(fps);
		        fpsObj.text = iFps.ToString();
			} else if (fpsObj.enabled){
				fpsObj.enabled = false;
			}
		}

	}
}