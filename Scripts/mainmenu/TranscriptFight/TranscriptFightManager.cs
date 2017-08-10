using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptFightManager : MonoBehaviour {

    public static TranscriptFightManager _instance;
    // 因为这个player经常用 所以其他地方 都从这里取 只在这里调用一次
    public GameObject player;

    public List<GameObject> enemys = new List<GameObject>();

    private void Awake()
    {
        _instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] enemysTmp = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject go in enemysTmp)
        {
            enemys.Add(go);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
