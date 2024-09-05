using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

//   常用纹理打一个包
//   纹理根据场景打包
//   2024.9.4    高腾帅

public enum TextureType : byte
{
    None,     //空状态
    Useing,   //使用中
    Unload    //无人用，可卸载
}


public class TextureInfo
{
    public string _name;
    public Texture _texture;
    public TextureType _textureType = TextureType.None;
    public int _num;
}

public class TextureMgr
{
    //纹理容量(可扩容)
    //private int _count = 100;

    //空白纹理
    Texture nullTexture = null;

    //单例
    private static TextureMgr _ins;

    //存放所有纹理的字典     
    private Dictionary<string, TextureInfo> _textureDics;

    //存放正在使用的纹理的集合
    private List<TextureInfo> _textureLists;

    public static TextureMgr Ins
    {
        get
        {
            if (_ins == null)
            {
                _ins = new TextureMgr();
            }
            return _ins;
        }
    }


    public void Init()
    {
        _textureDics = new Dictionary<string, TextureInfo>();
        _textureLists = new List<TextureInfo>();


        LoadCommonlyUsed();
    }

    /// <summary>
    /// 常用纹理加载   
    /// </summary>
    public void LoadCommonlyUsed()
    {
#if UNITY_EDITOR
        //Resources
        //_textureDics.Add();
#else
        //AssetBundle
        //_textureDics.Add();
#endif
    }

    /// <summary>
    /// 根据场景名加载纹理
    /// </summary>
    /// <param name="name">场景名的枚举</param>
    public void LoadOnSceneName(string name)
    {
        List<TextureInfo> infos = new List<TextureInfo>();
        switch (name)
        {
            case "枚举或者名称":
                infos = LoadSceneTexture(name);
                break;
        }

        for (int i = 0; i < infos.Count; i++)
        {
            _textureDics.Add(infos[i]._name, infos[i]);
        }

        infos = null;
    }


    /// <summary>
    /// 使用纹理
    /// </summary>
    /// <param name="name">预制件名字</param>
    /// <returns>纹理</returns>
    public Texture GetTexture(string name)
    {
        TextureInfo info = null;
        if (!_textureDics.TryGetValue(name, out info))
        {
            info = LoadTexture(name);
        }
        //引用计数
        info._num++;
        info._textureType = TextureType.Useing;

        if (!_textureLists.Contains(info))
            _textureLists.Add(info);

        return info._texture;
    }

    /// <summary>
    /// 从预制体上卸载纹理  人物死亡
    /// </summary>
    /// <param name="obj">预制体名称</param>
    /// <param name="textName">纹理名称</param>
    public void FramObjUnLoad(GameObject obj, string textName = "_MainTex")
    {
        TextureInfo info = _textureDics[obj.name];

        Material mainMaterial = obj.GetComponent<MeshRenderer>().material;

        if (mainMaterial != null)
        {
            mainMaterial.SetTexture(textName, nullTexture);
        }

        info._num--;
        if (info._num <= 0)
        {
            info._textureType = TextureType.Unload;
        }
    }

    /// <summary>
    /// 隔一段时间调用卸载  
    /// </summary>
    public void OnTimeUnLoad()
    {
        List<TextureInfo> nowLists = new List<TextureInfo>();
        //for(int i=0;i< _textureLists.Count; i++)
        //{
        //    if (_textureLists[i]._textureType == TextureType.Unload)
        //    {
        //        nowLists.Add( _textureLists[i] );
        //        _textureLists.RemoveAt(i);
        //        i--;
        //    }
        //}
        for (int i = _textureLists.Count - 1; i >= 0; i--)
        {
            if (_textureLists[i]._textureType == TextureType.Unload)
            {
                nowLists.Add(_textureLists[i]);
                _textureLists.RemoveAt(i);
            }
        }

        for (int i = 0; i < nowLists.Count; i++)
        {
            if (_textureDics.ContainsKey(nowLists[i]._name))
            {
                _textureDics.Remove(nowLists[i]._name);
            }
        }

        nowLists = null;
    }

    /// <summary>
    /// 手动调用卸载不用的纹理  
    /// </summary>
    public void UnLoadTexture()
    {
        List<TextureInfo> textInfos = new List<TextureInfo>();

        foreach (var item in _textureDics.Values)
        {
            if (item._textureType == TextureType.None)
            {
                textInfos.Add(item);
            }
        }

        for (int i = textInfos.Count - 1; i >= 0; i--)
        {
            if (_textureDics[textInfos[i]._name] != null)
            {
                _textureDics.Remove(textInfos[i]._name);
            }
        }

        textInfos = null;
    }





    /// <summary>
    /// 从资源中加载纹理
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private TextureInfo LoadTexture(string name)
    {
        TextureInfo info = new TextureInfo();
        Texture texture = null;
#if UNITY_EDITOR
        //Resources
#else
        //AssetBundle
#endif
        info._name = name;
        info._texture = texture;
        info._num = 0;
        //_textureDics.Add(name,new TextureInfo() {_name=name,_texture=texture,_num=0 });
        return _textureDics[name];
    }


    /// <summary>
    /// 加载场景纹理
    /// </summary>
    /// <param name="name">场景名</param>
    /// <returns></returns>
    private List<TextureInfo> LoadSceneTexture(string name)
    {
        List<TextureInfo> infos = new List<TextureInfo>();

        //拼接路径  或者   Config中的Config路径类
        string path = "" + name;

#if UNITY_EDITOR
        //Resources
        //_textureDics.Add();
#else
        //AssetBundle
        //_textureDics.Add();
#endif
        return infos;
    }
}

