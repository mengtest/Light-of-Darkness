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

    private ShortCutType type=ShortCutType.None;
    private UISprite icon;
    private int id;
    private SkillsInfo info;

    void Awake()
    {
        icon = transform.Find("icon").GetComponent<UISprite>();
        //icon.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SetSkill(int id)
    {
        this.id = id;
        info = SkillsInfo.instance.GetSkillInfoById(id);
        icon.gameObject.SetActive(true);
        icon.spriteName = info.icon_name;
        type = ShortCutType.Skill;
    }

    public void SetInventory(int id)
    {

    }
}
