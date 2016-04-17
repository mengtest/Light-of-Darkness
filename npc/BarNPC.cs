using UnityEngine;
using System.Collections;

public class BarNPC : NPC
{

    private PlayerStatus status;

    public TweenPosition questTween;
    public UILabel taskLabel;
    public GameObject acceptBtn;
    public GameObject cancelBtn;
    public GameObject okBtn;

    public bool isInTask = false;  //表示是否正在任务
    public int killCount = 0;  //表示任务进度，杀死几只小野狼

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        status = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    void OnMouseOver()  //当鼠标位于collider之上的时候，会在每一帧自动调用这个方法
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().Play();
            ShowQuest();
            if (isInTask)
            {
                showTaskProgress();
            }
            else
            {
                showTaskDescribe();
            }
        }
    }

    void showTaskDescribe()
    {
        taskLabel.text = "任务：\n杀死10只小野狼\n\n奖励：\n1000金币";
        acceptBtn.SetActive(true);
        cancelBtn.SetActive(true);
        okBtn.SetActive(false);
    }

    void showTaskProgress()
    {
        taskLabel.text = "任务：\n已杀死了" + killCount + "/10只狼\n\n奖励：\n1000金币";
        acceptBtn.SetActive(false);
        cancelBtn.SetActive(false);
        okBtn.SetActive(true);
    }

    //任务系统
    public void OnCloseButtonClick()
    {
        HideQuest();
    }

    public void OnAcceptButtonClick()
    {
        showTaskProgress();
        isInTask = true;
    }

    public void OnCancelButtonClick()
    {
        HideQuest();
    }

    public void OnOKClick()
    {
        if (killCount >= 10)
        {
            //完成任务
            status.AddCoin(1000);
            killCount = 0;
            isInTask = false;
            HideQuest();
        }
        else
        {
            HideQuest();
        }
    }

    void ShowQuest()
    {
        questTween.gameObject.SetActive(true);
        questTween.PlayForward();
    }

    void HideQuest()
    {
        questTween.PlayReverse();
    }
}
