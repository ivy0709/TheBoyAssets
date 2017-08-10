using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private Animation anim;
    [SerializeField]
    private Rigidbody rigid;
    private GameObject player;
    [SerializeField]
    private float playerDistance;


    [SerializeField]
    private float moveSpeed = 2.0f;
    [SerializeField]
    private float moveDistance = 15;

    private State curState = State.Idle;

    [SerializeField]
    private float attackDistance = 2.0f;
    [SerializeField]
    private int attackRate = 2;//几秒攻击一次
    [SerializeField]
    private float attackTimer = 2.0f;//计时器

    public float viewFiled = 50;
    public float turnSpeed = 1.0f;


    private int attackIdx = 0;
    private bool isAttacking = false;

    // 添加特效 特效集合 
    private BossEffect[] bossEffectArray;
    private Dictionary<string, BossEffect> bossEffectDict = new Dictionary<string, BossEffect>();


    public GameObject bossBulletPrefab;
    [SerializeField]
    private Transform bossBulletPos;

    private void Awake()
    {
        anim = this.GetComponent<Animation>();
        rigid = this.transform.GetComponent<Rigidbody>();
        bossEffectArray = this.GetComponentsInChildren<BossEffect>();
        bossBulletPos = this.transform.Find("bossBulletPos");
    }

    // Use this for initialization
    void Start()
    {
        player = TranscriptFightManager._instance.player;
        InvokeRepeating("CalcDistance", 0, 0.1f);
        foreach (BossEffect item in bossEffectArray)
        {
            bossEffectDict.Add(item.gameObject.name, item);
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (curState == State.Die)
        {
            // return;
        }
        if (playerDistance <= attackDistance)
        {
            // 因为boss的攻击动作的attack03时间过长，所以在动画结束的时候发送事件，表明动画已经结束，再进行计时
            if (!isAttacking)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer > attackRate)
                {
                    ChangeState(State.Attack);
                    attackTimer = 0;
                }
                else
                {
                    ChangeState(State.Idle);
                }
            }
        }
        else if (playerDistance < moveDistance)
        {
            Vector3 targetPos = player.transform.position;
            targetPos.y = this.transform.position.y;
            float angle = Vector3.Angle(targetPos - this.transform.position, this.transform.forward);
            if (angle > viewFiled)
            {
                TurnToPlayer();
                ChangeState(State.TurnTo);
            }
            else
            {
                MoveToPlayer();
                ChangeState(State.Walk);
            }
        }
        else
        {
            ChangeState(State.Idle);
        }
    }
    private void ChangeState(State type)
    {
        if (type == curState && type != State.Attack)
        {
            return;
        }
        if (type != State.Attack)
        {
            isAttacking = false;
        }

        if (type == State.Walk)
        {
            anim.CrossFade("walk");
        }
        if (type == State.TurnTo)
        {
            anim.CrossFade("walk");
        }
        else if (type == State.Idle)
        {
            anim.CrossFade("idle");
        }
        else if (type == State.Attack)
        {
            attackIdx = attackIdx % 3;
            anim.CrossFade("attack0" + (attackIdx + 1));
            attackIdx++;
            isAttacking = true;
        }
        else if (type == State.Die)
        {
            // 第一种死亡方式，动画播放
            anim.CrossFade("die");
            // 第二种死亡方式，破碎动画
            // this.GetComponentInChildren<MeshExploder>().Explode();
            // this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        }
        curState = type;
    }
    private float CalcDistance()
    {
        playerDistance = Vector3.Distance(this.transform.position, player.transform.position);
        return playerDistance;
    }

    private void MoveToPlayer()
    {
        Vector3 targetPos = player.transform.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        // 移动
        rigid.MovePosition(transform.position + moveSpeed * transform.forward * Time.deltaTime);
    }
    private void TurnToPlayer()
    {
        Vector3 targetPos = player.transform.position;
        targetPos.y = transform.position.y; // 保证不受y的影响
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation =
            Quaternion.Lerp(transform.rotation,
                            targetRotation,
                            turnSpeed * Time.deltaTime);
    }
    private void FinishAttacking()
    {
        isAttacking = false;
    }
    private BossEffect TryGetBossEffect(string effectName)
    {
        BossEffect item;
        bossEffectDict.TryGetValue(effectName, out item);
        return item;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="arg">1.技能名字 2.特效名字</param>
    private void Attack(string arg)
    {
        string[] arglist = arg.Split(',');
        string skillName = arglist[0];
        string effectName = arglist[1];
        int attackDamage = int.Parse(arglist[2]);
        if (skillName == "attack03")
        {
            GameObject go = GameObject.Instantiate(bossBulletPrefab, bossBulletPos.position, bossBulletPos.rotation);
            go.GetComponent<BossBullet>().attackDamage = attackDamage;
        }
        else
        {
            OnShowEffectNormal(effectName);
            if (CalcDistance() < attackDistance)
            {
                player.SendMessage("TakeDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
            }
        }

    }
    private void OnShowEffectNormal(string name)
    {
        BossEffect item = TryGetBossEffect(name);
        if (item != null)
        {
            item.Show();
        }
    }
}
