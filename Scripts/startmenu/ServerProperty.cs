using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerProperty : MonoBehaviour {

    private string servername = "2区 马达加斯加";
    public string Ip = "127.0.0.1";
    public int Count = 50;

    public string Name
    {
        get
        {
            return servername;
        }

        set
        {
            servername = value;
            transform.Find("servername").GetComponent<UILabel>().text = value;
        }
    }

    private void Start()
    {
        Name = servername;
    }

    public void OnPressed()
    {
        transform.root.SendMessage("OnSelectingServerItem", this.gameObject);
    }
}
