using UnityEngine;
using System.Collections;

public class FunctionBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnStatusBtnClick()
    {
        Status.instance.TransformState();
    }

    public void OnBagBtnClick()
    {
        Inventory.instance.TransformState();
    }

    public void OnEquipBtnClick()
    {
        Equipment.instance.TransformState();
    }

    public void OnSkillBtnClick()
    {
        Skill.instance.TransformState();
    }

    public void OnSettingBtnClick()
    {

    }
}
