using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScenePanelManager : MonoBehaviour {

    public static LoadScenePanelManager _instance;
    private GameObject bg;
    private UISlider prograssBar;
    private AsyncOperation ao;
    private bool isAsync = false;

    private void Awake()
    {
        bg = this.transform.Find("bg").gameObject;
        prograssBar = this.transform.Find("bg/prograssBar").GetComponent<UISlider>();
        _instance = this;
        this.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(isAsync)
        {
            prograssBar.value = ao.progress;
            if(prograssBar.value == 1.0f)
            {
                isAsync = false;
            }
        }
    }

    public void OnSelfShow(AsyncOperation aoItem)
    {
        this.gameObject.SetActive(true);
        bg.SetActive(true);
        this.ao = aoItem;
        isAsync = true;
    }
}
