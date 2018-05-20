using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour {

	GameObject[] players = new GameObject[2];
	GameObject manager;
	public float fuseMag = 1.15f;
	public float maxScale = 0.8f;
	AudioSource aud;
	public AudioClip[] mergeClips;
	public bool demoPiece = false;
	public float bumpAmt = 30f;
	// Use this for initialization
	void Start(){
		demoPiece = DemoMode.demoMode;
		if (!demoPiece){
			manager = GameObject.Find("ScoreMan");
			players[0] = GameObject.Find("P1");
			players[1] = GameObject.Find("P2");
		}
	}
	
	// Update is called once per frame
	void Update () {
		//jump up
		if (Input.GetKeyDown(KeyCode.Space) && Button.buttonDown) GetComponent<Rigidbody>().AddForce( Vector3.up * bumpAmt + Random.insideUnitSphere);
		if (demoPiece && !DemoMode.demoMode) gameObject.SetActive(false); // 
	}

	void OnTriggerEnter(Collider other) {
         if (other.gameObject.name == "net") {
         	if (demoPiece){
         		gameObject.SetActive(false);
         	} else {
	         	for (int i = 0; i < players.Length; i++){
	         		if (other.gameObject && players[i]){
	            	if (GetComponent<Renderer>().material.color == players[i].GetComponent<Renderer>().material.color){
	            		manager.GetComponent<Score>().playerScore[i] += (int)(transform.lossyScale.x * 20f);
	            	}
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
		             	aud = other.gameObject.GetComponent<AudioSource>();
		             	//Debug.Log("merge mag: " + other.transform.localScale.magnitude);
						aud.pitch = 1.5f - other.transform.localScale.magnitude;
						aud.PlayOneShot(mergeClips[Random.Range(0, mergeClips.Length)]);
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
