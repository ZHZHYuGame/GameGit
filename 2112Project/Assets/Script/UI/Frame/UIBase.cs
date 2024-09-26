using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    //C
    //持有M V
    //初始化 --》 M new V Get
                //面板赋值

    //M初始化    //添加监听
                //向服务器发消息
                //从缓存中读数据
   









    Image _image;
    
    //ui界面得CanvasGroup 用来调透明度
    CanvasGroup _canvasGroup;
    //打开
    public virtual void OpenUI()
    {
        //_canvasGroup.alpha = 1;
        _image.color = new Color(1, 1, 1, 1);
        gameObject.SetActive(true);
    }

    //关闭
    public virtual void CloseUI()
    {
        Destroy(gameObject);
    }

    //隐藏
    public virtual void HideUI()
    {
        gameObject.SetActive(false);
        //_canvasGroup.alpha = 0;
    }

    public void HideAllPanel()
    {
        gameObject.SetActive(false);
        //_canvasGroup.alpha = 0;
    }

    public virtual void Awake() { }

    public virtual void OnEnable() { }

    public virtual void Start()
    {
        _image = GetComponent<Image>();
        //_canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Update() { }





}
