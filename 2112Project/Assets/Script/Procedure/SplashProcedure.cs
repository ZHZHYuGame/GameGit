//using System;
//using UnityEngine;
//using UnityEngine.Video;
///// <summary>
///// 
///// </summary>
//public class SplashProcedure : ProcedureBase
//{
//   private VideoPlayer videoClip;
//    protected override void OnEnter()
//    {
//        //��UI���󣬲�������Ż�
//        videoClip = GameObject.Find("video").GetComponent<VideoPlayer>();
//        // ��ʾ��������
//        Debug.Log(">>>>���������������");
//        videoClip.Play();
//        videoClip.loopPointReached += OnVideoFinished;
//    }

//    private void OnVideoFinished(VideoPlayer source)
//    {
//        Debug.Log("��Ƶ������ɣ�");
//        videoClip.gameObject.transform.parent.gameObject.SetActive(false);
//        ProcedureManager.Instance.ChangeProcedure<InitConfigProcedure>();
//    }

//    protected override void OnUpdate()
//    {

//    }

//    protected override void OnExit()
//    {
//        // ��������������������
//        Debug.Log("�˳������������<<<<");
//    }
//}