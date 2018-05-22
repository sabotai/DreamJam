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
    public bool enableDebug = false;

	// Use this for initialization
	void Start () {
		fpsObj = GameObject.Find("FPS").GetComponent<Text>();
		lastInterval = Time.realtimeSinceStartup;
        frames = 0;
		
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
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
				Time.timeScale *= 1.2f;

			if (Input.GetKeyDown(KeyCode.Y))
				Time.timeScale /= 1.2f;

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