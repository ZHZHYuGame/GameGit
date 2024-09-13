using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ª˘¥°…Ë÷√
/// </summary>
public class BaseSetting : MonoBehaviour
{
    public Slider Slider;
    public Camera MainCamera;
    
    private void Awake()
    {
        LoadDataRefreshUI();
    }
    // Start is called before the first frame update
    void Start()
    {
        Slider.onValueChanged.AddListener((a) =>
        {
            Slider.value = a;
            MainCamera.fieldOfView = 30 + 60 * a;
            PlayerPrefs.SetFloat("CameraValue", MainCamera.fieldOfView);
            PlayerPrefs.SetFloat("CameraIndex", a);
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LoadDataRefreshUI()
    {
        float value0= PlayerPrefs.GetFloat("CameraValue");
        MainCamera.fieldOfView = value0;
        float index0= PlayerPrefs.GetFloat("CameraIndex");
        Slider.value = index0;
    }
}
