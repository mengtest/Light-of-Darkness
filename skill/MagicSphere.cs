using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagicSphere : MonoBehaviour
{
    public List<BabyWolf> wolfList = new List<BabyWolf>();
    public int attack = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == Tags.enemy)
        {
            BabyWolf wolf = col.GetComponent<BabyWolf>();
            int index = wolfList.IndexOf(wolf);  //判断是否伤害过
            if (index == -1)  //不存在，可以伤害狼
            {
                wolf.TakeDamage(attack);
                wolfList.Add(wolf);
            }
        }
    }
}
