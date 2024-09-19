using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ���׵���Ʒ��
/// </summary>
public class DealItem : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    public Text name_Text;
    public Image icon_Img;
    public Text price_Text;
    public Text num_Text;
    public Text quality_Text;
    public Button buy_Btn;
    public Good myGood;

    public float money = 5000;//ģ���������ô����

    /// <summary>
    /// ��ʼ������
    /// </summary>
    /// <param name="good"></param>
    public void InitData(Good good)
    {
        myGood = good;
        name_Text.text = good.goodName;
        AtlasMgr.Ins.Set2D(icon_Img, "GoodsAtlas", good.goodName);
        price_Text.text = "��" + good.goodPrice;
        quality_Text.text = good.goodQuality.ToString();

        buy_Btn.onClick.AddListener(() =>
        {
            if (money >= good.goodPrice)
            {
                //��������ɾ����Ӧ������
                RemovePurchasedItem(good);
                //����Ϣ����ң���ҽ�Ҽ��٣���Ʒ����
                MessageEventMgr.GetInstance().Dispatch(MessageType.Transaction_Succed, good);
                //����Ϣ�����ң����ҽ������
                MessageEventMgr.GetInstance().Dispatch(MessageType.Transaction_Succed, good.goodPrice);
            }
            else
            {
                print("��Ҳ��㣬�޷�����");
            }
        });
    }

    public void RemovePurchasedItem(Good good)
    {
        MessageEventMgr.GetInstance().Dispatch(MessageType.RemoveListGood, good);
    }
}
