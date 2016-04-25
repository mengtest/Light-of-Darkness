using UnityEngine;
using System.Collections;

public class GameLoad : MonoBehaviour
{
    public GameObject magicianPrefab;
    public GameObject swordmanPrefab;

    //加载旧游戏，在playerstatus里面改属性
    //SelectedCharacter
    //Name
    //Level
    //Hp
    //Mp
    //Hp_remain
    //Mp_remain
    //Attack
    //Attack_plus
    //Def
    //Def_plus
    //Speed
    //Speed_plus
    //Remain_point
    //Exp
    //Coin

    void Awake()  //必须在Awake里更新角色
    {
        int selectIndex = PlayerPrefs.GetInt("SelectedCharacter");  //获取存储选择的角色

        GameObject go = null;
        if (selectIndex == 0)
        {
            go = Instantiate(magicianPrefab);
        }
        else if (selectIndex == 1)
        {
            go = Instantiate(swordmanPrefab);
        }

        PlayerStatus ps = go.GetComponent<PlayerStatus>();

        string name = PlayerPrefs.GetString("Name");
        ps.name = name;

        int level = PlayerPrefs.GetInt("Level");
        ps.level = level;

        int hp = PlayerPrefs.GetInt("Hp");
        ps.hp = hp;

        int hp_remian = PlayerPrefs.GetInt("Hp_remain");
        ps.hp_remain = hp_remian;

        int mp = PlayerPrefs.GetInt("Mp");
        ps.mp = mp;

        int mp_remian = PlayerPrefs.GetInt("Mp_remain");
        ps.mp_remain = mp_remian;

        int attack = PlayerPrefs.GetInt("Attack");
        ps.attack = attack;

        int attack_plus = PlayerPrefs.GetInt("Attack_plus");
        ps.attack_plus = attack_plus;

        int def = PlayerPrefs.GetInt("Def");
        ps.def = def;

        int def_plus = PlayerPrefs.GetInt("Def_plus");
        ps.def_plus = def_plus;

        int speed = PlayerPrefs.GetInt("Speed");
        ps.speed = speed;

        int speed_plus = PlayerPrefs.GetInt("Speed_plus");
        ps.speed_plus = speed_plus;

        int remain_ponit = PlayerPrefs.GetInt("Remain_point");
        ps.remain_point = remain_ponit;

        float exp = PlayerPrefs.GetFloat("Exp");
        ps.exp = exp;

        int coinNumber = PlayerPrefs.GetInt("Coin");
        Inventory.instance.coinNumber = coinNumber;

        int killCount = PlayerPrefs.GetInt("KillCount");
        BarNPC.instance.killCount = killCount;

        int InTask = PlayerPrefs.GetInt("InTask");
        if (InTask == 1)  //更新是否接受了任务
        {
            BarNPC.instance.isInTask = true;
        }
        else
        {
            BarNPC.instance.isInTask = false;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
