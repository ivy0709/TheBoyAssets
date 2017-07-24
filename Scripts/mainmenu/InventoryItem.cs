using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem{

    private Inventory _inventory;
    private int _level;
    private int _count;

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
}
