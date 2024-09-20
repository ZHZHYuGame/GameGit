using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIPanelType
{
    Bag,            //背包
    Shop,           //商店
    Equip,          //装备
    Deal,           //交易
    Achievement,    //成就
    Intensify,      //强化
    Pet,            //宠物
    Encyclopedia,   //图鉴
    Skill,          //技能
    Vip,            //充值
    Activity,       //活动
    Email,          //邮件
    Set,            //设置
    Task,           //任务
    Chat,           //聊天
    Map,            //地图
    Wing,           //翅膀
    First           //首充
}


[System.Serializable]
public struct PanelPrefabConfig
{
    public UIPanelType type;
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

    //所有UI信息
    public List<PanelPrefabConfig> _panelConfigList;

    //提示框
    public TipsPanel tip;

    //储存所有的面板信息
    Dictionary<UIPanelType, PanelPrefabConfig> _allPanel = new Dictionary<UIPanelType, PanelPrefabConfig>();

    //储存打开过面板 并且持久的面板
    Dictionary<UIPanelType, UIBase> _openPanel = new Dictionary<UIPanelType, UIBase>();


    Stack<UIBase> _uIPanelGroup = new Stack<UIBase>();


<<<<<<< HEAD
=======
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
>>>>>>> ba83aeb45bbc05ddbf6f5529b3976dcab27dac40

    /// <summary>
    /// 打开UI
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
        //资源加载加载面板
        #region 模拟
        var panel = Instantiate(Resources.Load<GameObject>($"UI/{name}"), UIPanel);
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


    void Awake()
    {
        Registration();//注册所有面板
        DontDestroyOnLoad(gameObject.transform.parent.parent.gameObject);
    }

    /// <summary>
    /// 注册所有的面板
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
    /// 将所有面板加载到所有信息的字典中
    /// </summary>
    /// <param name="type"></param>
    /// <param name="panelConfig"></param>
    private void LoadAllPanel(UIPanelType type, PanelPrefabConfig panelConfig)
    {
<<<<<<< HEAD
        _allPanel.Add(type, panelConfig);
=======
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
>>>>>>> ba83aeb45bbc05ddbf6f5529b3976dcab27dac40
    }

    /// <summary>
    /// 单位本提示
    /// </summary>
<<<<<<< HEAD
    /// <param name="message"></param>
    public void OpenTips(string message)
=======
    /// <param name="saName">图集名字</param>
    /// <param name="spName">图片名字</param>
    /// <param name="name">物体名称</param>
    /// <param name="des">物体描述</param>
    /// <param name="price">物品价格</param>
    /// <param name="quality">物品品质</param>
    /// <param name="leftButton">按钮信息 （确认还是取消或者别的）</param>
    public void OpenAllSingleprompt(string saName, string spName, string name, string des, string price, string quality, string leftButton, string rightButton, CanvasType type = CanvasType.Tip)
>>>>>>> ba83aeb45bbc05ddbf6f5529b3976dcab27dac40
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
    /// 带图片提示  需要点击操作 确认 
    /// </summary>
    public void OpenTips(string message, string atlasName, string iconName, string btnText)
    {
        OnShowTip(true, true, true, false);

        tip._hint.text = message;
        tip._leftBtn.GetComponentInChildren<Text>().text = btnText;
        LeftbtnAlignmentCenter();
    }

    /// <summary>
    /// 带图片提示  需要两个按钮操作 
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
    /// tip面板当中隐藏或者显示
    /// </summary>
    /// <param name="type">false 单文本形式</param>
    /// <param name="icon">图像</param>
    /// <param name="leftBtn">左按钮</param>
    /// <param name="rightBtn">右按钮</param>
    private void OnShowTip(bool type, bool icon, bool leftBtn, bool rightBtn)
    {
        if (type)
        {
            tip.transform.DOKill();
            tip.transform.localPosition = tip._startPos;
            tip._backGround.color = new Color(1f, 1f, 1f, 1f);
            tip._hint.alignment = TextAnchor.UpperLeft;
            //AtlasMgr.Ins.Get2D(tip._icon, atlasName, iconName);//向图集 调用接口 传入 要赋图片的Image 图集名 和图片名 
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
    /// 单个按钮居中
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
    /// 按钮归位
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
    /// 获取对象身上得脚本
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
            Debug.Log($"获取GameObject为空,路径为{_canvasName}/{_uiName}{componentPath}");
        }
        return go.GetComponent<T>();
    }

    /// <summary>
    /// 设置显示在最上层
    /// </summary>
    /// <param name="go"></param>
    public void setAsLastSibling(UIPanelType type)
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
    public void setAsFirstSibling(UIPanelType type)
    {
        if (_openPanel.ContainsKey(type))
        {
            _openPanel[type].transform.SetAsFirstSibling();
        }
    }


    /// <summary>
    /// 隐藏所有打开面板
    /// </summary>
    public void HideAllPanel()
    {
        foreach (var item in _openPanel.Keys)
        {
            _openPanel[item].HideAllPanel();
        }
    }

    /// <summary>
    /// 打开上一个面板
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

}