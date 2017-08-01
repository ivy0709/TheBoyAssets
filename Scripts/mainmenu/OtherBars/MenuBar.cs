using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBar : MonoBehaviour {

    [SerializeField]
    private UIButton bagbtn;
    [SerializeField]
    private UIButton fightbtn;
    [SerializeField]
    private UIButton skillbtn;
    [SerializeField]
    private UIButton shopbtn;
    [SerializeField]
    private UIButton settingbtn;
    [SerializeField]
    private UIButton taskbtn;



    private void Awake()
    {
        bagbtn = transform.Find("bagbtn").GetComponent<UIButton>();
        fightbtn = transform.Find("fightbtn").GetComponent<UIButton>();
        skillbtn = transform.Find("skillbtn").GetComponent<UIButton>();
        shopbtn = transform.Find("shopbtn").GetComponent<UIButton>();
        settingbtn = transform.Find("settingbtn").GetComponent<UIButton>();
        taskbtn = transform.Find("taskbtn").GetComponent<UIButton>();


        EventDelegate ed = new EventDelegate(this, "OnKnapsackShow");
        bagbtn.onClick.Add(ed);
        EventDelegate ed1 = new EventDelegate(this, "OnTaskPanelShow");
        taskbtn.onClick.Add(ed1);
        EventDelegate ed2 = new EventDelegate(this, "OnSkillPanelShow");
        skillbtn.onClick.Add(ed2);

    }
    private void OnKnapsackShow()
    {
        KnapsacManager._instance.OnSelfShow();
    }
    private void OnTaskPanelShow()
    {
        TaskPanelManager._instance.OnSelfShow();
    }
    private void OnSkillPanelShow()
    {
        SkillPanelManager._instance.OnSelfShow();
    }
}
