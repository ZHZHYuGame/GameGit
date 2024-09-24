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
    public float RotateSpeed = 50;
    /// <summary>
    /// 旋转半径
    /// </summary>
    private float radius = 3;

    int m_rotateWeaponNum = 0;
    public Transform m_playerControl;
    void Start()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("CombatPrefab/Weapon"), transform);
        m_rotateWeaponNum++;
        go.name = m_rotateWeaponNum.ToString();
        go.SetActive(true);
        RotateLst.Add(go);
        ArrangeInCircle();
    }

    public void ArrangeInCircle()
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

        transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
    }

    public void AddWeapon()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("CombatPrefab/Weapon"), transform);
        go.SetActive(true);
        m_rotateWeaponNum++;
        go.name = m_rotateWeaponNum.ToString();
        RotateLst.Add(go);

        ArrangeInCircle();
    }
}
