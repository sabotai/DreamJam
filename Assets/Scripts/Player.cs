using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	Rigidbody rb;
	public float forceAmt = 3f;
	public float jumpAmt = 10f;
	public KeyCode fwdKey, backKey, rKey, lKey, jump, resetKey;
	bool jumped = false;
	Material myMat;
	float myMass;
	GameObject mySpawner;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		myMass = rb.mass;
		mySpawner = GameObject.Find(gameObject.name + "Spawner");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		myMat = GetComponent<Renderer>().material;

		if (Input.GetKey(fwdKey)){
			rb.AddForce(Vector3.forward * forceAmt * rb.mass);
		}
		if (Input.GetKey(lKey)){
			rb.AddForce(Vector3.left * forceAmt * rb.mass);
		}
		if (Input.GetKey(backKey)){
			rb.AddForce(-Vector3.forward * forceAmt * rb.mass);
		}
		if (Input.GetKey(rKey)){
			rb.AddForce(Vector3.right * forceAmt * rb.mass);
		}
		if (Input.GetKey(jump) && !jumped){
			rb.AddForce(Vector3.up * jumpAmt * rb.mass);
			jumped = true;
		}

		if (Input.GetKeyDown(resetKey)){
			Respawn();
		}
	}

	void OnCollisionEnter(Collision other) {
         if (other.transform.tag == "Platform" || other.transform.tag == "Pieces") {
            jumped = false;
         }
         if (other.transform.tag == "Pieces") {
	         if (other.gameObject.GetComponent<Renderer>().material.color == myMat.color){
	         	//Debug.Log("my block");
	         	Reset(other.gameObject);
	         } else {
	         	other.gameObject.GetComponent<Rigidbody>().mass = rb.mass * 1000000000000f;
	         	other.gameObject.GetComponent<Rigidbody>().drag = rb.mass * 1000000000000f;
	         	other.gameObject.GetComponent<Rigidbody>().angularDrag = rb.mass * 1000000000000f;
	         	//Debug.Log("enemy block");
	         }
	     }
     }	

     void Reset(GameObject resetMe){

	         resetMe.GetComponent<Rigidbody>().mass = 0.5f;
	         
	         resetMe.GetComponent<Rigidbody>().angularDrag = 0.01f;
	         resetMe.GetComponent<Rigidbody>().drag = 0f;
     }
     void OnCollisionExit(Collision other) {

         if (other.transform.tag == "Pieces") {
         	Reset(other.gameObject);
	         
	     }
     }
	void OnTriggerEnter(Collider other) {
         if (other.gameObject.name == "net") {
            //SceneManager.LoadScene(0);
            Respawn();
         }
     }

     void Respawn(){

            transform.position = mySpawner.transform.position;
            GetComponent<Rigidbody>().velocity = Vector3.down;
            enabled = false;
     }
}
