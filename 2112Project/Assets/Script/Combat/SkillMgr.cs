using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkillMgr : MonoBehaviour
{
    /// <summary>
    /// ��ɱ����,��������
    /// </summary>
    private int m_skillNum = 5;
    /// <summary>
    /// ���м���UI���
    /// </summary>
    private PropertiesMain m_Properties;
    /// <summary>
    /// ���н��е���ת
    /// </summary>
    private RotateCenter m_RotateCenter;
    /// <summary>
    /// �������
    /// </summary>
    private Transform m_PlayerControl;
    /// <summary>
    /// ���н�����ĵط�
    /// </summary>
    private Transform m_EndFilyWeapon;
    /// <summary>
    /// ��ת��Ĭ���ٶ�
    /// </summary>
    private float m_AddRotateSpeed = 50;
    /// <summary>
    /// ��תʱ��
    /// </summary>
    private float m_RotateSpeedTimer = 0.5f;
    /// <summary>
    /// �����Ƿ�ֹͣ��ת
    /// </summary>
    private bool m_isStopRotate;
    /// <summary>
    /// ���µļ��ܰ���
    /// </summary>
    private KeyCode m_KeySkill;
    

    private void Awake()
    {
        m_Properties = GameObject.Find("Canvas/HeroDisPanel/root").GetComponent<PropertiesMain>();
        m_RotateCenter = GameObject.Find("RotateWeaponCenter").GetComponent<RotateCenter>();
        m_PlayerControl = GameObject.Find("Player").transform;
        m_EndFilyWeapon = m_PlayerControl.Find("EndFilyWeapon");
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_KeySkill = KeyCode.Q;
        }else if (Input.GetKeyDown(KeyCode.W))
        {
            m_KeySkill = KeyCode.W;
        }else if (Input.GetKeyDown(KeyCode.E))
        {
            m_KeySkill = KeyCode.E;
        }


        if (Input.GetKeyDown(m_KeySkill))
        {
            switch (m_KeySkill)
            {
                case KeyCode.Q:
                    m_AddRotateSpeed += 500;
                    break;
                case KeyCode.W:
                    m_isStopRotate = true;
                    if (m_RotateCenter.RotateLst.Count <=1 )
                    {
                        m_isStopRotate = false;
                    }
                    if (m_RotateCenter.RotateLst.Count > 0)
                    {
                        int index = UnityEngine.Random.Range(0, m_RotateCenter.RotateLst.Count);
                        GameObject weapon = m_RotateCenter.RotateLst[index];
                        //weapon.transform. GetComponent<FilyWeapon>().enabled = true;
                        weapon.AddComponent<FilyWeapon>().PlayerPos( m_EndFilyWeapon, m_RotateCenter);
                        DelBeiDongSkill();
                    }
                    break;
                case KeyCode.E:
                    break;
                default:
                    Debug.Log("�밴����ȷ�ļ��ܰ�ť");
                    break;
            }
        }
        SetSkill01();
        SetSkill02();
    }
    /// <summary>
    /// W�����߼�
    /// </summary>
    private void SetSkill02()
    {
        if (!m_isStopRotate)
        {
            m_RotateCenter.RotateSpeed = m_AddRotateSpeed;
        }
        else
        {
            m_RotateCenter.RotateSpeed = 0;
        }
    }

    /// <summary>
    /// Q�����߼�
    /// </summary>
    private void SetSkill01()
    {
        if (m_AddRotateSpeed != 200)
        {
            m_RotateSpeedTimer -= Time.deltaTime;
            if (m_RotateSpeedTimer <= 0)
            {
                m_RotateSpeedTimer = 0.5f;
                m_AddRotateSpeed = 200;
            }
        }
    }

    public void AddBeiDongSkill()
    {
        if (!m_isStopRotate)
        {
            if (m_skillNum < 50)
            {
                m_skillNum += 1;
                m_Properties.SetSkillNum(m_skillNum);
            }
        }
    }
    public void DelBeiDongSkill()
    {
        if (m_skillNum >= 5)
        {
            m_skillNum -= 5;
            m_Properties.SetSkillNum(m_skillNum, m_isStopRotate);
        }
       
    }
}
