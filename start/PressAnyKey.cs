using UnityEngine;
using System.Collections;

public class PressAnyKey : MonoBehaviour
{
    private GameObject buttonContainer;
    private bool isPressAnyKey = false;  //表示是否有任何键被按下

    // Use this for initialization
    void Start()
    {
        buttonContainer = transform.parent.Find("buttonContainer").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressAnyKey == false)
        {
            if (Input.anyKey)
            {
                ShowButton();
            }
        }
    }

    void ShowButton()
    {
        gameObject.SetActive(false);
        buttonContainer.SetActive(true);
        isPressAnyKey = true;
    }
}
