using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptManager : MonoBehaviour {
    public TextAsset TranscriptText;
    private Dictionary<int, TranscriptInfo> transcriptList = new Dictionary<int, TranscriptInfo>();
    public static TranscriptManager _instance;
    private void Awake()
    {
        ReadTranscriptList();
        _instance = this;
    }
    private void ReadTranscriptList()
    {
        string Str = TranscriptText.ToString();
        string[] rowArray = Str.Split('\n');
        string[] colArray;
        foreach (string row in rowArray)
        {
            colArray = row.Split('|');
            TranscriptInfo item = new TranscriptInfo();

            item.Id = int.Parse(colArray[0]);
            item.NeedLevel= int.Parse(colArray[1]);
            item.Scenename = colArray[2];
            item.Describe = colArray[3];
            transcriptList.Add(item.Id, item);
        }
    }
    public TranscriptInfo GetTranscriptById(int id)
    {
        TranscriptInfo ret = null;
        transcriptList.TryGetValue(id, out ret);
        return ret;
    }
}
