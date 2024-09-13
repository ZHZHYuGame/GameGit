using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMgr : MonoBehaviour
{
    /// <summary>
    /// 击杀数量,被动技能
    /// </summary>
    private int m_skillNum = 1;

    private PropertiesMain m_Properties;
    // Start is called before the first frame update
    void Start()
    {
        m_Properties = GameObject.Find("Canvas/PropertiesPanel").GetComponent<PropertiesMain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeiDongSkill()
    {
        m_skillNum += 1;
        m_Properties.SetSkillNum(m_skillNum);
    }
}
