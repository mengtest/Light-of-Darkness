using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour
{
    public static Equipment instance;

    private TweenPosition tween;
    private bool isShow = false;

    public GameObject equipmentItem;
    private GameObject headgear;
    private GameObject armor;
    private GameObject rightHand;
    private GameObject leftHand;
    private GameObject shoe;
    private GameObject accessory;

    private PlayerStatus ps;
    public int attack = 0;
    public int def = 0;
    public int speed = 0;

    void Awake()
    {
        instance = this;
        tween = GetComponent<TweenPosition>();

        headgear = transform.Find("Headgear").gameObject;
        armor = transform.Find("Armor").gameObject;
        rightHand = transform.Find("Right-Hand").gameObject;
        leftHand = transform.Find("Left-Hand").gameObject;
        shoe = transform.Find("Shoe").gameObject;
        accessory = transform.Find("Accessory").gameObject;

        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TransformState()
    {
        if (isShow == false)
        {
            tween.PlayForward();
            isShow = true;
        }
        else
        {
            tween.PlayReverse();
            isShow = false;
        }
    }


    public bool Dress(int id)  //是否穿戴成功
    {
        ObjectsInfo info = ObjectsInfo.instance.GetObjectsInfoById(id);

        if (info.type != ObjectsType.Equip)  //穿戴的不是装备
        {
            return false;
        }
        if (ps.heroType == HeroType.Magician)  //角色魔法师，穿戴剑士装备
        {
            if (info.applicationType == ApplicationType.Swordman)
            {
                return false;
            }
        }
        if (ps.heroType == HeroType.Swordman)  //角色剑士，穿戴魔法师装备
        {
            if (info.applicationType == ApplicationType.Magician)
            {
                return false;
            }
        }

        GameObject parent = null;
        switch (info.dressType)
        {
            case DressType.Headgear:
                parent = headgear;
                break;
            case DressType.Armor:
                parent = armor;
                break;
            case DressType.RightHand:
                parent = rightHand;
                break;
            case DressType.LeftHand:
                parent = leftHand;
                break;
            case DressType.Shoe:
                parent = shoe;
                break;
            case DressType.Accessory:
                parent = accessory;
                break;
        }

        EquipmentItem item = parent.GetComponentInChildren<EquipmentItem>();

        if (item != null)  //已经穿戴同类型的装备
        {
            Inventory.instance.GetId(item.id);  //把已经穿戴的装备卸下，放回物品栏
            MinusProperty(item.id);  //减去之前装备的属性值

            item.SetInfo(info);  //换上新的装备，设置信息
            PlusProperty(id);  //加上现在装备的属性值
        }
        else  //没有穿戴同类型的装备
        {
            GameObject itemGo = NGUITools.AddChild(parent, equipmentItem);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.GetComponent<EquipmentItem>().SetInfo(info);
            PlusProperty(id);  //增加装备属性值
        }

        return true;
    }

    public void MinusProperty(int id)  //脱下装备减少属性
    {
        ObjectsInfo equipInfo = ObjectsInfo.instance.GetObjectsInfoById(id);
        ps.attack_plus -= equipInfo.attack;
        ps.def_plus -= equipInfo.def;
        ps.speed_plus -= equipInfo.speed;
        Status.instance.StatusShow();
    }

    public void PlusProperty(int id)  //穿上装备增加属性
    {
        ObjectsInfo equipInfo = ObjectsInfo.instance.GetObjectsInfoById(id);
        ps.attack_plus += equipInfo.attack;
        ps.def_plus += equipInfo.def;
        ps.speed_plus += equipInfo.speed;
        Status.instance.StatusShow();
    }
}
