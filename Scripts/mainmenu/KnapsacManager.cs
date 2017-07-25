using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsacManager : MonoBehaviour
{

    [SerializeField]
    private EquipPopup equipPopup;
    [SerializeField]
    private InventoryPopup inventoryPopup;

    private void Awake()
    {
        equipPopup = transform.Find("equipPopup").GetComponent<EquipPopup>();
        inventoryPopup = transform.Find("inventoryPopup").GetComponent<InventoryPopup>();
    }

    public void OnItemShowClicked(object[] objectArray)
    {
        InventoryItem it = objectArray[0] as InventoryItem;
        InventoryItemUI itUI = objectArray[2] as InventoryItemUI;
        // 如果要显示的物品是武器才需要分成左右两页显示
        if (it.Inventory.InventoryType == InventoryType.Equip)
        {
            bool isLeft = (bool)objectArray[1];
            equipPopup.OnShow(it, itUI, isLeft);
        }
        else
        {
            inventoryPopup.OnShow(it, itUI);
        }
    }

    public void OnEquipBtnClicked(object[] objectArray)
    {
        InventoryItem it = objectArray[0] as InventoryItem;
        InventoryItemUI itUI = objectArray[1] as InventoryItemUI;

        int idx = (int)it.Inventory.EquipType - 1;
        // 如果当前装备位上为空，那么肯定是从背包上点击的该装备 itUI 肯定不为空
        if (PlayerInfo._instance.GetEquipArray(idx) == null)
        {
            // 改变 IPos 即为 it.Inventory.EquipType
            it.IPos = (ItemPos)(idx + 1);
            PlayerInfo._instance.SetEquipArray(it);

            itUI.Set(null);
        }
        else
        {
            // 当前装备位子上已经有装备 有可能执行的是卸下按钮 或者是 装备的时候 需要换下。通过 itUI 是什么来判断
            if (itUI == null)
            {
                // 卸下按钮
                KnapsackInventory._instance.AddInventoryItemToContainer(it);
                // 清空装备位
                PlayerInfo._instance.ClearEquipArray(idx);
            }
            else
            {
                // 先把这个取下来
                InventoryItem tmp = PlayerInfo._instance.GetEquipArray(idx);

                // 改变 IPos 即为 it.Inventory.EquipType
                it.IPos = (ItemPos)(idx + 1);
                PlayerInfo._instance.SetEquipArray(it);

                // 再把tmp装到itUI上
                itUI.Set(tmp);
            }
        }
        equipPopup.OnCloseBtnClicked();
    }
    public void OnUpgradeBtnClicked(object[] objectArray)
    {
        InventoryItem it = objectArray[0] as InventoryItem;
        // 升到level需要消耗的金币
        // Level* price(售价)
        int needCoin = (it.Level + 1) * it.Inventory.Price;
        if(needCoin <= PlayerInfo._instance.Coin)
        {
            PlayerInfo._instance.Coin -= needCoin;
            it.Level += 1;
            equipPopup.OnUpdateItemLevel();
            // 升级到n级
            // 那么生命 伤害 战斗力 为1 + (n - 1) / 10倍
            // TODO
        }  
    }
}