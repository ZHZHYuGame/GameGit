using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{

    //ui界面得Image 用来调透明度
    Image _image;

    //打开
    public virtual void OpenUI()
    {
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
    }

    public void HideAllPanel()
    {
        gameObject.SetActive(false);
    }

    public virtual void Awake() { }

    public virtual void OnEnable() { }

    public virtual void Start() { _image = GetComponent<Image>(); }

    public virtual void Update() { }

    public virtual void OnDestroy() { }

    public virtual void OnDisable() { }



}
