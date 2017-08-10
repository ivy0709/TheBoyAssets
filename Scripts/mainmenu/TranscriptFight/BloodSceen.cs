using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSceen : MonoBehaviour {

    public static BloodSceen _instance;
    [SerializeField]
    private TweenAlpha tweenAlpha;


    private void Awake()
    {
        _instance = this;
        tweenAlpha = this.transform.GetComponent<TweenAlpha>();
    }

    public void OnShow()
    {
        tweenAlpha.ResetToBeginning();
        tweenAlpha.PlayForward();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
