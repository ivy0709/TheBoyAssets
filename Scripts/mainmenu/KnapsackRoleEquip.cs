using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackRoleEquip : MonoBehaviour {

    [SerializeField]
    private UISprite sprite = null;
    private InventoryItem it;
    private UISprite GetSprite()
    {
        if(sprite == null)
        {
            sprite = transform.Find("Sprite").GetComponent<UISprite>();
        }
        return sprite;
    }
	
    public void Set(InventoryItem item)
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

    void OnPress(bool isPressed)
    {
        if(isPressed == true && it != null)
        {
            object[] objectArray = new object[3];
            objectArray[0] = it;
            objectArray[1] = false;
            // 表示的  没有 UIItem 
            objectArray[2] = null;
            transform.parent.parent.SendMessage("OnItemShowClicked", objectArray);
        }
    }
}
