using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesMain : MonoBehaviour
{
    /// <summary>
    /// 属性面板
    /// </summary>
    public Image PpPanel;
    /// <summary>
    /// 上升的初始位置
    /// </summary>
    private Vector3 m_startPos;
    /// <summary>
    /// 上升的结束位置
    /// </summary>
    private Vector3 m_endPos;
    private float m_endYPos = 90f;
    private float m_elapsedTime = 0;
    private float m_duration = 0.5f;
    /// <summary>
    /// 持有旋转武器
    /// </summary>
    public RotateCenter m_rotateCenter;
    public Text skillNum;
    /// <summary>
    /// 持有技能面板
    /// </summary>
    private SkillMgr m_skillMgr;
    //血量,蓝量,能量条
    public Slider hpSlider;
    public Slider mpSlider;
    public Slider cdSlider;

    private float hpValue;
    private float hpValue_MAX = 100;
    private float mpValue;
    private float mpValue_MAX = 100;
    private float cdValue;
    private float cdValue_MAX = 100;

    public float HpValue
    {
        get { return hpValue; }
    }
    public float MpValue
    {
        get { return mpValue; }
    }
    public float CdValue
    {
        get { return cdValue; }
    }

    void Start()
    {
        skillNum.text = "5";
        m_startPos = PpPanel.rectTransform.position;
        m_endPos = new Vector3(m_startPos.x, m_endYPos, m_startPos.z);
        m_skillMgr=GameObject.Find("Canvas/Skills").GetComponent<SkillMgr>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_elapsedTime < m_duration)
        {
            m_elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(m_elapsedTime / m_duration);
            PpPanel.transform.position = Vector3.Lerp(m_startPos, m_endPos, t);
        }
    }

    public void SetSkillNum(int num,bool m_isStopRotate=false)
    {
        if (num % 5 == 0 && !m_isStopRotate)
        {
            m_rotateCenter.AddWeapon();
        }
        skillNum.text = num.ToString();
    }

    /// <summary>
    /// 增加能量的地方
    /// </summary>
    /// <param name="value"></param>
    public void AddCDValue(float value)
    {
        if (!m_skillMgr.m_isE)
        {
            if (cdValue <= cdValue_MAX)
            {
                cdValue += value;
                cdSlider.value = cdValue;
            }
        }
        
       
    }

    public void ClearCDValue()
    {
        cdValue = 0;
        cdSlider.value = cdValue;
    }
}
