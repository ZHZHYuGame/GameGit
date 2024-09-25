using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 画面设置
/// </summary>
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
    Light Light;
    private void Awake()
    {
        list.Add(GetResolutionData("1334*750"));
        list.Add(GetResolutionData("1920*1080"));
        list.Add(GetResolutionData("1280*720"));
        list.Add(GetResolutionData("200*100"));
        for(int i=0;i<list.Count;i++)
        {
            RateDropdown.options.Add(new Dropdown.OptionData(list[i].width + "*" + list[i].height));
        }       
    }
    void Start()
    {

        //如果之前设置过了，先刷新设置显示
        LoadRefreshUI();

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
            PlayerPrefs.SetFloat("LightValue", Light.intensity);
            PlayerPrefs.SetFloat("LightIndex", a);
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

       
       
        RateDropdown.onValueChanged.AddListener((index) =>
        {
            Screen.SetResolution(list[index].width, list[index].height, FullScreenMode.FullScreenWindow,new RefreshRate());
            PlayerPrefs.SetInt("ScreenRate", index);
            Debug.Log("ScreenWidth:" + list[index].width + "  ScreenHeight:" + list[index].height);
            PlayerPrefs.Save();
        });
    }
    /// <summary>
    /// 设置画质
    /// </summary>
    /// <param name="index"></param>
    private void SetQuality(int index)
    {
        int value = 0;
        switch (index)
        {
            case 0:
                value = 0;
               
                break;
            case 1:
                value = 2;
                break;
            case 2:
                value = 3;
                break;
            case 3:
                value = 5;
                break;
        }
        QualitySettings.SetQualityLevel(value);
        PlayerPrefs.SetInt("QualityValue", value);
        PlayerPrefs.SetInt("QualityIndex", index);
        Debug.Log("画面质量："+QualitySettings.GetQualityLevel());
    }
    /// <summary>
    /// 设置帧率
    /// </summary>
    /// <param name="index"></param>
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
        PlayerPrefs.SetInt("FramRateValue", Application.targetFrameRate);
        PlayerPrefs.SetInt("FramRateIndex",index);
        Debug.Log(Application.targetFrameRate);
    }




    
    /// <summary>
    /// 读取存储记录，初始化赋值UI显示,更新设置
    /// </summary>
    void LoadRefreshUI()
    {
        //画质
        int value0 = PlayerPrefs.GetInt("QualityValue");
        QualitySettings.SetQualityLevel(value0);
        int index0 = PlayerPrefs.GetInt("QualityIndex");
        QualityToggle[index0].isOn = true;

        //帧率
        int value1 = PlayerPrefs.GetInt("FramRateValue");
        Application.targetFrameRate = value1;
        int index1 = PlayerPrefs.GetInt("FramRateIndex");
        FramRateToggle[index1].isOn = true;

        //亮度
        float value2 = PlayerPrefs.GetFloat("LightValue");
        Light.intensity= value2;
        float index2 = PlayerPrefs.GetFloat("LightIndex");
        Slider.value= index2;


        //分辨率
        int index3 = PlayerPrefs.GetInt("ScreenRate");
        Screen.SetResolution(list[index3].width,list[index3].height, Screen.fullScreen);
        RateDropdown.SetValueWithoutNotify(index3);

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
