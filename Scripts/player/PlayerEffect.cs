using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour {
    private MeshRenderer[] meshRendererArray;
    private NcCurveAnimation[] ncCurveAnimationArray;

    private void Awake () {
        meshRendererArray = this.GetComponentsInChildren<MeshRenderer>();
        ncCurveAnimationArray = this.GetComponentsInChildren<NcCurveAnimation>();
    }
	

    public void Show()
    {
        foreach (MeshRenderer item in meshRendererArray)
        {
            item.enabled = true;
        }
        foreach (NcCurveAnimation item in ncCurveAnimationArray)
        {
            item.ResetAnimation();
        }
    }

}
