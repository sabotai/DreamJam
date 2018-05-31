using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	Rigidbody rb;
	public float forceAmt = 3f;
	public float jumpAmt = 10f;
	public KeyCode fwdKey, backKey, rKey, lKey, jump, resetKey;
	public int pNumber;
	bool jumped = false;
	Material myMat;
	float myMass;
	GameObject mySpawner;
	Vector3 startPos;
	AudioSource aud;
	public AudioClip[] jumpClip; 
	public AudioClip[] hitClip;
	public AudioClip respawnSound;
	public float bumpAmt = 1000f;
	public float minPitch, maxPitch;
	Transform myParent;
	float landingThresh = 2f;

	public GameObject slaveMaster;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		aud = GetComponent<AudioSource>();
		myMass = rb.mass;
		mySpawner = GameObject.Find(gameObject.name + "Spawner");
		myParent = transform.parent;
		jumped = true;
		startPos = transform.position;
	}
	void OnEnable(){

		rb = GetComponent<Rigidbody>();
		aud = GetComponent<AudioSource>();

	}
	void Awake(){

		rb = GetComponent<Rigidbody>();
		aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {	
				if (slaveMaster != null) {
					GetComponent<Renderer>().material = slaveMaster.GetComponent<Renderer>().material;
				}
		if (!Score.nextRound && !Score.gameOver){
			if (Input.GetButtonDown("Shared") && Button.buttonDown) {
				GetComponent<Rigidbody>().AddForce( (Vector3.up * bumpAmt + Random.insideUnitSphere));

			}
			myMat = GetComponent<Renderer>().material;

			if (Input.GetAxis("Vertical_P" + pNumber) > 0){
				rb.AddForce(Vector3.forward * forceAmt * rb.mass);
			}
			if (Input.GetAxis("Horizontal_P" + pNumber) < 0){
				rb.AddForce(Vector3.left * forceAmt * rb.mass);
			}
			if (Input.GetAxis("Vertical_P" + pNumber) < 0){
				rb.AddForce(-Vector3.forward * forceAmt * rb.mass);
			}
			if (Input.GetAxis("Horizontal_P" + pNumber) > 0){
				rb.AddForce(Vector3.right * forceAmt * rb.mass);
			}
			if (Input.GetButton("Primary_P" + pNumber) && !jumped){
				rb.AddForce(Vector3.up * jumpAmt * rb.mass);
				aud.pitch = Mathf.Clamp(rb.velocity.y, minPitch, maxPitch);
				aud.PlayOneShot(jumpClip[Random.Range(0, jumpClip.Length)]);
				jumped = true;
			}

			if (Input.GetButtonDown("Alt_P" + pNumber)){
				Respawn(true);
			}
		}
	}

	void OnCollisionEnter(Collision other) {
		float mag = rb.velocity.sqrMagnitude;
		aud.pitch = Mathf.Clamp(mag / 2f, minPitch, maxPitch);
		//Debug.Log("velocity= " + rb.velocity.sqrMagnitude);

        if (other.transform.tag == "Platform" || other.transform.tag == "Pieces") {
			if (mag > landingThresh || jumped) aud.PlayOneShot(hitClip[Random.Range(0, hitClip.Length)]);
            jumped = false;
         }
        if (other.transform.tag == "Pieces") {
			aud.PlayOneShot(hitClip[Random.Range(0, hitClip.Length)]);
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
         if (other.gameObject.name == "FallTrigger")  {
         	aud.pitch = 1f;
    		aud.PlayOneShot(respawnSound, 0.69f);
         }
         if (other.gameObject.name == "net") {
            //SceneManager.LoadScene(0);
            Respawn();
         }

     }

    public void Respawn(){
    		aud.pitch = 1f;
    		//aud.PlayOneShot(respawnSound, 0.69f);
    		transform.parent = myParent;
    		if (Score.gameOver) transform.position = startPos + Vector3.up * 3f;
    		else transform.position = startPos - Vector3.up * 3f;;//mySpawner.transform.position;
            GetComponent<Rigidbody>().velocity = Vector3.down;
            enabled = false;
     }

    public void Respawn(bool playSound){
    		transform.parent = myParent;
    		if (playSound){
    			aud.pitch = 2f;
    			aud.PlayOneShot(respawnSound, 0.3f);
    			transform.position = startPos - Vector3.up * 4f;
    		} else {
    			if (Score.gameOver) transform.position = startPos + Vector3.up * 3f;
    			else transform.position = startPos;//mySpawner.transform.position;

    		}
            GetComponent<Rigidbody>().velocity = Vector3.down;
            enabled = false;
     }
}
