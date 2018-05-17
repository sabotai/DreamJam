using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLaserA : MonoBehaviour {
	public Material laserMat;
	public float minAlpha = 20f/255f;
	public float maxAlpha = 1f;
	public static float alpha;
	public static float pAlpha;
	public float defaultA = 80f/255f;
	public float phoneA = 10f/255f;
	public static float incAmt = 0.01f;
	public EyePlayer eyeP;
	// Use this for initialization
	void Start () {
		phoneA = eyeP.phoneDist / eyeP.dist;
		if (minAlpha == 0f) minAlpha = (20f/255f);
		alpha = minAlpha;
		pAlpha = minAlpha;
		laserMat.color = new Color(laserMat.color.r, laserMat.color.g, laserMat.color.b, alpha);
	}
	
	// Update is called once per frame
	void Update () {
		alpha = 1f - DryEyes.lifeAmt;
		if (alpha != pAlpha || DryEyes.blinking){
			float a = alpha;
			if (RaisePhone.phoneRaised){
				a *= (phoneA / RaisePhone.pct);
				//Debug.Log("new alpha = " + a);
			} 
			if (DryEyes.blinking) {
				alpha = minAlpha;
				a = minAlpha;
			}	
			a = Mathf.Clamp(a, minAlpha, maxAlpha);
			alpha = a;
			laserMat.color = new Color(laserMat.color.r, laserMat.color.g, laserMat.color.b, a);

			pAlpha = alpha;
		}
	}
}
