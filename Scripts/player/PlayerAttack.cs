using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    // 添加特效 特效集合 
    private PlayerEffect[] playerEffectArray;
    public PlayerEffect[] playerEffectOutside;

    [SerializeField]
    private Transform hudPoint;
    [SerializeField]
    private GameObject damageHUDGo;
    [SerializeField]
    private HUDText damageHUDText;

    // 先暂时用这个
    [SerializeField]
    private int hp = 1000;

    [SerializeField]
    private PlayerTranscriptAnimation playerAnim;

    private Dictionary<string, PlayerEffect> playerEffectDict = new Dictionary<string, PlayerEffect>();

    public int ForwardDis = 2;
    public int ArroundDis = 2;
    private int[] damageValues = { 20, 30, 30, 30 };
    enum DetectDirection
    {
        Forward = 0,
        Around,
    }
    enum SpecialType
    {
        BornUnderEnemy = 0,
        FromPlayerToEnemy,
    }
    private void Awake()
    {
        playerEffectArray = this.GetComponentsInChildren<PlayerEffect>();
        playerAnim = this.GetComponent<PlayerTranscriptAnimation>();
        hudPoint = this.transform.Find("HUDPoint");
    }

    // Use this for initialization
    void Start () {
        foreach(PlayerEffect item in playerEffectArray)
        {
            playerEffectDict.Add(item.gameObject.name, item);
        }
        foreach (PlayerEffect item in playerEffectOutside)
        {
            playerEffectDict.Add(item.gameObject.name, item);
        }

        damageHUDGo = HpBarManager._instance.CreateDamageHUD(hudPoint);
        damageHUDText = damageHUDGo.GetComponent<HUDText>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 普通攻击的触发事件
    /// </summary>
    /// <param name="arg">字符串: 1. 技能名字 attack01 attack02 attack03 
    // 2. 对应的特效名字
    // 3. 对应的声音名字
    // 4. 对应的位置前移
    // 5. 对应的跳跃高度
    // 6. 加一个伤害值</param>
    private void Attack(string arg)
    {
        string[] arglist = arg.Split(',');
        string effectName = arglist[1];
        string soundName = arglist[2];
        float forwardStep = float.Parse(arglist[3]);
        OnShowEffectNormal(effectName);
        OnPlaySound(soundName);
        iTween.MoveBy(this.gameObject, forwardStep* Vector3.forward, 0.3f);

        // 敌人的take damage方法 必须传递是什么类型的攻击
        if (arglist[6] == "")
            return;
        ArrayList lists = GetEnemyList((DetectDirection)int.Parse(arglist[6]));
        foreach(GameObject item in lists)
        {
            string ttmp = arglist[5] + "," + arglist[3] + "," + arglist[4];
            item.SendMessage("OnTakeDamage", ttmp);
        }
    }
    /// <summary>
    /// 从 PlayerEffect脚本中的OnShow里进行显示
    /// </summary>
    /// <param name="name">特效名字</param>
    private void OnShowEffectNormal(string name)
    {
        PlayerEffect item = TryGetPlayerEffect(name);
        if(item != null)
        {
            item.Show();
        }
    }
    private void OnPlaySound(string name)
    {
        SoundManager._instance.Play(name);
    }


    /// <summary>
    /// 在特定位置生成特效
    /// </summary>
    /// <param name="args">1. 特效名称 2 生成特效的位置 3.目标位置 4，附近还是前方</param>
    private void OnShowEffectSpecial(string arg)
    {
        string[] arglist = arg.Split(',');
        string effectName = arglist[0];
        PlayerEffect item = TryGetPlayerEffect(effectName);
        if (item == null)
        {
            return;
        }
        ArrayList lists = GetEnemyList((DetectDirection)int.Parse(arglist[2]));
        if (int.Parse(arglist[1]) ==(int)SpecialType.BornUnderEnemy)
        {
            foreach (GameObject go in lists)
            {
                RaycastHit hit;
                if (IsColliderGround(go, out hit))
                {
                    // 出生在敌人脚下
                    GameObject.Instantiate(item.gameObject, hit.point, Quaternion.identity);
                }
            }
        }
        else if(int.Parse(arglist[1]) == (int)SpecialType.FromPlayerToEnemy)
        {
            foreach (GameObject go in lists)
            {
                // 出生在角色位置
                GameObject goEffect = GameObject.Instantiate(item.gameObject);
                goEffect.transform.position = transform.position + Vector3.up;
                goEffect.GetComponent<EffectSettings>().Target = go;
            }
        }
    }
    /// <summary>
    /// 从字典中获取特效
    /// </summary>
    /// <param name="effectName">特效名字</param>
    /// <returns></returns>
    private PlayerEffect TryGetPlayerEffect(string effectName)
    {
        PlayerEffect item;
        playerEffectDict.TryGetValue(effectName, out item);
        return item;
    }

    /// <summary>
    /// 检测go向正下方投射， 与 地面 之间的焦点。
    /// </summary>
    /// <param name="go">参考物</param>
    /// <param name="hit">碰撞点</param>
    /// <returns></returns>
    private static bool IsColliderGround(GameObject go, out RaycastHit hit)
    {
        //  + Vector3.up 为了防止敌人的位置和地面重合
        bool collider = Physics.Raycast(go.transform.position + Vector3.up,
            Vector3.down,
            out hit,
            5f,
            LayerMask.GetMask("Ground"));
        return collider;
    }

    /// <summary>
    /// 获取范围内的敌人列表
    /// </summary>
    /// <param name="type">探测的范围</param>
    /// <returns></returns>
    private ArrayList GetEnemyList(DetectDirection type)
    {
        ArrayList lists = new ArrayList();
        if(type == DetectDirection.Forward)
        {
            foreach(GameObject item in TranscriptFightManager._instance.enemys)
            {
                if (item == null) continue;
                // 是否在角色的前面 
                Vector3 local = transform.InverseTransformPoint(item.transform.position);
                if (local.z < -0.5)
                {
                    continue;
                }
                // 是否跟角色相差多少距离
                if(local.magnitude < ForwardDis)
                {
                    lists.Add(item);
                }
            }
        }
        else if(type == DetectDirection.Around)
        {
            foreach (GameObject item in TranscriptFightManager._instance.enemys)
            {
                if (item == null) continue;
                if (Vector3.Distance(item.transform.position, transform.position) < ArroundDis)
                {
                    lists.Add(item);
                }
            }
        }
        return lists;
    }

    private void TakeDamage(int damage)
    {
        if(hp < 0)
        {
            return;
        }
        hp -= damage;
        // 一定概率播放动画
        int cmp = Random.Range(1, 100);
        if(cmp < damage)
        {
            playerAnim.OnHit();
        }
        // 显示受伤害值
        damageHUDText.Add(damage, Color.red, 0.2f);
        // 屏幕显示血效果
        BloodSceen._instance.OnShow();
        // 播放受伤的声音
        SoundManager._instance.Play("Hurt");
    }
}
