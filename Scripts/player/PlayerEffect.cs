using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour {
    private MeshRenderer[] meshRendererArray;
    private NcCurveAnimation[] ncCurveAnimationArray;
    private GameObject effectOffset;

    private void Awake () {
        meshRendererArray = this.GetComponentsInChildren<MeshRenderer>();
        ncCurveAnimationArray = this.GetComponentsInChildren<NcCurveAnimation>();
        if(this.transform.Find("EffectOffset"))
        {
            effectOffset = this.transform.Find("EffectOffset").gameObject;
        }
    }
	

    public void Show()
    {
        if(effectOffset == null)
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
        else
        {
            effectOffset.SetActive(false);
            effectOffset.SetActive(true);
        }

    }

}
