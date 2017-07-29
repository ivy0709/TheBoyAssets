using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Vector3 offset;
    private Transform playerPos;

    // Use this for initialization
    void Start () {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = playerPos.position + offset;
	}
}
