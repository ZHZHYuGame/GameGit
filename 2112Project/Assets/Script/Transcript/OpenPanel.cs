using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public GameObject attackpanel;
    public Transform count;
    // Start is called before the first frame update
    void Start()
    {
        GameObject transcriptpanel = Instantiate(Resources.Load<GameObject>("TranscriptSetPanel"),count,false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
