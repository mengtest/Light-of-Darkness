using UnityEngine;
using System.Collections;

public enum HeroType
{
    Swordman,
    Magician,
    Common
}

public class PlayerStatus : MonoBehaviour {

    public HeroType heroType;

    public int level = 1;  //当前等级  升级经验：100+level*30
    public string name = "默认名字";
    public int hp = 100;  //hp最大值
    public int mp = 100;  //mp最大值
    public float hp_remain = 100;  //当前hp
    public float mp_remain = 100;  //当前mp

    public int attack = 20;
    public int attack_plus = 0;
    public int def = 20;
    public int def_plus = 0;
    public int speed = 5;
    public int speed_plus = 0;

    public int remain_point;  //剩余点数 

    public float exp = 0;  //当前经验

	// Use this for initialization
	void Start () {
        GetExp(0);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public bool GetPoint(int point = 1)
    {
        if (remain_point >= point)
        {
            remain_point -= point;
            return true;
        }
        return false;
    }

    public void GetDrug(int hp,int mp)
    {
        Debug.Log(hp + "  " + this.hp);
        Debug.Log(mp + "  " + this.mp);
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
    }

    public void GetExp(int exp)  //获得经验
    {
        this.exp += exp;
        int total_exp = 100 + level * 30;  //升级所需经验
        while(this.exp >= total_exp)  //用while可以防止一次多次升级
        {
            level++;
            this.exp -= total_exp;
            total_exp = 100 + level * 30;
        }

        ExpBar.instance.SetValue(this.exp / total_exp);  //更新经验条
    }
}
