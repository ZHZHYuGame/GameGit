using System.Collections.Generic;
using UnityEngine;

public class RotateCenter : MonoBehaviour
{
    // Start is called before the first frame update
    public int RotateCount = 1;
    public List<GameObject> RotateLst = new List<GameObject>();
    /// <summary>
    /// 旋转速度
    /// </summary>
    private int m_rotateSpeed = 200;
    /// <summary>
    /// 旋转半径
    /// </summary>
    private float radius = 3;

    Vector3 m_dir;

    public Transform m_playerControl;
    void Start()
    {
        for (int i = 0; i < RotateCount; i++)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("CombatPrefab/Weapon"), transform);
            go.SetActive(true);
            RotateLst.Add(go);
        }
        
        ArrangeInCircle();
    }

    void ArrangeInCircle()
    {
        for (int i = 0; i < RotateLst.Count; i++)
        {
            float angle = i * 360.0f / RotateLst.Count;
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
            RotateLst[i].transform.position = transform.position + offset;
            RotateLst[i].transform.LookAt(transform.position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = m_playerControl.transform.position;

        transform.Rotate(Vector3.up* m_rotateSpeed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("CombatPrefab/Weapon"), transform);
            go.SetActive(true);
            RotateLst.Add(go);
            ArrangeInCircle();
        }
    }
}
