using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterCreation : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public UIInput nameInput;
    private GameObject[] characterGameObjects;
    private int selectIndex = 0;
    private int length;  //所有可供选择的角色个数

    // Use this for initialization
    void Start()
    {
        length = characterPrefabs.Length;
        characterGameObjects = new GameObject[length];

        for (int i = 0; i < length; i++)
        {
            characterGameObjects[i] = (GameObject)Instantiate(characterPrefabs[i], transform.position, transform.rotation);
        }

        CharacterShow();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //更新角色显示
    void CharacterShow()
    {
        characterGameObjects[selectIndex].SetActive(true);  //显示选中角色

        for (int i = 0; i < length; i++)  //隐藏其他角色
        {
            if (i != selectIndex)
            {
                characterGameObjects[i].SetActive(false);
            }
        }
    }

    public void OnPrevButtonClick()  //点击上一个按钮
    {
        selectIndex++;
        selectIndex %= length;  //防止多于个数
        CharacterShow();
    }

    public void OnNextButtonClick()  //点击下一个按钮
    {
        selectIndex--;
        if (selectIndex == -1)  //防止少于个数
        {
            selectIndex = length - 1;
        }
        CharacterShow();
    }

    public void OnOkButtonClick()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectIndex);  //存储选择的角色
<<<<<<< HEAD
        PlayerPrefs.SetString("Name", nameInput.value);  //存储输入的名字
        SceneManager.LoadScene(2);  //加载下一个场景
=======
        PlayerPrefs.SetString("name", nameInput.value);  //存储输入的名字
        //加载下一个场景
        SceneManager.LoadScene(2);
>>>>>>> fdb802c8a5058c1a581959e554ff1feedb68e464
    }
}
