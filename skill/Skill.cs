using UnityEngine;
using System.Collections;
using System;

public class Skill : MonoBehaviour {

    public static Skill instance;
    private TweenPosition tween;
    private bool isShow = false;
    private PlayerStatus ps;

    public int[] magicianSkillIdList;
    public int[] swordmanSkillIdList;

    public UIGrid grid;
    public GameObject skillItem;

    void Awake()
    {
        instance = this;
        tween = GetComponent<TweenPosition>();
    }

	// Use this for initialization
	void Start () {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        int[] idList = null;
        switch (ps.heroType)
        {
            case HeroType.Magician:
                idList = magicianSkillIdList;
                break;
            case HeroType.Swordman:
                idList = swordmanSkillIdList;
                break;
        }

        foreach(int id in idList)
        {
            GameObject itemGo = NGUITools.AddChild(grid.gameObject, skillItem);
            grid.AddChild(itemGo.transform);
            itemGo.GetComponent<SkillItem>().SetId(id);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TransformState()
    {
        if (isShow == false)
        {
            tween.PlayForward();
            isShow = true;
            UpdateShow();
        }
        else
        {
            tween.PlayReverse();
            isShow = false;
        }
    }

    void UpdateShow()
    {
        SkillItem[] items = GetComponentsInChildren<SkillItem>();
        foreach(SkillItem item in items)
        {
            item.UpdateShow(ps.level);
        }
    }
}
