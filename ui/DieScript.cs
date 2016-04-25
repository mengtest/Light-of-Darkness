using UnityEngine;
using System.Collections;

public class DieScript : MonoBehaviour
{
    public static DieScript instance;

    private TweenPosition tween;

    // Use this for initialization
    void Start()
    {
        instance = this;
        tween = GetComponent<TweenPosition>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeadWindow()
    {
        tween.PlayForward();
    }

    public void YesButton()
    {
        tween.PlayReverse();
        SaveScript.instance.SaveWindow();
    }
}
