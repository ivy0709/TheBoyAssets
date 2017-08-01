using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptDiag : MonoBehaviour {
    [SerializeField]
    private UILabel decribe;
    [SerializeField]
    private UILabel energyText;
    [SerializeField]
    private UILabel energyValue;
    [SerializeField]
    private UIButton enterBtn;
    [SerializeField]
    private UIButton closeBtn;
    [SerializeField]
    private UILabel enterBtnLabel;
    [SerializeField]
    private TweenScale tweenScale;
    private TranscriptInfo it;
    private bool isWarning;
    private void Awake()
    {
        decribe = this.transform.Find("Sprite/describe").GetComponent<UILabel>();
        energyText = this.transform.Find("Sprite/Label").GetComponent<UILabel>();
        energyValue = this.transform.Find("Sprite/Label/value").GetComponent<UILabel>();
        enterBtnLabel = this.transform.Find("enterBtn/Label").GetComponent<UILabel>();

        enterBtn = this.transform.Find("enterBtn").GetComponent<UIButton>();
        closeBtn = this.transform.Find("closeBtn").GetComponent<UIButton>();
        EventDelegate ed1 = new EventDelegate(this, "OnCloseBtnClicked");
        closeBtn.onClick.Add(ed1);
        EventDelegate ed = new EventDelegate(this, "OnEnterBtnClicked");
        enterBtn.onClick.Add(ed);
        tweenScale = this.GetComponent<TweenScale>();
        it = null;
    }
    public void ShowWarning(TranscriptInfo item)
    {
        isWarning = true;
        decribe.text = "当前副本所需等级为" + item.NeedLevel + "。请升级后前来。";
        energyText.gameObject.SetActive(false);
        enterBtnLabel.text = "确定";
        tweenScale.PlayForward();
    }
    public void ShowEntering(TranscriptInfo item)
    {
        isWarning = false;
        decribe.text = item.Scenename + "   " + item.Describe;
        energyText.gameObject.SetActive(true);
        energyValue.text = "34";
        enterBtnLabel.text = "进入";
        tweenScale.PlayForward();
    }
    public void OnCloseBtnClicked()
    {
        tweenScale.PlayReverse();
    }
    public void OnEnterBtnClicked()
    {
        // 上传事件到 TranscriptPanel 
        if(!isWarning)
        {
            transform.parent.SendMessage("OnEnterBtnClicked", null);
        }
        tweenScale.PlayReverse();
    }
}
