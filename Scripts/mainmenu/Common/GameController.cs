using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	public static int GetExpRequiredByLevel(int level)
    {
        if (level < 1)
            return -1;
        int count = level - 1;
        return count * 100 + count * (count - 1) * 5;
    }


    public static string ChangeNumToSuitableStr(int num)
    {
        string Str = num < 10 ? "0" + num.ToString() : num.ToString();
        return Str;
    }

    public static int[] GetActuralEquipDamgageHpPower(InventoryItem item)
    {
        int[] ret = new int[3];
        // 那么生命 伤害 战斗力 为1 +(n - 1) / 10倍
        // n 为武器等级
        if (item.Level >= 1)
        {
            float factor = 1.0f + (item.Level - 1) / 10f;
            ret[0] = (int)(item.Inventory.EquipDamage * factor);
            ret[1] = (int)(item.Inventory.EquipHp * factor);
            ret[2] = (int)(item.Inventory.EquipPower * factor);
        }
        return ret;
    }
    public static int GetActuralEquipPower(InventoryItem item)
    {
        int ret = 0;
        // 那么生命 伤害 战斗力 为1 +(n - 1) / 10倍
        // n 为武器等级
        if (item.Level >= 1)
        {
            float factor = 1.0f + (item.Level - 1) / 10f;
            ret = (int)(item.Inventory.EquipPower * factor);
        }
        return ret;
    }
    public static int GetAddEquipPower(InventoryItem oldItem, InventoryItem newItem)
    {
        // 减去旧的 加上新的
        int ret = 0;
        // 那么生命 伤害 战斗力 为1 +(n - 1) / 10倍
        // n 为武器等级
        if (oldItem != null)
        {
            float factor = 1.0f + (oldItem.Level - 1) / 10f;
            ret -= (int)(oldItem.Inventory.EquipPower * factor);
        }
        if (newItem != null)
        {
            float factor = 1.0f + (newItem.Level - 1) / 10f;
            ret += (int)(newItem.Inventory.EquipPower * factor);
        }
        return ret;
    }
    public static int[] GetAddEquipDamageHpPower(InventoryItem oldItem, InventoryItem newItem)
    {
        // 减去旧的 加上新的
        int[] ret = new int[3];
        // 那么生命 伤害 战斗力 为1 +(n - 1) / 10倍
        // n 为武器等级
        if (oldItem != null)
        {
            float factor = 1.0f + (oldItem.Level - 1) / 10f;
            ret[0] -= (int)(oldItem.Inventory.EquipDamage * factor);
            ret[1] -= (int)(oldItem.Inventory.EquipHp * factor);
            ret[2] -= (int)(oldItem.Inventory.EquipPower * factor);
        }
        if (newItem != null)
        {
            float factor = 1.0f + (newItem.Level - 1) / 10f;
            ret[0] += (int)(newItem.Inventory.EquipDamage * factor);
            ret[1] += (int)(newItem.Inventory.EquipHp * factor);
            ret[2] += (int)(newItem.Inventory.EquipPower * factor);
        }
        return ret;
    }
}
