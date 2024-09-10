using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScene : MonoBehaviour
{
    private int m_AllWave = 20;//�ܹ�����
    private int m_WaveNum = 30;//һ������
    private float m_Timer = 5;//ÿ�����

    private Transform EnemyParent;
    void Start()
    {
        EnemyParent = transform.Find("EnemyParent").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //�������ɵ���
        if (m_AllWave > 0)
        {
            m_Timer -= Time.deltaTime;
            if (m_Timer <= 0)
            {
                m_Timer = 5;
                for (int i = 0; i < m_WaveNum; i++)
                {
                    GameObject e = Instantiate(Resources.Load<GameObject>("CombatPrefab/Enemy"), EnemyParent);
                    e.AddComponent<EnemyAI>();
                    Vector3 ranPos= UnityEngine.Random.insideUnitSphere * 50;
                    e.transform.position = new Vector3(ranPos.x, 0, ranPos.z);
                }
                m_AllWave--;
            }
        }
    }
}
