using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// �ʼ���Ϣ
/// </summary>
public class MailInfo 
{
    //�ʼ�id
    long _mailId;
    //�ʼ���������
    long _sendTime;
    //�ʼ���Ч�ĺ�����
    long _validTime;
    //�ʼ�ɾ����ʽ��0-���겻ɾ����1-����ɾ��
    int _deleteType;
    //����������
    string _senderName;
    //�ʼ�����
    string _subject;
    //�ʼ�����
    string _content;
    //�ʼ�����
    List<string> _mailParams = new List<string>();
    //�ʼ�����
    List<ItemInfo> _attachInfo = new List<ItemInfo>();
    //�ʼ�״̬��0��δ�� 1���Ѷ� 2����ȡ
    int _status;
    //�Ƿ���Ҫ������1����Ҫ��0������Ҫ��
    int _needParse;

    /**
	 * �ʼ�id
	 */
    public long MailId
    {
        set { _mailId = value; }
        get { return _mailId; }
    }

    /**
	 * �ʼ���������
	 */
    public long SendTime
    {
        set { _sendTime = value; }
        get { return _sendTime; }
    }

    /**
	 * �ʼ���Ч�ĺ�����
	 */
    public long ValidTime
    {
        set { _validTime = value; }
        get { return _validTime; }
    }

    /**
	 * �ʼ�ɾ����ʽ��0-���겻ɾ����1-����ɾ��
	 */
    public int DeleteType
    {
        set { _deleteType = value; }
        get { return _deleteType; }
    }

    /**
	 * ����������
	 */
    public string SenderName
    {
        set { _senderName = value; }
        get { return _senderName; }
    }

    /**
	 * �ʼ�����
	 */
    public string Subject
    {
        set { _subject = value; }
        get { return _subject; }
    }

    /**
	 * �ʼ�����
	 */
    public string Content
    {
        set { _content = value; }
        get { return _content; }
    }

    /**
	 * get �ʼ�����
	 * @return 
	 */
    public List<string> GetMailParams()
    {
        return _mailParams;
    }

    /**
	 * set �ʼ�����
	 */
    public void SetMailParams(List<string> mailParams)
    {
        _mailParams = mailParams;
    }

    /**
	 * get �ʼ�����
	 * @return 
	 */
    public List<ItemInfo> GetAttachInfo()
    {
        return _attachInfo;
    }

    /**
	 * set �ʼ�����
	 */
    public void SetAttachInfo(List<ItemInfo> attachInfo)
    {
        _attachInfo = attachInfo;
    }

    /**
	 * �ʼ�״̬��0��δ�� 1���Ѷ� 2����ȡ
	 */
    public int Status
    {
        set { _status = value; }
        get { return _status; }
    }

    /**
	 * �Ƿ���Ҫ������1����Ҫ��0������Ҫ��
	 */
    public int NeedParse
    {
        set { _needParse = value; }
        get { return _needParse; }
    }
}
