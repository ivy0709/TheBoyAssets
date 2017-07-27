using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour {

    [SerializeField]
    private UILabel label;
    [SerializeField]
    private TweenAlpha tweenAlpha;

    public static MessageManager _instance;

    private void Awake()
    {
        label = transform.Find("Label").GetComponent<UILabel>();
        tweenAlpha = transform.GetComponent<TweenAlpha>();

        EventDelegate ed = new EventDelegate(this, "OnCloseAfterFinished");
        tweenAlpha.AddOnFinished(ed);

        _instance = this;
        this.gameObject.SetActive(false);

    }
    public void OnShow(string str, float time = 0.6f)
    {
        this.gameObject.SetActive(true);
        label.text = str;
        tweenAlpha.PlayForward();
        Invoke("OnClose", time);

    }
    private void OnClose()
    {
        tweenAlpha.PlayReverse();
    }

    private void OnCloseAfterFinished()
    {
        // 等于0 说明是反向播放动画
        if(tweenAlpha.value == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
