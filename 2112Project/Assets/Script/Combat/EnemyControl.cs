using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    private Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyHurt(float atk)
    {
        m_Animator.SetTrigger("Hurt");
    }
}
