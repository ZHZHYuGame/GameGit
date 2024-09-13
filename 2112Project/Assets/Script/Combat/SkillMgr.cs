using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkillMgr : MonoBehaviour
{
    /// <summary>
    /// 击杀数量,被动技能
    /// </summary>
    private int m_skillNum = 5;
    /// <summary>
    /// 持有技能UI面板
    /// </summary>
    private PropertiesMain m_Properties;
    /// <summary>
    /// 持有剑刃的旋转
    /// </summary>
    private RotateCenter m_RotateCenter;
    /// <summary>
    /// 持有玩家
    /// </summary>
    private Transform m_PlayerControl;
    /// <summary>
    /// 持有剑飞向的地方
    /// </summary>
    private Transform m_EndFilyWeapon;
    /// <summary>
    /// 旋转的默认速度
    /// </summary>
    private float m_AddRotateSpeed = 200;
    /// <summary>
    /// 旋转时长
    /// </summary>
    private float m_RotateSpeedTimer = 0.5f;
    /// <summary>
    /// 剑刃是否停止旋转
    /// </summary>
    private bool m_isStopRotate;
    /// <summary>
    /// 按下的技能按键
    /// </summary>
    private KeyCode m_KeySkill;
    /// <summary>
    /// 是否按下E技能
    /// </summary>
    private bool m_isE;
    /// <summary>
    /// 持有主相机
    /// </summary>
    private Transform m_Camera;

    /// <summary>
    /// 是否Lerp收回相机
    /// </summary>
    private bool isCameraOff;


    /// <summary>
    /// E技能放大范围后旋转的时间
    /// </summary>
    private float m_BigRotateTimer = 2f;
    private void Awake()
    {
        m_Properties = GameObject.Find("Canvas/HeroDisPanel/root").GetComponent<PropertiesMain>();
        m_RotateCenter = GameObject.Find("RotateWeaponCenter").GetComponent<RotateCenter>();
        m_PlayerControl = GameObject.Find("Player").transform;
        m_EndFilyWeapon = m_PlayerControl.Find("EndFilyWeapon");
        m_Camera = GameObject.Find("Main Camera").transform;
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
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            m_KeySkill = KeyCode.W;
        }
        else if (Input.GetKeyDown(KeyCode.E))
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
                    if (m_RotateCenter.RotateLst.Count <= 1)
                    {
                        m_isStopRotate = false;
                    }
                    if (m_RotateCenter.RotateLst.Count > 0)
                    {
                        int index = UnityEngine.Random.Range(0, m_RotateCenter.RotateLst.Count);
                        GameObject weapon = m_RotateCenter.RotateLst[index];
                        weapon.AddComponent<FilyWeapon>().PlayerPos(m_EndFilyWeapon, m_RotateCenter);
                        DelBeiDongSkill();
                    }
                    break;
                case KeyCode.E:
                    if (m_skillNum < 50)
                    {
                        return;
                    }
                    m_RotateCenter.transform.localScale = Vector3.zero;
                    m_isE = true;
                    
                    break;
                default:
                    Debug.Log("请按下正确的技能按钮");
                    break;
            }
        }
        SetSkill01();
        SetSkill02();
        SetSkill03();
    }

    private void SetSkill03()
    {
        if (m_isE)
        {

            if (!isCameraOff)
            {
                m_RotateCenter.transform.localScale = Vector3.Lerp(m_RotateCenter.transform.localScale, new Vector3(10, 1.5f, 10), Time.deltaTime*2);
                if (m_Camera.transform.position.y <= 40)
                {
                    m_Camera.transform.position = Vector3.Lerp(m_Camera.transform.position, m_Camera.transform.position + Vector3.up * 10, Time.deltaTime * 10);
                }
                
                //m_Camera.GetComponent<Camera>().fieldOfView += Time.deltaTime * 20;
                if (m_RotateCenter.transform.localScale.x >= 2.5f)
                {
                    m_AddRotateSpeed += 200;
                    SetSkill01();
                    m_BigRotateTimer -= Time.deltaTime;
                    if(m_BigRotateTimer <= 0)
                    {
                        m_BigRotateTimer = 2;
                        isCameraOff = true;
                    }

                    
                }
            }
            

            if (isCameraOff)
            {
                m_Camera.transform.position = Vector3.Lerp(m_Camera.transform.position, new Vector3(m_PlayerControl.transform.position.x, 10, m_PlayerControl.transform.position.z), Time.deltaTime * 2);
                //m_Camera.GetComponent<Camera>().fieldOfView -= Time.deltaTime * 20;
                m_RotateCenter.transform.localScale = Vector3.Lerp(m_RotateCenter.transform.localScale, new Vector3(1f, 1f, 1), Time.deltaTime * 2);
                if (m_RotateCenter.transform.localScale.x <= 1f)
                {
                    isCameraOff = false;
                    m_isE = false;
                }
            }




            //m_EStopTimer -= Time.deltaTime;
            //m_Camera.transform.position = Vector3.Lerp(m_Camera.transform.position, m_Camera.transform.position + Vector3.up * 10, Time.deltaTime * 5);
            //if (m_EStopTimer <= 0)
            //{
            //    m_EStopTimer = 0.5f;
            //    m_Camera.transform.position = new Vector3(m_PlayerControl.transform.position.x, 10, m_PlayerControl.transform.position.z);
            //    m_AddRotateSpeed += 1000f;
            //}

            //m_ETimer -= Time.deltaTime;
            //if (m_ETimer <= 0)
            //{
            //    m_ETimer = 1f;
            //    m_RotateCenter.transform.localScale = new Vector3(1, 1, 1);
            //    m_Camera.transform.position = Vector3.up * 10;
            //    m_RotateCenter.ArrangeInCircle();
            //    m_isE = false;
            //}
        }
    }

    /// <summary>
    /// W技能逻辑
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
    /// Q技能逻辑
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
