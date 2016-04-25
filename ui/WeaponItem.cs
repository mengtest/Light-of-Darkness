using UnityEngine;
using System.Collections;

public class WeaponItem : MonoBehaviour
{
    private int id;
    private ObjectsInfo info;

    private UISprite icon_sprite;
    private UILabel name_label;
    private UILabel effect_label;
    private UILabel price_label;

    void Awake()
    {
        icon_sprite = transform.Find("icon").GetComponent<UISprite>();
        name_label = transform.Find("name").GetComponent<UILabel>();
        effect_label = transform.Find("effect").GetComponent<UILabel>();
        price_label = transform.Find("price").GetComponent<UILabel>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    //通过调用方法实现装备购买列表
    public void SetId(int id)
    {
        this.id = id;
        info = ObjectsInfo.instance.GetObjectsInfoById(id);

        icon_sprite.spriteName = info.icon_name;
        name_label.text = info.name;

        if (info.attack > 0)
        {
            effect_label.text = "伤害 +" + info.attack;
        }
        else if (info.def > 0)
        {
            effect_label.text = "防御 +" + info.def;
        }
        else if (info.speed > 0)
        {
            effect_label.text = "速度 +" + info.speed;
        }

        price_label.text = info.price_buy.ToString();
    }

    public void OnBuyButtonClick()
    {
        ShopWeapon.instance.OnBuyClick(id);
    }
}
