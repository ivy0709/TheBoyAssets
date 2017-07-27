using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackInventory : MonoBehaviour {

    public List<InventoryItemUI> ItemUIContainer;
    public static KnapsackInventory _instance;

    [SerializeField]
    private UIButton arrangeBtn;
    [SerializeField]
    private UILabel statLabel;

    private int statCounts = 0;

    public int StatCounts
    {
        get
        {
            return statCounts;
        }

        set
        {
            statLabel.text = value + "/" + ItemUIContainer.Count;
            statCounts = value;
        }
    }

    private void Awake()
    {
        arrangeBtn = transform.Find("arrangeBtn").GetComponent<UIButton>();
        statLabel = transform.Find("statLabel").GetComponent<UILabel>();
        EventDelegate ed = new EventDelegate(this, "OnArrangeBtnClicked");
        arrangeBtn.onClick.Add(ed);

        _instance = this;
    }
    void Start () {
        InitItemUIContainerShow();
    }
    public void OnArrangeBtnClicked()
    {
        InitItemUIContainerShow();
    }
    public void InitItemUIContainerShow()
    {
        List<InventoryItem> itemlist = InventoryManager._instance.InventoryItemlist;
        int tmp = 0;
        for(int i = 0; i < itemlist.Count; ++i)
        {
            if(itemlist[i].IPos == ItemPos.Kasnapsack)
            {
                ItemUIContainer[tmp++].It = itemlist[i];
            }
        }
        StatCounts = tmp;

        for (int i = tmp; i < ItemUIContainer.Count; ++i)
        {
            ItemUIContainer[tmp++].It = null;
        }
    }
    public void AddInventoryItemToContainer(InventoryItem it)
    {
        foreach(InventoryItemUI itemUI in ItemUIContainer)
        {
            if (itemUI.It == null)
            {
                if(it.IPos != ItemPos.Kasnapsack)
                {
                    it.IPos = ItemPos.Kasnapsack;
                }
                itemUI.UpdateItemUI(it);
                break;
            }
        }
        return;
    }
    public void ChangeStatCounts(int count)
    {
        StatCounts += count;
    }
}
