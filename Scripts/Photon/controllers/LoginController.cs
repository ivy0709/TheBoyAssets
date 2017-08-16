using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using LitJson;
using TaiDouCommon;
using TaiDouCommon.Model;
using TaiDouCommon.Tools;
using UnityEngine;

public class LoginController : ControllerBase
{
    private RoleController roleController;

    public override void Start()
    {
        base.Start();
        roleController = this.GetComponent<RoleController>();
    }

    // Use this for initialization
    public override OperationCode opCode
    {
        get { return OperationCode.Login; }
    }

    public void Login(string username, string password)
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
        //接受return code
        switch (response.ReturnCode)
        {
            case (short)ReturnCode.Success:
                // 向服务器发送请求 当前用户有没有角色，其实这两个是不是可以一起发送过来的 
                roleController.GetRoles();
                break;
            case (short)ReturnCode.Fail:
                string message = response.DebugMessage;
                MessageManager._instance.OnShow(message, 2.0f);
                break;
        }
    }
}
