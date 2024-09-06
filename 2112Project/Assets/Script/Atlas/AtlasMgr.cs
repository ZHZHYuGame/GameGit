using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum T2DType : byte
{
    None,     //��״̬
    Useing,   //ʹ����
    Unload    //�����ã���ж��
}

public class Texture2DInfo
{
    public string _name;
    public Texture2D _texture2D;
    public T2DType _textureType = T2DType.None;
    public int _num;
}


public class AtlasMgr
{
    private static AtlasMgr _ins;

    private Dictionary<string, Texture2DInfo> _texture2DDics = new Dictionary<string, Texture2DInfo>();



    public static AtlasMgr Ins
    {
        get
        {
            if (_ins == null)
            {
                _ins = new AtlasMgr();
            }
            return _ins;
        }
    }
}
