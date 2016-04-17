using UnityEngine;
using System.Collections;

public enum PlayerState
{
    Moving,
    Idle
}

public class PlayerMove : MonoBehaviour
{

    public bool isMoving = false;

    public float speed = 3.0f;
    public PlayerState state = PlayerState.Idle;
    private PlayDir dir;
    private CharacterController controller;

    // Use this for initialization
    void Start()
    {
        dir = GetComponent<PlayDir>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, dir.targetPosition);
        if (distance > 0.1f)
        {
            isMoving = true;
            state = PlayerState.Moving;
            controller.SimpleMove(transform.forward * speed);
        }
        else
        {
            isMoving = false;
            state = PlayerState.Idle;
        }
    }
}
