using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {
	//generic shake script
	public static bool shaking = false;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	public static IEnumerator Shake(Transform obj, float duration, float magnitude) {
		Debug.Log ("shaking...");


		float elapsed = 0.0f;

		Vector3 originalCamPos = obj.localPosition;

		while (elapsed < duration) {
			shaking = true;
			elapsed += Time.deltaTime;          

			float percentComplete = elapsed / duration;         
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;

			obj.localPosition = new Vector3(x + originalCamPos.x, y + originalCamPos.y, originalCamPos.z); 

			yield return null;
		}
		shaking = false;
		obj.localPosition = originalCamPos;
	}

}