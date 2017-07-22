using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStatus : MonoBehaviour {

    [SerializeField]
    private UISprite headSprite;

    [SerializeField]
    private UILabel levelLabel;
    [SerializeField]
    private UILabel playerNameLabel;

    [SerializeField]
    private UILabel fightValueLabel;


    [SerializeField]
    private UILabel expValueLabel;

    [SerializeField]
    private UISlider expSlider;


    [SerializeField]
    private UILabel coinValueLabel;
    [SerializeField]
    private UILabel diamondValueLabel;

    [SerializeField]
    private UIButton closeBtn;
    [SerializeField]
    private UIButton chgNameBtn;



    [SerializeField]
     private UILabel energyValueLabel;
    [SerializeField]
    private UILabel energyRestorePartLabel;
    [SerializeField]
    private UILabel energyRestoreAllLabel;


    [SerializeField]
    private UILabel toughenValueLabel;
    [SerializeField]
    private UILabel toughenRestorePartLabel;
    [SerializeField]
    private UILabel toughenRestoreAllLabel;



    private void Awake()
    {
        headSprite = transform.Find("headSprite").GetComponent<UISprite>();
        levelLabel = transform.Find("levelLabel").GetComponent<UILabel>();
        playerNameLabel = transform.Find("playerNameLabel").GetComponent<UILabel>();


        fightValueLabel = transform.Find("fightValueLabel").GetComponent<UILabel>();
        expValueLabel = transform.Find("expBar_bg/valueLabel").GetComponent<UILabel>();
        expSlider = transform.Find("expBar_bg/exp").GetComponent<UISlider>();


        coinValueLabel = transform.Find("coin/ValueLabel").GetComponent<UILabel>();
        diamondValueLabel = transform.Find("diamond/ValueLabel").GetComponent<UILabel>();

        closeBtn = transform.Find("closebtn").GetComponent<UIButton>();
        chgNameBtn = transform.Find("changeNameBtn").GetComponent<UIButton>();

        energyValueLabel = transform.Find("energy/ValueLabel").GetComponent<UILabel>();
        energyRestorePartLabel = transform.Find("energy/partNameLabel/valueLabel").GetComponent<UILabel>();
        energyRestoreAllLabel = transform.Find("energy/allNameLabel/valueLabel").GetComponent<UILabel>();

        toughenValueLabel = transform.Find("toughen/ValueLabel").GetComponent<UILabel>();
        toughenRestorePartLabel = transform.Find("toughen/partNameLabel/valueLabel").GetComponent<UILabel>();
        toughenRestoreAllLabel = transform.Find("toughen/allNameLabel /valueLabel").GetComponent<UILabel>();
}
}
