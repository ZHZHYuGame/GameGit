using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������Ϣ
/// </summary>
public class ItemInfo
{
    //����model
    int _itemId;
    //��������
    int _itemCount;
    //����ʱ���,��0�����õ��ߣ���0����1970.1.1��ʼ��������
    int _validity;

    /**
	 * ����model
	 */
    public int ItemId
    {
        set { _itemId = value; }
        get { return _itemId; }
    }

    /**
	 * ��������
	 */
    public int ItemCount
    {
        set { _itemCount = value; }
        get { return _itemCount; }
    }

    /**
	 * ����ʱ���,��0�����õ��ߣ���0����1970.1.1��ʼ��������
	 */
    public int Validity
    {
        set { _validity = value; }
        get { return _validity; }
    }
}
