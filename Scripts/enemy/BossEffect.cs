using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : MonoBehaviour {
    private float deactiveTime = 1.0f;
    private GameObject attack; 

    private void Awake()
    {
        attack = this.transform.Find("attack").gameObject;
        attack.SetActive(false);
    }

    // Update is called once per frame
    void OnDeactive() {
        attack.SetActive(false);
	}

    public void Show()
    {
        attack.SetActive(true);
        Invoke("OnDeactive", deactiveTime);
    }
}
