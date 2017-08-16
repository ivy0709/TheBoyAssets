using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using TaiDouCommon;
using TaiDouCommon.Model;
using TaiDouCommon.Tools;
using UnityEngine;

public class InventoryItemDBController :ControllerBase
{
    public override OperationCode opCode
    {
        get
        {
            return OperationCode.InventoryItemDB;
        }
    }

    public void GetInventoryItemDBs()
    {
        // 发送请求角色的请求
        Dictionary<byte, object> parameter = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameter, SubCode.GetInventoryItemDBs, ParameterCode.SubCode, false);
        PhotonEngine.Instance.SendRequest(opCode, parameter);
    }
    public void AddInventoryItemDB(InventoryItemDB db)
    {
        // 发送添加角色的请求
        Dictionary<byte, object> parameter = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameter, SubCode.AddInventoryItemDB, ParameterCode.SubCode, false);
        db.Role = null;
        ParameterTool.AddParameter(parameter, db, ParameterCode.SingleInventoryItem);
        PhotonEngine.Instance.SendRequest(opCode, parameter);
    }
    public void UpdateInventoryItemDB(InventoryItemDB db)
    {
        // 发送添加角色的请求
        Dictionary<byte, object> parameter = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameter, SubCode.UpdateInventoryItemDB, ParameterCode.SubCode, false);
        db.Role = null;
        ParameterTool.AddParameter(parameter, db, ParameterCode.SingleInventoryItem);
        PhotonEngine.Instance.SendRequest(opCode, parameter);
    }



    public override void OnOperationResponse(OperationResponse response)
    {
        SubCode subCode = ParameterTool.GetParameter<SubCode>(response.Parameters, ParameterCode.SubCode, false);

        switch (subCode)
        {
            case SubCode.GetInventoryItemDBs:
                // 返回的是 getroles的消息 应该是返回一系列的roles 
                List<InventoryItemDB> items = ParameterTool.GetParameter<List<InventoryItemDB>>(response.Parameters, ParameterCode.InventoryItemList);
                if (OnGetInventoryItemDBs != null)
                {
                    OnGetInventoryItemDBs(items);
                }
                break;

            case SubCode.AddInventoryItemDB:
                InventoryItemDB item = ParameterTool.GetParameter<InventoryItemDB>(response.Parameters, ParameterCode.SingleInventoryItem);
                if (OnAddInventoryItemDB != null)
                {
                    // 进行添加
                    OnAddInventoryItemDB(item);
                }
                break;
            case SubCode.UpdateInventoryItemDB:
                if (OnUpdateInventoryItemDB != null)
                {
                    OnUpdateInventoryItemDB();
                }
                Debug.Log(response.DebugMessage);
                break;

        }
    }

    public event OnGetInventoryItemDBsEvent OnGetInventoryItemDBs;
    public event OnAddInventoryItemDBEvent OnAddInventoryItemDB;
    public event OnUpdateInventoryItemDBEvent OnUpdateInventoryItemDB;

}
