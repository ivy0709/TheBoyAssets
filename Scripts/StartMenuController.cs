using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour {

    public TweenScale StartPanelTween;
    public TweenScale LoginPanelTween;
    public TweenScale RegisterPanelTween;
    public TweenScale ServerListPanelTween;


    public UILabel UserNameLabelStartPanel;

    public UIInput UserNameInputLoginPanel;
    public UIInput PasswordInputLoginPanel;

    public UIInput UserNameInputRegisterPanel;
    public UIInput PasswordInputRegisterPanel;
    public UIInput RePasswordInputRegisterPanel;

    // 不知道下面这俩是不是用同一个就行了 
    public string UserNameLoginPanel;
    public string PasswordLoginPanel;

    public string UserNameRegisterPanel;
    public string PasswordRegisterPanel;
    public string RePasswordRegisterPanel;

    public GameObject ServerItemBusy;
    public GameObject ServerItemFree;

    // UIGrid 是个脚本 ？
    public UIGrid ServerListGrid;
    private bool isInitServerList = false;


    private void Start()
    {
        InitServerList();
    }

    /// <summary>
    /// 初始化服务器列表 把列表信息加入到grid中 
    /// </summary>
    public void InitServerList()
    {
        if(!isInitServerList)
        {
            // 1. 连接一个IP地址，获取服务器列表信息 TODO
            // 2. 将信息初始化成控件再本地

            // 这里先随机生成
            GameObject go = null;
            for (int i = 0; i < 20; ++i)
            {
                int count = Random.Range(0, 100);
                string name = (i + 1) + "区 马达加斯加";
                string ip = "127.0.0.1";

                if(count > 50)
                {
                    go = NGUITools.AddChild(ServerListGrid.gameObject, ServerItemBusy);
                }
                else
                {
                    go = NGUITools.AddChild(ServerListGrid.gameObject, ServerItemFree);
                }
                ServerProperty serverItem = go.GetComponent<ServerProperty>();
                serverItem.Count = count;
                serverItem.Name = name;
                serverItem.Ip = ip;
                //ServerListGrid.gameObject.AddChild(go.transform);
            }
            ServerListGrid.repositionNow = true;
            ServerListGrid.Reposition();
            isInitServerList = true;
        }




    }


    /// <summary>
    /// 开始界面的姓名按钮
    /// </summary>
    public void OnLoginShowClicked()
    {
        // 进入登陆界面
        FromAPanel2BPanel(StartPanelTween, LoginPanelTween);
    }
    /// <summary>
    /// 开始界面的服务器名按钮
    /// </summary>
    public void OnServerListShowClicked()
    {
        // 选择服务器 进入服务器列表界面 TODO
        FromAPanel2BPanel(StartPanelTween, ServerListPanelTween);

        // 初始化服务器列表 在此函数start的时候做吧

    }
    /// <summary>
    /// 开始界面的开始游戏按钮
    /// </summary>
    public void OnEnterGameClicked()
    {
        // 1.检查用户名和密码 连接服务器 TODO
        // 2.进入选角色界面 TODO
    }


    /// <summary>
    /// 登录界面的确定按钮
    /// </summary>
    public void OnSureLoginClicked()
    {
        // 存下数据
        UserNameLoginPanel = UserNameInputLoginPanel.value;
        PasswordLoginPanel = PasswordInputLoginPanel.value;
        FromAPanel2BPanel(LoginPanelTween, StartPanelTween);
        // 让start界面的账号更新
        UserNameLabelStartPanel.text = UserNameLoginPanel;
        return;
    }
    /// <summary>
    /// 登录界面的注册按钮
    /// </summary>
    public void OnRegisterShowClicked()
    {
        FromAPanel2BPanel(LoginPanelTween, RegisterPanelTween);
    }
    /// <summary>
    /// 登录界面的关闭按钮
    /// </summary>
    public void OnCloseLoginClicked()
    {
        // 退出Login 返回到Start
        FromAPanel2BPanel(LoginPanelTween, StartPanelTween);
    }
    /// <summary>
    /// 注册界面的取消按钮
    /// </summary>
    public void OnCancelRegisterClicked()
    {
        // 返回start界面
        FromAPanel2BPanel(RegisterPanelTween, StartPanelTween);
    }
    /// <summary>
    /// 注册界面的注册并登录按钮
    /// </summary>
    public void OnRegisteRegisterClicked()
    {
        // 1. 本地验证 密码和确定密码是否一致 TODO 
        // 2. 存数据
        UserNameRegisterPanel = UserNameInputRegisterPanel.value;
        PasswordRegisterPanel = PasswordInputRegisterPanel.value;
        // 3. 返回start界面
        FromAPanel2BPanel(RegisterPanelTween, StartPanelTween);
        // 4. start界面显示账号名字
        UserNameLabelStartPanel.text = UserNameInputRegisterPanel.value;
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
    /// <param name="APanel"></param>  
    /// <param name="BPanel"></param>  
    private void FromAPanel2BPanel(TweenScale APanel, TweenScale BPanel)
    {
        APanel.PlayReverse();
        StartCoroutine(HidePanel(APanel.gameObject));
        BPanel.gameObject.SetActive(true);
        BPanel.PlayForward();
    }
}
