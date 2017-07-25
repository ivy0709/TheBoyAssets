using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum InfoType
{
    Name,
    HeadPic,
    Level,
    Power,
    Diamond,
    Exp,
    Coin,
    Energy,
    Toughen,
    Hp,
    Damage,
    HelmID,
    ClothID,
    WeaponID,
    ShoesID,
    NecklaceID,
    BraceletID,
    RingID,
    WingID,
    All,
}

public class PlayerInfo : MonoBehaviour {
    // 姓名
    // 头像
    // 等级
    // 战斗力
    // 经验数
    // 钻石数
    // 金币数
    // 体力数
    // 历练数
    #region property
    private string _name;
    private string _headPic;
    private int _level;
    private int _power;
    private int _diamond;
    private int _exp;
    private int _coin;
    private int _energy;
    private int _toughen;
    private int _hp;
    private int _damage;
   
    private InventoryItem[] equipArray = new InventoryItem[8];
    // 8个装备位 每次设置这个得时候 记得分发事件  设置函数
    public void SetEquipArray(InventoryItem item)
    {

        equipArray[(int)item.IPos - 1] = item;
        OnPlayerInfoChanged((InfoType)((int)item.IPos + 10));
    }
    // 穿进来的这个item是个空值
    public void ClearEquipArray(int idx)
    {
        equipArray[idx] = null;
        OnPlayerInfoChanged((InfoType)(idx + 1 + 10));
    }
    public InventoryItem GetEquipArray(int idx)
    {
        if(idx >= 0 && idx <=7)
        {
            return equipArray[idx];
        }
        return null;  
    }
    #endregion


    #region the get/set method
    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
            OnPlayerInfoChanged(InfoType.Name);

        }
    }

    public string HeadPic
    {
        get
        {
            return _headPic;
        }

        set
        {
            _headPic = value;
            OnPlayerInfoChanged(InfoType.HeadPic);

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
            OnPlayerInfoChanged(InfoType.Level);

        }
    }

    public int Power
    {
        get
        {
            return _power;
        }

        set
        {
            _power = value;
            OnPlayerInfoChanged(InfoType.Power);

        }
    }

    public int Diamond
    {
        get
        {
            return _diamond;
        }

        set
        {
            _diamond = value;
            OnPlayerInfoChanged(InfoType.Diamond);

        }
    }

    public int Exp
    {
        get
        {
            return _exp;
        }

        set
        {
            _exp = value;
            OnPlayerInfoChanged(InfoType.Exp);

        }
    }

    public int Coin
    {
        get
        {
            return _coin;
        }

        set
        {
            _coin = value;
            OnPlayerInfoChanged(InfoType.Coin);

        }
    }

    public int Energy
    {
        get
        {
            return _energy;
        }

        set
        {
            _energy = value;
            OnPlayerInfoChanged(InfoType.Energy);
        }
    }

    public int Toughen
    {
        get
        {
            return _toughen;
        }

        set
        {
            _toughen = value;
            OnPlayerInfoChanged(InfoType.Toughen);
        }
    }

    public int Hp
    {
        get
        {
            return _hp;
        }

        set
        {
            _hp = value;
            OnPlayerInfoChanged(InfoType.Hp);
        }
    }

    public int Damage
    {
        get
        {
            return _damage;
        }

        set
        {
            _damage = value;
            OnPlayerInfoChanged(InfoType.Damage);
        }
    }

    #endregion


    public float energyTimer = 0.0f;
    public float toughenTimer = 0.0f;

    private float restoreEnergyInterval = 60.0f;
    private float restoreToughenInterval = 60.0f;

    public  int energyMax = 100;
    public  int toughenMax = 50;



    public static PlayerInfo _instance;

    public delegate void OnPlayerInfoChangedEvent(InfoType type);
    public event OnPlayerInfoChangedEvent OnPlayerInfoChanged;

    #region unity event
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Init();
    }


    private void Update()
    {
        energyTimer += Time.deltaTime;
        // 一分钟恢复一格
        if (Energy < energyMax)
        {
            if (energyTimer >= restoreEnergyInterval)
            {
                Energy++;
                energyTimer -= restoreEnergyInterval;
 
                
            }
        }
        else
        {
            energyTimer = 0.0f;
        }
        
        toughenTimer += Time.deltaTime;
        if (Toughen < toughenMax)
        {
            if (toughenTimer >= restoreToughenInterval)
            {
                Toughen++;
                toughenTimer -= restoreToughenInterval;
            }
        }
        else
        {
            toughenTimer = 0.0f;
        }

    }

    #endregion


    /// <summary>
    /// 初始化人物信息
    /// </summary>
    private void Init()
    {
        // 应该根据选的角色来初始化 TODO
        // 这里随便写了 也方便测试
        Name = "小强";
        HeadPic = "头像底板男性";
        Level = 8;
        Power = 1234;
        Diamond = 11111;
        Exp = 456;
        Coin = 22222;
        Energy = 99;
        Toughen = 40;

        // 就初始化这4个 其他四个默认为空
        InitEquipArray();
        // 生命值和伤害等到把身上装备都换完以后再进行 调用Change事件
        InitHPDamagePower();
    }
    private void InitEquipArray()
    {
        List<InventoryItem> itemlist = InventoryManager._instance.InventoryItemlist;
        foreach(InventoryItem item in itemlist)
        {
            if(item.IPos >= ItemPos.Helm && item.IPos <= ItemPos.Wing)
            {
                // 把定死的四个赋值进去 看看其他的是不是空
                // i 为第几个元素
                SetEquipArray(item);
            }
        }
    }
    private void InitHPDamagePower()
    {
        for(int i = 0; i < 8; ++i)
        {
            if(equipArray[i] != null)
            {
                _hp += equipArray[i].Inventory.EquipHp;
                _damage += equipArray[i].Inventory.EquipDamage;
                _power += equipArray[i].Inventory.EquipPower;
            }
        }
        // 这里才会调用更新方法
        Hp = Level * 100 + _hp;
        Damage = Level * 50 + _damage;
        Hp = Hp + Damage + _power;
        return;
    }
    public void OnPlayerNameChanged(string newName)
    {
        Name = newName;
    }
}
