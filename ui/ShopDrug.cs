using UnityEngine;
using System.Collections;

public class ShopDrug : MonoBehaviour
{
    public static ShopDrug instance;

    private TweenPosition tween;
    private bool isShow = false;

    private GameObject numberDialog;
    private UIInput numberInput;
    private int buy_id = 0;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TransformState()
    {
        if (isShow == false)
        {
            tween.PlayForward();
            isShow = true;
        }
        else
        {
            tween.PlayReverse();
            isShow = false;
        }
    }

    public void OnCloseButtonClick()
    {
        numberDialog.SetActive(false);
        isShow = false;
        tween.PlayReverse();
    }

    public void OnBuyId1001()
    {
        Buy(1001);
    }

    public void OnBuyId1002()
    {
        Buy(1002);
    }

    public void OnBuyId1003()
    {
        Buy(1003);
    }

    void Buy(int id)
    {
        ShowNumberDialog();
        buy_id = id;
    }

    public void OnOkButtonClick()
    {
        int count = int.Parse(numberInput.value);
        ObjectsInfo info = ObjectsInfo.instance.GetObjectsInfoById(buy_id);
        int price = info.price_buy;
        int total = count * price;

        bool success = Inventory.instance.CostCoin(total);
        if (success)  //购买成功
        {
            if (count > 0)
            {
                Inventory.instance.GetId(buy_id, count);
            }
        }
        buy_id = 0;
        numberInput.value = "0";
        numberDialog.SetActive(false);
    }

    void ShowNumberDialog()  //点击购买按钮
    {
        numberDialog.SetActive(true);
        numberInput.value = "0";
    }

    //点击面板隐藏对话框
    public void OnClick()
    {
        numberDialog.SetActive(false);
    }
}
