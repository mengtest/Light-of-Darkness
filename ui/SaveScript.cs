using UnityEngine;
using System.Collections;

public class SaveScript : MonoBehaviour
{
    public static SaveScript instance;

    private TweenPosition tween;
    private PlayerStatus ps;
    private PlayDir dir;
    private Transform player;
    public Transform characterCreation;

    // Use this for initialization
    void Start()
    {
        instance = this;
        tween = GetComponent<TweenPosition>();
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        dir = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayDir>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveWindow()
    {
        tween.PlayForward();
    }

    public void YesButton()
    {
        tween.PlayReverse();
        BackTown();
    }

    public void NoButton()
    {
        tween.PlayReverse();
    }


    private void BackTown()
    {
        ps.hp_remain = ps.hp;
        HeadStatus.instance.UpdateShow();
        PlayerAttack.instance.state = PlayerState.ControlWalk;
        PlayerAttack.instance.target_normalAttack = null;
        player.position = characterCreation.position;
        player.transform.rotation = Quaternion.Euler(0, 180, 0);
        dir.targetPosition = player.position;
    }
}
