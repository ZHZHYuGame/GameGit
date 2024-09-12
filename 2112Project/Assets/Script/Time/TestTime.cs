using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTime : Singleton<TestTime>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (IAnimatable animatable in TimeManager.timerList)
        {
            animatable.AdvanceTime();
        }
        TimeManager.Instance.DoFrameOnce(2, Creat);
    }

    private void Creat()
    {
        Debug.Log("111");
    }

    public void StartCol(float delyTime, Action action)
    {
        StartCoroutine(IEnum(delyTime, action));
    }
    IEnumerator IEnum(float delyTime, Action action)
    {
        yield return new WaitForSeconds(delyTime);
        action?.Invoke();
    }
}
