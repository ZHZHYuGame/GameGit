using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : MonoBehaviour
{
    public Image _backGround;   //����
    public Text _hint;          //��ʾ����
    public Image _icon;         //ͷ��|ͼƬ
    public Button _leftBtn;     //��ͼƬ
    public Button _rightBtn;    //��ͼƬ
    public Button _closeBtn;    //�ر�

    public Vector2 _startPos;
    private void Start()
    {
        _startPos = transform.localPosition;
        _closeBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

}
