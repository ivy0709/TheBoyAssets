using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using TaiDouCommon;
using TaiDouCommon.Model;
using TaiDouCommon.Tools;
using UnityEngine;

public class RoleController : ControllerBase
{
    public override OperationCode opCode
    {
        get
        {
            return OperationCode.Role;
        }
    }

    public void GetRoles()
    {
        // 发送请求角色的请求
        Dictionary<byte, object> parameter = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameter, SubCode.GetRoles, ParameterCode.SubCode, false);
        PhotonEngine.Instance.SendRequest(opCode, parameter);
    }
    public void AddRole(Role role)
    {
        // 发送添加角色的请求
        Dictionary<byte, object> parameter = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameter, SubCode.AddRole, ParameterCode.SubCode, false);
        role.User = null;
        ParameterTool.AddParameter(parameter, role, ParameterCode.SingleRole);
        PhotonEngine.Instance.SendRequest(opCode, parameter);
    }
    public void UpdateRole(Role role)
    {
        // 发送添加角色的请求
        Dictionary<byte, object> parameter = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameter, SubCode.UpdateRole, ParameterCode.SubCode, false);
        role.User = null;
        ParameterTool.AddParameter(parameter, role, ParameterCode.SingleRole);
        PhotonEngine.Instance.SendRequest(opCode, parameter);
    }
    public void SelectedRole(Role role)
    {
        // 发送已经选择角色的信息
        Dictionary<byte, object> parameter = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameter, SubCode.SelectedRole, ParameterCode.SubCode, false);
        role.User = null;
        ParameterTool.AddParameter(parameter, role, ParameterCode.SingleRole);
        PhotonEngine.Instance.SendRequest(opCode, parameter);
    }
    public override void OnOperationResponse(OperationResponse response)
    {

        SubCode subCode = ParameterTool.GetParameter<SubCode>(response.Parameters, ParameterCode.SubCode, false);

        switch (subCode)
        {
            case SubCode.GetRoles:
                // 返回的是 getroles的消息 应该是返回一系列的roles 
                List<Role> roles = ParameterTool.GetParameter<List<Role>>(response.Parameters, ParameterCode.RoleList);
                OnGetRoles(roles);
                break;

            case SubCode.AddRole:
                Role role = ParameterTool.GetParameter<Role>(response.Parameters, ParameterCode.SingleRole);
                OnAddRole(role);
                break;

            case SubCode.SelectedRole:
                // 返回的是 getroles的消息 应该是返回一系列的roles 
                if (OnSelectedRole != null)
                {
                    OnSelectedRole();
                }
                break;
            case SubCode.UpdateRole:
                Debug.Log(response.DebugMessage);
                break;

        }
    }

    public event OnGetRolesEvent OnGetRoles;
    public event OnAddRoleEvent OnAddRole;
    public event OnSelectedRoleEvent OnSelectedRole;
}
