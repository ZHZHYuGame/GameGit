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
        video1 = GameObject.Find("Video/梦幻晚会");
        video1.gameObject.SetActive(false);  
        video2 = GameObject.Find("Video/秋季暮色");
        video2.gameObject.SetActive(false);
        video3 = GameObject.Find("Video/宇宙之路");
        video3.gameObject.SetActive(false);
        video4 = GameObject.Find("Video/晚霞落幕");
        video4.gameObject.SetActive(false);
        mepanel = GameObject.Find("Video");
    }
    // Start is called before the first frame update
    void Start()
    {
        close.onClick.AddListener(() =>
        {
            print("请用力点击，没检测到亲");
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
