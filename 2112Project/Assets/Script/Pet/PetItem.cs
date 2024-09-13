using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetItem : MonoBehaviour
{
    public Button PetButton;
    public Image PetIcon;
    public Image Lock;
    void Start()
    {
        
    }
    public void Init(PetData petData)
    {
        PetIcon.sprite = Resources.Load<Sprite>(petData.PetIcon);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
