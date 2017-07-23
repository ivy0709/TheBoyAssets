using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBar : MonoBehaviour {

    [SerializeField]
    private UISprite headSprite;
    [SerializeField]
    private UILabel levelLabel;
    [SerializeField]
    private UILabel playerNameLabel;
    [SerializeField]
    private UILabel energyValueLabel;
    [SerializeField]
    private UILabel toughenValueLabel;
    [SerializeField]
    private UISlider energySlider;
    [SerializeField]
    private UISlider toughenSlider;
    [SerializeField]
    private UIButton energyPlusBtn;
    [SerializeField]
    private UIButton toughtenPlusBtn;
    [SerializeField]
    private UIButton headBtn;

    private void Awake()
    {
        headSprite = transform.Find("headSprite").GetComponent<UISprite>();

        levelLabel = transform.Find("levelLabel").GetComponent<UILabel>();
        playerNameLabel = transform.Find("playerNameLabel").GetComponent<UILabel>();


        energyValueLabel = transform.Find("energyPrograss/ValueLabel").GetComponent<UILabel>();
        toughenValueLabel = transform.Find("toughenPrograss/ValueLabel").GetComponent<UILabel>();

        energySlider = transform.Find("energyPrograss").GetComponent<UISlider>();
        toughenSlider = transform.Find("toughenPrograss").GetComponent<UISlider>();


        energyPlusBtn = transform.Find("energyPlusBtn").GetComponent<UIButton>();
        toughtenPlusBtn = transform.Find("toughenPlusBtn").GetComponent<UIButton>();
        headBtn = transform.Find("headBtn").GetComponent<UIButton>();

        EventDelegate ed = new EventDelegate(this, "OnPlayerStatusShow");
        headBtn.onClick.Add(ed);

        return;
    }

    private void Start()
    {
        PlayerInfo._instance.OnPlayerInfoChanged += OnPlayerInfoChanged;
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
            headSprite.name = playerInfo.HeadPic;
        }
        else if (type == InfoType.Level)
        {
            levelLabel.text = playerInfo.Level.ToString();
        }
        else if (type == InfoType.Energy)
        {
            energyValueLabel.text = playerInfo.Energy + "/" + playerInfo.energyMax;
            energySlider.value = (float)playerInfo.Energy / (float)playerInfo.energyMax;
        }
        else if (type == InfoType.Toughen)
        {
            toughenValueLabel.text = playerInfo.Toughen + "/" + playerInfo.toughenMax;
            toughenSlider.value = (float)playerInfo.Toughen / (float)playerInfo.toughenMax;
        }
        else if (type == InfoType.All)
        {
            headSprite.spriteName = playerInfo.HeadPic;
            levelLabel.text = playerInfo.Level.ToString();
            playerNameLabel.text = playerInfo.Name;
            energyValueLabel.text = playerInfo.Energy + "/" + playerInfo.energyMax;
            energySlider.value = (float)playerInfo.Energy / (float)playerInfo.energyMax;
            toughenValueLabel.text = playerInfo.Toughen + "/" + playerInfo.toughenMax;
            toughenSlider.value = (float)playerInfo.Toughen / (float)playerInfo.toughenMax;
        }
        return;
    }
    private void OnPlayerStatusShow()
    {
        PlayerStatus._instance.OnPlayerStatusShow();
    }
}
