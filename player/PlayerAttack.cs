using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerState
{
    ControlWalk,
    NormalAttack,
    SkillAttack,
    Death
}

public enum AttackState  //攻击时候的状态
{
    Moving,
    Idle,
    Attack
}

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    public PlayerState state = PlayerState.ControlWalk;
    public AttackState attack_state = AttackState.Idle;

    public string aniname_now;  //当前状态
    public string aniname_idle;
    public string aniname_normalAttack;
    public string aniname_death;

    public float time_normalAttack;  //普通攻击的时间
    private float timer = 0f;
    public float minDistance = 2f;  //默认攻击的最小距离
    public float normalAttack_rate = 0.5f;  //普通攻击速率
    public float miss_rate = 0.2f;  //miss概率

    public Transform target_normalAttack;
    private PlayerMove move;
    private PlayerStatus ps;

    private bool showEffect = false;  //是否已经显示特效
    public GameObject effect;

    public AudioClip miss_sound;
    public GameObject hudTextPrefab;
    private GameObject hudTextGo;
    private GameObject hudTextFollow;
    private HUDText hudText;
    private UIFollowTarget followTarget;

    public GameObject body;
    private Color normal;

    public GameObject[] efxArray;
    private Dictionary<string, GameObject> efxDict = new Dictionary<string, GameObject>();

    public bool isLockingTarget = false;  //表示是否正在选择目标
    private SkillsInfo info = null;

    void Awake()
    {
        instance = this;
        move = GetComponent<PlayerMove>();
        ps = GetComponent<PlayerStatus>();

        hudTextFollow = transform.Find("HUDText").gameObject;
        normal = body.GetComponent<Renderer>().material.color;

        foreach (GameObject go in efxArray)
        {
            efxDict.Add(go.name, go);
        }
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
        if (!isLockingTarget && Input.GetMouseButtonDown(0) && state != PlayerState.Death)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && hitInfo.collider.tag == Tags.enemy)  //点击到了敌人
            {
                Debug.Log("-->Attack");
                target_normalAttack = hitInfo.collider.transform;
                aniname_now = aniname_normalAttack;
                state = PlayerState.NormalAttack;  //进入普通攻击的模式
                timer = 0f;
                showEffect = false;
            }
            else  //点击地面进行走路
            {
                target_normalAttack = null;
                state = PlayerState.ControlWalk;
            }
        }

        if (state == PlayerState.NormalAttack)
        {
            if (target_normalAttack == null)
            {
                state = PlayerState.ControlWalk;
            }
            else
            {
                float distance = Vector3.Distance(transform.position, target_normalAttack.position);
                if (distance <= minDistance)
                {
                    //进行攻击
                    transform.LookAt(target_normalAttack.position);
                    attack_state = AttackState.Attack;
                    timer += Time.deltaTime;
                    GetComponent<Animation>().CrossFade(aniname_now);
                    if (timer >= time_normalAttack)
                    {
                        //停止攻击
                        aniname_now = aniname_idle;
                        if (showEffect == false)
                        {
                            //播放特效
                            Debug.Log("-->Damage");
                            Instantiate(effect, target_normalAttack.position, Quaternion.identity);
                            target_normalAttack.GetComponent<BabyWolf>().TakeDamage(GetAttack());
                            showEffect = true;
                        }
                    }

                    if (timer >= (1f / normalAttack_rate))
                    {
                        aniname_now = aniname_normalAttack;
                        timer = 0f;
                        showEffect = false;
                    }
                }
                else
                {
                    //走向敌人
                    attack_state = AttackState.Moving;
                    move.SimpleMove(target_normalAttack.position);
                }
            }
        }

        if (isLockingTarget && Input.GetMouseButtonDown(0) && state != PlayerState.Death)  //准备释放技能
        {
            OnLockTarget();
        }
    }

    public int GetAttack()  //对敌人产生伤害值
    {
        return ps.attack + ps.attack_plus;
    }

    public void TakeDamage(int attack)  //受伤扣血
    {
        int def = ps.def + ps.def_plus;
        int temp = attack - (int)(def * 0.5);  //扣血公式，当小于1的时候，默认扣1
        if (temp < 1)
        {
            temp = 1;
        }
        float value = Random.Range(0f, 1f);
        if (value < miss_rate)
        {
            AudioSource.PlayClipAtPoint(miss_sound, transform.position);
            hudText.Add("miss", Color.grey, 0.1f);
        }
        else
        {
            //打中
            ps.hp_remain -= temp;
            hudText.Add("-" + temp, Color.red, 0.1f);  //显示掉血量 
            HeadStatus.instance.UpdateShow();

            StartCoroutine(ShowBodyRed());  //使用协同方法
            if (ps.hp_remain <= 0)
            {
                state = PlayerState.Death;
            }
        }
        if (state == PlayerState.Death) //如果死亡就播放死亡动画
        {
            DieScript.instance.DeadWindow();
            GetComponent<Animation>().CrossFade(aniname_death);
        }
    }

    IEnumerator ShowBodyRed()
    {
        body.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        body.GetComponent<Renderer>().material.color = normal;
    }

    public void UseSkill(SkillsInfo info)  //根据技能类型，使用技能
    {
        switch (info.applyType)
        {
            case ApplyType.Passive:  //增益技能
                StartCoroutine(OnPassiveSkillUse(info));
                break;
            case ApplyType.Buff:  //增强技能
                StartCoroutine(OnBuffSkillUse(info));
                break;
            case ApplyType.SingleTarget:  //单个目标攻击
                OnTargetSkillUse(info);
                break;
            case ApplyType.MultipleTarget:  //群体目标攻击
                OnTargetSkillUse(info);
                break;
        }
    }

    //增益技能
    IEnumerator OnPassiveSkillUse(SkillsInfo info)
    {
        state = PlayerState.SkillAttack;
        GetComponent<Animation>().CrossFade(info.ani_name);
        yield return new WaitForSeconds(info.ani_time);
        state = PlayerState.ControlWalk;

        //实例化特效
        GameObject prefab = null;
        efxDict.TryGetValue(info.efx_name, out prefab);
        Instantiate(prefab, transform.position, Quaternion.identity);

        int hp = 0, mp = 0;
        if (info.applyProperty == ApplyProperty.HP)
        {
            hp = info.applyValue;  //作用值为hp 
        }
        else if (info.applyProperty == ApplyProperty.MP)
        {
            mp = info.applyValue;  //作用值为mp
        }
        ps.GetDrug(hp, mp);  //使用加血加蓝技能相当于调用此函数
    }

    //增强技能
    IEnumerator OnBuffSkillUse(SkillsInfo info)
    {
        state = PlayerState.SkillAttack;
        GetComponent<Animation>().CrossFade(info.ani_name);
        yield return new WaitForSeconds(info.ani_time);
        state = PlayerState.ControlWalk;

        //实例化特效
        GameObject prefab = null;
        efxDict.TryGetValue(info.efx_name, out prefab);
        Instantiate(prefab, transform.position, Quaternion.identity);

        //都是乘以百分比
        if (info.applyProperty == ApplyProperty.Attack)  //增加攻击力
        {
            ps.attack = (int)(ps.attack * (info.applyValue / 100f));
        }
        else if (info.applyProperty == ApplyProperty.Def)  //增加防御力
        {
            ps.def = (int)(ps.def * (info.applyValue / 100f));
        }
        else if (info.applyProperty == ApplyProperty.AttackSpeed)  //增加攻击速度
        {
            normalAttack_rate *= (int)(info.applyValue / 100f);
            if (normalAttack_rate > 1f)
            {
                normalAttack_rate = 1f;
            }
        }
        else if (info.applyProperty == ApplyProperty.Speed)  //增加移动速度
        {
            ps.speed = (int)(ps.speed * (info.applyValue / 100f));
        }

        yield return new WaitForSeconds(info.applyTime);  //作用时间

        if (info.applyProperty == ApplyProperty.Attack)  //增加攻击力
        {
            ps.attack = (int)(ps.attack / (info.applyValue / 100f));
        }
        else if (info.applyProperty == ApplyProperty.Def)  //减少防御力
        {
            ps.def = (int)(ps.def / (info.applyValue / 100f));
        }
        else if (info.applyProperty == ApplyProperty.AttackSpeed)  //减少攻击速度
        {
            normalAttack_rate /= (int)(info.applyValue / 100f);
        }
        else if (info.applyProperty == ApplyProperty.Speed)  //减少移动速度
        {
            ps.speed = (int)(ps.speed / (info.applyValue / 100f));
        }
    }

    //准备选择目标
    void OnTargetSkillUse(SkillsInfo info)
    {
        state = PlayerState.SkillAttack;
        CursorManager.instance.SetTarget();
        isLockingTarget = true;
        this.info = info;
    }

    //选择目标完成，准备开始技能的释放
    void OnLockTarget()
    {
        isLockingTarget = false;
        if (info.applyType == ApplyType.SingleTarget)
        {
            StartCoroutine(OnLockSingleTarget());
        }
        else if (info.applyType == ApplyType.MultipleTarget)
        {
            StartCoroutine(OnLockMultipleTarget());
        }
        CursorManager.instance.SetNormal();
    }

    //单个目标技能实现
    IEnumerator OnLockSingleTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool isCollider = Physics.Raycast(ray, out hitInfo);
        if (isCollider && hitInfo.collider.tag == Tags.enemy)  //目标为敌人，可以开始攻击
        {
            transform.LookAt(hitInfo.collider.transform.position);
            state = PlayerState.SkillAttack;
            GetComponent<Animation>().CrossFade(info.ani_name);
            yield return new WaitForSeconds(info.ani_time);
            state = PlayerState.ControlWalk;

            //实例化特效
            GameObject prefab = null;
            efxDict.TryGetValue(info.efx_name, out prefab);
            Instantiate(prefab, hitInfo.collider.transform.position, Quaternion.identity);

            int damage = (int)(GetAttack() * info.applyValue / 100f);
            hitInfo.collider.GetComponent<BabyWolf>().TakeDamage(damage);  //技能伤害为普通攻击乘以百分比
        }
        else
        {
            state = PlayerState.ControlWalk;
            //目标不是敌人，取消攻击
        }
    }

    //群体目标技能实现
    IEnumerator OnLockMultipleTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool isCollider = Physics.Raycast(ray, out hitInfo);
        if (isCollider)  //可以开始攻击
        {
            transform.LookAt(hitInfo.point);
            state = PlayerState.SkillAttack;
            GetComponent<Animation>().CrossFade(info.ani_name);
            yield return new WaitForSeconds(info.ani_time);
            state = PlayerState.ControlWalk;

            //实例化特效
            GameObject prefab = null;
            efxDict.TryGetValue(info.efx_name, out prefab);
            GameObject go = (GameObject)Instantiate(prefab, hitInfo.point + Vector3.up, Quaternion.identity);
            int damage = (int)(GetAttack() * info.applyValue / 100f);  //技能伤害为普通攻击乘以百分比
            go.GetComponent<MagicSphere>().attack = damage;
        }
        else  //没有点击到碰撞体
        {
            state = PlayerState.ControlWalk;
        }
    }

    void OnDestroy()
    {
        Destroy(hudTextGo);
    }
}