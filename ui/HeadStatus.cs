using UnityEngine;
using System.Collections;

public class HeadStatus : MonoBehaviour
{
    public static HeadStatus instance;

    private PlayerStatus ps;

    private UILabel name;
    private UISlider hpBar;
    private UISlider mpBar;
    private UILabel hpLabel;
    private UILabel mpLabel;

    void Awake()
    {
        instance = this;

        name = transform.Find("Name").GetComponent<UILabel>();
        hpBar = transform.Find("HP").GetComponent<UISlider>();
        mpBar = transform.Find("MP").GetComponent<UISlider>();
        hpLabel = transform.Find("HP/Thumb/Label").GetComponent<UILabel>();
        mpLabel = transform.Find("MP/Thumb/Label").GetComponent<UILabel>();
    }

    // Use this for initialization
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        UpdateShow();  //一开始就需要更新信息
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))  //测试用，满血满蓝
        {
            ps.hp_remain = ps.hp;
            ps.mp_remain = ps.mp;
            UpdateShow();
        }
    }

    public void UpdateShow()
    {
        name.text = "Lv." + ps.level + " " + ps.name;
        hpBar.value = ps.hp_remain * 1.0f / ps.hp;
        mpBar.value = ps.mp_remain * 1.0f / ps.mp;

        hpLabel.text = ps.hp_remain + " / " + ps.hp;
        mpLabel.text = ps.mp_remain + " / " + ps.mp;
    }
}
