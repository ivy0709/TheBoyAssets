using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using TaiDouCommon;
using TaiDouCommon.Model;
using TaiDouCommon.Tools;
using UnityEngine;

public class TaskDBController : ControllerBase
{
    public override OperationCode opCode
    {
        get
        {
            return OperationCode.TaskDB;
        }
    }

    public void GetTaskDBs()
    {
        Dictionary<byte, object> parameter = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameter, SubCode.GetTaskDBs, ParameterCode.SubCode, false);
        PhotonEngine.Instance.SendRequest(opCode, parameter);
    }
    public void AddTaskDB(TaskDB task)
    {
        Dictionary<byte, object> parameter = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameter, SubCode.AddTaskDB, ParameterCode.SubCode, false);
        task.Role = null;
        ParameterTool.AddParameter(parameter, task, ParameterCode.SingleTask);
        PhotonEngine.Instance.SendRequest(opCode, parameter);
    }
    public void UpdateTaskDB(TaskDB task)
    {
        Dictionary<byte, object> parameter = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameter, SubCode.UpdateTaskDB, ParameterCode.SubCode, false);
        task.Role = null;
        ParameterTool.AddParameter(parameter, task, ParameterCode.SingleTask);
        PhotonEngine.Instance.SendRequest(opCode, parameter);
    }




    public override void OnOperationResponse(OperationResponse response)
    {
        SubCode subCode = ParameterTool.GetParameter<SubCode>(response.Parameters, ParameterCode.SubCode, false);

        switch (subCode)
        {
            case SubCode.GetTaskDBs:
                // 返回的是 getroles的消息 应该是返回一系列的roles 
                List<TaskDB> tasks = ParameterTool.GetParameter<List<TaskDB>>(response.Parameters, ParameterCode.TaskList);
                if (OnGetTaskDBs != null)
                {
                    OnGetTaskDBs(tasks);

                }
                break;

            case SubCode.AddTaskDB:
                TaskDB task = ParameterTool.GetParameter<TaskDB>(response.Parameters, ParameterCode.SingleTask);
                Debug.Log(response.DebugMessage);
                if (OnAddTaskDB != null)
                {
                    OnAddTaskDB(task);
                }
                break;

            case SubCode.UpdateTaskDB:
                Debug.Log(response.DebugMessage);
                if (OnUpdateTaskDB != null)
                {
                    OnUpdateTaskDB();
                }
                break;

        }
    }
    public event OnGetTaskDBsEvent OnGetTaskDBs;
    public event OnAddTaskDBEvent OnAddTaskDB;
    public event OnUpdateTaskDBEvent OnUpdateTaskDB;
}
