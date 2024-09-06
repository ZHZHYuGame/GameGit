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
    private float m_endYPos = 140f;
    private float m_elapsedTime = 0;
    private float m_duration = 0.5f;
    void Start()
    {
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
}
