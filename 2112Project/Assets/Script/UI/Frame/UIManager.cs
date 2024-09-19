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
    Bag = 1001,            //背包
    Shop = 1002,           //商店
    Equip = 1003,          //装备
    Deal = 1004,           //交易
    Achievement = 1005,    //成就
    Intensify = 1006,      //强化
    Pet = 1007,            //宠物
    Encyclopedia = 1008,   //图鉴
    Skill = 1009,          //技能
    Vip = 1010,            //充值
    Activity = 1011,       //活动
    Email = 1012,          //邮件
    Set = 1013,            //设置
    Task = 1014,           //任务
    Chat = 1015,           //聊天
    Map = 1016,            //地图
    Wing = 1017,           //翅膀
    First = 1018           //首充
}

public enum CanvasType
{
    BackGround = 0,     //背景
    UI = 1,             //UI
    Tip = 2,            //提示框
    Mask = 3,           //遮罩/引导
    Prefab = 4          //3D层
}


public class PanelPrefabConfig
{
    public int id;
    public string name;
    public bool isResident; //是否持久F
}

public class UIManager : Singleton<UIManager>
{

    //Canvas名字
    string _canvasName = "UICanvas/Canvas";

    //ui父类名字
    string _uiName = "UIPanel";

    //UI的父类 
    public Transform UIPanel;

    //所有按钮父节点
    public GameObject _btnParent;

    //所有层级
    public List<Transform> _allCanvas = new List<Transform>();

    //金币数量
    public Text _moneyNum;

    List<PanelPrefabConfig> panelPrefabs = new List<PanelPrefabConfig>();

    //储存所有的面板信息
    Dictionary<int, PanelPrefabConfig> _allPanel = new Dictionary<int, PanelPrefabConfig>();

    //储存打开过面板 并且持久的面板
    Dictionary<UIPanelType, UIBase> _openPanel = new Dictionary<UIPanelType, UIBase>();


    Stack<UIBase> _uIPanelGroup = new Stack<UIBase>();


    Stack<GameObject> _textPrompr = new Stack<GameObject>();//文本提示框对象池

    //提示框
    TipsPanel tip;

    //物品刷新是调用委托
    public Action MsgUpdate;

    void Awake()
    {
        Debug.Log(UIPanelType.Set.ToString());
        Registration();//注册所有面板
        //OpenAllPanel();//打开所有面板
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
    /// 注册所有的面板
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
    /// 将所有面板加载到所有信息的字典中
    /// </summary>
    /// <param name="type"></param>
    /// <param name="panelConfig"></param>
    private void LoadAllPanel(int id, PanelPrefabConfig panelConfig)
    {
        _allPanel.Add(id, panelConfig);
    }

    /// <summary>
    /// 打开UI
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
    /// 打开其他面板 
    /// </summary>
    /// <param name="type">那个调用的</param>
    /// <param name="otherType">要打开那个面板</param>
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
    /// 设置面板是否长久存在
    /// </summary>
    public void SetPanelResident(UIPanelType type)
    {
        _allPanel[(int)type].isResident = true;
    }

    /// <summary>
    /// 加载面板
    /// </summary>
    /// <param name="type"></param>
    void LoadPanel(UIPanelType type)
    {
        //资源加载加载面板

        #region 模拟
        var panel = Instantiate(Resources.Load<GameObject>($"UI/{_allPanel[(int)type].name}"), UIPanel);
        UIBase uiBase = panel.GetComponent<UIBase>();
        _openPanel.Add(type, uiBase);
        #endregion
    }

    /// <summary>
    /// 关闭UI
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
    /// 在列表里删除UI
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
    /// 文本提示框
    /// </summary>
    /// <param name="msg"></param>
    public void OpenTextPrompt(string msg, CanvasType type = CanvasType.Tip)
    {
        if (_textPrompr.Count <= 0)
        {
            #region 测试
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
    /// 鼠标移动到物体身上所显示的提示框
    /// </summary>
    /// <param name="saName">图集名字</param>
    /// <param name="spName">图片名字</param>
    /// <param name="name">物体名称</param>
    /// <param name="des">物体描述</param>
    /// <param name="price">物品价格</param>
    /// <param name="quality">物品品质</param>
    /// <param name="pos">鼠标位置</param>
    public void OpenMouseStayTip(string saName, string spName, string name, string des, string price, string quality, Vector2 pos, CanvasType type = CanvasType.Tip)
    {
        if (tip == null)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Tip"), _allCanvas[(int)type]);
            tip = obj.GetComponent<TipsPanel>();
        }
        TipButtonHide();
        tip._backGround.transform.position = pos;//更改为鼠标的位置
        if (!string.IsNullOrEmpty(saName) && !string.IsNullOrEmpty(spName))
        {
            AtlasMgr.Ins.Set2D(tip._icon, saName, spName); //图片赋值
        }
        tip._name.text = name;
        tip._des.text = des;
        tip._price.text = price;
        tip._quality.text = quality;
    }

    /// <summary>
    /// 打开单个按钮提示
    /// </summary>
    /// <param name="saName">图集名字</param>
    /// <param name="spName">图片名字</param>
    /// <param name="name">物体名称</param>
    /// <param name="des">物体描述</param>
    /// <param name="price">物品价格</param>
    /// <param name="quality">物品品质</param>
    /// <param name="leftButton">按钮信息 （确认还是取消或者别的）</param>
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
            AtlasMgr.Ins.Set2D(tip._icon, saName, spName); //图片赋值
        }
        tip._name.text = name;
        tip._des.text = des;
        tip._price.text = price;
        tip._quality.text = quality;
        tip._affirm.GetComponentInChildren<Text>().text = leftButton;
    }


    /// <summary>
    /// 打开所有提示
    /// </summary>
    /// <param name="saName">图集名字</param>
    /// <param name="spName">图片名字</param>
    /// <param name="name">物体名称</param>
    /// <param name="des">物体描述</param>
    /// <param name="price">物品价格</param>
    /// <param name="quality">物品品质</param>
    /// <param name="leftButton">按钮信息 （确认还是取消或者别的）</param>
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
            AtlasMgr.Ins.Set2D(tip._icon, saName, spName); //图片赋值
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
    /// 测试 
    /// </summary>
    IEnumerator RecycleText(GameObject text)
    {
        yield return new WaitForSecondsRealtime(3);
        text.transform.localPosition = Vector3.one * 1000;
        _textPrompr.Push(text);

    }


    /// <summary>
    /// 单个按钮居中
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
    /// 按钮归位
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
    /// 获取对象身上得脚本
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
            Debug.Log($"获取GameObject为空,路径为{_canvasName}/{_uiName}{componentPath}");
        }
        return go.GetComponent<T>();
    }

    /// <summary>
    /// 设置显示在最上层
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
    ///  设置显示在最上层
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
    /// 进入游戏场景时加载 
    /// </summary>
    public void EnterGameScene()
    {
        _btnParent.SetActive(false);
    }

    /// <summary>
    /// 退出游戏场景时加载 
    /// </summary>
    public void ExitGameScene()
    {
        _btnParent.SetActive(true);
    }

    /// <summary>
    /// 设置金币数量
    /// </summary>
    /// <param name="num"></param>
    public void SetMoney(int num)
    {
        _moneyNum.text = $"{num}";
    }

}