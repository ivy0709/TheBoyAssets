using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 与 EquipType 中的值对应
enum ItemPos
{
    Kasnapsack,
    Helm = 1,
    Cloth,
    Weapon,
    Shoes,
    Necklace,
    Bracelet,
    Ring,
    Wing,
}



public class InventoryItem{
    #region property
    private Inventory _inventory;
    private int _level;
    private int _count;
    private ItemPos _iPos;


    public Inventory Inventory
    {
        get
        {
            return _inventory;
        }

        set
        {
            _inventory = value;
        }
    }

    public int Level
    {
        get
        {
            return _level;
        }

        set
        {
            _level = value;
        }
    }

    public int Count
    {
        get
        {
            return _count;
        }

        set
        {
            _count = value;
        }
    }
    internal ItemPos IPos
    {
        get
        {
            return _iPos;
        }

        set
        {
            _iPos = value;
        }
    }
    #endregion
}
