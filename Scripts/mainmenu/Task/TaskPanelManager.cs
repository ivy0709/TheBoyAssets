using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPanelManager : MonoBehaviour {

    public static TaskPanelManager _instance;
    [SerializeField]
    private UIGrid taskListGrid;
    [SerializeField]
    private UIButton closeBtn;
    [SerializeField]
    private TweenPosition positionTween;
    public GameObject TaskItemPrefab;

    private void Awake()
    {
        _instance = this;
        taskListGrid = this.transform.Find("taskList/Grid").GetComponent<UIGrid>();
        closeBtn = this.transform.Find("closeBtn").GetComponent<UIButton>();
        positionTween = this.transform.GetComponent<TweenPosition>();

        EventDelegate ed1 = new EventDelegate(this, "OnCloseBtnClicked");
        closeBtn.onClick.Add(ed1);

        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        LoadTaskList();
    }
    private void LoadTaskList()
    {
        ArrayList tasklist = TaskManager._instance.TaskList;
        foreach(TaskInfo item in tasklist)
        {
            GameObject go = NGUITools.AddChild(taskListGrid.gameObject, TaskItemPrefab);
            TaskUIItem uiItem = go.GetComponent<TaskUIItem>();
            uiItem.Set(item);

        }
        taskListGrid.repositionNow = true;
        taskListGrid.Reposition();
    }
    #region 自身面板 的显示与隐藏
    public void OnCloseBtnClicked()
    {
        positionTween.PlayReverse();
        Invoke("SetSelfInActive", 0.4f);
    }
    public void OnSelfShow()
    {
        transform.gameObject.SetActive(true);
        positionTween.PlayForward();
    }
    private void SetSelfInActive()
    {
        transform.gameObject.SetActive(false);
    }
    #endregion
}
