using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptInfo{
    private int _id; // ID
    private int _needLevel; // 所需等级

    private string _scenename;// 名称
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

    public int NeedLevel
    {
        get
        {
            return _needLevel;
        }

        set
        {
            _needLevel = value;
        }
    }

    public string Scenename
    {
        get
        {
            return _scenename;
        }

        set
        {
            _scenename = value;
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
