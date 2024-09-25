using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    public Slider Progress_bar;
    public Button StartBtn;
    public Text tip;
    public Button Transcriptbutt;
    void Start()
    {
        StartBtn.onClick.AddListener(OnFight);
        Transcriptbutt.onClick.AddListener(OnTranscript);
        StartCoroutine(Logings());
    }

    IEnumerator Logings()
    {
        float value = 0;
        //WaitForSeconds wait = 
        float ram = 0;
        while (value < 100)
        {
            value+=10;
            Progress_bar.value = value;
            ram = Random.Range(0.1f, 1f);
            int n = Random.Range(0, 9);
            switch (n)
            {
                case 1: tip.text = "注意！怪物即将来袭，做好战斗准备注意！怪物即将来袭，做好战斗准备。"; break;
                case 2: tip.text = "你的生命值偏低，尽快寻找恢复道具。"; break;
                case 3: tip.text = "新区域已解锁，探索其中的神秘宝藏吧。"; break;
                case 4: tip.text = "发现隐藏任务，完成可获得额外奖励哦。"; break;
                case 5: tip.text = "任务时间紧迫，加快完成任务的步伐。"; break;
                case 6: tip.text = "技能冷却中，请合理安排攻击节奏。"; break;
                case 7: tip.text = "关卡难度提升，谨慎选择前进路线。"; break;
                case 8: tip.text = "背包已满，及时清理无用物品。"; break;
            }

            yield return new WaitForSeconds(ram);
        }

        StartBtn.gameObject.SetActive(true);
        Progress_bar.gameObject.SetActive(false);
    }

    private void OnFight()
    {
        SceneManager.LoadScene("HomeScene");
    }
    private void OnTranscript()
    {
        SceneManager.LoadScene("Transcript");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
