using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static MessageEventMgr eventMgr = new MessageEventMgr();

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Good good = new Good();
            good.goodId = 1001;
            good.goodName = "���;���";
            good.goodPrice = 10f;
            good.goodDes = "�ɼ���һ����˺�";
            good.goodType = GoodType.����;
            good.goodQuality = GoodQuality.��ɫ;

            //����Ϣ��������
            MessageEventMgr.GetInstance().Dispatch(MessageType.Transaction, good);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Good good = new Good();
            good.goodId = 1002;
            good.goodName = "˱ָԭζ����";
            good.goodPrice = 10f;
            good.goodDes = "�ɴ洢��Ʒ";
            good.goodType = GoodType.����;
            good.goodQuality = GoodQuality.��ɫ;

            //����Ϣ��������
            MessageEventMgr.GetInstance().Dispatch(MessageType.Transaction, good);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Good good = new Good();
            good.goodId = 1001;
            good.goodName = "ơ��ƿ��";
            good.goodPrice = 20f;
            good.goodDes = "�ɼ���һ����˺�";
            good.goodType = GoodType.ҩƷ;
            good.goodQuality = GoodQuality.��ɫ;

            //����Ϣ��������
            MessageEventMgr.GetInstance().Dispatch(MessageType.Transaction, good);
        }
    }
}
