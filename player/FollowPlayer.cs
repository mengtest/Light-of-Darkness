using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    public float scrollSpeed = 5.0f;
    public float rotateSpeed = 1.0f;

    private Vector3 offsetPosition;  //位置偏移
    private float distance = 0;
    private float x = 0;
    private bool isRotating = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        transform.LookAt(player.position);
        offsetPosition = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offsetPosition + player.position;
        ScrollView();  //控制视野的范围
        RotateView();  //控制视野的旋转
    }

    void ScrollView()
    {
        distance = offsetPosition.magnitude;
        //向前滑动返回正值（拉远视野）
        //向后滑动返回负值（拉近视野）
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance = Mathf.Clamp(distance, 2.0f, 18.0f);
        offsetPosition = offsetPosition.normalized * distance;
    }

    void RotateView()
    {
        Input.GetAxis("Mouse X");  //得到鼠标在水平方向的滑动
        if (Input.GetMouseButtonDown(1) && UICamera.hoveredObject.name == "UI Root")
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            transform.RotateAround(player.position, player.up, Input.GetAxis("Mouse X") * rotateSpeed);
            Vector3 originalPosition = transform.position;
            Quaternion originalRotation = transform.rotation;

            transform.RotateAround(player.position, transform.right, -Input.GetAxis("Mouse Y") * rotateSpeed);

            x = transform.eulerAngles.x;
            if (x < 10.0f || x > 80.0f)  //当超出范围时将属性改回原来
            {
                transform.position = originalPosition;
                transform.rotation = originalRotation;
            }

            offsetPosition = transform.position - player.position;
        }


    }
}
