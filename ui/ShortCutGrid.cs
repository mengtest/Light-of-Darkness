using UnityEngine;
using System.Collections;

public enum ShortCutType
{
    Skill,
    Drug,
    None
}

public class ShortCutGrid : MonoBehaviour {

    public KeyCode keyCode;

    private ShortCutType type;
    private UISprite icon;
    private int id;
    private SkillsInfo skillInfo;
    private ObjectsInfo objectInfo;

    private PlayerStatus ps;

    void Awake()
    {
        icon = transform.Find("icon").GetComponent<UISprite>();
    }

	// Use this for initialization
	void Start () {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keyCode))
        {
            if(type == ShortCutType.Drug)
            {
                OnDrugUse();
                HeadStatus.instance.UpdateShow();
            }
            else if(type == ShortCutType.Skill)
            {

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
