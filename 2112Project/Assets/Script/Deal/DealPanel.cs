using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������
/// </summary>
public class DealPanel : UIBase
{
    [Header("��ť")]
    public Button deal_Close_Btn;//�����йرհ�ť
    public Button search_Btn;//������ť

    [Header("�����")]
    public InputField search_Input;//���������

    [Header("���ڵ�")]
    public Transform content_Root;//Item���ڵ�
    public Transform goodType_Root;//���͸��ڵ�
    public Transform goodQuality_Root;//Ʒ�ʸ��ڵ�

    public GoodType goodType = GoodType.ȫ��;
    public GoodQuality goodQuality = GoodQuality.ȫ��;

    public List<Good> list_Goods = new List<Good>();//�洢���еĽ�����Ʒ
    public List<Good> templist = new List<Good>();//��ʱ����
    public List<Toggle> list_GoodType = new List<Toggle>();//��Ʒ���͵ļ���
    public List<Toggle> list_GoodQuality = new List<Toggle>();//��ƷƷ�ʵļ���

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //���������ر�
        deal_Close_Btn.onClick.AddListener(() => { UIManager.Instance.CloseUI(UIPanelType.Deal); });

        UpdateDealData(SelectSameGoods());
        OnClickTog();

        //��������
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
    /// ѡ�����ͣ�Ʒ����ͬ������
    /// </summary>
    /// <returns></returns>
    public List<Good> SelectSameGoods()
    {
        templist.Clear();
        if (goodType == GoodType.ȫ�� && goodQuality == GoodQuality.ȫ��)
            return list_Goods;

        if (goodType != GoodType.ȫ��)
        {
            foreach (var item in list_Goods)
            {
                if (item.goodType == goodType)
                {

                    templist.Add(item);
                }
            }
            if (goodQuality != GoodQuality.ȫ��)
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
    /// ����������Ʒ
    /// </summary>
    public void OnReceiveSellerGood(Message evt)
    {
        Good good = evt.data as Good;//ǿתΪGood����
        list_Goods.Add(good);
        //�򿪽�������ʱ�򣬳�ʼ������
        UpdateDealData(list_Goods);
    }

    /// <summary>
    /// ��ʼ������������
    /// </summary>
    public void UpdateDealData(List<Good> listGoods)
    {
        //�տ�ʼ��ʱ�����content_Root������
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
    /// ����֮���Ƴ�����
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

        //�����н���Ҫ�����׵���Ʒ
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
