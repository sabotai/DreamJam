using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
	public Transform[] wayPoints;
	public int currentPoint = 0;
	public float colorSpeed = 0.05f;
	public Renderer phone;
	// Use this for initialization
	void Start () {
		currentPoint = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Ray beam = new Ray(transform.position, transform.forward);

		Debug.DrawRay (beam.origin, beam.direction * Mathf.Infinity, Color.green);

		RaycastHit beamHit = new RaycastHit ();

		if (Physics.Raycast(beam, out beamHit, Mathf.Infinity, LayerMask.GetMask("Map")) && beamHit.transform == wayPoints[currentPoint]){
			PhoneChange(true);
			Debug.Log("GOOD");
		} else {
			PhoneChange(false);
			Debug.Log("BAD");
		}

		//if (currentPoint
	}

	void PhoneChange(bool up){
		Color col = phone.material.GetColor("_EmissionColor");
		Debug.Log(col);
		float amt = col.r;
		if (up){
			if (col.r < 1f)
				amt += colorSpeed * Time.deltaTime;
			
		} else {
			if (col.r > -2f)
				amt -= colorSpeed * Time.deltaTime;
		}
		phone.material.SetColor("_EmissionColor", new Color(amt, amt, amt, amt));
	}

	void OnTriggerEnter(Collider col){
		if (col.CompareTag("Point")){
			currentPoint++;
			phone.material.SetColor("_EmissionColor", new Color(1f,1f, 1f, 1f));
		}
	}
}
