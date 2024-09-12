using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetPanel : UIBase
{
    //�رհ�ť
    public Button ClosePanel;
    //Ԥ���常��
    public Transform ItemParent;
    //����ͼƬ
    public Image PetImage;
    //��������
    public Text PetName;
    //����ȼ�
    public Text PetLevel;
    //��ս��ť
    public Button ToFightButton;
    //��Ϣ��ť
    public Button RestButton;
    //������ť
    public Button FreeCaptiveAnimalsButton;
    //��������
    public Button ActiveSkillButton;
    //������������
    public Text ActiveSkillDes;
    //���������˺�
    public Text PhysicalInjury;
    //����ħ���˺�
    public Text MagicDamage;
    //������Ѫ
    public Text Hp;
    //����ħ��
    public Text Mp;
    //�����ٶ�
    public Text Speed;
    //�������
    public Text Defense;
    //���ﾭ��
    public Text Experience;
    //����ϴ��
    public Button Sophistication;
    //��������
    public Button Upgrade;
    //�������ݼ���
    public List<PetData> Pets = new List<PetData>();

    //��¼��ǰ����
    public PetData Pet;


    public override void Awake()
    {
        base.Awake();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        if(Pet != null)
        {
            UpdatePetPanel(Pet);
        }
        else
        {
            PetInit();
        }
    }
    //��������ʼ��
    private void PetInit()
    {
        for(int i=0;i<Pets.Count;i++)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("PetItem"), ItemParent);
            go.GetComponent<PetItem>().Init(Pets[i]);
        }
        //UpdatePetPanel(Pets[0]);

    }
    //���³������
    public void UpdatePetPanel(PetData petData)
    {
        Pet=petData;
        PetImage.sprite = Resources.Load<Sprite>(petData.PetIcon);
        PetName.text = "�������֣�" + petData.PetName;
        PetLevel.text = "����ȼ���" + petData.PetLevel;
        ActiveSkillButton.GetComponent<Image>().sprite = Resources.Load<Sprite>(petData.PetActiveSkill);
        ActiveSkillDes.text = "����������" + petData.PetActiveSkillDes;
        PhysicalInjury.text = "�����˺���" + petData.PhysicalInjury;
        MagicDamage.text = "ħ���˺���" + petData.MagicDamage;
        Hp.text = "��Ѫ��" + petData.Hp;
        Mp.text = "ħ����" + petData.Mp;
        Speed.text = "�ٶȣ�" + petData.Speed;
        Defense.text = "������" + petData.Defense;
        Experience.text = "���飺" + petData.Experience;
        if(petData.isToFight)
        {
            ToFightButton.gameObject.SetActive(false);
            RestButton.gameObject.SetActive(true);
        }
    }
    public override void Start()
    {
        base.Start();
        ClosePanel.onClick.AddListener(() =>
        {
            HideUI();
        });
        //��ս��ť
        ToFightButton.onClick.AddListener(() =>
        {

        });
        //��Ϣ��ť
        RestButton.onClick.AddListener(() =>
        {

        });
        //������ť
        FreeCaptiveAnimalsButton.onClick.AddListener(() =>
        {

        });
        Sophistication.onClick.AddListener(() =>
        {

        });
        Upgrade.onClick.AddListener(() =>
        {

        });
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OpenUI()
    {
        base.OpenUI();
    }

    public override void HideUI()
    {
        base.HideUI();
    }

    public override void CloseUI()
    {
        base.CloseUI();
    }

    public void Test()
    {
        print("���Գɹ�");
    }
}
