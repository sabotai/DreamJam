using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour {

	public GameObject[] players;
	GameObject manager;
	float fuseMag = 1.5f;
	// Use this for initialization
	void Start () {
		manager = GameObject.Find("Manager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
         if (other.gameObject.name == "net") {
         	for (int i = 0; i < players.Length; i++){
            	if (GetComponent<Renderer>().material.color == players[i].GetComponent<Renderer>().material.color){
            		manager.GetComponent<Score>().playerScore[i]++;
            	}
 	       }
         }
     }

    void OnCollisionEnter(Collision other) {
         if (other.transform.tag == "Pieces"){
         	if (GetComponent<Renderer>().material.color == other.gameObject.GetComponent<Renderer>().material.color) {
             //other.transform.parent = transform;
	         	if (other.gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude > fuseMag){
	         		//Debug.Log("BOOM");
	         		if (other.transform.lossyScale.sqrMagnitude >= transform.lossyScale.sqrMagnitude){
		             	other.transform.localScale += transform.localScale;
		             	other.gameObject.GetComponent<Rigidbody>().mass += gameObject.GetComponent<Rigidbody>().mass;
		             	other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		             	gameObject.SetActive(false);
	             	}
	         	}
         	} else {
         		/*
         		if (other.gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude > fuseMag){
	         		//Debug.Log("BOOM");
	         		if (other.transform.lossyScale.sqrMagnitude >= transform.lossyScale.sqrMagnitude){
		             	other.transform.localScale += transform.localScale;
		             	other.gameObject.GetComponent<Rigidbody>().mass += gameObject.GetComponent<Rigidbody>().mass;
		             	other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		             	gameObject.SetActive(false);
	             	}
	         	}
	         	*/
         	}
         }
     }
}
