using System.Collections;
using System.Collections.Generic;
using TaiDouCommon;
using TaiDouCommon.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour {

    public TweenScale StartPanelTween;
    public TweenScale LoginPanelTween;
    public TweenScale RegisterPanelTween;
    public TweenScale ServerListPanelTween;

    public TweenPosition StartPanelTweenPosition;
    public TweenPosition SelectedCharacterTweenPosition;
    public TweenPosition SelectingCharacterTweenPosition;

    // 开始面板的账户名字和服务器名字
    public UILabel UserNameLabelStartPanel;
    public UILabel ServerNameLabelStartPanel;

    // 已选角色的姓名和等级
    public UILabel CharacterNameSelectedPanel;
    public UILabel CharacterLevelSelectedPanel;
    // 更换角色的姓名输入 和boxcollider
    public UIInput InputCharacterNameSelectingPanel;
    public BoxCollider InputCharacterNameBoxColliderSelectingPanel;



    // 登陆界面和注册界面的输入框
    public UIInput UserNameInputLoginPanel;
    public UIInput PasswordInputLoginPanel;
    public UIInput UserNameInputRegisterPanel;
    public UIInput PasswordInputRegisterPanel;
    public UIInput RePasswordInputRegisterPanel;

    public string UserName;
    public string Password;

    // 服务器列表
    public GameObject ServerItemBusy;
    public GameObject ServerItemFree;
    public GameObject SelectedServerItem;

    /// <summary>
    /// 带有地板的prefab show
    /// </summary>
    public GameObject[] characterSelectingArray;

    /// <summary>
    /// 不带地板的prefab select
    /// </summary>
    public GameObject[] characterSelectedArray;

    /// <summary>
    /// 在选择角色的时候 记录上一次点击的是哪个角色
    /// </summary>
    public GameObject lastChosenCharacter;

    // UIGrid 是个脚本 ？
    public UIGrid ServerListGrid;
    private bool isInitServerList = false;


    private ServerProperty selectedServerProperty;

    public Transform selectedParent;

    [SerializeField]
    private LoginController loginController;
    private RegisterController registerController;
    private RoleController roleController;
    /// <summary>
    /// 简单的单例模式的实现
    /// </summary>
    public static StartMenuController _instance;

    
    private List<Role> curRoles;
    private Role curRole;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        selectedServerProperty = SelectedServerItem.GetComponent<ServerProperty>();
        loginController = this.GetComponent<LoginController>();
        registerController = this.GetComponent<RegisterController>();
        roleController = this.GetComponent<RoleController>();

        // 注册事件
        if (roleController != null)
        {
            roleController.OnGetRoles += OnGetRoles;
            roleController.OnAddRole += OnAddRole; 
            roleController.OnSelectedRole += OnSelectedRole; 
        }
    }

    private void Destory()
    {
        // 注销事件
        if (roleController != null)
        {
            roleController.OnGetRoles -= OnGetRoles;
            roleController.OnSelectedRole -= OnSelectedRole;
            roleController.OnAddRole -= OnAddRole;
        }
    }


    /// <summary>
    /// 开始界面的姓名按钮
    /// </summary>
    public void OnLoginShowClicked()
    {
        // 进入登陆界面
        FromAPanel2BPanelWithTween(StartPanelTween, LoginPanelTween);
    }
    /// <summary>
    /// 开始界面的服务器名按钮
    /// </summary>
    public void OnServerListShowClicked()
    {
        FromAPanel2BPanelWithTween(StartPanelTween, ServerListPanelTween);
    }
    /// <summary>
    /// 开始界面的进入游戏按钮
    /// </summary>
    public void OnEnterGameClicked()
    {
        // 先简单验证用户名或者密码不能为空吧
        if (UserName != null && UserName.Length > 3 && Password != null && Password.Length > 3)
        {
            // 发起验证用户名密码请求
            loginController.Login(UserName, Password);          
        }
        else
        {
            MessageManager._instance.OnShow("用户名或者密码为空或者长度不符合", 2.0f);
        }
    }


    /// <summary>
    /// 登录界面的确定按钮
    /// </summary>
    public void OnSureLoginClicked()
    {
        // 存下数据
        UserName = UserNameInputLoginPanel.value;
        Password = PasswordInputLoginPanel.value;
        FromAPanel2BPanelWithTween(LoginPanelTween, StartPanelTween);
        // 让start界面的账号更新
        UserNameLabelStartPanel.text = UserName;
        return;
    }
    /// <summary>
    /// 登录界面的注册按钮
    /// </summary>
    public void OnRegisterShowClicked()
    {
        FromAPanel2BPanelWithTween(LoginPanelTween, RegisterPanelTween);
    }
    /// <summary>
    /// 登录界面的关闭按钮
    /// </summary>
    public void OnCloseLoginClicked()
    {
        // 退出Login 返回到Start
        FromAPanel2BPanelWithTween(LoginPanelTween, StartPanelTween);
    }
    /// <summary>
    /// 注册界面的取消按钮
    /// </summary>
    public void OnCancelRegisterClicked()
    {
        // 返回start界面
        FromAPanel2BPanelWithTween(RegisterPanelTween, StartPanelTween);
        PasswordInputRegisterPanel.value = "";
        RePasswordInputRegisterPanel.value = "";
    }
    /// <summary>
    /// 注册界面的注册并登录按钮
    /// </summary>
    public void OnRegisteRegisterClicked()
    {
        string UserNameRegisterPanel = UserNameInputRegisterPanel.value;
        string PasswordRegisterPanel = PasswordInputRegisterPanel.value;
        string RePasswordRegisterPanel = RePasswordInputRegisterPanel.value;
        // 1. 本地验证 密码和确定密码是否一致 TODO 
        if (UserNameRegisterPanel == null || UserNameRegisterPanel.Length <= 3)
        {
            MessageManager._instance.OnShow("用户名为空或者长度小于等于3", 2.0f);
            return;
        }
        if (PasswordRegisterPanel == null || PasswordRegisterPanel.Length <= 3)
        {
            MessageManager._instance.OnShow("密码为空或者长度小于等于3", 2.0f);
            return;
        }
        if (PasswordRegisterPanel != RePasswordRegisterPanel)
        {
            MessageManager._instance.OnShow("密码不一致", 2.0f);
            return;
        }
        // 当前用户名和密码
        UserName = UserNameRegisterPanel;
        Password = PasswordRegisterPanel;
        // 发送给服务器注册事件
        registerController.Register(UserName, Password);
    }
    public void OnRegisterSuccess()
    {
        // start界面显示账号名字
        UserNameLabelStartPanel.text = UserName;

        // 返回start界面
        OnCancelRegisterClicked();
    }

    /// <summary>
    /// 注册界面的关闭按钮
    /// </summary>
    public void OnCloseRegisterClicked()
    {
        OnCancelRegisterClicked();
    }

    // 延迟失活界面
    IEnumerator HidePanel(GameObject go)
    {
        yield return new WaitForSeconds(0.4f);
        go.SetActive(false);
    }
    /// <summary>
    /// 使用A面板的退出动画 B面板的进入动画 进入B面板
    /// </summary>
    /// <param name="APanel">A面板要有消失的动画</param>  
    /// <param name="BPanel">B面板要有出现的动画</param>  
    private void FromAPanel2BPanelWithTween(UITweener APanel, UITweener BPanel)
    {
        APanel.PlayReverse();
        StartCoroutine(HidePanel(APanel.gameObject));
        BPanel.gameObject.SetActive(true);
        BPanel.PlayForward();
    }

    /// 处理在 serverlist 中点击事件
    /// </summary>
    /// <param name="serverGo">点击的serverItem</param>
    public void OnSelectingServerItem(GameObject serverGo)
    {
        selectedServerProperty = serverGo.GetComponent<ServerProperty>();
        // 改变sprite样式： 名字变了就 sprite也变了 
        SelectedServerItem.gameObject.GetComponent<UISprite>().spriteName = serverGo.GetComponent<UISprite>().spriteName;
        SelectedServerItem.gameObject.GetComponent<UIButton>().normalSprite = serverGo.GetComponent<UISprite>().spriteName;
        // 改变uilabel显示的区的名字
        SelectedServerItem.transform.Find("servername").GetComponent<UILabel>().text = selectedServerProperty.Name;
        SelectedServerItem.transform.Find("servername").GetComponent<UILabel>().color = serverGo.transform.Find("servername").GetComponent<UILabel>().color;

    }
    /// <summary>
    /// 选择服务器界面 点击当前选中的服务器触发的事件
    /// </summary>
    public void OnSelectedItemServerListClicked()
    {
        FromAPanel2BPanelWithTween(ServerListPanelTween, StartPanelTween);
        ServerNameLabelStartPanel.text = selectedServerProperty.Name;
    }
    /// <summary>
    /// 选择服务器界面 点击关闭按钮
    /// </summary>
    public void OnClosedServerListClicked()
    {
        FromAPanel2BPanelWithTween(ServerListPanelTween, StartPanelTween);
    }

    /// <summary>
    /// 已选择游戏角色界面的更换角色按钮
    /// </summary>
    public void OnChangeSelectedCharacterClicked()
    {
        FromAPanel2BPanelWithTween(SelectedCharacterTweenPosition, SelectingCharacterTweenPosition);
    }

    /// <summary>
    /// 已选择游戏角色界面的进入游戏按钮
    /// </summary>
    public void OnEnterGameSelectedCharacterClicked()
    {
        // 把当前选择的角色存入photonengine中
        PhotonEngine.Instance.role = curRole;
        // 给服务器发送 当前选择的角色的消息
        roleController.SelectedRole(curRole);
    }
    /// <summary>
    /// 可选择游戏角色界面的返回按钮
    /// </summary>
    public void OnReturnSelectingCharacterClicked()
    {
        FromAPanel2BPanelWithTween(SelectingCharacterTweenPosition, SelectedCharacterTweenPosition );
    }

    /// <summary>
    /// 可选择游戏角色界面的确认按钮
    /// </summary>
    public void OnSureSelectingCharacterClicked()
    {


        // 1. 检测是否有选择角色
        if (lastChosenCharacter == null)
        {
            MessageManager._instance.OnShow("没有选择任何角色", 2.0f);
            return;
        }

        // 2. 检测是否有输入姓名 长度不得小于等于3
        if (InputCharacterNameSelectingPanel.value == null || InputCharacterNameSelectingPanel.value.Length <= 3)
        {
            MessageManager._instance.OnShow("用户名为空或者长度小于等于3", 2.0f);
            return;
        }

        // 3. 选择的模型是否是已经在rolelist表里面的模型
        foreach (var role in curRoles)
        {
            if ((lastChosenCharacter.name.IndexOf("boy") >= 0 && role.IsMan)
                || (lastChosenCharacter.name.IndexOf("girl") >= 0 && !role.IsMan)
            )
            {
                // 如果选择的模型和当前选择的模型一样 那么不需要做改变
                if (role.IsMan == curRole.IsMan)
                {
                    // 因为当是已经创建的角色的时候 
                    // 直接返回
                    OnReturnSelectingCharacterClicked();
                }
                else
                {
                    // 如果不一样需要销毁了 重新摆放模型
                    CreateRoleInSelectedPanel(role);
                    OnReturnSelectingCharacterClicked();
                    curRole = role;
                }   
                return;
            }
        }

        // 发送给服务器添加角色的消息
        Role newRole = new Role();
        newRole.Name = InputCharacterNameSelectingPanel.value;
        newRole.Level = 1;
        newRole.IsMan = lastChosenCharacter.name.IndexOf("boy") >= 0;
        roleController.AddRole(newRole);
        return;
    }

    public void OnCharacterClicked(GameObject go)
    {
        // 是否重复点击了
        if (go == lastChosenCharacter)
        {
            return;
        }

        iTween.ScaleTo(go, new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        if (lastChosenCharacter != null)
        {
            iTween.ScaleTo(lastChosenCharacter, new Vector3(1f, 1f, 1f), 0.5f);
        }
        lastChosenCharacter = go;

        // 这个角色是否已经存在 如果存在 显示名字。并且名字不能修改
        foreach (var role in curRoles)
        {
            if ((lastChosenCharacter.name.IndexOf("boy") >= 0 && role.IsMan)
                || (lastChosenCharacter.name.IndexOf("girl") >= 0 && !role.IsMan)
            )
            {
                InputCharacterNameSelectingPanel.value = role.Name;
                InputCharacterNameBoxColliderSelectingPanel.enabled = false;
                return;
            }
        }
        // 否则
        InputCharacterNameSelectingPanel.value = "";
        InputCharacterNameBoxColliderSelectingPanel.enabled = true;
    }
    /// <summary>
    /// 接收到服务器收到的消息时 调用的事件
    /// </summary>
    /// <param name="roles"></param>
    public void OnGetRoles(List<Role> roles)
    {
        curRoles = roles;
        // 当有接受角色，这里设置将第一个角色信息添加到 selectedcha
        if (roles != null && roles.Count > 0)
        {
            // 得到第一个角色
            if (CreateRoleInSelectedPanel(roles[0]))
            {
                curRole = roles[0];
                FromAPanel2BPanelWithTween(StartPanelTweenPosition, SelectedCharacterTweenPosition);
            }
        }
        else
        {
            //显示selectingcha
            FromAPanel2BPanelWithTween(StartPanelTweenPosition, SelectingCharacterTweenPosition);
        }
    }

    public void OnAddRole(Role role)
    {
        // 需要更新当前的 curRoles 和 curRole
        if (curRoles == null)
        {
            curRoles = new List<Role>();
        }
        curRoles.Add(role);
        curRole = role;
        CreateRoleInSelectedPanel(role);
        OnReturnSelectingCharacterClicked();
    }
    

    public void OnSelectedRole()
    {
        // 隐藏当前面板
        SelectedCharacterTweenPosition.gameObject.SetActive(false);
        // 加载下一个场景
        AsyncOperation ao = SceneManager.LoadSceneAsync(1);
        // 显示进度条面板;
        LoadScenePanelManager._instance.OnSelfShow(ao);
    }


    private bool CreateRoleInSelectedPanel(Role role)
    {
        // 角色模型
        int index = -1;
        for (int i = 0; i < characterSelectingArray.Length; ++i)
        {
            if ((role.IsMan && characterSelectingArray[i].name.IndexOf("boy") >= 0) ||
                (!role.IsMan && characterSelectingArray[i].name.IndexOf("girl") >= 0)
            )
            {
                index = i;
                break;
            }
        }
        if (index == -1)
        {
            MessageManager._instance.OnShow("角色的模型找不到", 2.0f);
            return false;
        }
        GameObject.Destroy(selectedParent.GetComponentInChildren<Animation>().gameObject);
        GameObject go = GameObject.Instantiate(characterSelectedArray[index], Vector3.zero, Quaternion.identity);
        go.transform.parent = selectedParent;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = new Vector3(1, 1, 1);
        // 角色名字
        // 角色等级
        CharacterNameSelectedPanel.text = role.Name;
        CharacterLevelSelectedPanel.text = "LV." + role.Level;
        return true;
    }

}
