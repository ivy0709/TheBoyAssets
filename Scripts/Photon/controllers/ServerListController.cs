using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using TaiDouCommon;
using LitJson;
using TaiDouCommon.Tools;
using Random = UnityEngine.Random;

public class ServerListController : ControllerBase {

    public override void OnOperationResponse(OperationResponse response)
    {
        // 得到参数 从字典参数里面获得对应的object

        List<TaiDouCommon.Model.ServerProperty> serverlist =
            ParameterTool.GetParameter<List<TaiDouCommon.Model.ServerProperty>>(response.Parameters,
                ParameterCode.GetServerList);

        if(serverlist != null)
        {
            // 与客户端进行交互
            InitServerList(serverlist);
        }

    }

    private void InitServerList(List<TaiDouCommon.Model.ServerProperty> serverlist)
    {
        // 这里先随机生成
        GameObject go = null;
        foreach (var list in serverlist)
        {
          
            int count = list.Count;
            string name = list.ServerName;
            string ip = list.Ip + ":4530";

            if (count > 50)
            {
                go = NGUITools.AddChild(StartMenuController._instance.ServerListGrid.gameObject, 
                    StartMenuController._instance.ServerItemBusy);
            }
            else
            {
                go = NGUITools.AddChild(StartMenuController._instance.ServerListGrid.gameObject, 
                    StartMenuController._instance.ServerItemFree);
            }
            ServerProperty serverItem = go.GetComponent<ServerProperty>();
            serverItem.Count = count;
            serverItem.Name = name;
            serverItem.Ip = ip;
        }
        StartMenuController._instance.ServerListGrid.repositionNow = true;
        StartMenuController._instance.ServerListGrid.Reposition();
    }


    public void GetServerList()
    {
        PhotonEngine.Instance.SendRequest(OperationCode.GetServerList);
    }

    // Use this for initialization
    public override OperationCode opCode
    {
        get { return OperationCode.GetServerList; }
    }

    public override void Start () {
        base.Start();
        PhotonEngine.Instance.OnConnectedServer += GetServerList;
	}
    public override void OnDestroy()
    {
        base.OnDestroy();
        PhotonEngine.Instance.OnConnectedServer -= GetServerList;
    }
}
