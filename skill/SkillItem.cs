using UnityEngine;
using System.Collections;

public class SkillItem : MonoBehaviour {

    public int id;
    private SkillsInfo info;
    private UISprite icon_name;
    private UILabel name;
    private UILabel applyType;
    private UILabel des;
    private UILabel mp;

    private GameObject icon_mask;  //技能不可用时的遮罩

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitProperty()
    {
        icon_name = transform.Find("icon_name").GetComponent<UISprite>();
        name = transform.Find("property/name_bg/name").GetComponent<UILabel>();
        applyType = transform.Find("property/applyType_bg/applyType").GetComponent<UILabel>();
        des = transform.Find("property/des_bg/des").GetComponent<UILabel>();
        mp = transform.Find("property/mp_bg/mp").GetComponent<UILabel>();

        icon_mask = transform.Find("icon_mask").gameObject;
        //icon_mask.SetActive(false);
    }

    //通过调用这个方法可以更新显示技能
    public void  SetId(int id)
    {
        InitProperty();
        this.id = id;
        info = SkillsInfo.instance.GetSkillInfoById(id);
        icon_name.spriteName = info.icon_name;
        name.text = info.name;
        switch (info.applyType)
        {
            case ApplyType.Passive:
                applyType.text = "增益";
                break;
            case ApplyType.Buff:
                applyType.text = "增强";
                break;
            case ApplyType.SingleTarget:
                applyType.text = "单个敌人";
                break;
            case ApplyType.MultipleTarget:
                applyType.text = "所有敌人";
                break;
        }
        des.text = info.des;
        mp.text = info.mp+" MP";
    }

    public void UpdateShow(int level)
    {
        if (info.applyLevel <= level) //技能可用
        {
            icon_mask.SetActive(false);
            icon_name.GetComponent<SkillItemIcon>().enabled = true; 
        }
        else
        {
            icon_mask.SetActive(true);  //技能不可用
            icon_name.GetComponent<SkillItemIcon>().enabled = false; 
        }
    }
}
