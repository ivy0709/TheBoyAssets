using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTranscriptAnimation : MonoBehaviour {

    private Animator anim;

    private void Awake()
    {
        anim = this.transform.GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnUseSkillBtnClicked(bool isPressed, PosType iPos)
    {
        if(iPos == PosType.Basic)
        {
            if(isPressed)
            {
                anim.SetTrigger("Attack");
            }
        }
        else
        {
            if (isPressed)
            {
                anim.SetTrigger("Skill" + (int)iPos);
            }
        }
    } 
}
