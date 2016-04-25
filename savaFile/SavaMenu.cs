using UnityEngine;
using System.Collections;

public class SavaMenu : MonoBehaviour
{
    public static SavaMenu instance;
    private TweenPosition tween;

    private PlayerStatus ps;  //记录人物状态

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

    //KillCount
    //Coin

    void Awake()
    {
        instance = this;
        tween = GetComponent<TweenPosition>();
    }

    // Use this for initialization
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenMenu()
    {
        tween.PlayForward();
    }

    public void CloseMenu()
    {
        tween.PlayReverse();
    }

    public void SavaGame()  //开始存档
    {
        PlayerPrefs.SetInt("Level", ps.level);
        PlayerPrefs.SetInt("Hp", ps.hp);
        PlayerPrefs.SetInt("Hp_remain", ps.hp_remain);
        PlayerPrefs.SetInt("Mp", ps.mp);
        PlayerPrefs.SetInt("Mp_remain", ps.mp_remain);
        PlayerPrefs.SetInt("Attack", ps.attack);
        PlayerPrefs.SetInt("Attack_plus", ps.attack_plus);
        PlayerPrefs.SetInt("Def", ps.def);
        PlayerPrefs.SetInt("Def_plus", ps.def_plus);
        PlayerPrefs.SetInt("Speed", ps.speed);
        PlayerPrefs.SetInt("Speed_plus", ps.speed_plus);
        PlayerPrefs.SetInt("Remain_point", ps.remain_point);
        PlayerPrefs.SetFloat("Exp", ps.exp);

        PlayerPrefs.SetInt("KillCount", BarNPC.instance.killCount);
        if (BarNPC.instance.isInTask)  //正在任务
        {
            PlayerPrefs.SetInt("InTask", 1);
        }
        else
        {
            PlayerPrefs.SetInt("InTask", 0);
        }
        PlayerPrefs.SetInt("Coin", Inventory.instance.coinNumber);

        tween.PlayReverse();
    }
}
