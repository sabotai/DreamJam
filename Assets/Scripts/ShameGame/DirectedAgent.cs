using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedAgent : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent myAgent;
    public Transform target;
    GameObject[] randoTargets;
    float distanceThresh = 6f;

	// Use this for initialization
	void Start () {
        myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        randoTargets = GameObject.FindGameObjectsWithTag("Target");
        if (target == null) target = randoTargets[Random.Range(0, randoTargets.Length)].transform;
        //myAgent.destination = target.position;
        myAgent.SetDestination(target.position);

	}
	
	// Update is called once per frame
	void Update () {
		//if (target != null) myAgent.destination = target.position;
		if (Mathf.Abs(Vector3.Distance(target.position, transform.position)) < distanceThresh) {
			target = randoTargets[Random.Range(0, randoTargets.Length)].transform;
			//Debug.Log("setting new target from ..." + myAgent.destination.gameObject.name + " to " + target.gameObject.name);
			myAgent.SetDestination(target.position);
		}
	}
}