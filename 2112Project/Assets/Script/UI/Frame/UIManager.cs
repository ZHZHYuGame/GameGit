using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIPanelType
{
    Bag,            //����
    Shop,           //�̵�
    Equip,          //װ��
    Deal,           //����
    Achievement,    //�ɾ�
    Intensify,      //ǿ��
    Pet,            //����
    Encyclopedia,   //ͼ��
    Skill,          //����
    Vip,            //��ֵ
    Activity,       //�
    Email,          //�ʼ�
    Set,            //����
    Task,           //����
    Chat,           //����
    Map,            //��ͼ
    Wing,           //���
    First           //�׳�
}


[System.Serializable]
public struct PanelPrefabConfig
{
    public UIPanelType type;
    public string name;
    public bool isResident; //�Ƿ�־�
}

public class UIManager : Singleton<UIManager>
{

    //Canvas����
    string _canvasName = "UICanvas/Canvas";

    //ui��������
    string _uiName = "UIPanel";

    //UI�ĸ��� 
    public Transform UIPanel;

    //����UI��Ϣ
    public List<PanelPrefabConfig> _panelConfigList;

    //��ʾ��
    public TipsPanel tip;

    public GameObject _allUI;

    //�������е������Ϣ
    Dictionary<UIPanelType, PanelPrefabConfig> _allPanel = new Dictionary<UIPanelType, PanelPrefabConfig>();

    //����򿪹���� ���ҳ־õ����
    Dictionary<UIPanelType, UIBase> _openPanel = new Dictionary<UIPanelType, UIBase>();


    Stack<UIBase> _uIPanelGroup = new Stack<UIBase>();



    /// <summary>
    /// ��UI
    /// </summary>
    /// <param name="type"></param>
    public void OpenUI(UIPanelType type)
    {
        HideAllPanel();
        if (_openPanel.ContainsKey(type))
        {
            _openPanel[type].OpenUI();
        }
        else
        {
            LoadPanel(type, _allPanel[type].name);
        }
        setAsLastSibling(type);
        _uIPanelGroup.Push(_openPanel[type]);
    }

    void LoadPanel(UIPanelType type, string name)
    {
        //��Դ���ؼ������
        #region ģ��
        var panel = Instantiate(Resources.Load<GameObject>($"UI/{name}"), UIPanel);
        UIBase uiBase = panel.GetComponent<UIBase>();
        _openPanel.Add(type, uiBase);
        #endregion
    }

    /// <summary>
    /// �ر�UI
    /// </summary>
    /// <param name="type"></param>
    public void CloseUI(UIPanelType type)
    {
        if (_openPanel.ContainsKey(type))
        {
            if (_openPanel.Count > 0 && _uIPanelGroup.Count > 0)
                if (_uIPanelGroup.Peek() == _openPanel[type])
                {
                    _uIPanelGroup.Pop();
                    Debug.Log(111);
                    OpenLastPanel();
                    if (_allPanel[type].isResident)
                    {
                        _openPanel[type].HideUI();
                    }
                    else
                    {
                        _openPanel[type].CloseUI();
                        RemovePanel(type);
                    }
                }

        }
    }

    /// <summary>
    /// ���б���ɾ��UI
    /// </summary>
    /// <param name="type"></param>
    void RemovePanel(UIPanelType type)
    {
        if (_openPanel.ContainsKey(type))
        {
            _openPanel.Remove(type);
        }
    }


    void Awake()
    {
        Registration();//ע���������
        DontDestroyOnLoad(gameObject.transform.parent.parent.gameObject);
    }

    /// <summary>
    /// ע�����е����
    /// </summary>
    public void Registration()
    {
        for (int i = 0; i < _panelConfigList.Count; i++)
        {
            LoadAllPanel(_panelConfigList[i].type, _panelConfigList[i]);
        }
        _panelConfigList = null;
    }

    /// <summary>
    /// �����������ص�������Ϣ���ֵ���
    /// </summary>
    /// <param name="type"></param>
    /// <param name="panelConfig"></param>
    private void LoadAllPanel(UIPanelType type, PanelPrefabConfig panelConfig)
    {
        _allPanel.Add(type, panelConfig);
    }

    /// <summary>
    /// ��λ����ʾ
    /// </summary>
    /// <param name="message"></param>
    public void OpenTips(string message)
    {
        OnShowTip(false, false, false, false);
        tip._hint.text = message;

        tip.transform.DOLocalMove(new Vector2(0, 600), 3).OnComplete(() =>
        {
            tip.transform.localPosition = tip._startPos;
            tip.gameObject.SetActive(false);
        });

    }

    /// <summary>
    /// ��ͼƬ��ʾ  ��Ҫ������� ȷ�� 
    /// </summary>
    public void OpenTips(string message, string atlasName, string iconName, string btnText)
    {
        OnShowTip(true, true, true, false);

        tip._hint.text = message;
        tip._leftBtn.GetComponentInChildren<Text>().text = btnText;
        LeftbtnAlignmentCenter();
    }

    /// <summary>
    /// ��ͼƬ��ʾ  ��Ҫ������ť���� 
    /// </summary>
    public void OpenTips(string message, string atlasName, string iconName, string btn1Text, string btn2Text)
    {
        OnShowTip(true, true, true, true);
        tip._hint.text = message;
        tip._leftBtn.GetComponentInChildren<Text>().text = btn1Text;
        tip._rightBtn.GetComponentInChildren<Text>().text = btn2Text;
        BtnAlignmentCenter();
    }

    /// <summary>
    /// tip��嵱�����ػ�����ʾ
    /// </summary>
    /// <param name="type">false ���ı���ʽ</param>
    /// <param name="icon">ͼ��</param>
    /// <param name="leftBtn">��ť</param>
    /// <param name="rightBtn">�Ұ�ť</param>
    private void OnShowTip(bool type, bool icon, bool leftBtn, bool rightBtn)
    {
        if (type)
        {
            tip.transform.DOKill();
            tip.transform.localPosition = tip._startPos;
            tip._backGround.color = new Color(1f, 1f, 1f, 1f);
            tip._hint.alignment = TextAnchor.UpperLeft;
            //AtlasMgr.Ins.Get2D(tip._icon, atlasName, iconName);//��ͼ�� ���ýӿ� ���� Ҫ��ͼƬ��Image ͼ���� ��ͼƬ�� 
            tip._closeBtn.gameObject.SetActive(true);
        }
        else
        {
            tip._backGround.color = new Color(1f, 1f, 1f, 0);
            tip._hint.alignment = TextAnchor.MiddleCenter;
            tip._closeBtn.gameObject.SetActive(false);
        }
        tip.gameObject.SetActive(true);
        tip._icon.gameObject.SetActive(icon);
        tip._leftBtn.gameObject.SetActive(leftBtn);
        tip._rightBtn.gameObject.SetActive(rightBtn);
    }
    /// <summary>
    /// ������ť����
    /// </summary>
    void LeftbtnAlignmentCenter()
    {
        RectTransform needChange = tip._leftBtn.GetComponent<RectTransform>();
        needChange.anchorMin = new Vector2(0.5f, 0);
        needChange.anchorMax = new Vector2(0.5f, 0);
        needChange.pivot = new Vector2(0.5f, 0.5f);
        needChange.anchoredPosition = new Vector2(0, needChange.sizeDelta.y / 2);
    }

    /// <summary>
    /// ��ť��λ
    /// </summary>
    void BtnAlignmentCenter()
    {
        RectTransform needChange = tip._leftBtn.GetComponent<RectTransform>();
        needChange.anchorMin = new Vector2(0, 0);
        needChange.anchorMax = new Vector2(0, 0);
        needChange.pivot = new Vector2(0, 0);
        needChange.anchoredPosition = new Vector2(0, 0);

        RectTransform needChange1 = tip._rightBtn.GetComponent<RectTransform>();
        needChange1.anchorMin = new Vector2(1, 0);
        needChange1.anchorMax = new Vector2(1, 0);
        needChange1.pivot = new Vector2(1, 0);
        needChange1.anchoredPosition = new Vector2(1, 1);
    }



    /// <summary>
    /// ��ȡ�������ϵýű�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="componentPath"></param>
    /// <returns></returns>
    public T getComponent<T>(string componentPath) where T : MonoBehaviour
    {
        if (!componentPath.StartsWith("/"))
        {
            componentPath = "/" + componentPath;
        }

        GameObject go = GameObject.Find($"{_canvasName}/{_uiName}{componentPath}");

        if (go == null)
        {
            Debug.Log($"��ȡGameObjectΪ��,·��Ϊ{_canvasName}/{_uiName}{componentPath}");
        }
        return go.GetComponent<T>();
    }

    /// <summary>
    /// ������ʾ�����ϲ�
    /// </summary>
    /// <param name="go"></param>
    public void setAsLastSibling(UIPanelType type)
    {
        if (_openPanel.ContainsKey(type))
        {
            _openPanel[type].transform.SetAsLastSibling();
        }
    }

    /// <summary>
    ///  ������ʾ�����ϲ�
    /// </summary>
    /// <param name="go"></param>
    public void setAsFirstSibling(UIPanelType type)
    {
        if (_openPanel.ContainsKey(type))
        {
            _openPanel[type].transform.SetAsFirstSibling();
        }
    }


    /// <summary>
    /// �������д����
    /// </summary>
    public void HideAllPanel()
    {
        foreach (var item in _openPanel.Keys)
        {
            _openPanel[item].HideAllPanel();
        }
    }

    /// <summary>
    /// ����һ�����
    /// </summary>
    public void OpenLastPanel()
    {
        if (_uIPanelGroup.Count > 0)
        {
            UIBase uiBase = _uIPanelGroup.Peek();
            if (_openPanel.ContainsValue(uiBase))
            {
                uiBase.OpenUI();
            }
        }
    }

    /// <summary>
    /// ����ս������
    /// </summary>
    public void EnterBattleScene()
    {
        _allUI.SetActive(false);
    }

    /// <summary>
    /// �뿪ս������
    /// </summary>
    public void ExitBattleScene()
    {
        _allUI.SetActive(true);
    }

}