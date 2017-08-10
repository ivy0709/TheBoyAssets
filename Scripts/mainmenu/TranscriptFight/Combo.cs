using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour {
    public static Combo _instance;
    [SerializeField]
    private UILabel comboLabel; 


    private int comboCount;
    private float timer;

    public float intervalTime = 2.0f;

    private void Awake()
    {
        _instance = this;
        comboLabel = this.transform.Find("Label").GetComponent<UILabel>();
        Reset();
    }
	// Update is called once per frame
	void Update () {
        // 当timer 小于零了以后就不走计时逻辑了
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                Reset();
            }
        }
	}


    // Use this for initialization
    public void ComboPlus()
    {
        this.gameObject.SetActive(true);
        comboCount++;
        comboLabel.text = comboCount.ToString();
        timer = intervalTime;
        // 显示震动和放大效果
        transform.localScale = Vector3.one;
        iTween.ScaleBy(this.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.0f);
        iTween.ShakePosition(this.gameObject, new Vector3(0.2f, 0.2f, 0.2f), 0.2f);
    }

    private void Reset()
    {
        comboCount = 0;
        timer = 0.0f;
        this.gameObject.SetActive(false);
    }
}
