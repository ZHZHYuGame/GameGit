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
            good.goodName = "传送卷轴";
            good.goodPrice = 10f;
            good.goodDes = "可减少一半的伤害";
            good.goodType = GoodType.武器;
            good.goodQuality = GoodQuality.白色;

            //发消息给交易行
            MessageEventMgr.GetInstance().Dispatch(MessageType.Transaction, good);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Good good = new Good();
            good.goodId = 1002;
            good.goodName = "吮指原味鸡翅";
            good.goodPrice = 10f;
            good.goodDes = "可存储物品";
            good.goodType = GoodType.宝箱;
            good.goodQuality = GoodQuality.蓝色;

            //发消息给交易行
            MessageEventMgr.GetInstance().Dispatch(MessageType.Transaction, good);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Good good = new Good();
            good.goodId = 1001;
            good.goodName = "啤酒瓶子";
            good.goodPrice = 20f;
            good.goodDes = "可减少一半的伤害";
            good.goodType = GoodType.药品;
            good.goodQuality = GoodQuality.紫色;

            //发消息给交易行
            MessageEventMgr.GetInstance().Dispatch(MessageType.Transaction, good);
        }
    }
}
