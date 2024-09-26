using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBase : MonoBehaviour
{
    //C
    //����M V
    //��ʼ�� --�� M new V Get
                //��帳ֵ

    //M��ʼ��    //��Ӽ���
                //�����������Ϣ
                //�ӻ����ж�����
   









    Image _image;
    
    //ui�����CanvasGroup ������͸����
    CanvasGroup _canvasGroup;
    //��
    public virtual void OpenUI()
    {
        //_canvasGroup.alpha = 1;
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
