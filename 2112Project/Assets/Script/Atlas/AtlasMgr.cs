using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using static UnityEditor.Progress;

//���ع���  ͼƬ   ͼ����  ͼƬ��


public enum T2DType : byte
{
    None,     //��״̬
    Useing,   //ʹ����
    Unload    //�����ã���ж��
}

public class SpriteInfo
{
    public string _name;
    public Sprite _sprite;
    public int _num;
}


public class SpriteAtlasInfo
{
    public string _name;
    public T2DType _t2DType = T2DType.None;
    Dictionary<string, SpriteInfo> _spritesDic = new Dictionary<string, SpriteInfo>();

    public Dictionary<string, SpriteInfo> SpritesDic { get => _spritesDic; set => _spritesDic = value; }
}



public class AtlasMgr
{
    private static AtlasMgr _ins;


    private Dictionary<string, SpriteAtlasInfo> _spritesDics = new Dictionary<string, SpriteAtlasInfo>();

    //�鿴���ü������ֵ�
    private Dictionary<string, SpriteInfo> _nowSpriteDic = new Dictionary<string, SpriteInfo>();

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

    public void Init()
    {
        _spritesDics.Add("newTest", LoadSpriteAtlas("newTest"));
    }


    /// <summary>
    /// ����ͼƬ
    /// </summary>
    /// <param name="img">ͼƬ</param>
    /// <param name="saName">ͼ����</param>
    /// <param name="spName">ͼƬ��</param>
    public void Set2D(Image img, string saName, string spName)
    {
        if (string.IsNullOrEmpty(saName))
        {
            Debug.Log("������ͼ��������");
            return;
        }
        if (string.IsNullOrEmpty(spName))
        {
            Debug.Log("������ͼƬ������");
            return;
        }

        SpriteAtlasInfo info;
        if (!_spritesDics.TryGetValue(saName, out info))
        {
            info = LoadSpriteAtlas(saName);
            if (info == null)
            {
                Debug.Log("û�и�ͼ��");
                return;
            }
        }

        SpriteInfo spriteInfo = null;
        if (!info.SpritesDic.TryGetValue(spName, out spriteInfo))
        {
            Debug.Log("ͼ����û�и�ͼƬ");
            return;
        }

        spriteInfo._num++;
        info._t2DType = T2DType.Useing;

        //��������

        if (img.sprite != null)
        {
            string name = img.sprite.name.Replace("(Clone)", "");
            RemoveSprite(name);
        }


        //����ͼƬ
        img.sprite = spriteInfo._sprite;
    }


    /// <summary>
    /// ���ڵ���ɾ�����õ�ͼ��
    /// </summary>
    public void ReleaseAtlas()
    {
        List<SpriteAtlasInfo> lists = new List<SpriteAtlasInfo>();

        foreach (var item in _spritesDics)
        {
            if (item.Value._t2DType == T2DType.Unload || item.Value._t2DType == T2DType.None)
            {
                lists.Add(item.Value);
            }
        }

        for (int i = lists.Count - 1; i >= 0; i--)
        {
            if (_spritesDics.ContainsKey(lists[i]._name))
            {
                _spritesDics.Remove(lists[i]._name);
            }
        }

        lists = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="spName">ͼƬ����</param>
    public void RemoveUI(string spName)
    {
        string name = spName.Replace("(Clone)", "");
        RemoveSprite(spName);
    }

    /// <summary>
    /// UIɾ�������л�ͼƬ��ɾ��ͼƬ     
    /// </summary>
    /// <param name="spName">ͼƬ����</param>
    private void RemoveSprite(string spName)
    {
        if (_nowSpriteDic.ContainsKey(spName))
        {
            _nowSpriteDic[spName]._num--;
        }
    }


    /// <summary>
    /// ����ͼ��
    /// </summary>
    /// <param name="saName">ͼ����</param>
    /// <returns>Dic<string, SpriteInfo></returns>
    private SpriteAtlasInfo LoadSpriteAtlas(string saName)
    {
        SpriteAtlasInfo info = new SpriteAtlasInfo();
        SpriteAtlas SA = null;



#if UNITY_EDITOR
        SA = AssetDatabase.LoadAssetAtPath<SpriteAtlas>("Assets/" + saName + ".spriteatlas");
#else
        //AB��
#endif
        if (SA == null)
        {
            Debug.Log("û�и�ͼ��");
            return null;
        }


        //ͼ�����鸳ֵ
        info._name = saName;//item.name.Replace("(Clone)", "");

        Sprite[] sprites = new Sprite[SA.spriteCount];
        SA.GetSprites(sprites);

        foreach (var item in sprites)
        {
            SpriteInfo spriteInfo = new SpriteInfo();

            spriteInfo._name = item.name.Replace("(Clone)", "");
            spriteInfo._sprite = item;

            //����ͼ��info�ֵ�
            info.SpritesDic.Add(spriteInfo._name, spriteInfo);

            _nowSpriteDic.Add(spriteInfo._name, spriteInfo);
        }

        return info;
    }
}
