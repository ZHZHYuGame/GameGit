using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attackpanel : MonoBehaviour
{
    public Text datatext;
    float times = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        times += Time.deltaTime;
        if (times >= 2)
        {
            datatext.gameObject.SetActive(false);
            times = 0;
        }
    }
}
