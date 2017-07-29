using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAutoMove : MonoBehaviour {

    private NavMeshAgent navAgent;

    public float minDistance = 3.0f;

    //public GameObject testGo;
	// Use this for initialization
	void Start () {
        navAgent = transform.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (navAgent.enabled)
        {
            if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)
            {
                navAgent.isStopped = true;
                navAgent.enabled = false;
            }
            else
            {
                if (navAgent.remainingDistance < minDistance && navAgent.remainingDistance != 0)
                {
                    navAgent.isStopped = true;
                    navAgent.enabled = false;
                    TaskManager._instance.OnArrivedDestination();
                }
            }

        }

    }
    public void SetDestination(Vector3 pos)
    {
        navAgent.enabled = true;
        navAgent.SetDestination(pos);
    }
}
