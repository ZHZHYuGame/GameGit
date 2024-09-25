using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

/// <summary>
/// 邮件管理类
/// </summary>
public class MailMgr 
{
    public enum GetAttachRsult
    {
        Success,
        NoAttach,//没附件
        FullItem//背包满了
    }

    List<MailInfo> m_MailInfo = new List<MailInfo>();
    List<ItemInfo> m_CurAttachRewardList = new List<ItemInfo>();

    bool _hasNewMail = false;

    public List<ItemInfo> CurAttachRewardList
    {
        get { return m_CurAttachRewardList; }
    }

    /// <summary>
    /// 根据ID获取邮件信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public MailInfo GetMailInfoById(long id)
    {
        for (int i = 0; i < m_MailInfo.Count; i++)
        {
            if (m_MailInfo[i].MailId == id)
            {
                return m_MailInfo[i];
            }
        }
        return null;
    }

    public MailMgr()
    {
        //消息回调

        //固定时间循环调用
        //TimeManager.Instance.DoLoop(1,Tick);
    }

    void Tick()
    {
        if (m_MailInfo.Count == 0)
        {
            return;
        }

        RemoveExpiredMail();
    }

    void RemoveExpiredMail()
    {
        var mailCount = m_MailInfo.Count;

        for (var i = 0; i < m_MailInfo.Count;)
        {
            var mailInfo = m_MailInfo[i];
            var mailTime = mailInfo.ValidTime * 0.001f;
            if (Time.unscaledTime > mailTime)
            {
                m_MailInfo.Remove(mailInfo);
            }
            else
            {
                ++i;
            }
        }

        if (mailCount != m_MailInfo.Count)
        {
            UpdateMailList();
        }
    }

    /// <summary>
    /// 刷新邮件列表
    /// </summary>
    void UpdateMailList()
    {
        UpdateHasNewMailState();

        m_MailInfo.Sort(SortMail);

        UpdateSystemOpenState();

        MessageEventMgr.GetInstance().Dispatch(MessageType.OnUpdateMailListEvent,null);
    }

    #region 排序
    static int SortMail(MailInfo a, MailInfo b)
    {
        //有附件的永远按时间排
        //没附件的先按有没有读过 再按时间排

        var aHasAttach = a.GetAttachInfo().Count > 0;
        var bHasAttach = b.GetAttachInfo().Count > 0;
        if (aHasAttach && bHasAttach)
        {
            return a.SendTime > b.SendTime ? -1 : 1;
        }
        else if (aHasAttach && !bHasAttach)
        {
            if (b.Status == 0)
            {
                return a.SendTime > b.SendTime ? -1 : 1;
            }
            else
            {
                return -1;
            }
        }
        else if (!aHasAttach && bHasAttach)
        {
            if (a.Status == 0)
            {
                return a.SendTime > b.SendTime ? -1 : 1;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            return SortByStatus(a, b);
        }
    }

    static int SortByStatus(MailInfo a, MailInfo b)
    {
        //未读 --> 邮件越新
        if (a.Status == 0 && b.Status == 0)
        {
            return a.SendTime > b.SendTime ? -1 : 1;
        }
        else if (a.Status == 0 || b.Status == 0)
        {
            return a.Status == 0 ? -1 : 1;
        }
        else
        {
            return a.SendTime > b.SendTime ? -1 : 1;
        }
    }
    #endregion

    void UpdateHasNewMailState()
    {
        var hasNewMail = false;
        for (int i = 0; i < m_MailInfo.Count; i++)
        {
            if (m_MailInfo[i].Status == 0)
            {
                hasNewMail = true;
                break;
            }
        }

        HasNewMail = hasNewMail;
    }

    public bool HasNewMail
    {
        get { return _hasNewMail; }
        private set
        {
            if (_hasNewMail == value)
            {
                return;
            }

            _hasNewMail = value;

            //更新红点
        }
    }

    void UpdateSystemOpenState()
    {
        //GameManager.Instance.SystemUnLockMgr.UpdateSystemUnlockState(SystemType.Mail, isShowMailIcon);
    }

    bool isShowMailIcon
    {
        get
        {
            return m_MailInfo.Count > 0;
        }
    }
}
