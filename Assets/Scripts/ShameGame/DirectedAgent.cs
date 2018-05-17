using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedAgent : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent myAgent;
    public Transform target;
    public GameObject[] randoTargets;
    public float distanceThresh = 1f;

	// Use this for initialization
	void Start () {
        myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        randoTargets = GameObject.FindGameObjectsWithTag("Target");
        if (target == null) target = randoTargets[Random.Range(0, randoTargets.Length)].transform;

	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) myAgent.destination = target.position;
		if (Vector3.Distance(target.position, transform.position) < distanceThresh) target = randoTargets[Random.Range(0, randoTargets.Length)].transform;
	}
}