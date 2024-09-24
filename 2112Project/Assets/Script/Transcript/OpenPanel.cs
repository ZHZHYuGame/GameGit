using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.OpenUI(UIPanelType.Map);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
