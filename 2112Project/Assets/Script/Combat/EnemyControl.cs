using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    private Animator m_Animator;
    private bool isUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Init(bool _isUsed,Vector3 _scale,Vector3 _pos)
    {
        isUsed= _isUsed;
        this.transform.localScale= _scale;
        this.transform.position= _pos;
    }

    public bool GetUsed()
    {
        return isUsed;
    }
    public void EnemyHurt(float atk)
    {
        m_Animator.SetTrigger("Hurt");
        m_Animator.SetTrigger("Die");
    }

    public void EnemyDie()
    {
        isUsed = false;
        this.transform.localScale = Vector3.zero;
        this.transform.position = new Vector3(999, 999, 999);
    }
}
