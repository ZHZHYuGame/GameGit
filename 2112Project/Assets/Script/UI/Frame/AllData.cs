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
        //向服务器发送请求背包数据

        //向服务器发送请求宠物数据

    }

    /// <summary>
    /// 背包数据赋值 一开始就要接收
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