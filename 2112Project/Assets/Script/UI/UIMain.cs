using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    // Start is called before the first frame update
    public Button StartBtn;
    void Start()
    {
        StartBtn.onClick.AddListener(OnFight);
    }

    private void OnFight()
    {
        SceneManager.LoadScene("CombatScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
