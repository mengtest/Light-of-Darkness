﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//适用角色
public enum ApplyRole
{
    Swordman,
    Magician
}

//作用类型
public enum ApplyType
{
    Passive,
    Buff,
    SingleTarget,
    MultipleTarget,
}

//作用属性
public enum ApplyProperty
{
    Attack,
    Def,
    Speed,
    AttackSpeed,
    HP,
    MP
}

//释放类型
public enum ReleaseType
{
    Self,  //当前位置释放
    Enemy,  //指定敌人释放
    Position  //指定位置释放
}

public class SkillsInfo : MonoBehaviour {

    //0 id
    //1 名称
    //2 icon名称
    //3 Des描述
    //4 作用类型
    //5 作用属性
    //6 作用值
    //7 作用时间
    //8 消耗魔法值
    //9 冷却时间
    //10 适用角色
    //11 适用等级
    //12 释放类型
    //13 释放距离
    public int id;
    public string name;
    public string icon_name;
    public string des;
    public ApplyType applyType;
    public ApplyProperty applyProperty;
    public int applyValue;
    public int applyTime;
    public int hp;
    public int mp;
    public int coldTime;
    public ApplyRole applyRole;
    public int applyLevel;
    public ReleaseType releaseType;
    public float distance;

    public string efx_name;
    public string anitName;
    public float anitTime = 0;

    public static SkillsInfo instance;
    public TextAsset skillsInfoList;

    private Dictionary<int, SkillsInfo> skillsInfoDict = new Dictionary<int, SkillsInfo>();

    void Awake()
    {
        instance = this;
        InitSkillsInfoDict();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //初始化技能信息字典
    void InitSkillsInfoDict()
    {
        string text = skillsInfoList.text;
        string[] skillinfoArray = text.Split('\n');
        foreach (string skillinfoStr in skillinfoArray)
        {
            string[] pa = skillinfoStr.Split(',');
            SkillsInfo info = new SkillsInfo();
            info.id = int.Parse(pa[0]);
            info.name = pa[1];
            info.icon_name = pa[2];
            info.des = pa[3];


            string str_applytype = pa[4];
            switch (str_applytype)
            {
                case "Passive":
                    info.applyType = ApplyType.Passive;
                    break;
                case "Buff":
                    info.applyType = ApplyType.Buff;
                    break;
                case "SingleTarget":
                    info.applyType = ApplyType.SingleTarget;
                    break;
                case "MultiTarget":
                    info.applyType = ApplyType.MultipleTarget;
                    break;
            }
            string str_applypro = pa[5];
            switch (str_applypro)
            {
                case "Attack":
                    info.applyProperty = ApplyProperty.Attack;
                    break;
                case "Def":
                    info.applyProperty = ApplyProperty.Def;
                    break;
                case "Speed":
                    info.applyProperty = ApplyProperty.Speed;
                    break;
                case "AttackSpeed":
                    info.applyProperty = ApplyProperty.AttackSpeed;
                    break;
                case "HP":
                    info.applyProperty = ApplyProperty.HP;
                    break;
                case "MP":
                    info.applyProperty = ApplyProperty.MP;
                    break;
            }
            info.applyValue = int.Parse(pa[6]);
            info.applyTime = int.Parse(pa[7]);
            info.mp = int.Parse(pa[8]);
            info.coldTime = int.Parse(pa[9]);
            switch (pa[10])
            {
                case "Swordman":
                    info.applyRole = ApplyRole.Swordman;
                    break;
                case "Magician":
                    info.applyRole = ApplyRole.Magician;
                    break;
            }
            info.applyLevel = int.Parse(pa[11]);
            switch (pa[12])
            {
                case "Self":
                    info.releaseType = ReleaseType.Self;
                    break;
                case "Enemy":
                    info.releaseType = ReleaseType.Enemy;
                    break;
                case "Position":
                    info.releaseType = ReleaseType.Position;
                    break;
            }
            info.distance = float.Parse(pa[13]);
            //info.efx_name = pa[14];
            //info.anitName = pa[15];
            //info.anitTime = float.Parse(pa[16]);
            Debug.Log(info.id + info.name);
            skillsInfoDict.Add(info.id, info);
        }
    }

    public SkillsInfo GetSkillInfoById(int id)
    {
        SkillsInfo info = null;
        skillsInfoDict.TryGetValue(id, out info);
        return info;
    }
}