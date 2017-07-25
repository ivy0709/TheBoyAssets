using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPopup : MonoBehaviour {

    [SerializeField]
    private UISprite itemSprite;

    [SerializeField]
    private UILabel itemNameLabel;
    [SerializeField]
    private UILabel damageValueLabel;
    [SerializeField]
    private UILabel hpValueLabel;
    [SerializeField]
    private UILabel levelValueLabel;
    [SerializeField]
    private UILabel qualityValueLabel;
    [SerializeField]
    private UILabel powerValueLabel;
    [SerializeField]
    private UILabel describeLabel;
    [SerializeField]
    private UILabel equipLabel;

    [SerializeField]
    private UIButton closeBtn;
    [SerializeField]
    private UIButton equipBtn;
    [SerializeField]
    private UIButton upgradeBtn;

    private InventoryItemUI itUI;
    private InventoryItem it;

    private void Awake()
    {
        itemSprite = transform.Find("itemPic/Sprite").GetComponent<UISprite>();
        itemNameLabel = transform.Find("itemNameLabel").GetComponent<UILabel>();

        levelValueLabel = transform.Find("level/Label").GetComponent<UILabel>();

        damageValueLabel = transform.Find("damage/Label").GetComponent<UILabel>();
        hpValueLabel = transform.Find("hp/Label").GetComponent<UILabel>();
        powerValueLabel = transform.Find("power/Label").GetComponent<UILabel>();
        qualityValueLabel = transform.Find("quality/Label").GetComponent<UILabel>();
        describeLabel = transform.Find("Label").GetComponent<UILabel>();
        equipLabel = transform.Find("equipBtn/Label").GetComponent<UILabel>();

        
        closeBtn = transform.Find("closeBtn").GetComponent<UIButton>();
        equipBtn = transform.Find("equipBtn").GetComponent<UIButton>();
        upgradeBtn = transform.Find("upgradeBtn").GetComponent<UIButton>();

        EventDelegate ed = new EventDelegate(this, "OnCloseBtnClicked");
        closeBtn.onClick.Add(ed);
        EventDelegate ed1 = new EventDelegate(this, "OnEquipBtnClicked");
        equipBtn.onClick.Add(ed1);
        EventDelegate ed2 = new EventDelegate(this, "OnUpgradeBtnClicked");
        upgradeBtn.onClick.Add(ed2);

        itUI = null;
    }
    public void OnShow(InventoryItem item, InventoryItemUI itemUI , bool isLeft)
    {
        // 当点击的的是装备位上的装备的时候  itUI == null 
        // 当点击的的是背包位上的装备的时候  itUI != null 
        itUI = itemUI;
        it = item;
        this.gameObject.SetActive(true);
        if(isLeft == true)
        {
            this.gameObject.transform.position = new Vector3(-System.Math.Abs(this.gameObject.transform.position.x), this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
        else
        {
            this.gameObject.transform.position = new Vector3(System.Math.Abs(this.gameObject.transform.position.x), this.gameObject.transform.position.y, this.gameObject.transform.position.z);

        }
        if (itUI == null)
        {
            equipLabel.text = "卸下";
        }
        else
        {
            equipLabel.text = "装备";
        }
        itemSprite.spriteName = item.Inventory.Icon;
        itemNameLabel.text = item.Inventory.Name.ToString();

        levelValueLabel.text = item.Level.ToString();
        damageValueLabel.text = item.Inventory.EquipDamage.ToString();
        hpValueLabel.text = item.Inventory.EquipHp.ToString();
        powerValueLabel.text = item.Inventory.EquipPower.ToString();
        qualityValueLabel.text = item.Inventory.EquipQuality.ToString();

        describeLabel.text = item.Inventory.Describe;
    }

    public void OnCloseBtnClicked()
    {
        itUI = null;
        it = null;
        this.gameObject.SetActive(false);
    }
    
    private void OnEquipBtnClicked()
    {
        object[] objectArray = new object[2];
        objectArray[0] = it;
        objectArray[1] = itUI;
        transform.parent.SendMessage("OnEquipBtnClicked", objectArray);
    }

    private void OnUpgradeBtnClicked()
    {
        object[] objectArray = new object[1];
        objectArray[0] = it;
        transform.parent.SendMessage("OnUpgradeBtnClicked", objectArray);
    }
    public void OnUpdateItemLevel()
    {
        if(it != null)
        {
            levelValueLabel.text = it.Level.ToString();
        }
    }
}
