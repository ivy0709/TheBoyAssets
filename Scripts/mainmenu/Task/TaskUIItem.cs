using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskUIItem : MonoBehaviour {

    [SerializeField]
    private UISprite type;
    [SerializeField]
    private UISprite icon;

    [SerializeField]
    private UILabel taskName;
    [SerializeField]
    private UILabel describe;

    [SerializeField]
    private UILabel coin;
    [SerializeField]
    private UILabel diamond;

    [SerializeField]
    private UISprite coinSprite;
    [SerializeField]
    private UISprite diamondSprite;

    [SerializeField]
    private UIButton combat;
    [SerializeField]
    private UIButton getReward;

    [SerializeField]
    private UILabel combatLabel;//有时候显示战斗 有时候显示下一步

    private TaskInfo taskItem = new TaskInfo();

    private void Awake()
    {

        type = transform.Find("type").GetComponent<UISprite>();
        icon = transform.Find("icon/Sprite").GetComponent<UISprite>();

        taskName = transform.Find("name").GetComponent<UILabel>();
        describe = transform.Find("describe").GetComponent<UILabel>();

        coinSprite = transform.Find("coin").GetComponent<UISprite>();
        diamondSprite = transform.Find("diamond").GetComponent<UISprite>();

        coin = transform.Find("coin/Label").GetComponent<UILabel>();
        diamond = transform.Find("diamond/Label").GetComponent<UILabel>();

        combat = transform.Find("combat").GetComponent<UIButton>();
        getReward = transform.Find("getReward").GetComponent<UIButton>();

        combatLabel = transform.Find("combat/Label").GetComponent<UILabel>();

        EventDelegate ed = new EventDelegate(this, "OnCombat");
        combat.onClick.Add(ed);
        EventDelegate ed1 = new EventDelegate(this, "OnGetReward");
        getReward.onClick.Add(ed1);


    }

    public void Start()
    {
        taskItem.OnTaskProgressChangedEvent += this.OnTaskProgressChangedEvent;
    }

    public void Set(TaskInfo item)
    {
        taskItem = item;
        UpdateItemUI();
    }

    private void UpdateItemUI()
    {
        TaskInfo item = taskItem;
        switch (taskItem.TaskType)
        {
            case TaskType.Main:
                type.spriteName = "pic_主线";
                break;
            case TaskType.Reward:
                type.spriteName = "pic_奖赏";
                break;
            case TaskType.Daily:
                type.spriteName = "pic_日常";
                break;
        }
        icon.spriteName = item.Icon;

        taskName.text = item.Name;
        describe.text = item.Describe;
        if (item.CoinAward != 0)
        {
            coinSprite.gameObject.SetActive(true);
            coinSprite.spriteName = "金币";
            coin.text = "X" + item.CoinAward;
        }
        else
        {
            coinSprite.gameObject.SetActive(false);
        }
        if (item.DiamondAward != 0)
        {
            diamondSprite.gameObject.SetActive(true);
            diamondSprite.spriteName = "钻石";
            diamond.text = "X" + item.DiamondAward;
        }
        else
        {
            diamondSprite.gameObject.SetActive(false);
        }
        switch (taskItem.TaskProgress)
        {
            case TaskProgress.NoStart:
                getReward.gameObject.SetActive(false);
                combat.gameObject.SetActive(true);
                combatLabel.text = "下一步";
                break;
            case TaskProgress.Accept:
                getReward.gameObject.SetActive(false);
                combat.gameObject.SetActive(true);
                combatLabel.text = "战斗";
                break;
            case TaskProgress.Complete:
                getReward.gameObject.SetActive(true);
                combat.gameObject.SetActive(false);
                break;
        }
    }

    private void OnCombat()
    {
        TaskManager._instance.OnExcuteTask(taskItem);
    }
    private void OnGetReward()
    {

    }

    void OnTaskProgressChangedEvent()
    {
        UpdateItemUI();
    }
}
