using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum InfoType
{
    Name,
    HeadPic,
    Level,
    Fight,
    Diamond,
    Exp,
    Coin,
    Energy,
    Toughen,
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
    // 8个装备位
    private int _helmID;
    private int _clothID;
    private int _weaponID;
    private int _shoesID;
    private int _necklaceID;
    private int _braceletID;
    private int _ringID;
    private int _wingID;
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

    public int Fight
    {
        get
        {
            return _power;
        }

        set
        {
            _power = value;
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
        }
    }

    public int HelmID
    {
        get
        {
            return _helmID;
        }

        set
        {
            _helmID = value;
        }
    }

    public int ClothID
    {
        get
        {
            return _clothID;
        }

        set
        {
            _clothID = value;
        }
    }

    public int WeaponID
    {
        get
        {
            return _weaponID;
        }

        set
        {
            _weaponID = value;
        }
    }

    public int ShoesID
    {
        get
        {
            return _shoesID;
        }

        set
        {
            _shoesID = value;
        }
    }

    public int NecklaceID
    {
        get
        {
            return _necklaceID;
        }

        set
        {
            _necklaceID = value;
        }
    }

    public int BraceletID
    {
        get
        {
            return _braceletID;
        }

        set
        {
            _braceletID = value;
        }
    }

    public int RingID
    {
        get
        {
            return _ringID;
        }

        set
        {
            _ringID = value;
        }
    }

    public int WingID
    {
        get
        {
            return _wingID;
        }

        set
        {
            _wingID = value;
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
                OnPlayerInfoChanged(InfoType.Energy);
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
                OnPlayerInfoChanged(InfoType.Toughen);
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
        Fight = 1234;
        Diamond = 11111;
        Exp = 456;
        Coin = 22222;
        Energy = 99;
        Toughen = 40;
        OnPlayerInfoChanged(InfoType.All);
    }

    public void OnPlayerNameChanged(string newName)
    {
        Name = newName;
        OnPlayerInfoChanged(InfoType.Name);
    }
}
