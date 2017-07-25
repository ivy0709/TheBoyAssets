using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPopup : MonoBehaviour {

    // Use this for initialization
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

    private InventoryItemUI itUI;

    private void Awake()
    {
        itemSprite = transform.Find("bg/item/Sprite").GetComponent<UISprite>();
        itemNameLabel = transform.Find("bg/itemNameLabel").GetComponent<UILabel>();
        describeLabel = transform.Find("bg/introLabel").GetComponent<UILabel>();
        batchUseBtnLabel = transform.Find("bg/batchUseBtn/Label").GetComponent<UILabel>();
        closeBtn = transform.Find("bg/closeBtn").GetComponent<UIButton>();

        EventDelegate ed = new EventDelegate(this, "OnCloseBtnClicked");
        closeBtn.onClick.Add(ed);
        itUI = null;

    }

    public void OnShow(InventoryItem item, InventoryItemUI itemUI)
    {
        itUI = itemUI;
        // 其实这两个相等 itemUI.it == item

        this.gameObject.SetActive(true);

        itemSprite.spriteName = item.Inventory.Icon;
        itemNameLabel.text = item.Inventory.Name.ToString();
        describeLabel.text = item.Inventory.Describe;
        batchUseBtnLabel.text = "批量使用(" + item.Count + ")";
    }

    private void OnCloseBtnClicked()
    {
        itUI = null;
        this.gameObject.SetActive(false);
    }

}
