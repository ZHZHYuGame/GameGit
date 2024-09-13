using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Data;

public class TerrainMap : MonoBehaviour
{
    public Transform count;
    TerrainData terrainpass;
    public GameObject video1, video2, video3, video4;
    float times = 0, timea = 0, timee = 0;
    GameObject player;
    GameObject hole1, hole2, hole3;

    public Text enemytext;
    public static int enemynum = 0;
    float timet = 0;
    public Text timetext;
    public static float timenum = 200;
    float timep = 0;
    public Text datatext;
    private void Awake()
    {
        datatext.gameObject.SetActive(false);
        TextAsset str = Instantiate(Resources.Load<TextAsset>("terrain"));
        terrainpass = JsonConvert.DeserializeObject<TerrainData>(str.text);
        SpecialTimeController.Instance.Timer(200, timetext, false);
    }
    // Start is called before the first frame update
    void Start()
    {
        Scene();
        Generatingresource();
        GenerateEnemy();
        if (terrainpass.difficultytype == "简单")
        {
            datatext.gameObject.SetActive(true);
            datatext.text = "简单模式，杀敌三百";
        }
        if (terrainpass.difficultytype == "困难")
        {
            datatext.gameObject.SetActive(true);
            datatext.text = "困难模式，杀敌五百";
        }
        if (terrainpass.difficultytype == "噩梦")
        {
            datatext.gameObject.SetActive(true);
            datatext.text = "噩梦模式，杀敌一千";
        }
    }

    private void Scene()
    {
        if (terrainpass.maptype != null)
        {
            if (terrainpass.maptype == "梦幻晚会")
            {
                video1.gameObject.SetActive(true);
                video2.gameObject.SetActive(false);
                video3.gameObject.SetActive(false);
                video4.gameObject.SetActive(false);
            }
            if (terrainpass.maptype == "秋季暮色")
            {
                video1.gameObject.SetActive(false);
                video2.gameObject.SetActive(true);
                video3.gameObject.SetActive(false);
                video4.gameObject.SetActive(false);
            }
            if (terrainpass.maptype == "宇宙之路")
            {
                video1.gameObject.SetActive(false);
                video2.gameObject.SetActive(false);
                video3.gameObject.SetActive(true);
                video4.gameObject.SetActive(false);
            }
            if (terrainpass.maptype == "晚霞落幕")
            {
                video1.gameObject.SetActive(false);
                video2.gameObject.SetActive(false);
                video3.gameObject.SetActive(false);
                video4.gameObject.SetActive(true);
            }
        }
    }

    private void Generatingresource()
    {
        GameObject map = Instantiate(Resources.Load<GameObject>("Scene_Farm"), count, false);
        map.transform.position= Vector3.zero;

        player = Instantiate(Resources.Load<GameObject>("Role/1"));
        player.transform.position = new Vector3(271, 0, -41);


        hole1 = Instantiate(Resources.Load<GameObject>("Cube"));
        hole1.transform.position = new Vector3(225, 0, -90);
        hole2 = Instantiate(Resources.Load<GameObject>("Cube"));
        hole2.transform.position = new Vector3(239, 0, -17);
        hole3 = Instantiate(Resources.Load<GameObject>("Cube"));
        hole3.transform.position = new Vector3(279, 0, -73);
    }

    // Update is called once per frame
    void Update()
    {
        timet += Time.deltaTime;
        if (timet >= 1)
        {
            timenum--;
            timet = 0;
        }
        enemytext.text=enemynum.ToString();
        //timetext.text=timenum.ToString();
        
        if (terrainpass.difficultytype== "简单")
        {
            times += Time.deltaTime;
            if (times > 7)
            {
                GenerateEnemy();
                times = 0;
            }
            if (timenum <= 0 && enemynum >= 300)
            {
                LoadTransfer();
            }
            else if(timenum >= 0 && enemynum >= 300)
            {
                LoadTransfer();
            }
            else if(timenum<=0 && enemynum <= 300)
            {
                datatext.gameObject.SetActive(true);
                datatext.text = "任务未完成";
            }
        }
        if(terrainpass.difficultytype== "困难")
        {
            timea += Time.deltaTime;
            if (timea > 4)
            {
                GenerateEnemy();
                timea = 0;
            }
            if (timenum <= 0 && enemynum >= 500)
            {
                LoadTransfer();
            }
            else if (timenum >= 0 && enemynum >= 500)
            {
                LoadTransfer();
            }
            else if (timenum <= 0 && enemynum <= 500)
            {
                datatext.gameObject.SetActive(true);
                datatext.text = "任务未完成";
            }
        }
        if(terrainpass.difficultytype== "噩梦")
        {
            timee += Time.deltaTime;
            if (timee > 2)
            {
                GenerateEnemy();
                timee = 0;
            }
            if (timenum <= 0 && enemynum >= 1000)
            {
                LoadTransfer();
            }
            else if (timenum >= 0 && enemynum >= 1000)
            {
                LoadTransfer();
            }
            else if (timenum <= 0 && enemynum <= 1000)
            {
                datatext.gameObject.SetActive(true);
                datatext.text = "任务未完成";
            }
        }
        if (terrainpass.indexprop == true)
        {
            timep += Time.deltaTime;
            if (timep >= 10)
            {
                GameObject map = Instantiate(Resources.Load<GameObject>("prop"));
                map.transform.position = new Vector3(Random.Range(265,195),0,Random.Range(-30, -80));
                timep = 0;
            }
        }
    }
    private void LoadTransfer()
    {
        datatext.gameObject.SetActive(true);
        datatext.text = "传送阵已激活";
        GameObject transfer = Instantiate(Resources.Load<GameObject>("Transfer"));
        transfer.transform.position = terrainpass.pos;
    }

    private void GenerateEnemy()
    {
        if (terrainpass.enemytype == "A方式直线巡逻")
        {
            GameObject enemy1 = Instantiate(Resources.Load<GameObject>("Role/7"));
            enemy1.transform.position = new Vector3(265, 0, -80);
            GameObject enemy2 = Instantiate(Resources.Load<GameObject>("Role/7"));
            enemy2.transform.position = new Vector3(265, 0, -30);
            enemy2.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if(terrainpass.enemytype== "B方式四周巡逻")
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject enemy = Instantiate(Resources.Load<GameObject>("Role/7"));
                if (i == 0)
                {
                    enemy.transform.position = new Vector3(265, 0, -80);
                }
                if (i == 1)
                {
                    enemy.transform.position = new Vector3(265, 0, -30);
                    enemy.transform.eulerAngles = new Vector3(0, -90, 0);
                }
                if(i == 2)
                {
                    enemy.transform.position = new Vector3(195, 0, -30);
                    enemy.transform.eulerAngles = new Vector3(0, 180, 0);
                }
                if(i == 3)
                {
                    enemy.transform.position = new Vector3(195, 0, -80);
                    enemy.transform.eulerAngles = new Vector3(0, 90, 0);
                }
            }
        }
        if(terrainpass.enemytype== "C方式靠近出怪")
        {
            if (Vector3.Distance(hole1.transform.position, player.transform.position) <= 10)
            {
                GameObject enemy = Instantiate(Resources.Load<GameObject>("Role/7"));
                enemy.transform.position = hole1.transform.position;
            }
            if (Vector3.Distance(hole2.transform.position, player.transform.position) <= 10)
            {
                GameObject enemy1 = Instantiate(Resources.Load<GameObject>("Role/7"));
                enemy1.transform.position = hole2.transform.position;
            }
            if (Vector3.Distance(hole3.transform.position, player.transform.position) <= 10)
            {
                GameObject enemy2 = Instantiate(Resources.Load<GameObject>("Role/7"));
                enemy2.transform.position = hole3.transform.position;
            }

        }
    }
}
