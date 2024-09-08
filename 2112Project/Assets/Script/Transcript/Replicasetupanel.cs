using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Replicasetupanel : UIBase
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
    
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //transcriptbutt.onClick.AddListener(() =>
        //{
        //    transcriptSetPanel.transform.gameObject.SetActive(true);
        //    transcriptbutt.gameObject.SetActive(false);
        //});
        close.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("CombatScene");
        });
        //mapdro.onValueChanged.AddListener((maptypeindex) =>
        //{
        //    if (maptypeindex == 0)
        //    {
        //        video1.gameObject.SetActive(true);
        //    }
        //    if (maptypeindex == 1)
        //    {
        //        video2.gameObject.SetActive(true);
        //    }
        //    if (maptypeindex == 2)
        //    {
        //        video3.gameObject.SetActive(true);
        //    }
        //    if (maptypeindex == 3)
        //    {
        //        video4.gameObject.SetActive(true);
        //    }
        //});
        confirm.onClick.AddListener(() =>
        {
            UIManager.Instance.CloseUI(UIPanelType.Map);
            //mepanel.transform.GetComponent<MapGenerator>().enabled = true;
        });
        //enemybutt.onClick.AddListener(() =>
        //{
        //    enemynum = Random.Range(0, 16);
        //    RanLoadEnemyImage(enemynum);
        //});
    }
    private void RanLoadEnemyImage(int enemynum)
    {
        Sprite spr = Instantiate(Resources.Load<Sprite>("��/" + enemynum));
        enemyimage.sprite = spr;
        if(enemynum >= 0)
        {
            difficultytext.text = "һ��";
            recommendtext.text = "�Ѷ�һ�ǣ��Ƽ���ɫ�ȼ���60";
        }
        if(enemynum >= 5)
        {
            difficultytext.text = "����";
            recommendtext.text = "�Ѷȶ��ǣ��Ƽ���ɫ�ȼ���70";
        }
        if (enemynum >= 10)
        {
            difficultytext.text = "����";
            recommendtext.text = "�Ѷ����ǣ��Ƽ���ɫ�ȼ���90";
        }
    }
}
