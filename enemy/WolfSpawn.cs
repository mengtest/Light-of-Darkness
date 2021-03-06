﻿using UnityEngine;
using System.Collections;

public class WolfSpawn : MonoBehaviour
{
    public GameObject babyWolf;  //狼的prefab
    public int maxNum = 5;  //狼最大数量
    private int currentNum = 0;  //当前数量
    public float time = 3f;  //3秒产生一只狼
    private float timer = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentNum < maxNum)
        {
            timer += Time.deltaTime;
            if (timer >= time)
            {
                Vector3 pos = transform.position;  //随机生成小狼位置
                pos.x += Random.Range(-4, 4);
                pos.z += Random.Range(-4, 4);

                GameObject go = (GameObject)Instantiate(babyWolf, pos, Quaternion.identity);
                go.GetComponent<BabyWolf>().spawn = this;
                go.transform.parent = transform;
                timer = 0;
                currentNum++;
            }
        }
    }

    public void MinusNumber()
    {
        currentNum--;
    }
}
