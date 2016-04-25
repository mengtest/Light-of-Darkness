using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour
{
    private Camera minimapCamera;

    // Use this for initialization
    void Start()
    {
        minimapCamera = GameObject.FindGameObjectWithTag(Tags.minimap).GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnRoomInClick()  //放大
    {
        if (minimapCamera.orthographicSize <= 1)
        {
            minimapCamera.orthographicSize = 1;
        }
        else
        {
            minimapCamera.orthographicSize--;  //减小距离即放大地图
        }
    }

    public void OnRoomOutClick()  //缩小
    {
        if (minimapCamera.orthographicSize >= 10)
        {
            minimapCamera.orthographicSize = 10;
        }
        else
        {
            minimapCamera.orthographicSize++;  //增大距离即缩小地图
        }
    }
}
