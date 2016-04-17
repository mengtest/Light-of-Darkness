using UnityEngine;
using System.Collections;

public class EquipmentItem : MonoBehaviour {

    private UISprite sprite;
    public int id;
    private bool isHover = false;

    void Awake()
    {
        sprite = GetComponent<UISprite>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isHover)  //当鼠标在这个装备栏之上时，检测鼠标右键的点击
        {
            if (Input.GetMouseButtonDown(1))  //鼠标右键点击，表示卸下装备
            {
                Inventory.instance.GetId(id);
                Destroy(gameObject);
                Equipment.instance.MinusProperty(id);
            }
        }
	}

    public void SetId(int id)
    {
        this.id = id;
        ObjectsInfo info = ObjectsInfo.instance.GetObjectsInfoById(id);
        SetInfo(info);
    }

    public void SetInfo(ObjectsInfo info)
    {
        id = info.id;
        sprite.spriteName = info.icon_name;
    }

    public void OnHover(bool isOver)
    {
        isHover = isOver;
    }
}
