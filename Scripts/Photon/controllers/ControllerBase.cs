using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using TaiDouCommon;
using UnityEngine;

public abstract class ControllerBase : MonoBehaviour {

    public abstract OperationCode opCode { get; }

	// Use this for initialization
	public virtual void Start () {
        PhotonEngine.Instance.RegisterController(opCode, this);
	}


    public virtual void OnDestroy()
    {
        PhotonEngine.Instance.UnRegisterController(opCode);
    }

    public abstract void OnOperationResponse(OperationResponse response);

}
