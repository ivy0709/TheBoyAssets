using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour {

    public TweenScale StartPanelTween;
    public TweenScale LoginPanelTween;
    public TweenScale RegisterPanelTween;
    public TweenScale ServerListPanelTween;

    public TweenPosition StartPanelTweenPosition;
    public TweenPosition SelectedCharacterTweenPosition;
    public TweenPosition SelectingCharacterTweenPosition;


    public UILabel UserNameLabelStartPanel;
    public UILabel ServerNameLabelStartPanel;

    public UILabel CharacterNameSelectedPanel;
    public UILabel CharacterLevelSelectedPanel;

    public UIInput UserNameInputLoginPanel;
    public UIInput PasswordInputLoginPanel;

    public UIInput UserNameInputRegisterPanel;
    public UIInput PasswordInputRegisterPanel;
    public UIInput RePasswordInputRegisterPanel;

    public UIInput InputCharacterNameSelectingPanel;


    // 不知道下面这俩是不是用同一个就行了 
    public string UserNameLoginPanel;
    public string PasswordLoginPanel;

    public string UserNameRegisterPanel;
    public string PasswordRegisterPanel;
    public string RePasswordRegisterPanel;

    public GameObject ServerItemBusy;
    public GameObject ServerItemFree;
    public GameObject SelectedServerItem;

    /// <summary>
    /// 带有地板的prefab
    /// </summary>
    public GameObject[] characterSelectingArray;
    public GameObject[] characterSelectedArray;

    public GameObject lastChosenCharacter;

    // UIGrid 是个脚本 ？
    public UIGrid ServerListGrid;
    private bool isInitServerList = false;


    private ServerProperty selectedServerProperty;

    public Transform selectedParent;


    /// <summary>
    /// 简单的单例模式的实现
    /// </summary>
    public static StartMenuController _instance;



    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        InitServerList();
        selectedServerProperty = SelectedServerItem.GetComponent<ServerProperty>();
    }


    public void OnCharacterClicked(GameObject go)
    {
        if(go == lastChosenCharacter)
        {
            return;
        }

        iTween.ScaleTo(go, new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        if(lastChosenCharacter != null)
        {
            iTween.ScaleTo(lastChosenCharacter, new Vector3(1f, 1f, 1f), 0.5f);
        }
        lastChosenCharacter = go;
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
        FromAPanel2BPanelWithTween(StartPanelTween, LoginPanelTween);
    }
    /// <summary>
    /// 开始界面的服务器名按钮
    /// </summary>
    public void OnServerListShowClicked()
    {
        // 选择服务器 进入服务器列表界面 TODO
        FromAPanel2BPanelWithTween(StartPanelTween, ServerListPanelTween);
    }
    /// <summary>
    /// 开始界面的开始游戏按钮
    /// </summary>
    public void OnEnterGameClicked()
    {
        // 1.检查用户名和密码 连接服务器 TODO


        // 2.进入选角色界面 TODO
        FromAPanel2BPanelWithTween(StartPanelTweenPosition, SelectedCharacterTweenPosition);
    }


    /// <summary>
    /// 登录界面的确定按钮
    /// </summary>
    public void OnSureLoginClicked()
    {
        // 存下数据
        UserNameLoginPanel = UserNameInputLoginPanel.value;
        PasswordLoginPanel = PasswordInputLoginPanel.value;
        FromAPanel2BPanelWithTween(LoginPanelTween, StartPanelTween);
        // 让start界面的账号更新
        UserNameLabelStartPanel.text = UserNameLoginPanel;
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
        FromAPanel2BPanelWithTween(RegisterPanelTween, StartPanelTween);
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
    /// <param name="APanel">A面板要有消失的动画</param>  
    /// <param name="BPanel">B面板要有出现的动画</param>  
    private void FromAPanel2BPanelWithTween(UITweener APanel, UITweener BPanel)
    {
        APanel.PlayReverse();
        StartCoroutine(HidePanel(APanel.gameObject));
        BPanel.gameObject.SetActive(true);
        BPanel.PlayForward();
    }
//     /// <summary>
//     /// 使用A面板的退出动画 B面板的进入动画 进入B面板
//     /// </summary>
//     /// <param name="APanel">A面板要有消失的动画</param>  
//     /// <param name="BPanel">B面板要有出现的动画</param>  
//     private void FromAPanel2BPanelWithPositionTween(TweenScale APanel, TweenScale BPanel)
//     {
//         APanel.PlayReverse();
//         StartCoroutine(HidePanel(APanel.gameObject));
//         BPanel.gameObject.SetActive(true);
//         BPanel.PlayForward();
//        /// <summary>
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

        // 1. 检测是否有输入姓名
        // TODO
        // 2. 检测是否有选择角色
        if(lastChosenCharacter == null)
        {
            return;
        }
        // 3. 更新前一个页面的模型
        int index = -1;
        for(int i = 0; i < characterSelectingArray.Length; ++i)
        {
            if(lastChosenCharacter == characterSelectingArray[i])
            {
                index = i;
                break;
            }
        }
        if(index == -1)
        {
            return;
        }

        GameObject.Destroy(selectedParent.GetComponentInChildren<Animation>().gameObject);

        GameObject go = GameObject.Instantiate(characterSelectedArray[index], Vector3.zero, Quaternion.identity);
        go.transform.parent = selectedParent;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = new Vector3(1, 1, 1);

        // 4. 更新前一个页面角色的名字和等级
        CharacterNameSelectedPanel.text = InputCharacterNameSelectingPanel.value;
        CharacterLevelSelectedPanel.text = "LV.1";
        // 5. 返回前一个页面
        OnReturnSelectingCharacterClicked();
    }
}
