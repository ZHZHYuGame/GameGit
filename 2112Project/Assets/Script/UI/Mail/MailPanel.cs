using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings.Switch;

public class MailPanel : MonoBehaviour
{
    private void Awake()
    {
        //添加事件
        MessageEventMgr.GetInstance().AddListener(MessageType.OnUpdateMailListEvent, OnUpdateMailList);
    }

    private void OnDestroy()
    {
        //注销事件
        MessageEventMgr.GetInstance().RemoveListener(MessageType.OnUpdateMailListEvent, OnUpdateMailList);
    }

    /// <summary>
    /// 更新邮件列表
    /// </summary>
    void OnUpdateMailList(Message es)
    {
        //if (!_openClosePanel.IsOpened)
        //    return;

        SetAllMailList();
    }

    /// <summary>
    /// 设置全部邮件列表
    /// </summary>
    void SetAllMailList()
    {
        //var mailMgr = GameManager.Instance.MailMgr;
        //var mailList = mailMgr.MailInfo;
        //labNomail.text = LanUtil.GetLanStr(LanguageID.ID_3001);
        //labNomail.gameObject.SetActive(mailList.Count == 0);

        //if (wrapMail.MaxIndex == mailList.Count - 1)
        //{
        //    wrapMail.ForceUpdateItemList();
        //}
        //else
        //{
        //    wrapMail.MaxIndex = mailList.Count - 1;
        //}

        //btnGetAllAttch.isEnabled = mailMgr.HasAttachMail;
    }
}
