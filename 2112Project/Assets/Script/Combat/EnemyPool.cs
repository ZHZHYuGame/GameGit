using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    // Start is called before the first frame update
    private int Max_Count = 60;
    private List<GameObject>EnemyList=new List<GameObject>();
    int count;
    public Transform EnemyParent;
    void Start()
    {
        InvokeRepeating("GrowEnemy", 2, 5);
        for(int i=0;i<Max_Count; i++)
        {
            EnemyList.Add(Instantiate(Resources.Load<GameObject>("CombatPrefab/Enemy"), EnemyParent));
            EnemyList[i].AddComponent<EnemyAI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GrowEnemy()
    {
        for(int i=0;i<Max_Count; i++)
        {
            if (!EnemyList[i].GetComponent<EnemyControl>().GetUsed())
            {
                Vector3 ranPos = UnityEngine.Random.insideUnitSphere * 50;
                EnemyList[i].GetComponent<EnemyControl>().Init(true, Vector3.one, new Vector3(ranPos.x, 0, ranPos.z));
                count++;
                if (count == 30)
                {
                    count = 0;
                    return;
                }
                
            }
        }
        for(int i = 0; i < 30 - count; i++)
        {
            AddEnemy();
        }
        count = 0;
    }

    private void AddEnemy()
    {
        EnemyList.Add(Instantiate(Resources.Load<GameObject>("CombatPrefab/Enemy"), EnemyParent));
        ++Max_Count;
    }
}
