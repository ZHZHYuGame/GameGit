using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using static UnityEditor.Progress;

//加载规则  图片   图集名  图片名


public enum T2DType : byte
{
    None,     //空状态
    Useing,   //使用中
    Unload    //无人用，可卸载
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

    //查看引用计数的字典
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
    /// 设置图片
    /// </summary>
    /// <param name="img">图片</param>
    /// <param name="saName">图集名</param>
    /// <param name="spName">图片名</param>
    public void Set2D(Image img, string saName, string spName)
    {
        if (string.IsNullOrEmpty(saName))
        {
            Debug.Log("请输入图集的名称");
            return;
        }
        if (string.IsNullOrEmpty(spName))
        {
            Debug.Log("请输入图片的名称");
            return;
        }

        SpriteAtlasInfo info;
        if (!_spritesDics.TryGetValue(saName, out info))
        {
            info = LoadSpriteAtlas(saName);
            if (info == null)
            {
                Debug.Log("没有该图集");
                return;
            }
        }

        SpriteInfo spriteInfo = null;
        if (!info.SpritesDic.TryGetValue(spName, out spriteInfo))
        {
            Debug.Log("图集中没有该图片");
            return;
        }

        spriteInfo._num++;
        info._t2DType = T2DType.Useing;

        //计数减减

        if (img.sprite != null)
        {
            string name = img.sprite.name.Replace("(Clone)", "");
            RemoveSprite(name);
        }


        //设置图片
        img.sprite = spriteInfo._sprite;
    }


    /// <summary>
    /// 定期调用删除不用的图集
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
    /// <param name="spName">图片名称</param>
    public void RemoveUI(string spName)
    {
        string name = spName.Replace("(Clone)", "");
        RemoveSprite(spName);
    }

    /// <summary>
    /// UI删除或者切换图片，删除图片     
    /// </summary>
    /// <param name="spName">图片名称</param>
    private void RemoveSprite(string spName)
    {
        if (_nowSpriteDic.ContainsKey(spName))
        {
            _nowSpriteDic[spName]._num--;
        }
    }


    /// <summary>
    /// 加载图集
    /// </summary>
    /// <param name="saName">图集名</param>
    /// <returns>Dic<string, SpriteInfo></returns>
    private SpriteAtlasInfo LoadSpriteAtlas(string saName)
    {
        SpriteAtlasInfo info = new SpriteAtlasInfo();
        SpriteAtlas SA = null;



#if UNITY_EDITOR
        SA = AssetDatabase.LoadAssetAtPath<SpriteAtlas>("Assets/" + saName + ".spriteatlas");
#else
        //AB包
#endif
        if (SA == null)
        {
            Debug.Log("没有该图集");
            return null;
        }


        //图集数组赋值
        info._name = saName;//item.name.Replace("(Clone)", "");

        Sprite[] sprites = new Sprite[SA.spriteCount];
        SA.GetSprites(sprites);

        foreach (var item in sprites)
        {
            SpriteInfo spriteInfo = new SpriteInfo();

            spriteInfo._name = item.name.Replace("(Clone)", "");
            spriteInfo._sprite = item;

            //加入图集info字典
            info.SpritesDic.Add(spriteInfo._name, spriteInfo);

            _nowSpriteDic.Add(spriteInfo._name, spriteInfo);
        }

        return info;
    }
}
