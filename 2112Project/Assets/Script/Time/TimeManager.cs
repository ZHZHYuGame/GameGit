using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void Handler();
public delegate void Handler<T1>(T1 obj1);
public delegate void Handler<T1, T2>(T1 obj1, T2 obj2);
public delegate void Handler<T1, T2, T3>(T1 obj1, T2 obj2, T3 Obj3);

public interface IAnimatable
{
    void AdvanceTime();
}

/// <summary>
/// 时钟管理器（同一函数多次计时，默认会被后者覆盖,delay小于1会立即执行）
/// </summary>
public class TimeManager : Singleton<TimeManager>, IAnimatable
{
    //DateTime.UtcNow读取的时间一直都是系统的“世界标准时间”，不管系统的本地时区是否设置，读取的时间不会随这些设置变化；
    DateTime GameStartDateTime = DateTime.UtcNow;
    float _SystemTime = 0; // 秒
    public static List<IAnimatable> timerList = new List<IAnimatable>();

    private void Update()
    {
        var TimeSpan = DateTime.UtcNow - GameStartDateTime;
        _SystemTime = Convert.ToSingle(TimeSpan.TotalSeconds);
    }

    //基本的时间控制类
    public float GetGameTime
    {
        get { return Time.time; }
    }
    public float GetDeltaTime
    {
        get { return Time.deltaTime; }
    }

    public static float RealFixedDeltaTime
    {
        //执行物理特性和其他固定帧率更新
        get { return Time.fixedDeltaTime; }
    }

    public static float RealMaxmumDeltaTime
    {
        //任何Time.deltaTime的最大值，以秒为单位，限制两帧之间Time.time的增加
        get { return Time.maximumDeltaTime; }
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public float SystemTime
    {
        //返回游戏开始到现在经过的秒数
        get { return SystemTime; }
    }

    public int SystemTimeInt
    {
        //Mathf.CeilToInt向上取整，返回int类型整数
        get { return Mathf.CeilToInt(SystemTime); }
    }

    public TimeManager()
    {
        timerList.Add(this);
    }

    private List<TimerHandler> pool = new List<TimerHandler>();
    //用数组保证放入顺序执行
    private List<TimerHandler> handlers = new List<TimerHandler>();
    private int currFrame = 0;
    private uint index = 0;

    public void AdvanceTime()
    {
        currFrame++;
        for (int i = 0; i < handlers.Count; i++)
        {
            TimerHandler handler = handlers[i];
            long t = handler.userFrame ? currFrame : CurrentTime;
            if (t >= handler.exeTime)
            {
                Delegate method = handler.method;
                object[] args = handler.args;
                if (handler.repeat)
                {
                    while (t >= handler.exeTime)
                    {
                        handler.exeTime += handler.delay;
                        method.DynamicInvoke(args);
                    }
                }
                else
                {
                    Clear(handler.method);
                    method.DynamicInvoke(args);
                }
            }
        }
    }

    private object Create(bool userFrame, bool repeat, int delay, Delegate method, params object[] args)
    {
        if (method == null)
        {
            return null;
        }

        //如果执行时间小于1，直接执行
        if (delay < 1)
        {
            method.DynamicInvoke(args);
            return -1;
        }
        TimerHandler handler;
        if (pool.Count > 0)
        {
            handler = pool[pool.Count - 1];
            pool.Remove(handler);
        }
        else
        {
            handler = new TimerHandler();
        }
        handler.userFrame = userFrame;
        handler.repeat = repeat;
        handler.delay = delay;
        handler.method = method;
        handler.args = args;
        handler.exeTime = delay + (userFrame ? currFrame : CurrentTime);
        handlers.Add(handler);
        return method;
    }

    /// <summary>
    /// 定时执行一次（基于毫秒）
    /// 只会执行一次，执行完成后会自动调用cleraTimer
    /// </summary>
    /// <param name="delay">延迟时间（单位为帧）</param>
    /// <param name="method">结束时的回调方法</param>
    /// <param name="args">回调参数</param>
    public void DoOnce(int delay, Handler method)
    {
        Create(false, false, delay, method);
    }
    public void DoOnce<T1>(int delay, Handler<T1> method, params object[] args)
    {
        Create(false, false, delay, method, args);
    }
    public void DoOnce<T1, T2>(int delay, Handler<T1, T2> method, params object[] args)
    {
        Create(false, false, delay, method, args);
    }
    public void DoOnce<T1, T2, T3>(int delay, Handler<T1, T2, T3> method, params object[] args)
    {
        Create(false, false, delay, method, args);
    }

    /// <summary>
    /// 定时重复执行（基于毫秒）
    /// 以毫秒为间隔调用method
    /// 会重复调用method回调，直到程序主动调用cleraTimer
    /// </summary>
    /// <param name="delay">延迟时间（单位为帧）</param>
    /// <param name="method">结束时的回调方法</param>
    /// <param name="args">回调参数</param>
    public void DoLoop(int delay, Handler method)
    {
        Create(false, true, delay, method);
    }
    public void DoLoop<T1>(int delay, Handler<T1> method, params object[] args)
    {
        Create(false, true, delay, method, args);
    }
    public void DoLoop<T1, T2>(int delay, Handler<T1, T2> method, params object[] args)
    {
        Create(false, true, delay, method, args);
    }
    public void DoLoop<T1, T2, T3>(int delay, Handler<T1, T2, T3> method, params object[] args)
    {
        Create(false, true, delay, method, args);
    }

    /// <summary>
    /// 定时执行一次（基于帧率）
    /// </summary>
    /// <param name="delay">延迟时间（单位为帧）</param>
    /// <param name="method">结束时的回调方法</param>
    /// <param name="args">回调参数</param>
    public void DoFrameOnce(int delay, Handler method)
    {
        Create(true, false, delay, method);
    }
    public void DoFrameOnce<T1>(int delay, Handler<T1> method, params object[] args)
    {
        Create(true, false, delay, method, args);
    }
    public void DoFrameOnce<T1, T2>(int delay, Handler<T1, T2> method, params object[] args)
    {
        Create(true, false, delay, method, args);
    }
    public void DoFrameOnce<T1, T2, T3>(int delay, Handler<T1, T2, T3> method, params object[] args)
    {
        Create(true, false, delay, method, args);
    }

    /// <summary>
    /// 定时重复执行（基于帧率）
    /// 以帧速来调用，假设传1，1帧执行一次，传2，2帧执行一次
    /// </summary>
    /// <param name="delay">延迟时间（单位为帧）</param>
    /// <param name="method">结束时的回调方法</param>
    /// <param name="args">回调参数</param>
    public void DoFrameLoop(int delay, Handler method)
    {
        Create(true, true, delay, method);
    }
    public void DoFrameLoop<T1>(int delay, Handler<T1> method, params object[] args)
    {
        Create(true, true, delay, method, args);
    }
    public void DoFrameLoop<T1, T2>(int delay, Handler<T1, T2> method, params object[] args)
    {
        Create(true, true, delay, method, args);
    }
    public void DoFrameLoop<T1, T2, T3>(int delay, Handler<T1, T2, T3> method, params object[] args)
    {
        Create(true, true, delay, method, args);
    }

    /// <summary>
    /// 清理定时器
    /// </summary>
    /// <param name="method"></param>
    public void ClearTimer(Handler method)
    {
        Clear(method);
    }
    public void ClearTimer<T1>(Handler<T1> method)
    {
        Clear(method);
    }
    public void ClearTimer<T1, T2>(Handler<T1, T2> method)
    {
        Clear(method);
    }
    public void ClearTimer<T1, T2, T3>(Handler<T1, T2, T3> method)
    {
        Clear(method);
    }

    private void Clear(Delegate method)
    {
        TimerHandler handler = handlers.FirstOrDefault(t => t.method == method);
        if (handler != null)
        {
            handlers.Remove(handler);
            handler.clear();
            pool.Add(handler);
        }
    }

    /// <summary>
    /// 清理所有定时器
    /// </summary>
    public void ClearAllTimer()
    {
        foreach (TimerHandler handler in handlers)
        {
            Clear(handler.method);
            ClearAllTimer();
            return;
        }
    }

    public static void RemoveTimerMgr(TimeManager timeManager)
    {
        timerList.Remove(timeManager);
    }

    /// <summary>
    /// 游戏自启运行时间，毫秒
    /// </summary>
    public long CurrentTime
    {
        get { return (long)(Time.time * 1000); }
    }

    /// <summary>
    /// 定时处理器
    /// </summary>
    private class TimerHandler
    {
        //执行间隔
        public int delay;
        //是否重复执行
        public bool repeat;
        //是否用帧率
        public bool userFrame;
        //执行时间
        public long exeTime;
        //处理方法
        public Delegate method;
        //参数
        public object[] args;
        //清理
        public void clear()
        {
            method = null;
            args = null;
        }
    }
}