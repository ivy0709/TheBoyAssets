using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {

    public GameObject[] NPCArray;
    public Dictionary<int, GameObject> npcDict = new Dictionary<int, GameObject>();

    public GameObject Transcript;
    public static NPCManager _instance;

    private void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start () {
        InitNPCArray();
    }

    private void InitNPCArray()
    {
        foreach(GameObject npc in NPCArray)
        {
            int id = int.Parse(npc.name.Substring(0, 4));
            npcDict.Add(id, npc);
        }
    }
    public GameObject GetNPCById(int id)
    {
        GameObject npc = null;
        npcDict.TryGetValue(id, out npc);
        return npc;
    }
}
