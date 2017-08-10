using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarManager : MonoBehaviour {
    public GameObject HpBarPrefab;
    public GameObject DamageHUDPrefab;

    public static HpBarManager _instance;

    private void Awake()
    {
        _instance = this;
    }


    public GameObject CreateHpBar(Transform target)
    {
        GameObject go = NGUITools.AddChild(this.gameObject, HpBarPrefab);
        go.GetComponent<UIFollowTarget>().target = target;
        return go;
    }

    public GameObject CreateDamageHUD(Transform target)
    {
        GameObject go = NGUITools.AddChild(this.gameObject, DamageHUDPrefab);
        go.GetComponent<UIFollowTarget>().target = target;
        return go;
    }
}
