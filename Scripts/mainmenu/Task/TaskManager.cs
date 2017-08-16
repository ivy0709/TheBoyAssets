using System.Collections;
using System.Collections.Generic;
using TaiDouCommon.Model;
using UnityEngine;

public class TaskManager : MonoBehaviour {

    public TextAsset TaskText;
    public ArrayList TaskList = new ArrayList();
    public Dictionary<int, TaskInfo> TaskDict = new Dictionary<int, TaskInfo>();
    public static TaskManager _instance;
    private TaskInfo curTask;
    public TaskDBController taskController;


    private PlayerAutoMove playerAutoMove;


    public event OnSyncTaskCompletedEvent OnSyncTaskCompleted;
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
        _instance = this;
        taskController = this.GetComponent<TaskDBController>();
        // 进行事件的注册
        taskController.OnGetTaskDBs += OnGetTaskDBs;
        taskController.OnAddTaskDB += OnAddTaskDB;
        taskController.OnUpdateTaskDB += OnUpdateTaskDB;
    }

    private void Start()
    {
        ReadTaskList();
        // 进行服务器端的链接获取 taskDB 列表
        taskController.GetTaskDBs();
    }
    private void OnDestroy()
    {
        if (taskController != null)
        {
            taskController.OnGetTaskDBs -= OnGetTaskDBs;
            taskController.OnAddTaskDB -= OnAddTaskDB;
            taskController.OnUpdateTaskDB -= OnUpdateTaskDB;
        }
    }
    public void OnGetTaskDBs(List<TaskDB> list)
    {
        // 获取的 taskDB 列表
        if (list != null && list.Count > 0)
        {
            // 根据 TaskDB 里面的 taskID 查找 本地列表里面的 taskInfo  把这个 db 同步进去
            foreach (var item in list)
            {
                TaskInfo task = null;
                TaskDict.TryGetValue(item.TaskId, out task);
                if (task != null)
                {
                    // 同步其消息
                    // 同步完了以后才能加载 taskui
                    task.SyncTaskDB(item);
                }
            };
        }
        OnSyncTaskCompleted();
    }

    public void OnAddTaskDB(TaskDB task)
    {
        // 其实服务器端都没有必要回发这个task的信息 因为就是客户端传过去的 而已传过去之前已经有了
    }
    public void OnUpdateTaskDB()
    {
       
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
            TaskDict.Add(item.Id, item);
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
    /// <summary>
    /// 点击接受 更新任务状态
    /// </summary>
    public void OnAcceptTask()
    {
        curTask.TaskProgress = TaskProgress.Accept;
        curTask.UpdateTaskDB();
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
