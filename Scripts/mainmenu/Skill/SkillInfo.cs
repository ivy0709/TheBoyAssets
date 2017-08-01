using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SkillType
{
    Basic,
    Skill,
}

public enum PosType
{
    Basic,
    One = 1,
    Two,
    Three,
}
public class SkillInfo{
    private int _id; // ID
    private string _name;// 名称
    private string _icon;// 图集中的Sprite名称 图标 

    private PlayerType _playerType; // 角色类型(战士，女刺客 Warrior, FemaleAssassin)
    private SkillType _skillType;   //技能类型（基础攻击， 技能 Basic, Skill）
    private PosType _posType;  //位置(基础位置，一号技能位置，二号技能位置，三号技能位置 Basic, One, Two, Three)

    private int _coldTime;// 冷却时间
    private int _baseDamage;// 基础攻击力
    private int _level = 1;// 等级(默认为1级)

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

    internal PlayerType PlayerType
    {
        get
        {
            return _playerType;
        }

        set
        {
            _playerType = value;
        }
    }

    internal SkillType SkillType
    {
        get
        {
            return _skillType;
        }

        set
        {
            _skillType = value;
        }
    }

    internal PosType PosType
    {
        get
        {
            return _posType;
        }

        set
        {
            _posType = value;
        }
    }

    public int ColdTime
    {
        get
        {
            return _coldTime;
        }

        set
        {
            _coldTime = value;
        }
    }

    public int BaseDamage
    {
        get
        {
            return _baseDamage;
        }

        set
        {
            _baseDamage = value;
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

    public void LevelUp(int count)
    {
        // 还有其他一些操作
        Level += count;
    }
}
