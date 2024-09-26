using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ϵͳ����
/// </summary>
public class SystemControl : UIBase
{
    public GameObject[] Panels; //BasePanel, GraphicPanel, OperatePanel, AudioPanel, CommonPanel;
    public Toggle[] Toggles;
    public Button closeBtn;

    /// <summary>
    /// ��Ϸ��ʼ���ȸ������� ��������ʼ������
    /// </summary>
    public void Init()
    {

    }

    void SetPanelActive(int index)
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if (index != i)
            {
                Panels[i].SetActive(false);
            }
            else
            {
                Panels[i].SetActive(true);
            }
        }
    }


    public override void OpenUI()
    {
        base.OpenUI();
    }

    public override void CloseUI()
    {
        base.CloseUI();
    }

    public override void HideUI()
    {
        base.HideUI();
    }

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Start()
    {
        base.Start();
        closeBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        for (int i = 0; i < Toggles.Length; i++)
        {
            int index = i;
            Toggles[index].onValueChanged.AddListener((a) =>
            {
                SetPanelActive(index);
            });
        }
        Assembly ass = Assembly.Load(Application.streamingAssetsPath);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }
}
