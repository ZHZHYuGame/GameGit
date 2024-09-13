using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 邮件信息
/// </summary>
public class MailInfo 
{
    //邮件id
    long _mailId;
    //邮件发送日期
    long _sendTime;
    //邮件有效的毫秒数
    long _validTime;
    //邮件删除方式，0-读完不删除，1-读完删除
    int _deleteType;
    //发送者名字
    string _senderName;
    //邮件标题
    string _subject;
    //邮件内容
    string _content;
    //邮件参数
    List<string> _mailParams = new List<string>();
    //邮件附件
    List<ItemInfo> _attachInfo = new List<ItemInfo>();
    //邮件状态，0：未读 1：已读 2：已取
    int _status;
    //是否需要解析（1：需要，0：不需要）
    int _needParse;

    /**
	 * 邮件id
	 */
    public long MailId
    {
        set { _mailId = value; }
        get { return _mailId; }
    }

    /**
	 * 邮件发送日期
	 */
    public long SendTime
    {
        set { _sendTime = value; }
        get { return _sendTime; }
    }

    /**
	 * 邮件有效的毫秒数
	 */
    public long ValidTime
    {
        set { _validTime = value; }
        get { return _validTime; }
    }

    /**
	 * 邮件删除方式，0-读完不删除，1-读完删除
	 */
    public int DeleteType
    {
        set { _deleteType = value; }
        get { return _deleteType; }
    }

    /**
	 * 发送者名字
	 */
    public string SenderName
    {
        set { _senderName = value; }
        get { return _senderName; }
    }

    /**
	 * 邮件标题
	 */
    public string Subject
    {
        set { _subject = value; }
        get { return _subject; }
    }

    /**
	 * 邮件内容
	 */
    public string Content
    {
        set { _content = value; }
        get { return _content; }
    }

    /**
	 * get 邮件参数
	 * @return 
	 */
    public List<string> GetMailParams()
    {
        return _mailParams;
    }

    /**
	 * set 邮件参数
	 */
    public void SetMailParams(List<string> mailParams)
    {
        _mailParams = mailParams;
    }

    /**
	 * get 邮件附件
	 * @return 
	 */
    public List<ItemInfo> GetAttachInfo()
    {
        return _attachInfo;
    }

    /**
	 * set 邮件附件
	 */
    public void SetAttachInfo(List<ItemInfo> attachInfo)
    {
        _attachInfo = attachInfo;
    }

    /**
	 * 邮件状态，0：未读 1：已读 2：已取
	 */
    public int Status
    {
        set { _status = value; }
        get { return _status; }
    }

    /**
	 * 是否需要解析（1：需要，0：不需要）
	 */
    public int NeedParse
    {
        set { _needParse = value; }
        get { return _needParse; }
    }
}
