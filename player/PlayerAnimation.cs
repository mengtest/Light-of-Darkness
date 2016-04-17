using UnityEngine;
using System.Collections;
using System;

public class PlayerAnimation : MonoBehaviour {

    private PlayerMove move;

	// Use this for initialization
	void Start () {
        move = GetComponent<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
        if (move.state == PlayerState.Moving)
        {
            PlayAnimation("Run");
        }else if (move.state == PlayerState.Idle)
        {
            PlayAnimation("Idle");
        }
    }

    void PlayAnimation(string anim)
    {
        GetComponent<Animation>().CrossFade(anim);
    }
}
