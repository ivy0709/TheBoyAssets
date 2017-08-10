using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    TurnTo,
    Walk,
    Attack,
    Die,
}


public class Enemy : MonoBehaviour {

    public GameObject BloodEffect;
    [SerializeField]
    private Transform bloodPoint;

    [SerializeField]
    private Transform hpBarPoint;
    [SerializeField]
    private UISlider hpBarSlider;
    [SerializeField]
    private GameObject hpBarGo;
    [SerializeField]
    private GameObject damageHUDGo;
    [SerializeField]
    private HUDText damageHUDText;

    [SerializeField]
    public int hpMax = 200;
    private int hpNow;

    [SerializeField]
    private Animation anim;

    [SerializeField]
    private float moveSpeed = 1.5f;
    private GameObject player;
    [SerializeField]
    private float playerDistance = 15;
    [SerializeField]
    private float moveDistance = 15;
    [SerializeField]
    private CharacterController cc;

    private State curState = State.Idle;

    [SerializeField]
    private float attackDistance = 1.8f;
    [SerializeField]
    private int attackRate = 2;//几秒攻击一次
    [SerializeField]
    private float attackTimer = 2.0f;//计时器
    public int attackDamage = 20;


    [SerializeField]
    private float dieDownSpeed = 1.0f;
    private float dieDownDistance;


    public int HP
    {
        get
        {
            return hpNow;
        }

        set
        {
            hpNow = value;
        }
    }
    #region unity event
    private void Awake()
    {
        bloodPoint = this.transform.Find("BloodPoint");
        hpBarPoint = this.transform.Find("HpBarPoint");
        anim = this.GetComponent<Animation>();
        cc = this.GetComponent<CharacterController>();
        hpNow = hpMax;
    }
    private void Start()
    {
        TranscriptFightManager._instance.enemys.Add(this.gameObject);
        player = TranscriptFightManager._instance.player;
        InvokeRepeating("CalcDistance", 0, 0.1f);
        hpBarGo = HpBarManager._instance.CreateHpBar(hpBarPoint);
        hpBarSlider = hpBarGo.transform.Find("bg").GetComponent<UISlider>();
        hpBarSlider.value = (float)hpNow / hpMax;

        damageHUDGo = HpBarManager._instance.CreateDamageHUD(hpBarPoint);
        damageHUDText = damageHUDGo.GetComponent<HUDText>();
    }
    private void Update()
    {
        if(curState == State.Die)
        {
            dieDownDistance += dieDownSpeed * Time.deltaTime;
            transform.Translate(-transform.up * dieDownSpeed * Time.deltaTime);
            if(dieDownDistance > 4)
            {
                Destroy(this.gameObject);
            }
            return;
        }
        if (playerDistance <= attackDistance)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer > attackRate)
            {
                ChangeState(State.Attack);
                attackTimer = 0;
            }
            else
            {
                if(!anim.IsPlaying("attack01"))
                    ChangeState(State.Idle);
            }
        }
        else if (playerDistance < moveDistance )
        {
            MoveToPlayer();
            ChangeState(State.Walk);
        }
        else
        {
            ChangeState(State.Idle);
        }
    }
    #endregion
    private void ChangeState(State type)
    {
        if(type == curState)
        {
            return;
        }

        if(type == State.Walk)
        {
            anim.Play("walk");
        }else if(type == State.Idle)
        {
            anim.Play("idle");  
        }
        else if (type == State.Attack)
        {
            anim.Play("attack01");
        }
        else if (type == State.Die)
        {
            // 第一种死亡方式，动画播放
            anim.Play("die");
            // 第二种死亡方式，破碎动画
            // this.GetComponentInChildren<MeshExploder>().Explode();
            // this.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        }
        curState = type;
    }


    private void MoveToPlayer()
    {
        Vector3 targetPos = player.transform.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        cc.SimpleMove(transform.forward * moveSpeed);
    }
    private void OnDead()
    {
        ChangeState(State.Die);
        // 敌人列表删除
        TranscriptFightManager._instance.enemys.Remove(this.gameObject);
        // 头顶血条
        Destroy(hpBarGo);
        // 头顶伤害值
        Destroy(damageHUDGo);
        // 敌人的碰撞器失效
        this.transform.GetComponent<CharacterController>().enabled = false;
    }

    private void Attack()
    {
        // 再次计算距离
        if(CalcDistance() < attackDistance)
        {
            player.SendMessage("TakeDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
        }

    }
    private float CalcDistance()
    {
        playerDistance = Vector3.Distance(this.transform.position, player.transform.position);
        return playerDistance;
    }
    public void OnTakeDamage(string args)
    {
        string[] arglist = args.Split(',');
        // 伤害
        int damage = int.Parse(arglist[0]);

        if (damage <= 0 || HP <= 0) return;


        HP -= damage;
        Combo._instance.ComboPlus();
        hpBarSlider.value = (float)HP / hpMax;
        damageHUDText.Add(damage, Color.red, 0.2f);

        // 播放受伤动画
        anim.Play("takedamage");
        // 后退浮空效果        
        float forwardStep = float.Parse(arglist[1]);
        float upStep = float.Parse(arglist[2]);
        if(upStep > 0.1f || forwardStep > 0.1f)
        {

            iTween.MoveBy(this.gameObject,
                transform.InverseTransformDirection(TranscriptFightManager._instance.player.transform.forward) * forwardStep
                + Vector3.up * upStep,
                0.3f);
        }
        // 血块特效
        GameObject.Instantiate(BloodEffect, bloodPoint.position, Quaternion.identity);

        // 播放受伤的声音
        SoundManager._instance.Play("Hurt");
        // 是否死亡
        if (HP <= 0)
        {
            OnDead();
        }
    }
    
}
