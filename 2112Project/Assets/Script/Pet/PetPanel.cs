using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetPanel : UIBase
{
    //关闭按钮
    public Button ClosePanel;
    //预制体父级
    public Transform ItemParent;
    //宠物图片
    public Image PetImage;
    //宠物名字
    public Text PetName;
    //宠物等级
    public Text PetLevel;
    //出战按钮
    public Button ToFightButton;
    //休息按钮
    public Button RestButton;
    //放生按钮
    public Button FreeCaptiveAnimalsButton;
    //主动技能
    public Button ActiveSkillButton;
    //主动技能描述
    public Text ActiveSkillDes;
    //宠物物理伤害
    public Text PhysicalInjury;
    //宠物魔法伤害
    public Text MagicDamage;
    //宠物气血
    public Text Hp;
    //宠物魔法
    public Text Mp;
    //宠物速度
    public Text Speed;
    //宠物防御
    public Text Defense;
    //宠物经验
    public Text Experience;
    //宠物洗练
    public Button Sophistication;
    //宠物升级
    public Button Upgrade;
    //宠物数据集合
    public List<PetData> Pets = new List<PetData>();

    //记录当前宠物
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
    //宠物面板初始化
    private void PetInit()
    {
        for(int i=0;i<Pets.Count;i++)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("PetItem"), ItemParent);
            go.GetComponent<PetItem>().Init(Pets[i]);
        }
        //UpdatePetPanel(Pets[0]);

    }
    //更新宠物面板
    public void UpdatePetPanel(PetData petData)
    {
        Pet=petData;
        PetImage.sprite = Resources.Load<Sprite>(petData.PetIcon);
        PetName.text = "宠物名字：" + petData.PetName;
        PetLevel.text = "宠物等级：" + petData.PetLevel;
        ActiveSkillButton.GetComponent<Image>().sprite = Resources.Load<Sprite>(petData.PetActiveSkill);
        ActiveSkillDes.text = "技能描述：" + petData.PetActiveSkillDes;
        PhysicalInjury.text = "物理伤害：" + petData.PhysicalInjury;
        MagicDamage.text = "魔法伤害：" + petData.MagicDamage;
        Hp.text = "气血：" + petData.Hp;
        Mp.text = "魔法：" + petData.Mp;
        Speed.text = "速度：" + petData.Speed;
        Defense.text = "防御：" + petData.Defense;
        Experience.text = "经验：" + petData.Experience;
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
        //出战按钮
        ToFightButton.onClick.AddListener(() =>
        {

        });
        //休息按钮
        RestButton.onClick.AddListener(() =>
        {

        });
        //放生按钮
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
        print("测试成功");
    }
}
