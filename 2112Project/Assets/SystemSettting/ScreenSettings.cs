using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ��������
/// </summary>
public class ScreenSettings : MonoBehaviour
{
    //����
    public Toggle[] QualityToggle;

    //�ֱ���
    public Dropdown RateDropdown;
    List<ResolutionData> list=new List<ResolutionData>();

    //֡��
    public Toggle[] FramRateToggle;

    //����
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

        //���֮ǰ���ù��ˣ���ˢ��������ʾ
        LoadRefreshUI();

        //����
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
        //���ȸ���
        Slider.onValueChanged.AddListener((a) =>
        {
            Slider.value = a;
            Light.intensity= Slider.value*3;
            PlayerPrefs.SetFloat("LightValue", Light.intensity);
            PlayerPrefs.SetFloat("LightIndex", a);
        });
        //ע��֡�ʵ���¼�
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
    /// ���û���
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
        Debug.Log("����������"+QualitySettings.GetQualityLevel());
    }
    /// <summary>
    /// ����֡��
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
    /// ��ȡ�洢��¼����ʼ����ֵUI��ʾ,��������
    /// </summary>
    void LoadRefreshUI()
    {
        //����
        int value0 = PlayerPrefs.GetInt("QualityValue");
        QualitySettings.SetQualityLevel(value0);
        int index0 = PlayerPrefs.GetInt("QualityIndex");
        QualityToggle[index0].isOn = true;

        //֡��
        int value1 = PlayerPrefs.GetInt("FramRateValue");
        Application.targetFrameRate = value1;
        int index1 = PlayerPrefs.GetInt("FramRateIndex");
        FramRateToggle[index1].isOn = true;

        //����
        float value2 = PlayerPrefs.GetFloat("LightValue");
        Light.intensity= value2;
        float index2 = PlayerPrefs.GetFloat("LightIndex");
        Slider.value= index2;


        //�ֱ���
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
