using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackRole : MonoBehaviour {

    [SerializeField]
    private UILabel playerNameLabel;
    [SerializeField]
    private UILabel damageValueLabel;
    [SerializeField]
    private UILabel hpValueLabel;

    [SerializeField]
    private UILabel expValueLabel;
    [SerializeField]
    private UISlider expSlider;


    [SerializeField]
    private KnapsackRoleEquip helm;
    [SerializeField]
    private KnapsackRoleEquip cloth;
    [SerializeField]
    private KnapsackRoleEquip weapon;
    [SerializeField]
    private KnapsackRoleEquip shoes;
    [SerializeField]
    private KnapsackRoleEquip necklace;
    [SerializeField]
    private KnapsackRoleEquip bracelet;
    [SerializeField]
    private KnapsackRoleEquip ring;
    [SerializeField]
    private KnapsackRoleEquip wing;

    private KnapsackRoleEquip[] EquipArray = new KnapsackRoleEquip[8];

    private void Awake()
    {
        playerNameLabel = transform.Find("playerNameLabel").GetComponent<UILabel>();
        damageValueLabel = transform.Find("damage/Sprite/Label").GetComponent<UILabel>();
        hpValueLabel = transform.Find("hp/Sprite/Label").GetComponent<UILabel>();

        expValueLabel = transform.Find("exp/Label").GetComponent<UILabel>();
        expSlider = transform.Find("exp/Slider").GetComponent<UISlider>();

        EquipArray[0] = transform.Find("helmet").GetComponent<KnapsackRoleEquip>();
        EquipArray[1] = transform.Find("cloth").GetComponent<KnapsackRoleEquip>();
        EquipArray[2] = transform.Find("weapon").GetComponent<KnapsackRoleEquip>();
        EquipArray[3] = transform.Find("shoes").GetComponent<KnapsackRoleEquip>();
        EquipArray[4] = transform.Find("necklace").GetComponent<KnapsackRoleEquip>();
        EquipArray[5] = transform.Find("bracelet").GetComponent<KnapsackRoleEquip>();
        EquipArray[6] = transform.Find("ring").GetComponent<KnapsackRoleEquip>();
        EquipArray[7] = transform.Find("wing").GetComponent<KnapsackRoleEquip>();
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

        if(type >= InfoType.HelmID && type <= InfoType.WingID)
        {
            EquipArray[type - InfoType.HelmID].Set(playerInfo.GetEquipArray(type - InfoType.HelmID));
        }
        else if (type == InfoType.Name)
        {
            playerNameLabel.text = playerInfo.Name;
        }
        else if (type == InfoType.Damage)
        {
            damageValueLabel.text = playerInfo.Damage.ToString();
        }
        else if (type == InfoType.Hp)
        {
            hpValueLabel.text = playerInfo.Hp.ToString();
        }
        else if (type == InfoType.Exp)
        {
            int expMax = GameController.GetExpRequiredByLevel(playerInfo.Level + 1);
            expValueLabel.text = playerInfo.Exp + "/" + expMax;
            expSlider.value = (float)playerInfo.Exp / (float)expMax;
        }
        return;
    }
}
