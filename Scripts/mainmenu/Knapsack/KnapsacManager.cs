using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsacManager : MonoBehaviour
{
    public static KnapsacManager _instance;


    [SerializeField]
    private EquipPopup equipPopup;
    [SerializeField]
    private InventoryPopup inventoryPopup;
    [SerializeField]
    private KnapsackRole role;
    [SerializeField]
    private UIButton sellBtn;
    [SerializeField]
    private UIButton closeBtn;
    [SerializeField]
    private UILabel price;
    [SerializeField]
    private TweenPosition positionTween;

    [SerializeField]
    private InventoryItemUI curClickedItemUI;
    //[SerializeField]
    //private PowerShow powerShow;
    private void Awake()
    {
        equipPopup = transform.Find("equipPopup").GetComponent<EquipPopup>();
        inventoryPopup = transform.Find("inventoryPopup").GetComponent<InventoryPopup>();
        role = transform.Find("role").GetComponent<KnapsackRole>();
        closeBtn = transform.Find("closeBtn").GetComponent<UIButton>();
        sellBtn = transform.Find("inventory/sellBtn").GetComponent<UIButton>();
        price = transform.Find("inventory/price/Label").GetComponent<UILabel>();
        positionTween = transform.GetComponent<TweenPosition>();


        EventDelegate ed = new EventDelegate(this, "OnSellBtnClicked");
        sellBtn.onClick.Add(ed);

        EventDelegate ed1 = new EventDelegate(this, "OnCloseBtnClicked");
        closeBtn.onClick.Add(ed1);

        _instance = this;
        DisableSellBtn();
        this.gameObject.SetActive(false);
        //powerShow = transform.Find("powerShow").GetComponent<PowerShow>();
    }
    #region 出售按钮的相关功能
    private void DisableSellBtn()
    {
        sellBtn.SetState(UIButtonColor.State.Disabled, true);
        sellBtn.GetComponent<Collider>().enabled = false;
        price.text = "";
        curClickedItemUI = null;
    }
    private void EnableSellBtn()
    {
        if(curClickedItemUI != null)
        {
            sellBtn.SetState(UIButtonColor.State.Normal, true);
            sellBtn.GetComponent<Collider>().enabled = true;
            price.text = curClickedItemUI.It.Inventory.Price * curClickedItemUI.It.Count + "";
        }      
    }
    private void OnSellBtnClicked()
    {
        // 卖了
        if (curClickedItemUI != null)
        {
            sellBtn.SetState(UIButtonColor.State.Normal, true);
            sellBtn.GetComponent<Collider>().enabled = true;
            PlayerInfo._instance.Coin += curClickedItemUI.It.Inventory.Price * curClickedItemUI.It.Count;
            // 删除这个东西 
            curClickedItemUI.DeleteInventoryItem();
            // 关闭界面两个 Popup
            if ((equipPopup.gameObject.activeSelf))
            {
                equipPopup.OnCloseBtnClicked();
            }
            else if ((inventoryPopup.gameObject.activeSelf))
            {
                inventoryPopup.OnCloseBtnClicked();
            }
        }
    }
    #endregion

    /// <summary>
    /// 物品点击事件 触发物品详细信息的弹出框
    /// </summary>
    /// <param name="objectArray"></param>
    public void OnItemShowClicked(object[] objectArray)
    {
        InventoryItem it = objectArray[0] as InventoryItem;
        if(objectArray[1] == null)
        {
            // UIItem为空  是装备位上点击的Item
            KnapsackRoleEquip equipUI = objectArray[2] as KnapsackRoleEquip;
            if (inventoryPopup.enabled)
            {
                inventoryPopup.OnCloseBtnClicked();
            }
            equipPopup.OnShow(it, null, equipUI);
        }
        else
        {
            // 点击 UIItem的时候 显示要区别是物品 还是 装备 弹出不同信息框
            InventoryItemUI itUI = objectArray[1] as InventoryItemUI;
            if (it.Inventory.InventoryType == InventoryType.Equip)
            {
                if (inventoryPopup.enabled)
                {
                    inventoryPopup.OnCloseBtnClicked();
                }
                equipPopup.OnShow(it, itUI, null);
                curClickedItemUI = itUI;
                EnableSellBtn();
            }
            else
            {
                if (equipPopup.enabled)
                {
                    equipPopup.OnCloseBtnClicked();
                }
                inventoryPopup.OnShow(it, itUI);
                curClickedItemUI = itUI;
                EnableSellBtn();
            }
        }
    }
    /// <summary>
    /// 处理装备的穿上和卸下
    /// </summary>
    /// <param name="objectArray"></param>
    public void EquipOrUnEquipItem(object[] objectArray)
    {
        InventoryItem it = objectArray[0] as InventoryItem;
        InventoryItemUI itUI = objectArray[1] as InventoryItemUI;
        KnapsackRoleEquip equipUI = objectArray[2] as KnapsackRoleEquip;
        int idx = (int)it.Inventory.EquipType - 1;
        // 如果itUI == null 那么从装备位置上点击的事件:卸下操作
        if (itUI == null)
        {
            // 仅卸下
            PlayerInfo._instance.UpdateEquipArray(idx, null);
            // 放入背包中
            KnapsackInventory._instance.AddInventoryItemToContainer(it);
        }
        else
        {
            // 仅装备
            if (PlayerInfo._instance.GetEquipArray(idx) == null)
            {   
                // 在idx的位置上 装备上 it
                PlayerInfo._instance.UpdateEquipArray(idx, it);
                // 置空 UIItem
                itUI.UpdateItemUI(null);
            }
            else
            {
                // 换装
                itUI.UpdateItemUI(PlayerInfo._instance.GetEquipArray(idx));
                PlayerInfo._instance.UpdateEquipArray(idx, it);
            }
        }
        equipPopup.OnCloseBtnClicked();
    }
    /// <summary>
    /// 处理装备的升级
    /// </summary>
    /// <param name="objectArray"></param>
    public void OnUpgradeBtnClicked(object[] objectArray)
    {
        InventoryItem it = objectArray[0] as InventoryItem;
        // 升到level需要消耗的金币
        // Level* price(售价)
        int needCoin = (it.Level + 1) * it.Inventory.Price;
        if(needCoin <= PlayerInfo._instance.Coin)
        {
            PlayerInfo._instance.Coin -= needCoin;
            // 武器的等级加1  武器的生命 伤害 战斗力 为1 + (n - 1) / 10倍 同时EquipPop需要更新
            // 怎么记录基础的呢 
            it.Level += 1;
            equipPopup.OnUpdateItemShow();
            // TODO
        }
        else
        {
            MessageManager._instance.OnShow("金币不足，无法升级");
        }
    }
    /// <summary>
    /// 处理物品的使用
    /// </summary>
    /// <param name="itUI"></param>
    public void OnInventoryUse(InventoryItemUI itUI)
    {
        // 使用效果 TODO

        // 减少数量
        itUI.ReduceInventoryCount(1);
    }
    /// <summary>
    /// 处理物品的批量使用
    /// </summary>
    /// <param name="itUI"></param>
    public void OnInventoryBatchUse(InventoryItemUI itUI)
    {
        // 使用效果 TODO

        // 减少数量
        itUI.ReduceInventoryCount(itUI.It.Count);
    }


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