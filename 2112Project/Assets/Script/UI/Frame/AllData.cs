using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllData : MonoBehaviour
{
    public List<Data> _bagData = new List<Data>();
    public List<Data> _shopData = new List<Data>();
    public List<PetData> _petData = new List<PetData>();

    

    private void Awake()
    {
        
    }

    private void Start()
    {
        //��������������󱳰�����

        //����������������������

    }

    /// <summary>
    /// �������ݸ�ֵ һ��ʼ��Ҫ����
    /// </summary>
    public void OnReceiveBagMsg()
    {

    }


    public void OnReceiveShopMsg()
    {

    }

    public void OnReceivePetMsg()
    {

    }

}


public class Data
{
    public int Id;
    public int Icon;
    public string Name;
    public string Type;
    public string Description;
    public int Num;
    public int Atk;
    public int Defense;
    public int Speed;
    public int Sale;
    public string Panth;
}