using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesMain : MonoBehaviour
{
    /// <summary>
    /// �������
    /// </summary>
    public Image PpPanel;
    /// <summary>
    /// �����ĳ�ʼλ��
    /// </summary>
    private Vector3 m_startPos;
    /// <summary>
    /// �����Ľ���λ��
    /// </summary>
    private Vector3 m_endPos;
    private float m_endYPos = 140f;
    private float m_elapsedTime = 0;
    private float m_duration = 0.5f;
    /// <summary>
    /// ������ת����
    /// </summary>
    public RotateCenter m_rotateCenter;
    public Text skillNum;
    void Start()
    {
        skillNum.text = "1";
        m_startPos = PpPanel.rectTransform.position;
        m_endPos = new Vector3(m_startPos.x, m_endYPos, m_startPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_elapsedTime < m_duration)
        {
            m_elapsedTime += Time.deltaTime;
            float t=Mathf.Clamp01(m_elapsedTime/m_duration);
            PpPanel.transform.position = Vector3.Lerp(m_startPos, m_endPos, t);
        }
    }

    public void SetSkillNum(int num)
    {
        if (num <= 50)
        {
            if (num % 5 == 0)
            {
                m_rotateCenter.AddWeapon();
            }
            skillNum.text = num.ToString();
        }
    }
}
