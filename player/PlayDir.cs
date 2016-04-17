using UnityEngine;
using System.Collections;

public class PlayDir : MonoBehaviour
{

    public GameObject effect_click_prefab;
    public Vector3 targetPosition = Vector3.zero;
    private bool isMoving = false;  //表示鼠标是否按下

    private PlayerMove playerMove;

    // Use this for initialization
    void Start()
    {
        targetPosition = transform.position;
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(UICamera.hoveredObject.name);
        //if (Input.GetMouseButtonDown(0) && UICamera.hoveredObject == null)
        if (Input.GetMouseButtonDown(0) && UICamera.hoveredObject.name == "UI Root")
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && hitInfo.collider.tag == Tags.ground)
            {
                isMoving = true;
                ShowClickEffect(hitInfo.point);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

        if (isMoving)
        {
            //得到移动的位置，让主角朝向点击位置
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && hitInfo.collider.tag == Tags.ground)
            {
                LookAtTarget(hitInfo.point);
            }
        }
        else if (playerMove.isMoving)  //防止地形不平坦导致角色无法停止
        {
            LookAtTarget(targetPosition);
        }
    }

    //实例化点击效果
    void ShowClickEffect(Vector3 hitPoint)
    {
        hitPoint = new Vector3(hitPoint.x, hitPoint.y + 0.1f, hitPoint.z);
        GameObject.Instantiate(effect_click_prefab, hitPoint, Quaternion.identity);
    }

    //点击鼠标改变主角朝向
    void LookAtTarget(Vector3 hitPoint)
    {
        targetPosition = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
        transform.LookAt(targetPosition);
    }
}
