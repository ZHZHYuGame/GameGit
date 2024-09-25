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
                case 1: tip.text = "ע�⣡���Ｔ����Ϯ������ս��׼��ע�⣡���Ｔ����Ϯ������ս��׼����"; break;
                case 2: tip.text = "�������ֵƫ�ͣ�����Ѱ�һָ����ߡ�"; break;
                case 3: tip.text = "�������ѽ�����̽�����е����ر��ذɡ�"; break;
                case 4: tip.text = "��������������ɿɻ�ö��⽱��Ŷ��"; break;
                case 5: tip.text = "����ʱ����ȣ��ӿ��������Ĳ�����"; break;
                case 6: tip.text = "������ȴ�У�������Ź������ࡣ"; break;
                case 7: tip.text = "�ؿ��Ѷ�����������ѡ��ǰ��·�ߡ�"; break;
                case 8: tip.text = "������������ʱ����������Ʒ��"; break;
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
