using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour
{
    public static Status instance;

    private PlayerStatus ps;
    private PlayerMove move;  //用来改变角色移动速度

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

        
    }

    // Use this for initialization
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        move = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerMove>();
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

        float speed = (ps.speed+ps.speed_plus)*0.2f;  //乘以0.2，防止速度太快
        if(speed<4)
        {
            speed = 4;
        }
        if (speed>10)
        {
            speed = 10;
        }
        move.speed = speed;  //控制好速度之后更新速度

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
        bool success = ps.CostPoint();
        if (success)
        {
            ps.attack_plus++;
            StatusShow();
        }
    }

    public void OnDefPlusClick()
    {
        bool success = ps.CostPoint();
        if (success)
        {
            ps.def_plus++;
            StatusShow();
        }
    }

    public void OnSpeedPlusClick()
    {
        bool success = ps.CostPoint();
        if (success)
        {
            ps.speed_plus++;
            StatusShow();
        }
    }
}
