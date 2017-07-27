using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackRoleEquip : MonoBehaviour {

    [SerializeField]
    private UISprite sprite = null;
    private InventoryItem it;

    public InventoryItem It
    {
        get
        {
            return it;
        }

        set
        {
            Set(value);
        }
    }

    private UISprite GetSprite()
    {
        if(sprite == null)
        {
            sprite = transform.Find("Sprite").GetComponent<UISprite>();
        }
        return sprite;
    }
	
    private void Set(InventoryItem item)
    {
        it = item;
        if (item != null)
        {
            GetSprite().spriteName = item.Inventory.Icon;
        }
        else
        {
            GetSprite().spriteName = "bg_道具";
        }
    }

    void OnClick()
    {
        if(It != null)
        {
            object[] objectArray = new object[3];
            objectArray[0] = It;
            // 是否持有UIItem 
            objectArray[1] = null;
            // 是否持有EquipUI 
            objectArray[2] = this;
            transform.parent.parent.SendMessage("OnItemShowClicked", objectArray);
        }
    }
}
