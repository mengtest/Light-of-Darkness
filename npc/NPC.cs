﻿using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        CursorManager.instance.SetNPCTalk();
    }

    void OnMouseExit()
    {
        CursorManager.instance.SetNormal();
    }
}
