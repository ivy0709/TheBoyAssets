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
    }
    private void OnKnapsackShow()
    {
        KnapsacManager._instance.OnSelfShow();
    }
}
