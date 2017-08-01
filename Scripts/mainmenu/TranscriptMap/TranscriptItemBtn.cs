using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptItemBtn : MonoBehaviour {

    public int Id;
    private TranscriptInfo it;

    private void Start()
    {
        it = TranscriptManager._instance.GetTranscriptById(Id);
    }
    private void OnClick()
    {
        transform.parent.SendMessage("OnTranscriptBtnClicked", it);
    }
}
