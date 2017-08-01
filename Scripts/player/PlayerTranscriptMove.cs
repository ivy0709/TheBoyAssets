using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTranscriptMove : MonoBehaviour {

    private Animator anim;
    private Rigidbody rigid;
    public float speed = 5;

    private void Awake()
    {
        anim = this.transform.GetComponent<Animator>();
        rigid = this.transform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 vel = rigid.velocity;
        // 如果有按键按下 那么进行自身朝向的改变
        if (Mathf.Abs(h) > 0.05f || Mathf.Abs(v) > 0.05f)
        {
            if(anim.GetCurrentAnimatorStateInfo(1).IsName("Empty State"))
            {
                anim.SetBool("Move", true);
                rigid.velocity = new Vector3(h * speed, vel.y, v * speed);
                transform.LookAt(new Vector3(h, 0, v) + transform.position);  //transform.rotation = Quaternion.LookRotation(new Vector3(h, 0, v));
            }
        }
        else
        {
            anim.SetBool("Move", false);
            rigid.velocity = new Vector3(0, vel.y, 0);
        }
    }
}
