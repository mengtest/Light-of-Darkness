using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public GameObject inventoryItem;
    public List<InventoryItemGrid> itemGridList = new List<InventoryItemGrid>();
    public UILabel coinNumberLabel;
    public int coinNumber = 1000;

    private TweenPosition tween;
    private bool isShow = false;

    void Awake()
    {
        instance = this;
        tween = GetComponent<TweenPosition>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))  //测试药品
        {
            GetId(Random.Range(1001, 1004));
        }
        if (Input.GetKeyDown(KeyCode.X))  //测试装备
        {
            GetId(Random.Range(2001, 2023));
        }
    }

    public void TransformState()
    {
        if (isShow == false)
        {
            isShow = true;
            tween.PlayForward();
        }
        else
        {
            isShow = false;
            tween.PlayReverse();
        }
    }

    //拾取到物品，并添加到物品栏里面
    public void GetId(int id, int num = 1)
    {
        //第一步：查找在所有物品中是否存在该物品
        //第二步：如果存在，让numLabel+1
        //第三步：如果不存在，查找空的格子，然后创建新的Iventory-item放入里面
        InventoryItemGrid grid = null;

        foreach (InventoryItemGrid temp in itemGridList)
        {
            if (temp.id == id)
            {
                grid = temp;
                break;
            }
        }

        if (grid != null)  //存在的情况
        {
            grid.PlusNumber(num);
        }
        else  //不存在的情况
        {
            foreach (InventoryItemGrid temp in itemGridList)
            {
                if (temp.id == 0)
                {
                    grid = temp;
                    break;
                }
            }
            if (grid != null)
            {
                GameObject itemGo = NGUITools.AddChild(grid.gameObject, inventoryItem);
                itemGo.transform.localPosition = Vector3.zero;
                itemGo.GetComponent<UISprite>().depth = 3;
                grid.SetId(id, num);
            }
        }
    }

    public bool MinusId(int id, int count = 1)  //减去物品
    {
        InventoryItemGrid grid = null;

        foreach (InventoryItemGrid temp in itemGridList)
        {
            if (temp.id == id)
            {
                grid = temp;
                break;
            }
        }
        if (grid == null)
        {
            return false;
        }
        else
        {
            bool isSuccess = grid.MinusNumber(count);
            return isSuccess;
        }
    }

    public bool CostCoin(int Count)  //花费金币
    {
        if (coinNumber > Count)
        {
            coinNumber -= Count;
            coinNumberLabel.text = coinNumber.ToString();
            return true;
        }
        return false;
    }

    public void AddCoin(int count)  //获得金币
    {
        coinNumber += count;
        coinNumberLabel.text = coinNumber.ToString();
    }
}
