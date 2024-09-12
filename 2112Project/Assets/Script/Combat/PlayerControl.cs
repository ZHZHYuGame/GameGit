using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 角色控制脚本
/// </summary>
public class PlayerControl : MonoBehaviour
{
    /// <summary>
    /// 持有摇杆数据
    /// </summary>
    private ETCMove m_EtcMove;
    /// <summary>
    /// 是否在移动播放动画
    /// </summary>
    private bool m_IsMoving;
    private Animator m_Animator;
    /// <summary>
    /// 连招计数器
    /// </summary>
    private int m_atkCount;
    /// <summary>
    /// 连招计时器
    /// </summary>
    private float m_atkTimer;
    private float m_interval = 2f;
    /// <summary>
    /// 是否正在攻击
    /// </summary>
    public bool isAtk;
    private HandWeapon m_handWeapon;


    void Start()
    {
        m_EtcMove = GameObject.Find("Canvas/ETCParent/ETC").GetComponent<ETCMove>();
        m_Animator = GetComponent<Animator>();
        m_handWeapon=GetComponentInChildren<HandWeapon>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }
    private void Attack()
    {
        if(Input.GetKeyDown(KeyCode.A)&&!isAtk)
        {
            isAtk = true;
            m_handWeapon.isAtk = true;
            m_atkCount++;
            if (m_atkCount > 2)
            {
                m_atkCount = 1;
            }
            m_atkTimer = m_interval;
            m_Animator.SetTrigger("Atk");
            m_Animator.SetInteger("AtkCount", m_atkCount);
        }

        //连击切换时间
        if (m_atkTimer != 0)
        {
            m_atkTimer -= Time.deltaTime;
            if(m_atkTimer <= 0)
            {
                m_atkTimer = 0;
                m_atkCount = 0;
            }
        }
    }

    /// <summary>
    /// 攻击动画执行结束的回调
    /// </summary>
    public void AttackOver()
    {
        m_handWeapon.isAtk = false;
        isAtk = false;
    }

    private void Move()
    {
        if(!isAtk)
        {
            float H = m_EtcMove.GetMovePos("Horizontal");
            float V = m_EtcMove.GetMovePos("Vertical");
            Vector3 pos = new Vector3(H, 0, V);
            if (pos != Vector3.zero)
            {
                transform.LookAt(pos + transform.position);
                transform.Translate(Vector3.forward * (5 * Time.deltaTime));
                m_IsMoving = true;
            }
            else
            {
                m_IsMoving = false;
            }
            m_Animator.SetBool("isMove", m_IsMoving);
        }
        else
        {
            transform.Translate(Vector3.forward * 1 * Time.deltaTime);
        }

        //transform.Translate(Vector3.forward * 10 * Time.deltaTime*Input.GetAxisRaw("Horizontal"));
        //transform.Rotate(Vector3.up * 100 * Time.deltaTime * Input.GetAxisRaw("Vertical"));
        
    }
}
