using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    public static PlayerStatus _instance;


    [SerializeField]
    private UISprite headSprite;

    [SerializeField]
    private UILabel levelLabel;
    [SerializeField]
    private UILabel playerNameLabel;
    [SerializeField]
    private UILabel powerValueLabel;


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
    private UIButton sureChangeNameBtn;
    [SerializeField]
    private UIButton cancelChangeNameBtn;

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

    [SerializeField]
    private UIInput newNameInput;

    [SerializeField]
    private TweenPosition positionTween;

    [SerializeField]
    private GameObject chgName;

    #region the unity event
    private void Awake()
    {
        headSprite = transform.Find("headSprite").GetComponent<UISprite>();
        levelLabel = transform.Find("levelLabel").GetComponent<UILabel>();
        playerNameLabel = transform.Find("playerNameLabel").GetComponent<UILabel>();


        powerValueLabel = transform.Find("fightValueLabel").GetComponent<UILabel>();
        expValueLabel = transform.Find("expBar_bg/valueLabel").GetComponent<UILabel>();
        expSlider = transform.Find("expBar_bg/exp").GetComponent<UISlider>();


        coinValueLabel = transform.Find("coin/ValueLabel").GetComponent<UILabel>();
        diamondValueLabel = transform.Find("diamond/ValueLabel").GetComponent<UILabel>();

        closeBtn = transform.Find("closebtn").GetComponent<UIButton>();
        chgNameBtn = transform.Find("changeNameBtn").GetComponent<UIButton>();
        sureChangeNameBtn = transform.Find("changeName/sureBtn").GetComponent<UIButton>();
        cancelChangeNameBtn = transform.Find("changeName/cancelBtn").GetComponent<UIButton>();

        newNameInput = transform.Find("changeName/Input").GetComponent<UIInput>();

        energyValueLabel = transform.Find("energy/ValueLabel").GetComponent<UILabel>();
        energyRestorePartLabel = transform.Find("energy/partNameLabel/valueLabel").GetComponent<UILabel>();
        energyRestoreAllLabel = transform.Find("energy/allNameLabel/valueLabel").GetComponent<UILabel>();

        toughenValueLabel = transform.Find("toughen/ValueLabel").GetComponent<UILabel>();
        toughenRestorePartLabel = transform.Find("toughen/partNameLabel/valueLabel").GetComponent<UILabel>();
        toughenRestoreAllLabel = transform.Find("toughen/allNameLabel/valueLabel").GetComponent<UILabel>();

        

        positionTween = transform.GetComponent<TweenPosition>();

        chgName = transform.Find("changeName").gameObject;

        EventDelegate ed = new EventDelegate(this, "OnCloseBtnClicked");
        closeBtn.onClick.Add(ed);

        EventDelegate ed1 = new EventDelegate(this, "OnChangNameBtnClicked");
        chgNameBtn.onClick.Add(ed1);

        EventDelegate ed2 = new EventDelegate(this, "OnSureChangeNameBtnClicked");
        sureChangeNameBtn.onClick.Add(ed2);

        EventDelegate ed3 = new EventDelegate(this, "OnCancelChangeNameBtnClicked");
        cancelChangeNameBtn.onClick.Add(ed3);

        _instance = this;

        gameObject.SetActive(false);

    }

    private void Start()
    {
        PlayerInfo._instance.OnPlayerInfoChanged += OnPlayerInfoChanged;
        OnPlayerInfoChanged(InfoType.All);
    }
    private void Update()
    {
        OnChangeEnergyAndToughenRestoreTime();
    }

    private void OnDestroy()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= OnPlayerInfoChanged;
    }
    #endregion



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
        else if (type == InfoType.Power)
        {
            powerValueLabel.text = playerInfo.Power.ToString();
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
            headSprite.spriteName = playerInfo.HeadPic;
            playerNameLabel.text = playerInfo.Name;
            levelLabel.text = playerInfo.Level.ToString();
            energyValueLabel.text = playerInfo.Energy + "/" + playerInfo.energyMax;
            toughenValueLabel.text = playerInfo.Toughen + "/" + playerInfo.toughenMax;

            int expMax = GameController.GetExpRequiredByLevel(playerInfo.Level + 1);
            expValueLabel.text = playerInfo.Exp + "/" + expMax;
            expSlider.value = (float)playerInfo.Exp / (float)expMax;

            powerValueLabel.text = playerInfo.Power.ToString();
            coinValueLabel.text = playerInfo.Coin.ToString();
            diamondValueLabel.text = playerInfo.Diamond.ToString();
        }
        return;
    }
    private void OnChangeEnergyAndToughenRestoreTime()
    {
        // 假设就是60秒显示一次了 方便实现
        // 不过感觉有问题 就是焦点不在游戏上 就不会计时

        if(PlayerInfo._instance.energyMax <= PlayerInfo._instance.Energy)
        {
            energyRestorePartLabel.text = "00:00:00";
            energyRestoreAllLabel.text = "00:00:00";
        }
        else
        {
            if (PlayerInfo._instance.energyTimer > 0.0f)
            {
                int leftTime = 60 - (int)PlayerInfo._instance.energyTimer;

                string leftTimeStr = GameController.ChangeNumToSuitableStr(leftTime);
                energyRestorePartLabel.text = "00:00:" + leftTimeStr;

                // 分钟小时  playerInfo.Energy + "/" + playerInfo.energyMax;
                int min = (PlayerInfo._instance.energyMax - PlayerInfo._instance.Energy - 1);
                int hour = min / 60;
                min = min % 60;
                string minStr = GameController.ChangeNumToSuitableStr(min);
                string hourStr = GameController.ChangeNumToSuitableStr(hour);
                energyRestoreAllLabel.text = hourStr + ":" + minStr + ":" + leftTimeStr;
            }
        }
        if (PlayerInfo._instance.toughenMax <= PlayerInfo._instance.Toughen)
        {
            toughenRestorePartLabel.text = "00:00:00";
            toughenRestoreAllLabel.text = "00:00:00";
        }
        else
        {
            if (PlayerInfo._instance.toughenTimer > 0.0f)
            {
                int leftTime = 60 - (int)PlayerInfo._instance.toughenTimer;
                string leftTimeStr = GameController.ChangeNumToSuitableStr(leftTime);
                toughenRestorePartLabel.text = "00:00:" + leftTimeStr;

                int min = (PlayerInfo._instance.toughenMax - PlayerInfo._instance.Toughen - 1);
                int hour = min / 60;
                min = min % 60;
                string minStr = GameController.ChangeNumToSuitableStr(min);
                string hourStr = GameController.ChangeNumToSuitableStr(hour);
                toughenRestoreAllLabel.text = hourStr + ":" + minStr + ":" + leftTimeStr;

            }
        }
        return;
    }
    #region 改名字按钮
    public void OnChangNameBtnClicked()
    {
        chgName.SetActive(true);
    }
    public void OnSureChangeNameBtnClicked()
    {
        string newName = newNameInput.value;
        PlayerInfo._instance.OnPlayerNameChanged(newName);
        chgName.SetActive(false);
    }
    public void OnCancelChangeNameBtnClicked()
    {
        chgName.SetActive(false);
    }
    #endregion

    #region 自身面板 的显示与隐藏
    public void OnCloseBtnClicked()
    {
        positionTween.PlayReverse();
        Invoke("SetSelfInActive", 0.4f);
    }
    public void OnSelfShow()
    {
        transform.gameObject.SetActive(true);
        positionTween.PlayForward();
    }
    private void SetSelfInActive()
    {
        transform.gameObject.SetActive(false);
    }
    #endregion
}