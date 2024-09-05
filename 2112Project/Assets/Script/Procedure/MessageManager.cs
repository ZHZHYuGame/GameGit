using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 消息中心
/// </summary>
/// <typeparam name="T"></typeparam>
public class MessageManager<T> : Singleton<MessageManager<T>>
{
    Dictionary<int, Action<T>> dic = new Dictionary<int, Action<T>>();
    public void OnAddListEvent(int id, Action<T> action)
    {
        if (dic.ContainsKey(id))
        {
            dic[id] += action;
        }
        else
        {
            dic.Add(id, action);
        }
    }
    public void OnBroadCast(int id, T t)
    {
        if (dic.ContainsKey(id))
        {
            dic[id](t);
        }
    }

    public void OnRemoveListEvent(int id, Action<T> action)
    {
        if (dic.ContainsKey(id))
        {
            dic[id] -= action;
            if (dic[id] == null)
            {
                dic.Remove(id);
            }
        }


    }
}

