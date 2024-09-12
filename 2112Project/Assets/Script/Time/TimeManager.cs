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
/// ʱ�ӹ�������ͬһ������μ�ʱ��Ĭ�ϻᱻ���߸���,delayС��1������ִ�У�
/// </summary>
public class TimeManager : Singleton<TimeManager>, IAnimatable
{
    //DateTime.UtcNow��ȡ��ʱ��һֱ����ϵͳ�ġ������׼ʱ�䡱������ϵͳ�ı���ʱ���Ƿ����ã���ȡ��ʱ�䲻������Щ���ñ仯��
    DateTime GameStartDateTime = DateTime.UtcNow;
    float _SystemTime = 0; // ��
    public static List<IAnimatable> timerList = new List<IAnimatable>();

    private void Update()
    {
        var TimeSpan = DateTime.UtcNow - GameStartDateTime;
        _SystemTime = Convert.ToSingle(TimeSpan.TotalSeconds);
    }

    //������ʱ�������
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
        //ִ���������Ժ������̶�֡�ʸ���
        get { return Time.fixedDeltaTime; }
    }

    public static float RealMaxmumDeltaTime
    {
        //�κ�Time.deltaTime�����ֵ������Ϊ��λ��������֮֡��Time.time������
        get { return Time.maximumDeltaTime; }
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public float SystemTime
    {
        //������Ϸ��ʼ�����ھ���������
        get { return SystemTime; }
    }

    public int SystemTimeInt
    {
        //Mathf.CeilToInt����ȡ��������int��������
        get { return Mathf.CeilToInt(SystemTime); }
    }

    public TimeManager()
    {
        timerList.Add(this);
    }

    private List<TimerHandler> pool = new List<TimerHandler>();
    //�����鱣֤����˳��ִ��
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

        //���ִ��ʱ��С��1��ֱ��ִ��
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
    /// ��ʱִ��һ�Σ����ں��룩
    /// ֻ��ִ��һ�Σ�ִ����ɺ���Զ�����cleraTimer
    /// </summary>
    /// <param name="delay">�ӳ�ʱ�䣨��λΪ֡��</param>
    /// <param name="method">����ʱ�Ļص�����</param>
    /// <param name="args">�ص�����</param>
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
    /// ��ʱ�ظ�ִ�У����ں��룩
    /// �Ժ���Ϊ�������method
    /// ���ظ�����method�ص���ֱ��������������cleraTimer
    /// </summary>
    /// <param name="delay">�ӳ�ʱ�䣨��λΪ֡��</param>
    /// <param name="method">����ʱ�Ļص�����</param>
    /// <param name="args">�ص�����</param>
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
    /// ��ʱִ��һ�Σ�����֡�ʣ�
    /// </summary>
    /// <param name="delay">�ӳ�ʱ�䣨��λΪ֡��</param>
    /// <param name="method">����ʱ�Ļص�����</param>
    /// <param name="args">�ص�����</param>
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
    /// ��ʱ�ظ�ִ�У�����֡�ʣ�
    /// ��֡�������ã����贫1��1ִ֡��һ�Σ���2��2ִ֡��һ��
    /// </summary>
    /// <param name="delay">�ӳ�ʱ�䣨��λΪ֡��</param>
    /// <param name="method">����ʱ�Ļص�����</param>
    /// <param name="args">�ص�����</param>
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
    /// ����ʱ��
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
    /// �������ж�ʱ��
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
    /// ��Ϸ��������ʱ�䣬����
    /// </summary>
    public long CurrentTime
    {
        get { return (long)(Time.time * 1000); }
    }

    /// <summary>
    /// ��ʱ������
    /// </summary>
    private class TimerHandler
    {
        //ִ�м��
        public int delay;
        //�Ƿ��ظ�ִ��
        public bool repeat;
        //�Ƿ���֡��
        public bool userFrame;
        //ִ��ʱ��
        public long exeTime;
        //������
        public Delegate method;
        //����
        public object[] args;
        //����
        public void clear()
        {
            method = null;
            args = null;
        }
    }
}