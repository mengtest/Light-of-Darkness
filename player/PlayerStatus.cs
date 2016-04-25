using UnityEngine;
using System.Collections;

public enum HeroType
{
    Swordman,
    Magician,
    Common
}

public class PlayerStatus : MonoBehaviour
{
    public HeroType heroType;

    public int level = 1;  //升级经验：100+level*30
    public string name = "默认名字";
    public int hp = 100;  //hp最大值
    public int mp = 100;  //mp最大值
    public int hp_remain = 100;  //当前hp
    public int mp_remain = 100;  //当前mp

    public int attack = 10;
    public int attack_plus = 0;
    public int def = 10;
    public int def_plus = 0;
    public int speed = 10;
    public int speed_plus = 0;

    public int remain_point;  //剩余点数 

    public float exp = 0;  //当前经验

    public GameObject prefab;

    // Use this for initialization
    void Start()
    {
        GetExp(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CostPoint(int point = 1)
    {
        if (remain_point >= point)
        {
            remain_point -= point;
            return true;
        }
        return false;
    }

    public void GetDrug(int hp, int mp)
    {
        hp_remain += hp;
        mp_remain += mp;
        if (hp_remain > this.hp)
        {
            hp_remain = this.hp;
        }
        if (mp_remain > this.mp)
        {
            mp_remain = this.mp;
        }
        HeadStatus.instance.UpdateShow();  //更新血蓝信息
    }

    public void GetExp(int exp)  //获得经验
    {
        this.exp += exp;
        int total_exp = 100 + level * 30;  //升级所需经验
        while (this.exp >= total_exp)  //用while可以防止一次多次升级
        {
            level++;
            hp += 10;
            mp += 10;
            hp_remain = hp;
            mp_remain = mp;
            remain_point += 5;
            HeadStatus.instance.UpdateShow();  //更新头像显示

            attack += 10;
            def += 10;
            speed += 1;
            Status.instance.StatusShow();  //更新面板显示

            this.exp -= total_exp;
            total_exp = 100 + level * 30;

            Instantiate(prefab, transform.position, Quaternion.identity);  //升级特效
        }
        ExpBar.instance.SetValue(this.exp / total_exp);  //更新经验条
    }

    public bool TakeMp(int count)  //取得mp
    {
        if (mp_remain >= count)
        {
            mp_remain -= count;
            HeadStatus.instance.UpdateShow();
            return true;
        }
        else
        {
            return false;
        }
    }
}
