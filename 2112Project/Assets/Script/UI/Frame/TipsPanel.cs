using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : MonoBehaviour
{
    public Image _backGround;   //背景
    public Image _icon;         //头像|图片
    public Text _name;          //提示文字
    public Text _des;           //描述
    public Text _price;         //价格
    public Text _quality;       //品质
    public Button _affirm;       //确认
    public Button _cancel;       //取消

    public Vector2 _startPos;
    private void Start()
    {
        _startPos = transform.localPosition;
    }

}
