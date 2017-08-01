using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    // 为了方便整理管理声音 所以把这个声音单独做成一个gameobject里的脚本 做成单例
    public static SoundManager _instance;

    // 添加声音 声音集合
    public AudioClip[] SoundClipArray;
    // 播放源组件
    public AudioSource SoundSource;

    private Dictionary<string, AudioClip> soundClipDict = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start () {
        foreach(AudioClip item in SoundClipArray)
        {
            soundClipDict.Add(item.name, item);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Play(string name)
    {
        AudioClip item;
        if (soundClipDict.TryGetValue(name, out item))
        {
            SoundSource.PlayOneShot(item);
        }
    }
    public void Play(string name, AudioSource source)
    {
        AudioClip item;
        if (soundClipDict.TryGetValue(name, out item))
        {
            source.PlayOneShot(item);
        }
    }
}
