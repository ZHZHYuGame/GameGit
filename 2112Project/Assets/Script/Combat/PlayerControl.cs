using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��ɫ���ƽű�
/// </summary>
public class PlayerControl : MonoBehaviour
{
    /// <summary>
    /// ����ҡ������
    /// </summary>
    private ETCMove m_EtcMove;
    /// <summary>
    /// �Ƿ����ƶ����Ŷ���
    /// </summary>
    private bool m_IsMoving;
    private Animator m_Animator;
    /// <summary>
    /// ���м�����
    /// </summary>
    private int m_atkCount;
    /// <summary>
    /// ���м�ʱ��
    /// </summary>
    private float m_atkTimer;
    private float m_interval = 2f;
    /// <summary>
    /// �Ƿ����ڹ���
    /// </summary>
    public bool isAtk;
    /// <summary>
    ///��ת����
    /// </summary>
    public GameObject rotateWeapon;
    /// <summary>
    /// ��ת�ٶ�
    /// </summary>
    private float m_rotateSpeed = 300f;
    /// <summary>
    /// ��ת�뾶
    /// </summary>
    private float m_distance = 3;
    private Vector3 m_dir;

    private HandWeapon m_handWeapon;

    void Start()
    {
        rotateWeapon.SetActive(true);
        m_EtcMove = GameObject.Find("Canvas/ETCParent/ETC").GetComponent<ETCMove>();
        m_Animator = GetComponent<Animator>();
        m_handWeapon=GetComponentInChildren<HandWeapon>();
        m_dir = rotateWeapon.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        //rotateWeapon.transform.eulerAngles = new Vector3(0,180,0);
        rotateWeapon.transform.LookAt(transform.position);
        rotateWeapon.transform.position = transform.position + m_dir.normalized * m_distance;
        rotateWeapon.transform.RotateAround(transform.position, Vector3.up, m_rotateSpeed * Time.deltaTime);
        m_dir=rotateWeapon.transform.position-transform.position;
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

        //�����л�ʱ��
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
    /// ��������ִ�н����Ļص�
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
            float H = m_EtcMove.GetMovePos("H");
            float V = m_EtcMove.GetMovePos("V");
            Vector3 pos = new Vector3(H, 0, V);
            if (pos != Vector3.zero)
            {
                transform.LookAt(pos + transform.position);
                transform.Translate(Vector3.forward * (3 * Time.deltaTime));
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
        
    }
}
