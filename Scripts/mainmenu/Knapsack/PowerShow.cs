using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShow : MonoBehaviour {

    [SerializeField]
    private UILabel label;
    [SerializeField]
    private TweenAlpha tweenAlpha;


    [SerializeField]
    private float startValue = 0;
    [SerializeField]
    private int endValue = 0;
    [SerializeField]
    private float speed = 2000;
    [SerializeField]
    private bool isStart = false;
    [SerializeField]
    private bool isAdd = true;
    public static PowerShow _instance;
    private void Awake()
    {
        label = transform.Find("Label").GetComponent<UILabel>();
        tweenAlpha = transform.GetComponent<TweenAlpha>();

        EventDelegate ed = new EventDelegate(this, "OnCloseAfterFinished");
        tweenAlpha.AddOnFinished(ed);
        _instance = this;
        this.gameObject.SetActive(false);
    }
    public void OnShow(int startValue, int AddValue)
    {
        if(AddValue == 0)
        {
            return;
        }
        this.gameObject.SetActive(true);
        tweenAlpha.PlayForward();
        this.startValue = startValue;
        this.endValue = startValue + AddValue;
        if(AddValue > 0)
        {
            isAdd = true;
        }else
        {
            isAdd = false;
        }
        isStart = true;
    }
    private void Update()
    {
        if(isStart)
        {
            if(isAdd)
            {
                startValue += speed * Time.deltaTime;
                if ((int)startValue > endValue)
                {
                    isStart = false;
                    startValue = endValue;
                }
            }
            else
            {
                startValue -= speed * Time.deltaTime;
                if ((int)startValue < endValue)
                {
                    isStart = false;
                    startValue = endValue;
                }
            }

            label.text = (int)startValue + "";
            if(isStart == false)
            {
                tweenAlpha.PlayReverse();
            }
        }
    }
    private void OnCloseAfterFinished()
    {
        // 等于0 说明是反向播放动画
        if (tweenAlpha.value == 0)
        {
            this.gameObject.SetActive(false);
        }
    }

}
