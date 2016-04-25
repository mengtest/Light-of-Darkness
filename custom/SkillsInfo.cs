using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

public class SkillsInfo : MonoBehaviour
{
    public static SkillsInfo instance;

    public TextAsset skillsInfoList;

    private Dictionary<int, SkillsInfo> skillsInfoDict = new Dictionary<int, SkillsInfo>();

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
    //14 特效名称
    //15 动画名称
    //16 动画时间
    public int id;
    public string name;
    public string icon_name;
    public string des;
    public ApplyType applyType;
    public ApplyProperty applyProperty;
    public int applyValue;
    public int applyTime;
    public int mp;
    public int coldTime;
    public ApplyRole applyRole;
    public int applyLevel;
    public ReleaseType releaseType;
    public float distance;
    public string efx_name;
    public string ani_name;
    public float ani_time = 0;

    void Awake()
    {
        instance = this;
        InitSkillsInfoDict();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public SkillsInfo GetSkillInfoById(int id)
    {
        SkillsInfo info = null;
        skillsInfoDict.TryGetValue(id, out info);
        return info;
    }

    //初始化技能信息字典
    void InitSkillsInfoDict()
    {
        string text = skillsInfoList.text;
        string[] strArray = text.Split('\n');
        foreach (string str in strArray)
        {
            string[] proArray = str.Split(',');
            SkillsInfo info = new SkillsInfo();

            info.id = int.Parse(proArray[0]);
            info.name = proArray[1];
            info.icon_name = proArray[2];
            info.des = proArray[3];

            switch (proArray[4])
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
                case "MultipleTarget":
                    info.applyType = ApplyType.MultipleTarget;
                    break;
            }

            switch (proArray[5])
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

            info.applyValue = int.Parse(proArray[6]);
            info.applyTime = int.Parse(proArray[7]);
            info.mp = int.Parse(proArray[8]);
            info.coldTime = int.Parse(proArray[9]);

            switch (proArray[10])
            {
                case "Swordman":
                    info.applyRole = ApplyRole.Swordman;
                    break;
                case "Magician":
                    info.applyRole = ApplyRole.Magician;
                    break;
            }

            info.applyLevel = int.Parse(proArray[11]);

            switch (proArray[12])
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

            info.distance = float.Parse(proArray[13]);
            info.efx_name = proArray[14];
            info.ani_name = proArray[15];
            info.ani_time = float.Parse(proArray[16]);

            skillsInfoDict.Add(info.id, info);
        }
    }
}
