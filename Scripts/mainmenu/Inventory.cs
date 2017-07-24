using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InventoryType
{
    Equip,
    Drug,
    Box,
}

public enum EquipType
{
    Helm,
    Cloth,
    Weapon,
    Shoes,
    Necklace,
    Bracelet,
    Ring,
    Wing,
}


public class Inventory{

    private int _id; // ID
    private string _name;// 名称
    private string _icon;// 图集中的Sprite名称 图标 
    private InventoryType _inventoryType;// 类型（Equip，Drug）
    private EquipType _equipType;// 装备类型 
    private int _price = 0;// 售价
    private int _equipStar = 0;// 星级
    private int _equipQuality = 0;// 品质
    private int _equipDamage = 0;// 伤害
    private int _equipHp = 0;// 生命
    private int _equipPower = 0;// 战斗力
    private InfoType _infoType;// 作用类型
    private int _applyValue = 0;// 作用值
    private string _describe;// 描述

    public int Id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }

    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
        }
    }

    public string Icon
    {
        get
        {
            return _icon;
        }

        set
        {
            _icon = value;
        }
    }

    public InventoryType InventoryType
    {
        get
        {
            return _inventoryType;
        }

        set
        {
            _inventoryType = value;
        }
    }

    public EquipType EquipType
    {
        get
        {
            return _equipType;
        }

        set
        {
            _equipType = value;
        }
    }

    public int Price
    {
        get
        {
            return _price;
        }

        set
        {
            _price = value;
        }
    }

    public int EquipStar
    {
        get
        {
            return _equipStar;
        }

        set
        {
            _equipStar = value;
        }
    }

    public int EquipQuality
    {
        get
        {
            return _equipQuality;
        }

        set
        {
            _equipQuality = value;
        }
    }

    public int EquipDamage
    {
        get
        {
            return _equipDamage;
        }

        set
        {
            _equipDamage = value;
        }
    }

    public int EquipHp
    {
        get
        {
            return _equipHp;
        }

        set
        {
            _equipHp = value;
        }
    }

    public int EquipPower
    {
        get
        {
            return _equipPower;
        }

        set
        {
            _equipPower = value;
        }
    }

    public InfoType InfoType
    {
        get
        {
            return _infoType;
        }

        set
        {
            _infoType = value;
        }
    }

    public int ApplyValue
    {
        get
        {
            return _applyValue;
        }

        set
        {
            _applyValue = value;
        }
    }

    public string Describe
    {
        get
        {
            return _describe;
        }

        set
        {
            _describe = value;
        }
    }
}
