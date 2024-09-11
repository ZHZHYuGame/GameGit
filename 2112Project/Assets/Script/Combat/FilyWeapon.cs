using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilyWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform m_EndFilyWeapon;
    private RotateCenter rotateCenter;
    private float delTimer = 0.3f; 
    public void PlayerPos(Transform end, RotateCenter rotateMgr)
    {
        m_EndFilyWeapon = end;
        rotateCenter= rotateMgr;
        transform.localScale = new Vector3(2, 2, 2);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(m_EndFilyWeapon);
        transform.position = Vector3.Lerp(transform.position, m_EndFilyWeapon.position, Time.deltaTime * 10);
        delTimer -= Time.deltaTime;
        if (delTimer <= 0)
        {
            delTimer = 0.3f;
            for (int i = 0; i < rotateCenter.RotateLst.Count; i++)
            {
                if (gameObject.name == rotateCenter.RotateLst[i].name)
                {
                    Destroy(gameObject);
                    Debug.Log(gameObject.name + "----" + rotateCenter.RotateLst[i].name);
                    rotateCenter.RotateLst.RemoveAt(i);
                }
            }
        }
            

            
        
    }
}
