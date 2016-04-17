using UnityEngine;
using System.Collections;

public class ItemDescribe : MonoBehaviour
{

    public static ItemDescribe instance;
    private UILabel label;
    private float timer = 0;

    void Awake()
    {
        instance = this;
        label = GetComponentInChildren<UILabel>();
        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void Show(int id)
    {
        timer = 0.1f;
        gameObject.SetActive(true);
        transform.position = UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
        ObjectsInfo info = ObjectsInfo.instance.GetObjectsInfoById(id);
        string des = "";
        switch (info.type)
        {
            case ObjectsType.Drug:
                des = GetDrugInfo(info);
                break;
            case ObjectsType.Equip:
                des = GetEquipInfo(info);
                break;
        }
        label.text = des;
    }

    string GetDrugInfo(ObjectsInfo info)
    {
        string str = "\n";
        str += "  名称： " + info.name + "\n\n";
        str += "  血量值： " + info.hp + "\n\n";
        str += "  魔法值： " + info.mp + "\n\n";
        str += "  出售价： " + info.price_sell + "\n\n";
        str += "  购买价： " + info.price_buy + "\n";
        return str;
    }

    string GetEquipInfo(ObjectsInfo info)
    {
        string str = "\n";
        str += "  名称： " + info.name + "\n\n";

        switch (info.dressType)
        {
            case DressType.Headgear:
                str += "  穿戴类型： 头盔\n\n";
                break;
            case DressType.Armor:
                str += "  穿戴类型： 盔甲\n\n";
                break;
            case DressType.RightHand:
                str += "  穿戴类型： 左手\n\n";
                break;
            case DressType.LeftHand:
                str += "  穿戴类型： 右手\n\n";
                break;
            case DressType.Shoe:
                str += "  穿戴类型： 鞋子\n\n";
                break;
            case DressType.Accessory:
                str += "  穿戴类型： 首饰\n\n";
                break;
        }

        switch (info.applicationType)
        {
            case ApplicationType.Swordman:
                str += "  适用类型： 剑士\n\n";
                break;
            case ApplicationType.Magician:
                str += "  适用类型： 魔法师\n\n";
                break;
            case ApplicationType.Common:
                str += "  适用类型： 通用\n\n";
                break;
        }

        str += "  攻击值： " + info.attack + "\n\n";
        str += "  防御值： " + info.def + "\n\n";
        str += "  速度值： " + info.speed + "\n\n";

        str += "  出售价： " + info.price_sell + "\n\n";
        str += "  购买价： " + info.price_buy + "\n";
        return str;
    }
}
