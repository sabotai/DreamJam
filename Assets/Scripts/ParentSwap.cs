using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSwap : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other) {
         if ( (other.transform.tag == "Pieces" || other.transform.tag == "Players")) {
             //other.transform.parent = transform;
             other.transform.SetParent(transform.parent);
         }
     }
     void OnCollisionStay(Collision other) {
         if ( (other.transform.tag == "Pieces" || other.transform.tag == "Players")) {
             //other.transform.parent = transform;
             other.transform.SetParent(transform.parent);
         }
     }
 
    void OnCollisionExit(Collision other) {
         if ( (other.transform.tag == "Pieces" || other.transform.tag == "Players")) {
             other.transform.parent = null;
         }
     }
}
