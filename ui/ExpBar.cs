﻿using UnityEngine;
using System.Collections;

public class ExpBar : MonoBehaviour
{
    public static ExpBar instance;

    private UISlider progressBar;

    void Awake()
    {
        instance = this;
        progressBar = GetComponent<UISlider>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetValue(float value)
    {
        progressBar.value = value;
    }
}
