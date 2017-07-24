using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public TextAsset InventoryList;
    public IDictionary<int, Inventory> InventoryDic;
    
    private void ReadInventoryList()
    {
        string Str = InventoryList.ToString();
        string[] rowArray = Str.Split('\n');
        string[] colArray;
        
        foreach (string row in rowArray)
        {
            colArray = Str.Split('|');
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
                    case "Box":
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
            // 作用类型 作用值 描述
            if (item.InventoryType == InventoryType.Drug)
            {
                switch (colArray[11])
                {
                    //(Helm, Cloth, Weapon, Shoes, Necklace, Bracelet, Ring, Wing) 
                    case "Energy":
                        item.InfoType = InfoType.Energy;
                        break;
                }
            }
            item.ApplyValue = int.Parse(colArray[12]);
            item.Describe = colArray[13];
            InventoryDic.Add(item.Id, item);
        }
    }
}
