using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

/// <summary>
/// �ʼ�������
/// </summary>
public class MailMgr 
{
    public enum GetAttachRsult
    {
        Success,
        NoAttach,//û����
        FullItem//��������
    }

    List<MailInfo> m_MailInfo = new List<MailInfo>();
    List<ItemInfo> m_CurAttachRewardList = new List<ItemInfo>();

    bool _hasNewMail = false;

    public List<ItemInfo> CurAttachRewardList
    {
        get { return m_CurAttachRewardList; }
    }

    /// <summary>
    /// ����ID��ȡ�ʼ���Ϣ
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
        //��Ϣ�ص�

        //�̶�ʱ��ѭ������
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
    /// ˢ���ʼ��б�
    /// </summary>
    void UpdateMailList()
    {
        UpdateHasNewMailState();

        m_MailInfo.Sort(SortMail);

        UpdateSystemOpenState();

        MessageEventMgr.GetInstance().Dispatch(MessageType.OnUpdateMailListEvent,null);
    }

    #region ����
    static int SortMail(MailInfo a, MailInfo b)
    {
        //�и�������Զ��ʱ����
        //û�������Ȱ���û�ж��� �ٰ�ʱ����

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
        //δ�� --> �ʼ�Խ��
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

            //���º��
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
