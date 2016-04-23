using UnityEngine;
using System.Collections;

public class InventoryItem : UIDragDropItem
{

    private UISprite sprite;
    private int id;
    private bool isHover = false;

    new void Awake()
    {
        base.Awake();
        sprite = GetComponent<UISprite>();
    }

    new void Update()
    {
        base.Update();
        if (isHover)
        {
            ItemDescribe.instance.Show(id);
            if(Input.GetMouseButtonDown(1))
            {
                bool success = Equipment.instance.Dress(id);
                if (success)
                {
                    transform.parent.GetComponent<InventoryItemGrid>().MinusNumber();
                }
            }
        }
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        Debug.Log(surface.tag);
        if (surface != null)
        {
            if (surface.tag == Tags.inventoryItemGrid)  //当拖放到空格子里
            {
                if (surface == transform.parent.gameObject)  //当拖动到自己的格子里
                {   
                }
                else
                {
                    InventoryItemGrid oldParent = transform.parent.GetComponent<InventoryItemGrid>();
                    InventoryItemGrid newParent = surface.GetComponent<InventoryItemGrid>();
                    transform.parent = surface.transform;
                    newParent.SetId(oldParent.id, oldParent.num);

                    oldParent.ClearInfo();  //清空信息
                }
            }
            else if (surface.tag == Tags.inventoryItem)  //当拖放到有物品的格子里
            {
                InventoryItemGrid grid1 = transform.parent.GetComponent<InventoryItemGrid>();
                InventoryItemGrid grid2 = surface.transform.parent.GetComponent<InventoryItemGrid>();
                int id = grid1.id;
                int num = grid1.num;
                grid1.SetId(grid2.id, grid2.num);
                grid2.SetId(id, num);
            }
            else if(surface.tag == Tags.shortCut)  //当拖放到快捷栏时
            {
                surface.GetComponent<ShortCutGrid>().SetInventory(id);
            }
            else //当拖放到空白的地方时
            { 
            }

            ResetPosition();
        }
        else
        {
            ResetPosition();
        }
    }

    public void SetId(int id)
    {
        ObjectsInfo info = ObjectsInfo.instance.GetObjectsInfoById(id);
        sprite.spriteName = info.name;
    }

    public void SetIconName(int id,string icon_name)
    {
        this.id = id;
        sprite.spriteName = icon_name;
    }

    void ResetPosition()
    {
        transform.localPosition = Vector3.zero;
    }

    public void OnHoverOver()  //鼠标移入
    {
        isHover = true;
    }

    public void OnHoverOut()  //鼠标移出
    {
        isHover = false;
    }
}
