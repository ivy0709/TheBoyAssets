using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    // 添加特效 特效集合 
    private PlayerEffect[] playerEffectArray;
    private Dictionary<string, PlayerEffect> playerEffectDict = new Dictionary<string, PlayerEffect>();
    private void Awake()
    {
        playerEffectArray = this.GetComponentsInChildren<PlayerEffect>();
    }

    // Use this for initialization
    void Start () {
        foreach(PlayerEffect item in playerEffectArray)
        {
            playerEffectDict.Add(item.gameObject.name, item);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    // 1. 技能名字 attack01 attack02 attack03
    // 2. 对应的特效名字
    // 3. 对应的声音名字
    // 4. 对应的位置前移
    // 5. 对应的跳跃高度
    private void Attack(string arg)
    {
        string[] arglist = arg.Split(',');
        string effectName = arglist[1];
        string soundName = arglist[2];
        float forwardStep = float.Parse(arglist[3]);
        //OnShowEffect(effectName);
        OnShowEffect(effectName);
        OnPlaySound(soundName);
        iTween.MoveBy(this.gameObject, forwardStep* Vector3.forward, 0.3f);
    }
    private void OnShowEffect(string name)
    {
        PlayerEffect item;
        if(playerEffectDict.TryGetValue(name, out item))
        {
            item.Show();
        }
    }
    private void OnPlaySound(string name)
    {
        SoundManager._instance.Play(name);
    }
}
