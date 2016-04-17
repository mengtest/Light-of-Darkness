using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour
{

    public static Status instance;
    private PlayerStatus ps;

    private TweenPosition tween;
    private bool isShow = false;

    private UILabel attackLabel;
    private UILabel defLabel;
    private UILabel speedLabel;
    private UILabel pointRemainLabel;
    private UILabel summaryLabel;

    private GameObject attackButtonGo;
    private GameObject defButtonGo;
    private GameObject speedButtonGo;

    void Awake()
    {
        instance = this;
        tween = GetComponent<TweenPosition>();

        attackLabel = transform.Find("attack").GetComponent<UILabel>();
        defLabel = transform.Find("def").GetComponent<UILabel>();
        speedLabel = transform.Find("speed").GetComponent<UILabel>();
        pointRemainLabel = transform.Find("point_remain").GetComponent<UILabel>();
        summaryLabel = transform.Find("summary").GetComponent<UILabel>();

        attackButtonGo = transform.Find("attack_plus").gameObject;
        defButtonGo = transform.Find("def_plus").gameObject;
        speedButtonGo = transform.Find("speed_plus").gameObject;

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
            StatusShow();
            tween.PlayForward();
            isShow = true;
        }
        else
        {
            tween.PlayReverse();
            isShow = false;
        }
    }

    public void StatusShow()  //根据PlayerStatus更新属性
    {
        attackLabel.text = ps.attack + " + " + ps.attack_plus;
        defLabel.text = ps.def + " + " + ps.def_plus;
        speedLabel.text = ps.speed + " + " + ps.speed_plus;

        pointRemainLabel.text = ps.remain_point + "";

        summaryLabel.text = "伤害 = " + (ps.attack + ps.attack_plus) + "  " +
            "防御 = " + (ps.def + ps.def_plus) + "  " +
            "速度 = " + (ps.speed + ps.speed_plus);

        if (ps.remain_point > 0)
        {
            attackButtonGo.SetActive(true);
            defButtonGo.SetActive(true);
            speedButtonGo.SetActive(true);
        }
        else
        {
            attackButtonGo.SetActive(false);
            defButtonGo.SetActive(false);
            speedButtonGo.SetActive(false);
        }
    }

    public void OnAttackPlusClick()
    {
        bool success = ps.GetPoint();
        if (success)
        {
            ps.attack_plus++;
            StatusShow();
        }
    }

    public void OnDefPlusClick()
    {
        bool success = ps.GetPoint();
        if (success)
        {
            ps.def_plus++;
            StatusShow();
        }
    }

    public void OnSpeedPlusClick()
    {
        bool success = ps.GetPoint();
        if (success)
        {
            ps.speed_plus++;
            StatusShow();
        }
    }
}
