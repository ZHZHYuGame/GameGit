using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品数据
/// </summary>
public class Good 
{
    public int goodId;//物品id
    public string goodName;//物品名称
    public float goodPrice;//物品价格
    public string goodDes;//物品描述
    public GoodType goodType;//物品类型
    public GoodQuality goodQuality;//物品品质
}

/// <summary>
/// 物品品质
/// </summary>
public enum GoodQuality
{
    全部,白色,蓝色,紫色,红色
}

/// <summary>
/// 物品类型
/// </summary>
public enum GoodType
{
    全部,武器,宝箱,药品
}
