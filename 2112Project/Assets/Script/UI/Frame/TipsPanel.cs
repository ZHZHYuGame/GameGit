using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : MonoBehaviour
{
    public Image _backGround;   //±³¾°
    public Text _hint;          //ÌáÊ¾ÎÄ×Ö
    public Image _icon;         //Í·Ïñ|Í¼Æ¬
    public Button _leftBtn;     //×óÍ¼Æ¬
    public Button _rightBtn;    //ÓÒÍ¼Æ¬
    public Button _closeBtn;    //¹Ø±Õ

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
