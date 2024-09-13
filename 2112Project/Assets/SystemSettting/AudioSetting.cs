using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ��������
/// </summary>
public class AudioSetting : MonoBehaviour
{
    AudioManager audioManager;

    public Toggle[] musicToggle;

    //������������
    public Slider backSlider;

    //��Ч��������
    public Slider effectSlider;
    private void Awake()
    {

    }
    string str1 = "MusicSwitch";
    string str2 = "BackGroundMusic";
    string str3 = "EffectMusic";
    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        LoadRefreshUI();
        for (int i = 0; i < musicToggle.Length; i++)
        {
            int index = i;
            musicToggle[index].onValueChanged.AddListener((a) =>
            {
                if (a)
                {
                    if(index==0)//��
                    {
                        audioManager.SetAllVolume(1);                       
                    }
                    else
                    {
                        audioManager.SetAllVolume(0);
                    }
                    PlayerPrefs.SetFloat(str1, index);
                }
            });
        }

        backSlider.onValueChanged.AddListener((a) =>
        {
            backSlider.value = a;
            audioManager.SetBackGroundVolume(a);
            PlayerPrefs.SetFloat(str2, a);
        });
        effectSlider.onValueChanged.AddListener((a) =>
        {
            effectSlider.value = a;
            audioManager.SetAllVolume(a);
            PlayerPrefs.SetFloat(str3, a);
        });
    }


    /// <summary>
    /// ��ȡ�洢��¼����ʼ����ֵUI��ʾ,��������
    /// </summary>
    void LoadRefreshUI()
    {
        //���ֿ���
        float value1 = PlayerPrefs.GetFloat(str1);
        musicToggle[(int)value1].isOn = true;
        audioManager.SetAllVolume((int)value1);

        //��������
        float value2 = PlayerPrefs.GetFloat(str2);
        backSlider.value = value2;
        audioManager.SetBackGroundVolume((int)value2);
        //��Ч����
        float value3 = PlayerPrefs.GetFloat(str3);
        effectSlider.value = value3;
        audioManager.SetAllEffectVolme(value3);
    }
}
