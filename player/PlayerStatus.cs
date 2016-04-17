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

    public int level = 1;  //人物等级
    public int hp = 100;
    public int mp = 100;
    public int coin = 200;  //金币数量

    public int attack = 20;
    public int attack_plus = 0;
    public int def = 20;
    public int def_plus = 0;
    public int speed = 20;
    public int speed_plus = 0;

    public int remain_point;  //剩余点数 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddCoin(int coin)
    {
        this.coin += coin;
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
}
