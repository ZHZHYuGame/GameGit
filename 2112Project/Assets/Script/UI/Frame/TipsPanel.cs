using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : MonoBehaviour
{
    public Image _backGround;   //����
    public Image _icon;         //ͷ��|ͼƬ
    public Text _name;          //��ʾ����
    public Text _des;           //����
    public Text _price;         //�۸�
    public Text _quality;       //Ʒ��
    public Button _affirm;       //ȷ��
    public Button _cancel;       //ȡ��

    public Vector2 _startPos;
    private void Start()
    {
        _startPos = transform.localPosition;
    }

}
