using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemUI : MonoBehaviour {

    public InventoryItem it;

    [SerializeField]
    private UISprite itemSprite;

    [SerializeField]
    private UILabel itemCount;

    private void Awake()
    {
        it = null;
        itemSprite = transform.Find("Sprite").GetComponent<UISprite>();
        itemCount = transform.Find("Label").GetComponent<UILabel>();
    }

    public void Set(InventoryItem item)
    {
        if(item == null)
        {
            it = null;
            itemSprite.spriteName = "bg_道具";
            itemCount.text = "";
        }
        else
        {
            it = item;
            itemSprite.spriteName = item.Inventory.Icon;
            if (item.Count > 1)
            {
                itemCount.text = item.Count.ToString();
            }
            else
            {
                itemCount.text = "";
            }
        }
    }
    void OnPress(bool isPressed)
    {
        if (isPressed == true && it != null)
        {
            object[] objectArray = new object[3];
            objectArray[0] = it;
            // 下面那个参数用到的是就是只有类型是 Equip ，所以就设置成 true即可
            objectArray[1] = true;
            objectArray[2] = this;
            transform.parent.parent.parent.SendMessage("OnItemShowClicked", objectArray);
        }
    }

}
