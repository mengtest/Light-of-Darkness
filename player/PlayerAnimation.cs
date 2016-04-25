using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMove move;
    private PlayerAttack attack;

    public string aniname_run;
    public string aniname_idle;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<PlayerMove>();
        attack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (attack.state == PlayerState.ControlWalk)
        {
            if (move.state == ControlWalk.Moving)
            {
                PlayAnimation(aniname_run);
            }
            else if (move.state == ControlWalk.Idle)
            {
                PlayAnimation(aniname_idle);
            }
        }
        else if (attack.state == PlayerState.NormalAttack)
        {
            if (attack.attack_state == AttackState.Moving)
            {
                PlayAnimation(aniname_run);
            }
        }
    }

    void PlayAnimation(string anim)
    {
        GetComponent<Animation>().CrossFade(anim);
    }
}
