using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerVillageMove : MonoBehaviour {

    public float speed = 25;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update () {
        float h = Input.GetAxis("Horizontal"); 
        float v = Input.GetAxis("Vertical");

        // 如果有按键按下 那么进行自身朝向的改变
        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)
        {
            Vector3 vel = GetComponent<Rigidbody>().velocity;
            GetComponent<Rigidbody>().velocity = new Vector3(-h * speed, vel.y, -v * speed);
            transform.rotation = Quaternion.LookRotation(new Vector3(-h, 0, -v));
        }
        else
        {
            if(agent.enabled)
            {
                if (agent.velocity != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(agent.velocity);
                }
            }
            else
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}
