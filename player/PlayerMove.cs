using UnityEngine;
using System.Collections;

public enum ControlWalk
{
    Moving,
    Idle
}

public class PlayerMove : MonoBehaviour
{
    public ControlWalk state = ControlWalk.Idle;
    private PlayDir dir;
    private CharacterController controller;
    private PlayerAttack attack;

    public bool isMoving = false;
    public float speed = 4f;

    // Use this for initialization
    void Start()
    {
        dir = GetComponent<PlayDir>();
        controller = GetComponent<CharacterController>();
        attack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack.state == PlayerState.ControlWalk)
        {
            float distance = Vector3.Distance(transform.position, dir.targetPosition);
            if (distance > 0.1f)
            {
                isMoving = true;
                state = ControlWalk.Moving;
                controller.SimpleMove(transform.forward * speed);
            }
            else
            {
                isMoving = false;
                state = ControlWalk.Idle;
            }
        }
    }

    public void SimpleMove(Vector3 targetPos)
    {
        transform.LookAt(targetPos);
        controller.SimpleMove(transform.forward * speed);
    }
}
