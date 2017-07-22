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
        toughenRestoreAllLabel = transform.Find("toughen/allNameLabel/valueLabel").GetComponent<UILabel>();
    }

    private void Start()
    {
        PlayerInfo._instance.OnPlayerInfoChanged += OnPlayerInfoChanged;
        OnPlayerInfoChanged(InfoType.All);
    }
    private void Update()
    {
        if(PlayerInfo._instance.energyTimer > 0.0f)
        {
            int leftTime = 60 - (int)PlayerInfo._instance.energyTimer;


            string leftTimeStr = leftTime < 10 ? "0" + leftTime.ToString() : leftTime.ToString();
            energyRestorePartLabel.text = "00:00:" + leftTimeStr;


            string leftTimeMin = "00";
            string leftTimehour = "00";
            // 分钟小时  playerInfo.Energy + "/" + playerInfo.energyMax;
            int min = (PlayerInfo._instance.energyMax - PlayerInfo._instance.Energy - 1);
            int hour = min % 60;
            if(min > 0)
            {
                leftTimeMin = min < 10 ? "0" + min.ToString() : min.ToString();
                leftTimehour = hour < 10 ? "0" + hour.ToString() : hour.ToString();
            }
            energyRestoreAllLabel.text = leftTimehour + ":" + leftTimehour + ":" + leftTimeStr;
            // energyRestoreAllLabel = transform.Find("energy/allNameLabel/valueLabel").GetComponent<UILabel>();


        }
        if (PlayerInfo._instance.toughenTimer > 0.0f)
        {
            int leftTime = 60 - (int)PlayerInfo._instance.toughenTimer;
            string leftTimeStr = leftTime < 10 ? "09" : leftTime.ToString();
            toughenRestorePartLabel.text = "00:00:" + leftTimeStr;

        }
    }
    private void Ondestroy()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= OnPlayerInfoChanged;
    }

    private void OnPlayerInfoChanged(InfoType type)
    {
        PlayerInfo playerInfo = PlayerInfo._instance;
        if (type == InfoType.Name)
        {
            playerNameLabel.text = playerInfo.Name;
        }
        else if (type == InfoType.HeadPic)
        {
            headSprite.spriteName = playerInfo.HeadPic;
        }
        else if (type == InfoType.Level)
        {
            levelLabel.text = playerInfo.Level.ToString();
        }
        else if (type == InfoType.Energy)
        {
            energyValueLabel.text = playerInfo.Energy + "/" + playerInfo.energyMax;
        }
        else if (type == InfoType.Toughen)
        {
            toughenValueLabel.text = playerInfo.Toughen + "/" + playerInfo.toughenMax;
        }
        else if (type == InfoType.Exp)
        {
            int expMax = GameController.GetExpRequiredByLevel(playerInfo.Level + 1);
            expValueLabel.text = playerInfo.Exp + "/" + expMax;
            expSlider.value = (float)playerInfo.Exp / (float)expMax;
        }
        else if (type == InfoType.Fight)
        {
            fightValueLabel.text = playerInfo.Fight.ToString();
        }
        else if (type == InfoType.Coin)
        {
            coinValueLabel.text = playerInfo.Coin.ToString();
        }
        else if (type == InfoType.Diamond)
        {
            diamondValueLabel.text = playerInfo.Diamond.ToString();
        }
        else if (type == InfoType.All)
        {
            headSprite.name = playerInfo.HeadPic;
            playerNameLabel.text = playerInfo.Name;
            levelLabel.text = playerInfo.Level.ToString();
            energyValueLabel.text = playerInfo.Energy + "/" + playerInfo.energyMax;
            toughenValueLabel.text = playerInfo.Toughen + "/" + playerInfo.toughenMax;

            int expMax = GameController.GetExpRequiredByLevel(playerInfo.Level + 1);
            expValueLabel.text = playerInfo.Exp + "/" + expMax;
            expSlider.value = (float)playerInfo.Exp / (float)expMax;

            fightValueLabel.text = playerInfo.Fight.ToString();
            coinValueLabel.text = playerInfo.Coin.ToString();
            diamondValueLabel.text = playerInfo.Diamond.ToString();
        }
        return;
    }
}
