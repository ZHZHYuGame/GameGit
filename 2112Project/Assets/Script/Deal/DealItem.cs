using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 交易的物品类
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

    public float money = 5000;//模拟玩家有这么多金币

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="good"></param>
    public void InitData(Good good)
    {
        myGood = good;
        name_Text.text = good.goodName;
        AtlasMgr.Ins.Set2D(icon_Img, "GoodsAtlas", good.goodName);
        price_Text.text = "￥" + good.goodPrice;
        quality_Text.text = good.goodQuality.ToString();

        buy_Btn.onClick.AddListener(() =>
        {
            if (money >= good.goodPrice)
            {
                //从容器里删除对应的数据
                RemovePurchasedItem(good);
                //发消息给买家，买家金币减少，物品增加
                MessageEventMgr.GetInstance().Dispatch(MessageType.Transaction_Succed, good);
                //发消息给卖家，卖家金币增加
                MessageEventMgr.GetInstance().Dispatch(MessageType.Transaction_Succed, good.goodPrice);
            }
            else
            {
                print("金币不足，无法购买");
            }
        });
    }

    public void RemovePurchasedItem(Good good)
    {
        MessageEventMgr.GetInstance().Dispatch(MessageType.RemoveListGood, good);
    }
}
