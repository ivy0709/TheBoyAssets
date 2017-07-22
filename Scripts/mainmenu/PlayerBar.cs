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

    }

    private void Update()
    {
        
    }

}
