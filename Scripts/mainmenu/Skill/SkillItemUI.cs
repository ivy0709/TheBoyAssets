using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItemUI : MonoBehaviour {

    public PosType IPos;
    [SerializeField]
    private UISprite icon;
    [SerializeField]
    private UIButton btn;

    private SkillInfo skill;

    private void Awake()
    {
        icon = this.transform.GetComponent<UISprite>();
        btn = this.transform.GetComponent<UIButton>();
    }
    private void Start()
    {
        skill = SkillManager._instance.GetSkillByPosType(IPos);
        if(skill != null)
        {
            icon.spriteName = skill.Icon;
            btn.normalSprite = icon.spriteName;
        }
    }

    void OnClick()
    {
        if (skill != null)
        {
            transform.parent.parent.SendMessage("OnSkillItemClicked", skill);
        }
    }
}
