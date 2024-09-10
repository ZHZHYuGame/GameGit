using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EHp : MonoBehaviour
{
    // Start is called before the first frame update\
    private Slider m_slider;
    private GameObject m_hpGame;
    private float m_value;
    public Transform target;
    public float m_Value
    {
        get { return m_value; }
    }
    void Start()
    {

        m_value = 100;

        gameObject.SetActive(true);

        m_slider = transform.GetComponent<Slider>();

        //m_slider.value = m_value;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null&& m_hpGame!=null)
        {
            m_hpGame.transform.position = Camera.main.WorldToScreenPoint(target.position);
        }
    }

    public void TargetHp(Transform targets)
    {
        target = targets;
    }
}
