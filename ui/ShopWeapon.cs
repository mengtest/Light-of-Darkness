using UnityEngine;
using System.Collections;

public class ShopWeapon : MonoBehaviour
{

    public static ShopWeapon instance;
    public int[] weaponIdArray;
    public UIGrid grid;
    public GameObject weaponItem;

    private TweenPosition tween;
    private bool isShow = false;

    private GameObject numberDialog;
    private UIInput numberInput;
    private int buyId = 0;

    void Awake()
    {
        instance = this;
        tween = GetComponent<TweenPosition>();
        numberDialog = transform.Find("NumberDialog").gameObject;
        numberInput = transform.Find("NumberDialog/NumberInput").GetComponent<UIInput>();
        numberDialog.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        InitShopWeapon();
    }

    // Update is called once per frame
    void Update()
    {

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

    public void OnCloseButtonClick()
    {
        numberDialog.SetActive(false);
        isShow = false;
        tween.PlayReverse();
    }


    //初始化武器商店信息
    void InitShopWeapon()
    {
        foreach (int id in weaponIdArray)
        {
            GameObject itemGo = NGUITools.AddChild(grid.gameObject, weaponItem);
            grid.AddChild(itemGo.transform);
            itemGo.GetComponent<WeaponItem>().SetId(id);
        }
    }

    //OK按钮点击的时候
    public void OnOKButtonClick()
    {
        int count = int.Parse(numberInput.value);
        int price = ObjectsInfo.instance.GetObjectsInfoById(buyId).price_buy;
        int total = price * count;

        bool success = Inventory.instance.GetCoin(total);  //背包里的钱是否足够
        if (success)
        {
            if (count > 0)
            {
                Inventory.instance.GetId(buyId, count);
            }
        }
        buyId = 0;
        numberInput.value = "0";
        numberDialog.SetActive(false);
    }

    public void OnBuyClick(int id)  //点击购买按钮
    {
        numberDialog.SetActive(true);
        numberInput.value = "0";
        buyId = id;
    }

    //点击面板隐藏对话框
    public void OnClick()
    {
        numberDialog.SetActive(false);
    }
}
