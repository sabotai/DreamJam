using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour {

	GameObject[] players = new GameObject[2];
	GameObject manager;
	public float fuseMag = 1.15f;
	public float maxScale = 0.8f;
	// Use this for initialization
	void Start () {
		manager = GameObject.Find("Manager");
		players[0] = GameObject.Find("P1");
		players[1] = GameObject.Find("P2");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && Button.buttonAvailable) GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere + Vector3.up * 30f);
	}

	void OnTriggerEnter(Collider other) {
         if (other.gameObject.name == "net") {
         	for (int i = 0; i < players.Length; i++){
         		if (other.gameObject && players[i]){
            	if (GetComponent<Renderer>().material.color == players[i].GetComponent<Renderer>().material.color){
            		manager.GetComponent<Score>().playerScore[i] += (int)(transform.lossyScale.x * 20f);
            	}
            }
 	       }
         }
     }

    void OnCollisionEnter(Collision other) {
         //if (other.transform.tag == "Pieces" || other.transform.tag == "Players"){
         if (other.transform.tag == "Pieces"){
         	if (GetComponent<Renderer>().material.color == other.gameObject.GetComponent<Renderer>().material.color) {
             //other.transform.parent = transform;
	         	if (other.gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude > fuseMag){
	         		//Debug.Log("BOOM");
	         		if (other.transform.lossyScale.sqrMagnitude >= transform.lossyScale.sqrMagnitude && other.transform.lossyScale.sqrMagnitude < maxScale){

		             	other.transform.localScale += transform.localScale;
		             	other.gameObject.GetComponent<Rigidbody>().mass += gameObject.GetComponent<Rigidbody>().mass;
		             	//other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
