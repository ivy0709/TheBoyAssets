using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptMapManager : MonoBehaviour {

    [SerializeField]
    private TweenPosition tweenPos;
    private TranscriptDiag diag;
    [SerializeField]
    private UIButton backBtn;
    private void Awake()
    {
        tweenPos = this.GetComponent<TweenPosition>();
        diag = this.transform.Find("diag").GetComponent<TranscriptDiag>();
        backBtn = this.transform.Find("backBtn").GetComponent<UIButton>();

        EventDelegate ed1 = new EventDelegate(this, "OnCloseBtnClicked");
        backBtn.onClick.Add(ed1);
    }
    private void OnTranscriptBtnClicked(TranscriptInfo item)
    {
        if(item.NeedLevel > PlayerInfo._instance.Level)
        {
            diag.ShowWarning(item);
        }
        else
        {
            diag.ShowEntering(item);
        }
    }
    private void OnCloseBtnClicked()
    {
        tweenPos.PlayReverse();
    }
    private void OnSelfShow()
    {
        tweenPos.PlayForward();
    }
}
