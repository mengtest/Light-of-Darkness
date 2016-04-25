using UnityEngine;
using System.Collections;

public enum ShortCutType
{
    Skill,
    Drug,
    None
}

public class ShortCutGrid : MonoBehaviour
{
    public KeyCode keyCode;

    private ShortCutType type = ShortCutType.None;
    private UISprite icon;
    public int id = 0;  //如果id为0，表示此处为空
    private SkillsInfo skillInfo;
    private ObjectsInfo objectInfo;

    private PlayerStatus ps;
    private PlayerAttack pa;

    void Awake()
    {
        icon = transform.Find("icon").GetComponent<UISprite>();
    }

    // Use this for initialization
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        pa = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode) && pa.state != PlayerState.Death)  //按下快捷键，并且角色没有死亡，则可以使用药品或者技能
        {
            if (type == ShortCutType.Drug)  //使用药品
            {
                OnDrugUse();
            }
            else if (type == ShortCutType.Skill)  //使用技能
            {
                //1.得到技能所需的mp
                bool success = ps.TakeMp(skillInfo.mp);
                if (success)
                {
                    //mp足够，使用技能
                    pa.UseSkill(skillInfo);
                }
                else
                {
                    //mp不足，不做处理
                }
            }
        }
    }

    public void SetSkill(int id)
    {
        this.id = id;
        skillInfo = SkillsInfo.instance.GetSkillInfoById(id);
        icon.gameObject.SetActive(true);
        icon.spriteName = skillInfo.icon_name;
        type = ShortCutType.Skill;
    }

    public void SetInventory(int id)
    {
        this.id = id;
        objectInfo = ObjectsInfo.instance.GetObjectsInfoById(id);
        if (objectInfo.type == ObjectsType.Drug)
        {
            icon.gameObject.SetActive(true);
            icon.spriteName = objectInfo.icon_name;
            type = ShortCutType.Drug;
        }
    }

    public void OnDrugUse()
    {
        bool success = Inventory.instance.MinusId(id);
        if (success)
        {
            ps.GetDrug(objectInfo.hp, objectInfo.mp);
        }
        else
        {
            type = ShortCutType.None;
            icon.gameObject.SetActive(false);
            id = 0;
            objectInfo = null;
        }
    }
}
