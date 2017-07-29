﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {

    public TextAsset TaskText;
    public ArrayList TaskList = new ArrayList();
    public static TaskManager _instance;
    private TaskInfo curTask;

    private PlayerAutoMove playerAutoMove;

    public PlayerAutoMove PlayerAutoMove
    {
        get
        {
            if(playerAutoMove == null)
            {
                playerAutoMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAutoMove>();
            }

            return playerAutoMove;
        }
    }

    private void Awake()
    {
        ReadTaskList();
        // LoadTaskList();
        _instance = this;
    }
    private void ReadTaskList()
    {
        string Str = TaskText.ToString();
        string[] rowArray = Str.Split('\n');
        string[] colArray;
        // 跳过第一行
        foreach (string row in rowArray)
        {
            colArray = row.Split('|');
            // 1001|Main|试练之地|男性 手镯(2)|通过试练之地完成新手挑战|500|1000|你好，英雄，准备好开始了吗？|1001|1001
            TaskInfo item = new TaskInfo();
            item.Id = int.Parse(colArray[0]);
            switch (colArray[1])
            {
                case "Main":
                    item.TaskType = TaskType.Main;
                    break;
                case "Reward":
                    item.TaskType = TaskType.Reward;
                    break;
                case "Daily":
                    item.TaskType = TaskType.Daily;
                    break;
            }
            item.Name = colArray[2];
            item.Icon = colArray[3];
            item.Describe = colArray[4];
            item.CoinAward = int.Parse(colArray[5]);
            item.DiamondAward = int.Parse(colArray[6]);
            item.TalkInfo = colArray[7];
            item.NpcID = int.Parse(colArray[8]);
            item.MapID = int.Parse(colArray[9]);
            TaskList.Add(item);
        }
    }

    public void LoadTaskList()
    {
        // TODO
        // 用 ReadTaskList

    }

    public void OnExcuteTask(TaskInfo task)
    {
        // 执行自动寻路
        // 关闭任务界面


        // 当前执行任务
        curTask = task;
        if(curTask.TaskProgress == TaskProgress.NoStart)
        {
            // 寻路到NPC所在的位置
            PlayerAutoMove.SetDestination(NPCManager._instance.GetNPCById(task.NpcID).transform.position);
        }
        else if (curTask.TaskProgress == TaskProgress.Accept)
        {
            // 寻路到副本入口
            PlayerAutoMove.SetDestination(NPCManager._instance.Transcript.transform.position);
        }
        TaskPanelManager._instance.OnCloseBtnClicked();
    }
    public void OnAcceptTask()
    {
        curTask.TaskProgress = TaskProgress.Accept;
        OnExcuteTask(curTask);
    }
    public void OnArrivedDestination()
    {
        // 当前执行任务
        if (curTask.TaskProgress == TaskProgress.NoStart)
        {
            DiagUI._instance.OnShow(curTask.TalkInfo);

        }
        else if (curTask.TaskProgress == TaskProgress.Accept)
        {
            // 进入副本
        }
    }
}