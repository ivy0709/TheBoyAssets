using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TaiDouCommon;
using TaiDouCommon.Model;

public class PhotonEngine : MonoBehaviour,IPhotonPeerListener {

    // 保存当前登陆的角色
    public Role role;

    // 整个游戏中只有存在一个
    private static PhotonEngine _instance;
    public static PhotonEngine Instance
    {
        get
        {
            return _instance;
        }
    }

    // 统一管理 注册在每一个Control中进行注册
    public Dictionary<byte, ControllerBase> controllers = new Dictionary<byte, ControllerBase>();



    private PhotonPeer peer;
    private bool isConnected = false;



    // 方便修改
    public ConnectionProtocol protocol = ConnectionProtocol.Tcp;
    public string serverAddr = "127.0.0.1:4530";
    public string applicationName = "TaiDouServer";

    // 定义连接上服务器的事件
    public delegate void OnConnectedServerEvent();
    public event OnConnectedServerEvent OnConnectedServer;


    private void Awake()
    {
        _instance = this;
        peer = new PhotonPeer(this, protocol);
        peer.Connect(serverAddr, applicationName);
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        peer.Service();
	}

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(level + ": " + message);
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        // 根据 OperationCode，分发到不同的controller进行处理
        ControllerBase controller = null;
        if (controllers.TryGetValue(operationResponse.OperationCode, out controller))
        {
            controller.OnOperationResponse(operationResponse);
        }
        else
        {
            Debug.Log("cannot find the controller from the operation code:" + operationResponse.OperationCode);
        }

    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log("OnStatusChanged:" + statusCode);
        switch (statusCode)
        {
            case StatusCode.Connect:
                isConnected = true;
                OnConnectedServer();
                break;
        }
    }

    public void OnEvent(EventData eventData)
    {
        //throw new NotImplementedException();
    }
    public void SendRequest(OperationCode op)
    {
        // 只发消息号 没有参数
        Debug.Log("SendRequest:" + op);
        peer.OpCustom((byte)op, null, true);
    }
    public void SendRequest(OperationCode op, Dictionary<byte, object> parameters)
    {
        // 发消息号 和 参数
        Debug.Log("SendRequest:" + op);
        peer.OpCustom((byte)op, parameters, true);
    }

    public void RegisterController(OperationCode code, ControllerBase controller)
    {
        if (!controllers.ContainsKey((byte) code))
        {
            controllers.Add((byte)code, controller); 
        } 
    }
    public void UnRegisterController(OperationCode code)
    {
        controllers.Remove((byte)code);
    }
}
