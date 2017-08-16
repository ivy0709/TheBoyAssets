using System.Collections;
using System.Collections.Generic;
using TaiDouCommon.Model;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public TextAsset InventoryText;
    // 策划配置的表里面的所有的道具信息
    public Dictionary<int, Inventory> InventoryDic = new Dictionary<int, Inventory>();

    // 该角色所拥有的道具信息 应该放到角色相关信息里吧
    public List<InventoryItem> InventoryItemlist = new List<InventoryItem>();

    public static InventoryManager _instance;

    public InventoryItemDBController inventoryItemDBController;
    private void Awake()
    {
        ReadInventoryList();
        _instance = this;
        inventoryItemDBController = this.GetComponent<InventoryItemDBController>();
        inventoryItemDBController.OnAddInventoryItemDB += OnAddInventoryItemDB;
        inventoryItemDBController.OnGetInventoryItemDBs += OnGetInventoryItemDBs;
        inventoryItemDBController.OnUpdateInventoryItemDB += OnUpdateInventoryItemDB;
    }

    private void Start()
    {
        // 向服务器询问 inventoryitemDB
        inventoryItemDBController.GetInventoryItemDBs();
    }

    private void OnDestroy()
    {

        if (inventoryItemDBController != null)
        {
            inventoryItemDBController.OnAddInventoryItemDB -= OnAddInventoryItemDB;
            inventoryItemDBController.OnGetInventoryItemDBs -= OnGetInventoryItemDBs;
            inventoryItemDBController.OnUpdateInventoryItemDB -= OnUpdateInventoryItemDB;
        }
    }

    private void ReadInventoryList()
    {
        string Str = InventoryText.ToString();
        string[] rowArray = Str.Split('\n');
        string[] colArray;
        // 跳过第一行
        bool IsFirstRow = true;
        foreach (string row in rowArray)
        {
            if(IsFirstRow)
            {
                IsFirstRow = false;
                continue;
            }
            colArray = row.Split('|');
            // ID 名称 图标 类型（Equip，Drug） 装备类型(Helm, Cloth, Weapon, Shoes, Necklace, Bracelet, Ring, Wing) 
            Inventory item = new Inventory();
            item.Id = int.Parse(colArray[0]);
            item.Name = colArray[1];
            item.Icon = colArray[2];
            switch(colArray[3])
            {
                case "Equip":
                    item.InventoryType = InventoryType.Equip;
                    break;
                case "Drug":
                    item.InventoryType = InventoryType.Drug;
                    break;
                case "Box":
                    item.InventoryType = InventoryType.Box;
                    break;
            }
            if(item.InventoryType == InventoryType.Equip)
            {
                switch (colArray[4])
                {
                    //(Helm, Cloth, Weapon, Shoes, Necklace, Bracelet, Ring, Wing) 
                    case "Helm":
                        item.EquipType = EquipType.Helm;
                        break;
                    case "Cloth":
                        item.EquipType = EquipType.Cloth;
                        break;
                    case "Weapon":
                        item.EquipType = EquipType.Weapon;
                        break;
                    case "Shoes":
                        item.EquipType = EquipType.Shoes;
                        break;
                    case "Necklace":
                        item.EquipType = EquipType.Necklace;
                        break;
                    case "Bracelet":
                        item.EquipType = EquipType.Bracelet;
                        break;
                    case "Ring":
                        item.EquipType = EquipType.Ring;
                        break;
                    case "Wing":
                        item.EquipType = EquipType.Wing;
                        break;
                }
            }
            // 售价    
            item.Price = int.Parse(colArray[5]);
            // 星级 品质 伤害 生命 战斗力 
            if (item.InventoryType == InventoryType.Equip)
            {
                item.EquipStar = int.Parse(colArray[6]);
                item.EquipQuality = int.Parse(colArray[7]);
                item.EquipDamage = int.Parse(colArray[8]);
                item.EquipHp = int.Parse(colArray[9]);
                item.EquipPower = int.Parse(colArray[10]);

            }
            // 作用类型 作用值
            if (item.InventoryType == InventoryType.Drug)
            {
                switch (colArray[11])
                {
                    //(Helm, Cloth, Weapon, Shoes, Necklace, Bracelet, Ring, Wing) 
                    case "Energy":
                        item.InfoType = InfoType.Energy;
                        break;
                }
                item.ApplyValue = int.Parse(colArray[12]);
            }
            // 描述
            item.Describe = colArray[13];
            InventoryDic.Add(item.Id, item);
        }
    }

    public void LoadInventoryOwned()
    {
        // 应该是加载角色档案 获得所拥有的道具信息 TODO
        // 这里随机生成吧
        for(int i = 0; i < 16; ++i)
        {
            int id = Random.Range(1001, 1019);
            Inventory inventory = new Inventory();
            bool isExist = InventoryDic.TryGetValue(id, out inventory);
            if(isExist)
            {
                if(inventory.InventoryType == InventoryType.Equip)
                {
                    InventoryItem item = new InventoryItem();
                    // 引用的 InventoryDic里的inventory
                    item.Inventory = inventory;
                    item.Count = 1;
                    item.IPos = ItemPos.Kasnapsack;
                    item.Level = Random.Range(1,10);
                    InventoryItemlist.Add(item);
                }
                else
                {
                    bool isSame = false;
                    foreach(InventoryItem item in InventoryItemlist)
                    {
                        if(item.Inventory.Id == id)
                        {
                            item.Count++;
                            isSame = true;
                            break;
                        }
                    }
                    if(!isSame)
                    {
                        InventoryItem item = new InventoryItem();
                        item.Inventory = inventory;
                        item.Count = 1;
                        item.IPos = ItemPos.Kasnapsack;
                        InventoryItemlist.Add(item);
                    }

                }
            }
        }
            
        // 硬写死四个 在装备位置上
        for (int i = 0; i < 4; ++i)
        {
            Inventory inventory = new Inventory();
            InventoryDic.TryGetValue(1004 + i, out inventory);
            InventoryItem item = new InventoryItem();
            item.Inventory = inventory;
            item.Count = 1;
            item.Level = Random.Range(1, 10);
            item.IPos = (ItemPos)inventory.EquipType;
            InventoryItemlist.Add(item);
        }
    }


    public void OnGetInventoryItemDBs(List<InventoryItemDB> list)
    {
        // 得到了服务器的返回的 itemdb
        if (list != null && list.Count > 0)
        {
            // 创建 InventoryItemlist
            InventoryItem item = new InventoryItem();
            // 引用的 InventoryDic里的inventory
            item.Inventory = inventory;
            item.Count = 1;
            item.IPos = ItemPos.Kasnapsack;
            item.Level = Random.Range(1, 10);
            InventoryItemlist.Add(item);


        }
    }

    public void OnAddInventoryItemDB(InventoryItemDB db)
    {
        
    }

    public void OnUpdateInventoryItemDB()
    {
        
    }


}
