using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackInventory : MonoBehaviour {

    public List<InventoryItemUI> ItemUIContainer;
    public static KnapsackInventory _instance;
    private void Awake()
    {
        _instance = this;
    }
    void Start () {

        InitItemUIContainerShow();


    }

    public void InitItemUIContainerShow()
    {
        List<InventoryItem> itemlist = InventoryManager._instance.InventoryItemlist;

        for(int i = 0; i < itemlist.Count; ++i)
        {
            if(itemlist[i].IPos == ItemPos.Kasnapsack)
            {
                ItemUIContainer[i].Set(itemlist[i]);
            }
        }
        for (int i = itemlist.Count; i < ItemUIContainer.Count; ++i)
        {
            ItemUIContainer[i].Set(null);
        }
    }
    public void AddInventoryItemToContainer(InventoryItem it)
    {
        foreach(InventoryItemUI itemUI in ItemUIContainer)
        {
            if (itemUI.it == null)
            {
                itemUI.Set(it);
                break;
            }
        }
        return;
    }
}
