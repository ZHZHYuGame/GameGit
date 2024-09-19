using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    private Animator m_Animator;
    private bool isUsed = false;
    private float _atkSpeed = 0.5f;

    /// <summary>
    /// 持有技能类
    /// </summary>
    private SkillMgr m_SkillMgr;
    // Start is called before the first frame update
    void Start()
    {
        m_SkillMgr=GameObject.Find("Canvas/Skills").GetComponent<SkillMgr>();
        print(m_SkillMgr.name);
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
        //死亡例子特效待优化
        GameObject go = Instantiate(Resources.Load<GameObject>("CombatPrefab/Effects/Leaves PS"));
        go.transform.position = transform.position + Vector3.up * 2;
        Destroy(go, 0.5f);
        isUsed = false;
        this.transform.localScale = Vector3.zero;
        this.transform.position = new Vector3(999, 999, 999);
        

        m_SkillMgr.AddBeiDongSkill();
    }
    public void Attack()
    {
        _atkSpeed -= Time.deltaTime;
        if (_atkSpeed<=0)
        {
            _atkSpeed = 0.5f;
            m_Animator.SetTrigger("Atk");
            m_Animator.SetInteger("AtkCount",1);
        }
    }

    public void AttackOver()
    {
        print("敌人攻击结束");
    }
}
