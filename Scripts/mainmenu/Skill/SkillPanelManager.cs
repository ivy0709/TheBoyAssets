using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelManager : MonoBehaviour {

    private SkillInfo curSkill;

    [SerializeField]
    private UILabel skillName;
    [SerializeField]
    private UILabel describe;

    [SerializeField]
    private UIButton upgradeBtn;
    [SerializeField]
    private UIButton closeBtn;

    [SerializeField]
    private UILabel upgradeLabel;//有时候显示选择技能 升级 金币不足 最大等级

    [SerializeField]
    private TweenPosition tweenPos;

    public static SkillPanelManager _instance;

    private void Awake()
    {
        skillName = transform.Find("bg/name").GetComponent<UILabel>();
        describe = transform.Find("bg/describe").GetComponent<UILabel>();


        upgradeBtn = transform.Find("upgradeBtn").GetComponent<UIButton>();
        closeBtn = transform.Find("closeBtn").GetComponent<UIButton>();

        upgradeLabel = transform.Find("upgradeBtn/Label").GetComponent<UILabel>();

        tweenPos = transform.GetComponent<TweenPosition>();

        EventDelegate ed = new EventDelegate(this, "OnCloseBtnClicked");
        closeBtn.onClick.Add(ed);
        EventDelegate ed1 = new EventDelegate(this, "OnUpgradeBtnClicked");
        upgradeBtn.onClick.Add(ed1);

        _instance = this;
        UpdateSkillItem(null);

    }

    private void UpdateSkillItem(SkillInfo skill)
    {
        curSkill = skill;
        if (curSkill == null)
        {
            skillName.text = "";
            describe.text = "";
            DisableUpgradeBtn("选择升级");
        }
        else
        {
            skillName.text = curSkill.Name + " Lv." + curSkill.Level;
            // 再看下视频
            describe.text = "当前技能的攻击力:"+ (curSkill.Level * curSkill.BaseDamage) + " 下一级技能的攻击力:"+ ((curSkill.Level + 1) * curSkill.BaseDamage) + "   升级下一级所需要的金币数量:" + ((curSkill.Level + 1) * 500);
            UpdateUpgradeLabel();
        }
    }
    /// <summary>
    /// 更新Upgrade按钮的显示 同时返回能否进行升级
    /// </summary>
    /// <returns></returns>
    private bool UpdateUpgradeLabel()
    {
        if(curSkill != null)
        {
            // 当前等级 已经达到 人物等级
            if(curSkill.Level >= PlayerInfo._instance.Level)
            {
                DisableUpgradeBtn("最大等级");
                return false;
            }
            // 金币不足
            if ((curSkill.Level + 1)*500 >= PlayerInfo._instance.Coin)
            {
                DisableUpgradeBtn("金币不足");
                return false;
            }
            // 可以升级
            EnableUpgradeBtn("升级");
            return true;
        }
        return false;
    }
    private void EnableUpgradeBtn(string str)
    {
        if(curSkill != null)
        {
            upgradeBtn.SetState(UIButtonColor.State.Normal, true);
            upgradeBtn.GetComponent<Collider>().enabled = true;
            upgradeLabel.text = str;
        }

    }
    private void DisableUpgradeBtn(string str)
    {
        upgradeBtn.SetState(UIButtonColor.State.Disabled, true);
        upgradeBtn.GetComponent<Collider>().enabled = false;
        upgradeLabel.text = str;
    }

    private void OnCloseBtnClicked()
    {
        tweenPos.PlayReverse();
    }
    private void OnUpgradeBtnClicked()
    {
        if(UpdateUpgradeLabel() == true)
        {
            // 扣除金币
            PlayerInfo._instance.Coin -= (curSkill.Level + 1) * 500;
            curSkill.LevelUp(1);
            UpdateSkillItem(curSkill);
        }
    }
    private void OnSkillItemClicked(SkillInfo skill)
    {
        UpdateSkillItem(skill);
    }
    public void OnSelfShow()
    {
        UpdateSkillItem(null);
        tweenPos.PlayForward();
    }
}
