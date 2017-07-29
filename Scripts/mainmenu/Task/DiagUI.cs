using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagUI : MonoBehaviour {
    public static DiagUI _instance;

    private UILabel talkInfo;
    private UIButton acceptBtn;
    private TweenPosition tweenPos;

    private void Awake()
    {
        _instance = this;
        talkInfo = this.transform.Find("Label").GetComponent<UILabel>();
        acceptBtn = this.transform.Find("btn").GetComponent<UIButton>();
        tweenPos = this.transform.GetComponent<TweenPosition>();

        EventDelegate ed1 = new EventDelegate(this, "OnAcceptBtnClicked");
        acceptBtn.onClick.Add(ed1);
    }

    public void OnAcceptBtnClicked() {
        tweenPos.PlayReverse();
        TaskManager._instance.OnAcceptTask();
    }

    public void OnShow(string str)
    {
        talkInfo.text = str;
        tweenPos.PlayForward();
    }


}
