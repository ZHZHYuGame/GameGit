using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

//   ���������һ����
//   ������ݳ������
//   2024.9.4    ����˧

public enum TextureType : byte
{
    None,     //��״̬
    Useing,   //ʹ����
    Unload    //�����ã���ж��
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
    //��������(������)
    //private int _count = 100;

    //�հ�����
    Texture nullTexture = null;

    //����
    private static TextureMgr _ins;

    //�������������ֵ�     
    private Dictionary<string, TextureInfo> _textureDics;

    //�������ʹ�õ�����ļ���
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
    /// �����������   
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
    /// ���ݳ�������������
    /// </summary>
    /// <param name="name">��������ö��</param>
    public void LoadOnSceneName(string name)
    {
        List<TextureInfo> infos = new List<TextureInfo>();
        switch (name)
        {
            case "ö�ٻ�������":
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
    /// ʹ������
    /// </summary>
    /// <param name="name">Ԥ�Ƽ�����</param>
    /// <returns>����</returns>
    public Texture GetTexture(string name)
    {
        TextureInfo info = null;
        if (!_textureDics.TryGetValue(name, out info))
        {
            info = LoadTexture(name);
        }
        //���ü���
        info._num++;
        info._textureType = TextureType.Useing;

        if (!_textureLists.Contains(info))
            _textureLists.Add(info);

        return info._texture;
    }

    /// <summary>
    /// ��Ԥ������ж������  ��������
    /// </summary>
    /// <param name="obj">Ԥ��������</param>
    /// <param name="textName">��������</param>
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
    /// ��һ��ʱ�����ж��  
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
    /// �ֶ�����ж�ز��õ�����  
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
    /// ����Դ�м�������
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
    /// ���س�������
    /// </summary>
    /// <param name="name">������</param>
    /// <returns></returns>
    private List<TextureInfo> LoadSceneTexture(string name)
    {
        List<TextureInfo> infos = new List<TextureInfo>();

        //ƴ��·��  ����   Config�е�Config·����
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

