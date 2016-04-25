using UnityEngine;
using System.Collections;

public class PlayDir : MonoBehaviour
{
    private PlayerMove playerMove;
    private PlayerAttack attack;

    public GameObject effect_click_prefab;
    public Vector3 targetPosition = Vector3.zero;
    private bool isMoving = false;  //表示鼠标是否按下

    // Use this for initialization
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        attack = GetComponent<PlayerAttack>();
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (attack.state == PlayerState.Death)
        {
            return;  //死亡就不可以再继续走路
        }
        else {
            if (UICamera.hoveredObject)  //底下只有UI Root的时候可以走动
            {
                if (!attack.isLockingTarget && Input.GetMouseButtonDown(0) && UICamera.hoveredObject.name == "UI Root")
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
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //得到移动的位置，让主角朝向点击位置
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
        }
    }

    //实例化点击效果
    void ShowClickEffect(Vector3 hitPoint)
    {
        hitPoint = new Vector3(hitPoint.x, hitPoint.y + 0.1f, hitPoint.z);
        Instantiate(effect_click_prefab, hitPoint, Quaternion.identity);
    }

    //点击鼠标改变主角朝向
    void LookAtTarget(Vector3 hitPoint)
    {
        targetPosition = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
        transform.LookAt(targetPosition);
    }
}
