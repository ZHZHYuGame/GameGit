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
    // Start is called before the first frame update
    void Start()
    {
        Slider.onValueChanged.AddListener((a) =>
        {
            Slider.value = a;
            MainCamera.fieldOfView = 30 + 60 * a;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
