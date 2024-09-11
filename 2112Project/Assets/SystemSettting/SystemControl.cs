using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// œµÕ≥…Ë÷√
/// </summary>
public class SystemControl:Singleton<SystemControl>
{
    public GameObject[] Panels; //BasePanel, GraphicPanel, OperatePanel, AudioPanel, CommonPanel;
    public Toggle[] Toggles;
    public Button closeBtn;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        for(int i=0;i<Toggles.Length;i++)
        {
            int index = i;
            Toggles[index].onValueChanged.AddListener((a) =>
            {
                SetPanelActive(index);
            });
        }
    }

    void SetPanelActive(int index)
    {
        for(int i=0;i<Panels.Length;i++)
        {
            if(index!=i)
            {
                Panels[i].SetActive(false);
            }
            else
            {
                Panels[i].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
