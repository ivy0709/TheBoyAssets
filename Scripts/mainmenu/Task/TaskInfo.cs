using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType
{
    Main,
    Reward,
    Daily,
}
// i.未开始
// ii.接受任务
// iii.任务完成
// iv.	获取奖励（结
public enum TaskProgress
{
    NoStart,
    Accept,
    Complete,
    Reward,
}


public class TaskInfo
{
    // 1001|Main|试练之地|男性 手镯(2)|通过试练之地完成新手挑战|500|1000|你好，英雄，准备好开始了吗？|1001|1001
    private int _id; // ID
    private TaskType _taskType; // 任务类型（Main,Reward，Daily）
    private string _name;// 名称
    private string _icon;// 图集中的Sprite名称 图标 
    private string _describe;// 任务描述
    private int _coinAward;// 获得的金币奖励
    private int _diamondAward;// 获得的钻石奖励
    private string _talkInfo;// 跟npc交谈的话语
    private int _npcID;// NPC id
    private int _mapID;// 副本 id

    public delegate void OnTaskProgressChanged();
    public event OnTaskProgressChanged OnTaskProgressChangedEvent;


    // 应该把这个分离出来的
    private TaskProgress _taskProgress = TaskProgress.NoStart;// 任务的状态

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

    public TaskType TaskType
    {
        get
        {
            return _taskType;
        }

        set
        {
            _taskType = value;
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

    public int CoinAward
    {
        get
        {
            return _coinAward;
        }

        set
        {
            _coinAward = value;
        }
    }

    public int DiamondAward
    {
        get
        {
            return _diamondAward;
        }

        set
        {
            _diamondAward = value;
        }
    }

    public string TalkInfo
    {
        get
        {
            return _talkInfo;
        }

        set
        {
            _talkInfo = value;
        }
    }

    public TaskProgress TaskProgress
    {
        get
        {
            return _taskProgress;
        }

        set
        {
            if(_taskProgress != value)
            {
                _taskProgress = value;
                OnTaskProgressChangedEvent();
            }
           
        }
    }

    public int NpcID
    {
        get
        {
            return _npcID;
        }

        set
        {
            _npcID = value;
        }
    }

    public int MapID
    {
        get
        {
            return _mapID;
        }

        set
        {
            _mapID = value;
        }
    }
}
