using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 附件道具信息
/// </summary>
public class ItemInfo
{
    //道具model
    int _itemId;
    //道具数量
    int _itemCount;
    //过期时间点,（0，永久道具，非0，从1970.1.1开始的秒数）
    int _validity;

    /**
	 * 道具model
	 */
    public int ItemId
    {
        set { _itemId = value; }
        get { return _itemId; }
    }

    /**
	 * 道具数量
	 */
    public int ItemCount
    {
        set { _itemCount = value; }
        get { return _itemCount; }
    }

    /**
	 * 过期时间点,（0，永久道具，非0，从1970.1.1开始的秒数）
	 */
    public int Validity
    {
        set { _validity = value; }
        get { return _validity; }
    }
}
