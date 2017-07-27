using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemUI : MonoBehaviour {

    #region property
    private InventoryItem it;
    [SerializeField]
    private UISprite itemSprite;
    [SerializeField]
    private UILabel itemCount;
    #endregion

    #region the set/get method
    public InventoryItem It
    {
        get
        {
            return it;
        }
        set
        {
            it = value;
            // 同时更新图片和数量显示
            UpdateSprite();
            UpdateCount();
        }
    }
    #endregion

    /// <summary>
    /// 更新 ItemUI，调用此函数会涉及到 statCounts 值的更新
    /// </summary>
    /// <param name="item">更新为item</param>
    public void UpdateItemUI(InventoryItem item)
    {
        // statCounts 的更新
        if (It == null)
        {
            if(item == null)
            {
                return;
            }
            else
            {
                transform.parent.parent.gameObject.SendMessage("ChangeStatCounts", 1);
            }
        }
        else
        {
            if(item == null)
            {
                transform.parent.parent.gameObject.SendMessage("ChangeStatCounts", -1);
            }
        }
        It = item;
    }
    private void UpdateSprite()
    {
        if (It == null)
        {
            itemSprite.spriteName = "bg_道具";
        }
        else
        {
            itemSprite.spriteName = It.Inventory.Icon;
        }
    }
    // ItemUI上的个数的显示更新
    private void UpdateCount()
    {
        if(It == null)
        {
            itemCount.text = "";
        }
        else
        {
            if (It.Count > 1)
            {
                itemCount.text = It.Count.ToString();
            }
            else
            {
                itemCount.text = "";
            }
        }
    }
    #region unity event
    private void Awake()
    {
        itemSprite = transform.Find("Sprite").GetComponent<UISprite>();
        itemCount = transform.Find("Label").GetComponent<UILabel>();
        // 因为It = null 会触发更新图片, 所以要先找到图片
        It = null;
    }
    void OnClick()
    {
        if (It != null)
        {
            object[] objectArray = new object[3];
            objectArray[0] = It;
            // 是否持有UIItem
            objectArray[1] = this;
            // 是否持有equipUI
            objectArray[2] = null;
            transform.parent.parent.parent.SendMessage("OnItemShowClicked", objectArray);
        }
    }
    #endregion

    /// <summary>
    /// 减少 数量。
    /// </summary>
    /// <param name="Count">减少的个数</param>
    public void ReduceInventoryCount(int Count)
    {
        if (It.Count - Count >= 1)
        {
            It.Count -= Count;
            // 更新数量显示
            UpdateCount();
        }
        else
        {
            DeleteInventoryItem();
        }
    }
    public void DeleteInventoryItem()
    {
        if(It != null)
        {
            InventoryManager._instance.InventoryItemlist.Remove(It);
            UpdateItemUI(null);
        }
    }
}
