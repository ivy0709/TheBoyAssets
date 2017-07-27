using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPopup : MonoBehaviour {

    [SerializeField]
    private UISprite itemSprite;
    [SerializeField]
    private UILabel itemNameLabel;
    [SerializeField]
    private UILabel describeLabel;
    [SerializeField]
    private UILabel batchUseBtnLabel;
    [SerializeField]
    private UIButton closeBtn;
    [SerializeField]
    private UIButton useBtn;
    [SerializeField]
    private UIButton batchUseBtn;

    private InventoryItemUI itUI;


    private void Awake()
    {
        itemSprite = transform.Find("bg/item/Sprite").GetComponent<UISprite>();
        itemNameLabel = transform.Find("bg/itemNameLabel").GetComponent<UILabel>();
        describeLabel = transform.Find("bg/introLabel").GetComponent<UILabel>();

        batchUseBtnLabel = transform.Find("bg/batchUseBtn/Label").GetComponent<UILabel>();
        closeBtn = transform.Find("bg/closeBtn").GetComponent<UIButton>();
        useBtn = transform.Find("bg/useBtn").GetComponent<UIButton>();
        batchUseBtn = transform.Find("bg/batchUseBtn").GetComponent<UIButton>();


        EventDelegate ed = new EventDelegate(this, "OnCloseBtnClicked");
        closeBtn.onClick.Add(ed);

        EventDelegate ed1 = new EventDelegate(this, "OnUseBtnClicked");
        useBtn.onClick.Add(ed1);

        EventDelegate ed2 = new EventDelegate(this, "OnBatchUseBtnClicked");
        batchUseBtn.onClick.Add(ed2);

        itUI = null;

    }

    public void OnShow(InventoryItem item, InventoryItemUI itemUI)
    {
        this.gameObject.SetActive(true);
        itemSprite.spriteName = item.Inventory.Icon;
        itemNameLabel.text = item.Inventory.Name.ToString();
        describeLabel.text = item.Inventory.Describe;
        batchUseBtnLabel.text = "批量使用(" + item.Count + ")";
        // 第一次 setActive 为true的时候 会执行一次Awake 所以要吧itUI的赋值放在后面
        // 其实这两个相等 itemUI.it == item
        itUI = itemUI;

    }
    public void OnCloseBtnClicked()
    {
        itUI = null;
        this.gameObject.SetActive(false);
        transform.parent.SendMessage("DisableSellBtn", null);
    }
    private void OnUseBtnClicked()
    {
        this.transform.parent.SendMessage("OnInventoryUse",itUI);
        OnCloseBtnClicked();
    }
    private void OnBatchUseBtnClicked()
    {
        this.transform.parent.SendMessage("OnInventoryBatchUse", itUI);
        OnCloseBtnClicked();

    }

}
