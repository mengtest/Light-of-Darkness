using UnityEngine;
using System.Collections;

public class InventoryItemGrid : MonoBehaviour
{
    public int id = 0;
    public int num = 0;
    private UILabel numLabel;
    private ObjectsInfo info = null;

    void Awake()
    {
        numLabel = GetComponentInChildren<UILabel>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlusNumber(int num = 1)
    {
        this.num += num;
        numLabel.text = this.num.ToString();
    }

    public bool MinusNumber(int num = 1)  //用来减去数量，返回值表示是否减去成功
    {
        if (this.num >= num)
        {
            this.num -= num;
            numLabel.text = this.num.ToString();
            if (this.num == 0)  //要清空这个格子
            {
                ClearInfo();  //清空信息
                Destroy(GetComponentInChildren<InventoryItem>().gameObject);  //销毁格子
            }
            return true;
        }
        return false;
    }

    public void SetId(int id, int num = 1)
    {
        this.id = id;
        info = ObjectsInfo.instance.GetObjectsInfoById(id);
        InventoryItem item = GetComponentInChildren<InventoryItem>();
        item.SetIconName(id, info.icon_name);
        numLabel.enabled = true;
        this.num = num;
        numLabel.text = num.ToString();
    }

    public void ClearInfo()  //清空格子储存的物品信息
    {
        id = 0;
        info = null;
        num = 0;
        numLabel.enabled = false;
    }
}
