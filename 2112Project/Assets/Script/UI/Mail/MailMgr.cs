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
    }
}
