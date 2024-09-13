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
    }
}
