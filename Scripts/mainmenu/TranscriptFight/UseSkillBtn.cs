using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkillBtn : MonoBehaviour {

    public PosType posType = PosType.Basic;
    public float ColdTime = 4.0f;
    private float leftTime = 0.0f;
    private UIButton btn;
    private UISprite mask = null;

    public PlayerTranscriptAnimation playerAnimation;

    private void Start()
    {
        playerAnimation = TranscriptFightManager._instance.player.GetComponent<PlayerTranscriptAnimation>();
        btn = this.GetComponent<UIButton>();
        if(posType != PosType.Basic)
        {
            mask = this.transform.Find("Sprite").GetComponent<UISprite>();
        }
    }

    private void EnableBtn()
    {
        btn.GetComponent<Collider>().enabled = true;
    }
    private void DisableBtn()
    {
        btn.GetComponent<Collider>().enabled = false;
        btn.SetState(UIButtonColor.State.Normal, true);
    }
	// Update is called once per frame
	void Update () {
		if(leftTime > 0 && mask)
        {
            leftTime -= Time.deltaTime;
            if (leftTime <= 0)
            {
                leftTime = 0.0f;
                EnableBtn();
            }
            mask.fillAmount = leftTime / ColdTime;
        }
    }

    private void OnPress(bool isPressed)
    { 
        // 上传到Player TODO
        playerAnimation.OnUseSkillBtnClicked(isPressed, posType);
        if (isPressed)
        {
           
            // 冷却时间
            if (posType != PosType.Basic)
            {
                leftTime = ColdTime;
                DisableBtn();
            }

        }
    }

}
