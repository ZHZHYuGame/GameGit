using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainUI : MonoBehaviour
{
    public Button transcriptbutt,close,confirm;
    public Dropdown mapdro;
    public GameObject transcriptSetPanel;
    public GameObject video1,video2,video3,video4;
    private void Awake()
    {
        transcriptSetPanel.transform.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        transcriptbutt.onClick.AddListener(() =>
        {
            transcriptSetPanel.transform.gameObject.SetActive(true);
            transcriptbutt.gameObject.SetActive(false); 
        });
        close.onClick.AddListener(() =>
        {
            transcriptSetPanel.transform.gameObject.SetActive(false);
        });
        mapdro.onValueChanged.AddListener((maptypeindex) =>
        {
            if(maptypeindex == 0)
            {
                video1.gameObject.SetActive(true);
            }
            if(maptypeindex == 1)
            {
                video2.gameObject.SetActive(true);
            }
            if(maptypeindex == 2)
            {
                video3.gameObject.SetActive(true);
            }
            if (maptypeindex == 3)
            {
                video4.gameObject.SetActive(true);
            }
        });
        confirm.onClick.AddListener(() =>
        {
            transcriptSetPanel.transform.gameObject.SetActive(false);
        });
    }
}
