﻿using UnityEngine;
using System.Collections;

public enum WolfState
{
    Idle,
    Walk,
    Attack,
    Death
}

public class BabyWolf : MonoBehaviour
{
    public WolfState state = WolfState.Idle;
    public string aniname_death;
    public string aniname_idle;
    public string aniname_walk;
    public string aniname_now;

    private float time = 1f;
    private float timer = 0f;
    public float speed = 1;  //狼的移动速度
    public int hp = 100;  //狼的血量
    public int attack_normal = 10;  //普通攻击量
    public int attack_crazy = 20;  //疯狂攻击量
    public float miss = 0.2f;  //miss的概率
    public int exp = 20;  //杀死后获得的经验

    public Color normal;
    public GameObject body;

    public AudioClip miss_sound;
    private GameObject hudTextFollow;
    private GameObject hudTextGo;
    public GameObject hudTextPrefab;
    private HUDText hudText;
    private UIFollowTarget followTarget;

    private CharacterController cc;

    public string aniname_normalAttack;
    public float time_normalAttack;

    public string aniname_crazyAttack;
    public float time_crazyAttack;
    public float crazyAttack_rate = 0.2f;  //疯狂攻击的概率

    public string aniname_attack_now;

    public float attack_rate = 1;  //每秒攻击速率
    public float attack_timer = 0;

    public Transform target;
    public float minDistance = 1f;  //最小距离，在这个距离内开始攻击
    public float maxDistance = 5f;  //最大距离，在这个距离外停止攻击

    public WolfSpawn spawn;
    private PlayerStatus ps;
    private PlayerAttack playerAttack;

    void Awake()
    {
        aniname_attack_now = aniname_idle;

        cc = GetComponent<CharacterController>();
        normal = body.GetComponent<Renderer>().material.color;
        hudTextFollow = transform.Find("HUDText").gameObject;
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    // Use this for initialization
    void Start()
    {
        hudTextGo = NGUITools.AddChild(HUDTextParent.instance.gameObject, hudTextPrefab);

        hudText = hudTextGo.GetComponent<HUDText>();
        followTarget = hudTextGo.GetComponent<UIFollowTarget>();
        followTarget.target = hudTextFollow.transform;
        followTarget.gameCamera = Camera.main;
        followTarget.uiCamera = UICamera.currentCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == WolfState.Death)
        {
            //死亡
            GetComponent<Animation>().CrossFade(aniname_death);
        }
        else if (state == WolfState.Attack)
        {
            //自动攻击状态
            AutoAttack();
        }
        else
        {
            //巡逻
            GetComponent<Animation>().CrossFade(aniname_now);

            if (aniname_now == aniname_walk)
            {
                cc.SimpleMove(transform.forward * speed);
            }

            timer += Time.deltaTime;
            if (timer > time)
            {
                //表示计时结束，切换状态
                timer = 0;
                RandomState();
            }
        }

        if (Input.GetKeyDown(KeyCode.F2))  //测试用
        {
            TakeDamage(100);
        }
    }

    void AutoAttack()  //小狼自动攻击
    {
        if (target != null)
        {
            playerAttack = target.GetComponent<PlayerAttack>();
            if (playerAttack.state == PlayerState.Death)  //人物死亡，停止攻击
            {
                target = null;
                state = WolfState.Idle;
                return;
            }

            float distance = Vector3.Distance(transform.position, target.position);
            if (distance > maxDistance)  //人物超过距离，停止攻击
            {
                target = null;
                state = WolfState.Idle;
            }
            else if (distance < minDistance)  //自动攻击
            {
                transform.LookAt(target);
                attack_timer += Time.deltaTime;
                GetComponent<Animation>().CrossFade(aniname_attack_now);
                if (aniname_attack_now == aniname_normalAttack)  //攻击计时，普通攻击
                {
                    if (attack_timer > time_normalAttack)  //时间到了，攻击停止，产生伤害
                    {
                        target.GetComponent<PlayerAttack>().TakeDamage(attack_normal);
                        aniname_attack_now = aniname_idle;
                    }
                }
                else if (aniname_attack_now == aniname_crazyAttack)  //攻击计时，普通攻击
                {
                    if (attack_timer > time_crazyAttack)  //时间到了，攻击停止，产生伤害
                    {
                        target.GetComponent<PlayerAttack>().TakeDamage(attack_crazy);
                        aniname_attack_now = aniname_idle;
                    }
                }
                if (attack_timer > (1f / attack_rate))  //再次进行攻击
                {
                    RandomAttack();
                    attack_timer = 0f;
                }
            }
            else  //朝向角色移动
            {
                transform.LookAt(target);
                GetComponent<Animation>().CrossFade(aniname_walk);
                cc.SimpleMove(transform.forward * speed);
            }
        }
        else
        {
            state = WolfState.Idle;
        }
    }

    void RandomAttack()  //随机攻击
    {
        float value = Random.Range(0f, 1f);  //0-1之间的数字
        if (value < crazyAttack_rate)
        {
            aniname_attack_now = aniname_crazyAttack;
        }
        else
        {
            aniname_attack_now = aniname_normalAttack;
        }
    }

    void RandomState()  //随机状态，防止动作死板
    {
        int value = (int)Random.Range(0f, 2f);  //0-1之间的数字
        if (value == 1)
        {
            aniname_now = aniname_idle;
        }
        else
        {
            if (aniname_now != aniname_walk)
            {
                transform.Rotate(transform.up * Random.Range(0, 360));  //当状态切换到行走的时候生成新的方向
            }
            aniname_now = aniname_walk;
        }
    }

    public void TakeDamage(int attack)  //受到伤害
    {
        if (state == WolfState.Death)
        {
            return;
        }

        target = GameObject.FindGameObjectWithTag(Tags.player).transform;  //受到攻击的时候小狼进行自动攻击
        state = WolfState.Attack;

        float value = Random.Range(0f, 1f);
        if (value < miss)  //miss
        {
            AudioSource.PlayClipAtPoint(miss_sound, transform.position);  //miss时候的声音
            hudText.Add("Miss", Color.grey, 0.1f);  //显示miss效果
        }
        else  //打中
        {
            hp -= attack;
            hudText.Add("-" + attack, Color.red, 0.1f);  //显示掉血量 
            StartCoroutine(ShowBodyRed());  //使用协同方法
            if (hp <= 0)
            {
                state = WolfState.Death;
                Destroy(gameObject, 2);
            }
        }
    }

    IEnumerator ShowBodyRed()
    {
        body.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        body.GetComponent<Renderer>().material.color = normal;
    }

    void OnDestroy()
    {
        ps.GetExp(exp);  //获得经验值
        BarNPC.instance.OnKillWolf();
        spawn.MinusNumber();
        Destroy(hudTextGo);
    }

    void OnMouseEnter()
    {
        if (PlayerAttack.instance.isLockingTarget == false)
        {
            CursorManager.instance.SetAttack();
        }
    }

    void OnMouseExit()
    {
        if (PlayerAttack.instance.isLockingTarget == false)
        {
            CursorManager.instance.SetNormal();
        }
    }
}
