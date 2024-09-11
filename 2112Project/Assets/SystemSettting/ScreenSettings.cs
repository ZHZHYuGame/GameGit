using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSettings : MonoBehaviour
{
    //画质
    public Toggle[] QualityToggle;

    //分辨率
    public Dropdown RateDropdown;
    List<ResolutionData> list=new List<ResolutionData>();

    //帧率
    public Toggle[] FramRateToggle;

    //亮度
    public Slider Slider;
    public Light Light;
    // Start is called before the first frame update
    void Start()
    {
        //画质
        for (int i = 0; i < QualityToggle.Length; i++)
        {
            int index = i;
            QualityToggle[index].onValueChanged.AddListener((a) =>
            {
                if (a)
                {
                    SetQuality(index);
                }
            });
        }
        //亮度更改
        Slider.onValueChanged.AddListener((a) =>
        {
            Slider.value = a;
            Light.intensity= Slider.value*3;
        });
        //注册帧率点击事件
        for(int i=0;i< FramRateToggle.Length;i++)
        {
            int index = i;
            FramRateToggle[index].onValueChanged.AddListener((a) =>
            {
                if(a)
                {
                    SetFramRate(index);
                }               
            });
        }

        //设置分辨率
        LoadResolution();
        list.Add(GetResolutionData("1334*750"));
        list.Add(GetResolutionData("1920*1080"));
        list.Add(GetResolutionData("1280*720"));
        list.Add(GetResolutionData("800*600"));
        RateDropdown.onValueChanged.AddListener((index) =>
        {
            // SetResolution(list[index].width, list[index].height);
            ChangeResolution(list[index].width, list[index].height);
        });
    }
    private void SetQuality(int index)
    {
        switch (index)
        {
            case 0:
                QualitySettings.SetQualityLevel(0);
                break;
            case 1:
                QualitySettings.SetQualityLevel(2);
                break;
            case 2:
                QualitySettings.SetQualityLevel(4);
                break;
            case 3:
                QualitySettings.SetQualityLevel(5);
                break;
        }
        Debug.Log("画面质量："+QualitySettings.GetQualityLevel());
    }
    private void SetFramRate(int index)
    {
        switch(index)
        {
            case 0:
                Application.targetFrameRate = 30;               
                break;
            case 1:
                Application.targetFrameRate = 60;
                break;
            case 2:
                Application.targetFrameRate = 90;
                break;
            case 3:
                Application.targetFrameRate = 120;
                break;
        }
        Debug.Log(Application.targetFrameRate);
        SaveFramRate();
    }
    private void SaveFramRate()
    {
        PlayerPrefs.SetInt("FramRate", Application.targetFrameRate);
    }



    void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, true);
        SaveResolution(width, height);
    }

    public void ChangeResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
        SaveResolution(width, height);
    }

    void SaveResolution(int width, int height)
    {
        PlayerPrefs.SetInt("ScreenWidth", width);
        PlayerPrefs.SetInt("ScreenHeight", height);
        Debug.Log("ScreenWidth:" + width + "  ScreenHeight:" + height);
        PlayerPrefs.Save();
    }

    void LoadResolution()
    {
        int width = PlayerPrefs.GetInt("ScreenWidth", Screen.width);
        int height = PlayerPrefs.GetInt("ScreenHeight", Screen.height);
        if(width!=0&& height!=0) 
        Screen.SetResolution(width, height, Screen.fullScreen);
    }
    public ResolutionData GetResolutionData(string str)
    {
        string[] arr = str.Split('*');
        ResolutionData data = new ResolutionData(int.Parse(arr[0]), int.Parse(arr[1])); 
        return data;
    }
}

public class ResolutionData
{
    public int width;
    public int height;

    public ResolutionData(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
}
