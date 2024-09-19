using DG.Tweening;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public enum UIPanelType
{
    Bag = 1001,            //����
    Shop = 1002,           //�̵�
    Equip = 1003,          //װ��
    Deal = 1004,           //����
    Achievement = 1005,    //�ɾ�
    Intensify = 1006,      //ǿ��
    Pet = 1007,            //����
    Encyclopedia = 1008,   //ͼ��
    Skill = 1009,          //����
    Vip = 1010,            //��ֵ
    Activity = 1011,       //�
    Email = 1012,          //�ʼ�
    Set = 1013,            //����
    Task = 1014,           //����
    Chat = 1015,           //����
    Map = 1016,            //��ͼ
    Wing = 1017,           //���
    First = 1018           //�׳�
}

public enum CanvasType
{
    BackGround = 0,     //����
    UI = 1,             //UI
    Tip = 2,            //��ʾ��
    Mask = 3,           //����/����
    Prefab = 4          //3D��
}


public class PanelPrefabConfig
{
    public int id;
    public string name;
    public bool isResident; //�Ƿ�־�F
}

public class UIManager : Singleton<UIManager>
{

    //Canvas����
    string _canvasName = "UICanvas/Canvas";

    //ui��������
    string _uiName = "UIPanel";

    //UI�ĸ��� 
    public Transform UIPanel;

    //���а�ť���ڵ�
    public GameObject _btnParent;

    //���в㼶
    public List<Transform> _allCanvas = new List<Transform>();

    //�������
    public Text _moneyNum;

    List<PanelPrefabConfig> panelPrefabs = new List<PanelPrefabConfig>();

    //�������е������Ϣ
    Dictionary<int, PanelPrefabConfig> _allPanel = new Dictionary<int, PanelPrefabConfig>();

    //����򿪹���� ���ҳ־õ����
    Dictionary<UIPanelType, UIBase> _openPanel = new Dictionary<UIPanelType, UIBase>();


    Stack<UIBase> _uIPanelGroup = new Stack<UIBase>();


    Stack<GameObject> _textPrompr = new Stack<GameObject>();//�ı���ʾ������

    //��ʾ��
    TipsPanel tip;

    //��Ʒˢ���ǵ���ί��
    public Action MsgUpdate;

    void Awake()
    {
        Debug.Log(UIPanelType.Set.ToString());
        Registration();//ע���������
        //OpenAllPanel();//���������
        DontDestroyOnLoad(transform.parent.gameObject);
    }

    

    private void OpenAllPanel()
    {
        foreach (var item in _allPanel.Keys)
        {
            OpenUI((UIPanelType)_allPanel[item].id);
        }

        foreach (var item in _allPanel.Keys)
        {
            _openPanel[(UIPanelType)_allPanel[item].id].HideUI();
        }

    }

    /// <summary>
    /// ע�����е����
    /// </summary>
    public void Registration()
    {
        string myuimsg = File.ReadAllText($"{Application.dataPath}/Resources/myuimsg.json");
        panelPrefabs = JsonConvert.DeserializeObject<List<PanelPrefabConfig>>(myuimsg);
        for (int i = 0; i < panelPrefabs.Count; i++)
        {
            LoadAllPanel(panelPrefabs[i].id, panelPrefabs[i]);
        }
    }

    /// <summary>
    /// �����������ص�������Ϣ���ֵ���
    /// </summary>
    /// <param name="type"></param>
    /// <param name="panelConfig"></param>
    private void LoadAllPanel(int id, PanelPrefabConfig panelConfig)
    {
        _allPanel.Add(id, panelConfig);
    }

    /// <summary>
    /// ��UI
    /// </summary>
    /// <param name="type"></param>
    public void OpenUI(UIPanelType type)
    {
        if (_openPanel.ContainsKey(type))
        {
            _openPanel[type].OpenUI();
        }
        else
        {
            LoadPanel(type);
        }
    }

    /// <summary>
    /// ��������� 
    /// </summary>
    /// <param name="type">�Ǹ����õ�</param>
    /// <param name="otherType">Ҫ���Ǹ����</param>
    public void OpenOtherUI(UIPanelType type, UIPanelType otherType)
    {
        if (_openPanel.ContainsKey(type))
        {
            _uIPanelGroup.Push(_openPanel[type]);
            _openPanel[type].HideUI();
        }
        OpenUI(otherType);
    }

    /// <summary>
    /// ��������Ƿ񳤾ô���
    /// </summary>
    public void SetPanelResident(UIPanelType type)
    {
        _allPanel[(int)type].isResident = true;
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="type"></param>
    void LoadPanel(UIPanelType type)
    {
        //��Դ���ؼ������

        #region ģ��
        var panel = Instantiate(Resources.Load<GameObject>($"UI/{_allPanel[(int)type].name}"), UIPanel);
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
            if (_allPanel[(int)type].isResident)
            {
                Debug.Log("111");
                _openPanel[type].HideUI();
            }
            else
            {
                _openPanel[type].CloseUI();
                RemovePanel(type);
            }
        }

        if (_uIPanelGroup.Count > 0)
        {
            UIBase ui = _uIPanelGroup.Pop();
            ui.OpenUI();
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



    /// <summary>
    /// �ı���ʾ��
    /// </summary>
    /// <param name="msg"></param>
    public void OpenTextPrompt(string msg, CanvasType type = CanvasType.Tip)
    {
        if (_textPrompr.Count <= 0)
        {
            #region ����
            GameObject textPrompr = Instantiate(Resources.Load<GameObject>("UI/TextPrompt"), _allCanvas[(int)type]);
            _textPrompr.Push(textPrompr);
            #endregion
        }

        GameObject text = _textPrompr.Pop();
        text.GetComponent<Text>().text = msg;
        text.transform.localPosition = Vector3.zero;

        StartCoroutine(RecycleText(text));
        //TimeManager.Instance.DoFrameOnce(3,RecycleText(text));
    }

    /// <summary>
    /// ����ƶ���������������ʾ����ʾ��
    /// </summary>
    /// <param name="saName">ͼ������</param>
    /// <param name="spName">ͼƬ����</param>
    /// <param name="name">��������</param>
    /// <param name="des">��������</param>
    /// <param name="price">��Ʒ�۸�</param>
    /// <param name="quality">��ƷƷ��</param>
    /// <param name="pos">���λ��</param>
    public void OpenMouseStayTip(string saName, string spName, string name, string des, string price, string quality, Vector2 pos, CanvasType type = CanvasType.Tip)
    {
        if (tip == null)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Tip"), _allCanvas[(int)type]);
            tip = obj.GetComponent<TipsPanel>();
        }
        TipButtonHide();
        tip._backGround.transform.position = pos;//����Ϊ����λ��
        if (!string.IsNullOrEmpty(saName) && !string.IsNullOrEmpty(spName))
        {
            AtlasMgr.Ins.Set2D(tip._icon, saName, spName); //ͼƬ��ֵ
        }
        tip._name.text = name;
        tip._des.text = des;
        tip._price.text = price;
        tip._quality.text = quality;
    }

    /// <summary>
    /// �򿪵�����ť��ʾ
    /// </summary>
    /// <param name="saName">ͼ������</param>
    /// <param name="spName">ͼƬ����</param>
    /// <param name="name">��������</param>
    /// <param name="des">��������</param>
    /// <param name="price">��Ʒ�۸�</param>
    /// <param name="quality">��ƷƷ��</param>
    /// <param name="leftButton">��ť��Ϣ ��ȷ�ϻ���ȡ�����߱�ģ�</param>
    public void OpensASinglePrompt(string saName, string spName, string name, string des, string price, string quality, string leftButton, CanvasType type = CanvasType.Tip)
    {
        if (tip == null)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Tip"), _allCanvas[(int)type]);
            tip = obj.GetComponent<TipsPanel>();
        }

        TipAButtonShow();
        LeftbtnAlignmentCenter();
        if (!string.IsNullOrEmpty(saName) && !string.IsNullOrEmpty(spName))
        {
            AtlasMgr.Ins.Set2D(tip._icon, saName, spName); //ͼƬ��ֵ
        }
        tip._name.text = name;
        tip._des.text = des;
        tip._price.text = price;
        tip._quality.text = quality;
        tip._affirm.GetComponentInChildren<Text>().text = leftButton;
    }


    /// <summary>
    /// ��������ʾ
    /// </summary>
    /// <param name="saName">ͼ������</param>
    /// <param name="spName">ͼƬ����</param>
    /// <param name="name">��������</param>
    /// <param name="des">��������</param>
    /// <param name="price">��Ʒ�۸�</param>
    /// <param name="quality">��ƷƷ��</param>
    /// <param name="leftButton">��ť��Ϣ ��ȷ�ϻ���ȡ�����߱�ģ�</param>
    public void OpenAllSingleprompt(string saName, string spName, string name, string des, string price, string quality, string leftButton, string rightButton, CanvasType type = CanvasType.Tip)
    {
        if (tip == null)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Tip"), _allCanvas[(int)type]);
            tip = obj.GetComponent<TipsPanel>();
        }
        TipButtonShow();
        BtnAlignmentCenter();
        if (!string.IsNullOrEmpty(saName) && !string.IsNullOrEmpty(spName))
        {
            AtlasMgr.Ins.Set2D(tip._icon, saName, spName); //ͼƬ��ֵ
        }
        tip._name.text = name;
        tip._des.text = des;
        tip._price.text = price;
        tip._quality.text = quality;
        tip._affirm.GetComponentInChildren<Text>().text = leftButton;
    }

    void TipButtonHide()
    {
        tip._affirm.gameObject.SetActive(false);
        tip._cancel.gameObject.SetActive(false);
    }

    void TipAButtonShow()
    {
        tip._affirm.gameObject.SetActive(true);
        tip._cancel.gameObject.SetActive(false);
    }

    void TipButtonShow()
    {
        tip._affirm.gameObject.SetActive(true);
        tip._cancel.gameObject.SetActive(true);
    }

    public void CloseTip()
    {
        if (tip != null)
        {
            tip.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ���� 
    /// </summary>
    IEnumerator RecycleText(GameObject text)
    {
        yield return new WaitForSecondsRealtime(3);
        text.transform.localPosition = Vector3.one * 1000;
        _textPrompr.Push(text);

    }


    /// <summary>
    /// ������ť����
    /// </summary>
    void LeftbtnAlignmentCenter()
    {
        RectTransform needChange = tip._affirm.GetComponent<RectTransform>();
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
        RectTransform needChange = tip._affirm.GetComponent<RectTransform>();
        needChange.anchorMin = new Vector2(0, 0);
        needChange.anchorMax = new Vector2(0, 0);
        needChange.pivot = new Vector2(0, 0);
        needChange.anchoredPosition = new Vector2(0, 0);

        RectTransform needChange1 = tip._cancel.GetComponent<RectTransform>();
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
    public T GetComponent<T>(string componentPath) where T : MonoBehaviour
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
    public void SetAsLastSibling(UIPanelType type)
    {
        if (_openPanel.ContainsKey(type))
        {
            _openPanel[type].transform.SetAsLastSibling();
        }
    }

    public void GetSprit()
    {

    }


    /// <summary>
    ///  ������ʾ�����ϲ�
    /// </summary>
    /// <param name="go"></param>
    public void SetAsFirstSibling(UIPanelType type)
    {
        if (_openPanel.ContainsKey(type))
        {
            _openPanel[type].transform.SetAsFirstSibling();
        }
    }

    /// <summary>
    /// ������Ϸ����ʱ���� 
    /// </summary>
    public void EnterGameScene()
    {
        _btnParent.SetActive(false);
    }

    /// <summary>
    /// �˳���Ϸ����ʱ���� 
    /// </summary>
    public void ExitGameScene()
    {
        _btnParent.SetActive(true);
    }

    /// <summary>
    /// ���ý������
    /// </summary>
    /// <param name="num"></param>
    public void SetMoney(int num)
    {
        _moneyNum.text = $"{num}";
    }

}