using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBar : MonoBehaviour {
    [SerializeField]
    private UILabel coinValueLabel;
    [SerializeField]
    private UILabel diamondValueLabel;

    [SerializeField]
    private UIButton coinPlusBtn;
    [SerializeField]
    private UIButton diamondPlusBtn;



    private void Awake()
    {
        coinValueLabel = transform.Find("bg_coin/Label").GetComponent<UILabel>();
        diamondValueLabel = transform.Find("bg_diamond/Label").GetComponent<UILabel>();

        coinPlusBtn = transform.Find("bg_coin/Button").GetComponent<UIButton>();
        diamondPlusBtn = transform.Find("bg_diamond/Button").GetComponent<UIButton>();
    }
    private void Start()
    {
        PlayerInfo._instance.OnPlayerInfoChanged += OnPlayerInfoChanged;
    }
    private void OnDestroy()
    {
        PlayerInfo._instance.OnPlayerInfoChanged -= OnPlayerInfoChanged;
    }

    private void OnPlayerInfoChanged(InfoType type)
    {
        PlayerInfo playerInfo = PlayerInfo._instance;
        if (type == InfoType.Coin)
        {
            coinValueLabel.text = playerInfo.Coin.ToString();
        }
        else if (type == InfoType.Diamond)
        {
            diamondValueLabel.text = playerInfo.Diamond.ToString();
        }
        else if (type == InfoType.All)
        {
            coinValueLabel.text = playerInfo.Coin.ToString();
            diamondValueLabel.text = playerInfo.Diamond.ToString();
        }
        return;
    }
}
