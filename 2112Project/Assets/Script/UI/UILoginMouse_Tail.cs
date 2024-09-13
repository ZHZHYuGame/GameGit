using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoginMouse_Tail : MonoBehaviour
{
    public float distance = 10f;
    public Vector3 offset = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

    }
    Vector3 newPosition;
    // Update is called once per frame
    void Update()
    {
        if (Camera.main == null) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        newPosition = ray.GetPoint(distance);
        transform.position = newPosition + offset;
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Login/CFXR Hit D 3D (Yellow)"));
            go.transform.position = newPosition ;
            Destroy(go, 0.5f);
        }
    }
}
