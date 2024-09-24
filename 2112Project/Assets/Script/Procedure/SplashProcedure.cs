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
//        //有UI面板后，补充代码优化
//        videoClip = GameObject.Find("video").GetComponent<VideoPlayer>();
//        // 显示启动画面
//        Debug.Log(">>>>进入启动画面程序");
//        videoClip.Play();
//        videoClip.loopPointReached += OnVideoFinished;
//    }

//    private void OnVideoFinished(VideoPlayer source)
//    {
//        Debug.Log("视频播放完成！");
//        videoClip.gameObject.transform.parent.gameObject.SetActive(false);
//        ProcedureManager.Instance.ChangeProcedure<InitConfigProcedure>();
//    }

//    protected override void OnUpdate()
//    {

//    }

//    protected override void OnExit()
//    {
//        // 隐藏启动画面等清理操作
//        Debug.Log("退出启动画面程序<<<<");
//    }
//}