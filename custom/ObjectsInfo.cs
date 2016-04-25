using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ObjectsType
{
    Drug,
    Equip,
    Mat
}

public enum DressType
{
    Headgear,
    Armor,
    RightHand,
    LeftHand,
    Shoe,
    Accessory
}

public enum ApplicationType
{
    Swordman,  //剑士
    Magician,  //魔法师
    Common  //通用
}

public class ObjectsInfo : MonoBehaviour
{
    public static ObjectsInfo instance;

    public TextAsset objectsInfoList;

    private Dictionary<int, ObjectsInfo> objectInfoDict = new Dictionary<int, ObjectsInfo>();

    //0 id 
    //1 名称
    //2 icon名称

    //3 类型(药品Drag) 
    //4 加血量值
    //5 加魔法值
    //6 出售价
    //7 购买价
    public int id;
    public string name;
    public string icon_name;  //图集中的名字
    public ObjectsType type;
    public int hp;
    public int mp;
    public int price_sell;
    public int price_buy;

    //3 类型(装备Equip)
    //4 攻击
    //5 防御
    //6 速度
    //7 穿戴类型
    //8 适用类型
    //9 出售价
    //10 购买价
    public int attack;
    public int def;
    public int speed;
    public DressType dressType;
    public ApplicationType applicationType;

    void Awake()
    {
        instance = this;
        ReadInfo();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public ObjectsInfo GetObjectsInfoById(int id)
    {
        ObjectsInfo info = null;
        objectInfoDict.TryGetValue(id, out info);
        return info;
    }

    void ReadInfo()
    {
        string text = objectsInfoList.text;
        string[] strArray = text.Split('\n');

        foreach (string str in strArray)
        {
            ObjectsInfo info = new ObjectsInfo();
            string[] proArray = str.Split(',');

            info.id = int.Parse(proArray[0]);
            info.name = proArray[1];
            info.icon_name = proArray[2];

            string str_type = proArray[3];
            ObjectsType type = ObjectsType.Drug;
            switch (str_type)
            {
                case "Drug":
                    type = ObjectsType.Drug;
                    break;
                case "Equip":
                    type = ObjectsType.Equip;
                    break;
                case "Mat":
                    type = ObjectsType.Mat;
                    break;
            }

            if (type == ObjectsType.Drug)
            {
                info.hp = int.Parse(proArray[4]);
                info.mp = int.Parse(proArray[5]);
                info.price_sell = int.Parse(proArray[6]);
                info.price_buy = int.Parse(proArray[7]);
                info.type = type;
            }
            else if (type == ObjectsType.Equip)
            {
                info.attack = int.Parse(proArray[4]);
                info.def = int.Parse(proArray[5]);
                info.speed = int.Parse(proArray[6]);
                info.price_sell = int.Parse(proArray[9]);
                info.price_buy = int.Parse(proArray[10]);
                info.type = type;

                string str_dressType = proArray[7];  //穿戴类型
                switch (str_dressType)
                {
                    case "Headgear":
                        info.dressType = DressType.Headgear;
                        break;
                    case "Armor":
                        info.dressType = DressType.Armor;
                        break;
                    case "RightHand":
                        info.dressType = DressType.RightHand;
                        break;
                    case "LeftHand":
                        info.dressType = DressType.LeftHand;
                        break;
                    case "Shoe":
                        info.dressType = DressType.Shoe;
                        break;
                    case "Accessory":
                        info.dressType = DressType.Accessory;
                        break;
                }

                string str_appType = proArray[8];  //适用类型
                switch (str_appType)
                {
                    case "Swordman":
                        info.applicationType = ApplicationType.Swordman;
                        break;
                    case "Magician":
                        info.applicationType = ApplicationType.Magician;
                        break;
                    case "Common":
                        info.applicationType = ApplicationType.Common;
                        break;
                }
            }

            objectInfoDict.Add(info.id, info); //添加到字典中，id为key，可以很方便地根据id查找到物品
        }
    }
}
