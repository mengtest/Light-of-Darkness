using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonContainer : MonoBehaviour
{
    //1->游戏数据的保存和场景之间游戏数据的传递使用Prefs
    //2->场景的分类
    //1.开始界面
    //2.角色选择界面
    //3.游戏主界面（村庄）

    public GameObject magicianPrefab;
    public GameObject swordmanPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //开始新游戏
    public void OnNewGame()  //新游戏默认的属性
    {
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("Hp", 100);
        PlayerPrefs.SetInt("Hp_remain", 100);
        PlayerPrefs.SetInt("Mp", 100);
        PlayerPrefs.SetInt("Mp_remain", 100);
        PlayerPrefs.SetInt("Attack", 10);
        PlayerPrefs.SetInt("Attack_plus", 0);
        PlayerPrefs.SetInt("Def", 10);
        PlayerPrefs.SetInt("Def_plus", 0);
        PlayerPrefs.SetInt("Speed", 10);
        PlayerPrefs.SetInt("Speed_plus", 0);
        PlayerPrefs.SetInt("Remain_point", 5);
        PlayerPrefs.SetFloat("Exp", 0f);

        PlayerPrefs.SetInt("KillCount", 0);
        PlayerPrefs.SetInt("InTask", 0);  //新游戏默认未接受任务
        PlayerPrefs.SetInt("Coin", 1000);

        SceneManager.LoadScene(1);
    }

    //加载旧游戏
    public void OnLoadGame()
    {
        SceneManager.LoadScene(2);  //直接到第二个场景
    }
}
