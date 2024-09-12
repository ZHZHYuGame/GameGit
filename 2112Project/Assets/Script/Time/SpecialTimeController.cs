using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialTimeController : Singleton<SpecialTimeController>
{
    public void HitPause(int duration)
    {
        StartCoroutine(Pause(duration));
    }
    IEnumerator Pause(int duration)
    {
        float pauseTime = duration / 60f;
        TimeManager.Instance.SetTimeScale(0);
        yield return new WaitForSecondsRealtime(pauseTime);
        TimeManager.Instance.SetTimeScale(1);
    }
    public void Timer(int countdownTime,Text text, bool IsCorrect)
    {
        StartCoroutine(TimerController(countdownTime, text, IsCorrect));
    }
    private IEnumerator TimerController(int countdownTime,Text text, bool IsCorrect)
    {
        //����ʱ
        if(IsCorrect)
        {
            while (countdownTime >= 0)
            {
                // ����ʣ��ķ��Ӻ���
                int minutes = (int)(countdownTime / 60);
                int seconds = countdownTime % 60;

                text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                yield return new WaitForSeconds(1f);
                countdownTime++;
            }
        }
        else
        {
            while (countdownTime >= 0)
            {
                // ����ʣ��ķ��Ӻ���
                int minutes = (int)(countdownTime / 60);
                int seconds = countdownTime % 60;

                text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                yield return new WaitForSeconds(1f);
                countdownTime--;
            }
            if(countdownTime<0)
            {
                Debug.Log("��Ϸʧ��");
                TimeManager.Instance.SetTimeScale(0);
            }
        }
    }
}
