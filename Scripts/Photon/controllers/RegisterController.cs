using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using TaiDouCommon;
using TaiDouCommon.Model;
using TaiDouCommon.Tools;
using UnityEngine;

public class RegisterController :ControllerBase
{
    public override OperationCode opCode
    {
        get
        {
            return OperationCode.Register;
        }
    }

    public void Register(string username, string password)
    {

        // 发送用户名 密码到服务器进行验证
        User user = new User()
        {
            UserName = username,
            Password = password
        };
        Dictionary<byte, object> parameter = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameter, user, ParameterCode.User);
        PhotonEngine.Instance.SendRequest(opCode, parameter);
    }

    public override void OnOperationResponse(OperationResponse response)
    {
        switch (response.ReturnCode)
        {
            case (short)ReturnCode.Success:
                // 登陆游戏 进入选人界面
                StartMenuController._instance.OnRegisterSuccess();
                MessageManager._instance.OnShow(response.DebugMessage, 2.0f);
                break;

            case (short)ReturnCode.Fail:
                MessageManager._instance.OnShow(response.DebugMessage, 2.0f);
                break;
        }
    }
}
