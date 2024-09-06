using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ETCMove : MonoBehaviour,IDragHandler,IEndDragHandler
{
    private float r = 100;
    private Vector3 m_start;
    private RectTransform m_them;
    private void Start()
    {
        m_start = transform.position;
        m_them = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Vector3.ClampMagnitude(Input.mousePosition - m_start, r) + m_start;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = m_start;
    }

    public float GetMovePos(string name)
    {
        switch (name)
        {
            case "H":
                return m_them.anchoredPosition.x / r;
            case "V":
                return m_them.anchoredPosition.y / r;
            default:
                return 0;
        }
    }
}
