using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenClosePanel : MonoBehaviour
{
    public Button _openCloseBtn;

    public UIPanelType _panelType;

    void Start()
    {
        _openCloseBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI(_panelType);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
