using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTime : Singleton<TestTime>
{
    // Start is called before the first frame update
    void Start()
    {
        TimeManager.Instance.DoLoop(2000, Creat);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (IAnimatable animatable in TimeManager.timerList)
        {
            animatable.AdvanceTime();
        }
        
    }

    private void Creat()
    {
        Debug.Log("111");
    }


}
