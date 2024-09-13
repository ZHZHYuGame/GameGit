using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainUI : MonoBehaviour
{
    public Button transcriptbutt,close,confirm;
    public Dropdown mapdro;
    public GameObject transcriptSetPanel;
    public GameObject video1,video2,video3,video4;
    public Image enemyimage;
    public Button enemybutt;
    int enemynum;
    public Text recommendtext, difficultytext;
    public GameObject mepanel;
    public GameObject AttackPanel;
    private void Awake()
    {
        transcriptSetPanel.transform.gameObject.SetActive(false);
        AttackPanel.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        transcriptbutt.onClick.AddListener(() =>
        {
            transcriptSetPanel.transform.gameObject.SetActive(true);
            transcriptbutt.gameObject.SetActive(false); 
        });
        close.onClick.AddListener(() =>
        {
            transcriptbutt.gameObject.SetActive(true);
            transcriptSetPanel.transform.gameObject.SetActive(false);
        });
        mapdro.onValueChanged.AddListener((maptypeindex) =>
        {
            //if(maptypeindex == 0)
            //{
            //    video1.gameObject.SetActive(true);
            //    video2.gameObject.SetActive(false);
            //    video3.gameObject.SetActive(false);
            //    video4.gameObject.SetActive(false);
            //}
            //if(maptypeindex == 1)
            //{
            //    video1.gameObject.SetActive(false);
            //    video2.gameObject.SetActive(true);
            //    video3.gameObject.SetActive(false);
            //    video4.gameObject.SetActive(false);
            //}
            //if(maptypeindex == 2)
            //{
            //    video1.gameObject.SetActive(false);
            //    video2.gameObject.SetActive(false);
            //    video3.gameObject.SetActive(true);
            //    video4.gameObject.SetActive(false);
            //}
            //if (maptypeindex == 3)
            //{
            //    video1.gameObject.SetActive(false);
            //    video2.gameObject.SetActive(false);
            //    video3.gameObject.SetActive(false);
            //    video4.gameObject.SetActive(true);
            //}
        });
        confirm.onClick.AddListener(() =>
        {
            transcriptSetPanel.transform.gameObject.SetActive(false);
            mepanel.transform.GetComponent<TerrainMap>().enabled = true;
            AttackPanel.gameObject.SetActive(true);
        });
        enemybutt.onClick.AddListener(() =>
        {
            enemynum=Random.Range(0, 16);
            RanLoadEnemyImage(enemynum);
        });
    }
    private void RanLoadEnemyImage(int enemynum)
    {
        Sprite spr = Instantiate(Resources.Load<Sprite>("怪/" + enemynum));
        enemyimage.sprite = spr;
        if(enemynum >= 0)
        {
            difficultytext.text = "一星";
            recommendtext.text = "难度一星，推荐角色等级：60";
        }
        if(enemynum >= 5)
        {
            difficultytext.text = "二星";
            recommendtext.text = "难度二星，推荐角色等级：70";
        }
        if (enemynum >= 10)
        {
            difficultytext.text = "三星";
            recommendtext.text = "难度三星，推荐角色等级：90";
        }
    }
}
