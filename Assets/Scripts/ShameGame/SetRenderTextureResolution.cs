using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRenderTextureResolution : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void updateRes(float minWidth, float minHeight, float maxWidth, float maxHeight, float pct){

		float newW = Mathf.Lerp(minWidth, maxWidth, pct);
		float newH = Mathf.Lerp(minHeight, maxHeight, pct);
		
		//update resolution of render textures
		RenderTexture newRT = new RenderTexture( (int)newW, (int)newH, 16, RenderTextureFormat.ARGBFloat );
		newRT.filterMode = FilterMode.Point;
		Camera.main.targetTexture = newRT;

	}
}
