using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ʒ����
/// </summary>
public class Good 
{
    public int goodId;//��Ʒid
    public string goodName;//��Ʒ����
    public float goodPrice;//��Ʒ�۸�
    public string goodDes;//��Ʒ����
    public GoodType goodType;//��Ʒ����
    public GoodQuality goodQuality;//��ƷƷ��
}

/// <summary>
/// ��ƷƷ��
/// </summary>
public enum GoodQuality
{
    ȫ��,��ɫ,��ɫ,��ɫ,��ɫ
}

/// <summary>
/// ��Ʒ����
/// </summary>
public enum GoodType
{
    ȫ��,����,����,ҩƷ
}
