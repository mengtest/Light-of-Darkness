using UnityEngine;
using System.Collections;

public class ShopDrugNPC : NPC
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))  //显示药品购买列表
        {
            GetComponent<AudioSource>().Play();
            ShopDrug.instance.TransformState();
        }
    }
}
