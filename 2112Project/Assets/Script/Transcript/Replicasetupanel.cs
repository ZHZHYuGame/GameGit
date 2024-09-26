using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Replicasetupanel : UIBase
{
    public Button close,confirm;
    public Dropdown mapdro;
    GameObject video1,video2,video3,video4;
    public Image enemyimage;
    public Button enemybutt;
    int enemynum;
    public Text recommendtext, difficultytext;
    public GameObject mepanel;
    
    private void Awake()
    {
        video1 = GameObject.Find("Video/�λ����");
        video1.gameObject.SetActive(false);  
        video2 = GameObject.Find("Video/�＾ĺɫ");
        video2.gameObject.SetActive(false);
        video3 = GameObject.Find("Video/����֮·");
        video3.gameObject.SetActive(false);
        video4 = GameObject.Find("Video/��ϼ��Ļ");
        video4.gameObject.SetActive(false);
        mepanel = GameObject.Find("Video");
    }
    // Start is called before the first frame update
    void Start()
    {
        close.onClick.AddListener(() =>
        {
            print("�����������û��⵽��");
            //SceneManager.LoadScene("CombatScene");
        });
        mapdro.onValueChanged.AddListener((maptypeindex) =>
        {
            if(video1!=null && video2!=null && video3!=null && video4 != null)
            {
                if (maptypeindex == 0)
                {
                    video1.gameObject.SetActive(true);
                    video2.gameObject.SetActive(false);
                    video3.gameObject.SetActive(false);
                    video4.gameObject.SetActive(false);
                }
                if (maptypeindex == 1)
                {
                    video1.gameObject.SetActive(false);
                    video2.gameObject.SetActive(true);
                    video3.gameObject.SetActive(false);
                    video4.gameObject.SetActive(false);
                }
                if (maptypeindex == 2)
                {
                    video1.gameObject.SetActive(false);
                    video2.gameObject.SetActive(false);
                    video3.gameObject.SetActive(true);
                    video4.gameObject.SetActive(false);
                }
                if (maptypeindex == 3)
                {
                    video1.gameObject.SetActive(false);
                    video2.gameObject.SetActive(false);
                    video3.gameObject.SetActive(false);
                    video4.gameObject.SetActive(true);
                }
            }
        });
        confirm.onClick.AddListener(() =>
        {
            //UIManager.Instance.CloseUI(UIPanelType.Map);
            transform.gameObject.SetActive(false);
            mepanel.transform.GetComponent<TerrainMap>().enabled = true;
        });
        enemybutt.onClick.AddListener(() =>
        {
            enemynum = Random.Range(0, 16);
            RanLoadEnemyImage(enemynum);
        });
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
