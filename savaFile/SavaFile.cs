using UnityEngine;
using System.Collections;

public class SavaFile : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().Play();
            SavaMenu.instance.OpenMenu();  //打开存档菜单
        }
    }
}
