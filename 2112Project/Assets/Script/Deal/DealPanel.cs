using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 交易面板
/// </summary>
public class DealPanel : UIBase
{
    [Header("按钮")]
    public Button deal_Close_Btn;//交易行关闭按钮
    public Button search_Btn;//搜索按钮

    [Header("输入框")]
    public InputField search_Input;//搜索输入框

    [Header("根节点")]
    public Transform content_Root;//Item根节点
    public Transform goodType_Root;//类型根节点
    public Transform goodQuality_Root;//品质根节点

    public GoodType goodType = GoodType.全部;
    public GoodQuality goodQuality = GoodQuality.全部;

    public List<Good> list_Goods = new List<Good>();//存储所有的交易物品
    public List<Good> templist = new List<Good>();//临时集合
    public List<Toggle> list_GoodType = new List<Toggle>();//物品类型的集合
    public List<Toggle> list_GoodQuality = new List<Toggle>();//物品品质的集合

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //交易行面板关闭
        deal_Close_Btn.onClick.AddListener(() => { UIManager.Instance.CloseUI(UIPanelType.Deal); });

        UpdateDealData(SelectSameGoods());
        OnClickTog();

        //进行搜索
        search_Btn.onClick.AddListener(OnSearchBtn);
    }
    private void OnSearchBtn()
    {
        SelectRoot(goodType_Root);
        SelectRoot(goodQuality_Root);
        for (int i = 0; i < list_Goods.Count; i++)
        {
            if (list_Goods[i].goodName.Contains(search_Input.text))
            {
                templist.Add(list_Goods[i]);
            }
        }
        UpdateDealData(templist);
        search_Input.text = "";
    }

    private void OnClickTog()
    {
        for (int i = 0; i < list_GoodType.Count; i++)
        {
            int index = i;
            list_GoodType[i].onValueChanged.AddListener((v) =>
            {
                if (v)
                {
                    goodType = (GoodType)index;
                    UpdateDealData(SelectSameGoods());
                };
            });
        }

        for (int i = 0; i < list_GoodQuality.Count; i++)
        {
            int index = i;
            list_GoodQuality[i].onValueChanged.AddListener((v) =>
            {
                if (v)
                {
                    goodQuality = (GoodQuality)index;
                    UpdateDealData(SelectSameGoods());
                };
            });
        }
    }

    public void SelectRoot(Transform tran)
    {
        for (int i = 0; i < tran.childCount; i++)
        {
            if (i == 0)
            {
                tran.GetChild(i).GetComponent<Toggle>().isOn = true;
            }
            else
            {
                tran.GetChild(i).GetComponent<Toggle>().isOn = false;
            }
        }
    }


    /// <summary>
    /// 选择类型，品质相同的数据
    /// </summary>
    /// <returns></returns>
    public List<Good> SelectSameGoods()
    {
        templist.Clear();
        if (goodType == GoodType.全部 && goodQuality == GoodQuality.全部)
            return list_Goods;

        if (goodType != GoodType.全部)
        {
            foreach (var item in list_Goods)
            {
                if (item.goodType == goodType)
                {

                    templist.Add(item);
                }
            }
            if (goodQuality != GoodQuality.全部)
            {
                for (int i = templist.Count - 1; i >= 0; i--)
                {
                    if (templist[i].goodQuality != goodQuality)
                        templist.RemoveAt(i);
                }
            }
            return templist;
        }
        else
        {
            foreach (var item in list_Goods)
            {
                if (item.goodQuality == goodQuality)
                {
                    templist.Add(item);
                }
            }
            return templist;
        }
    }

    /// <summary>
    /// 接收卖方物品
    /// </summary>
    public void OnReceiveSellerGood(Message evt)
    {
        Good good = evt.data as Good;//强转为Good类型
        list_Goods.Add(good);
        //打开交易面板的时候，初始化所有
        UpdateDealData(list_Goods);
    }

    /// <summary>
    /// 初始化交易行数据
    /// </summary>
    public void UpdateDealData(List<Good> listGoods)
    {
        //刚开始的时候清空content_Root的子类
        if (content_Root.childCount != 0)
        {
            for (int i = 0; i < content_Root.childCount; i++)
            {
                Destroy(content_Root.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < listGoods.Count; i++)
        {
            if (listGoods[i] != null)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("UI/DealItem"), content_Root);
                obj.GetComponent<DealItem>().InitData(listGoods[i]);
            }
        }
    }
    /// <summary>
    /// 购买之后移除数据
    /// </summary>
    /// <param name="evt"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    private void OnRemoveListGood(Message evt)
    {
        Good good = evt.data as Good;
        if (list_Goods.Contains(good))
        {
            list_Goods.Remove(good);
            UpdateDealData(SelectSameGoods());
        }
    }
    public override void Awake()
    {
        base.Awake();

        //交易行接收要被交易的物品
        MessageEventMgr.GetInstance().AddListener(MessageType.Transaction, OnReceiveSellerGood);
        MessageEventMgr.GetInstance().AddListener(MessageType.RemoveListGood, OnRemoveListGood);
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

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        MessageEventMgr.GetInstance().RemoveListener(MessageType.Transaction, OnReceiveSellerGood);
        MessageEventMgr.GetInstance().RemoveListener(MessageType.RemoveListGood, OnRemoveListGood);
    }
}
