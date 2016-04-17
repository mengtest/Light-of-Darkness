using UnityEngine;
using System.Collections;

public class ButtonContainer : MonoBehaviour {

    //1->游戏数据的保存和场景之间游戏数据的传递使用Prefs
    //2->场景的分类
      //1.开始界面
      //2.角色选择界面
      //3.游戏主界面（村庄）

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //开始新游戏
    public void OnNewGame()
    {
        PlayerPrefs.SetInt("DataFromSave", 0);  //DataFromSave表示数据来自保存
    }

    //加载旧游戏
    public void OnLoadGame()
    {
        PlayerPrefs.SetInt("DataFromSave", 1);
    }
}
