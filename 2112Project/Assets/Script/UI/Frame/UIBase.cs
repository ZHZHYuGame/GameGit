using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{

    //ui�����Image ������͸����
    Image _image;

    //��
    public virtual void OpenUI()
    {
        _image.color = new Color(1, 1, 1, 1);
        gameObject.SetActive(true);
    }

    //�ر�
    public virtual void CloseUI()
    {

        Destroy(gameObject);
    }

    //����
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
