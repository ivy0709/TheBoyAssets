using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerVillageAnimation : MonoBehaviour {

    private NavMeshAgent navAgent;
    private Animator anim;
    private Rigidbody rigid;
    private void Awake()
    {
        anim = this.transform.GetComponent<Animator>();
        rigid = this.transform.GetComponent<Rigidbody>();
        navAgent = transform.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update () {
        if(navAgent.enabled)
        {
            anim.SetBool("Move", true);
        }
        else
        {
            if (rigid.velocity.magnitude > 0.5f)
            {
                anim.SetBool("Move", true);
            }
            else
            {
                anim.SetBool("Move", false);
            }
        }
	}
}
