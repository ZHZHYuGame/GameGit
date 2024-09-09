using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 服务器类，使用单例模式管理网络连接
/// </summary>
public class NetManager : Singleton<NetManager>
{
    private Socket main_socket; // 主Socket用于监听客户端连接
    public List<Client> allusers = new List<Client>(); // 存储所有连接的客户端

    /// <summary>
    /// 初始化服务器，设置监听端口并开始接受客户端连接
    /// </summary>
    public void Init()
    {
        main_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Console.WriteLine(18888 + "测试服务器开启");
        main_socket.Bind(new IPEndPoint(IPAddress.Any, 18888)); // 绑定IP地址和端口
        main_socket.Listen(1000); // 设置最大监听队列长度为1000
        main_socket.BeginAccept(AsyAcceptCall, null); // 异步接受客户端连接
    }

    /// <summary>
    /// 异步接收客户端连接回调方法
    /// </summary>
    /// <param name="ar">异步结果</param>
    private void AsyAcceptCall(IAsyncResult ar)
    {
        try
        {
            Socket socket = main_socket.EndAccept(ar); // 完成接受客户端连接
            IPEndPoint ip = socket.RemoteEndPoint as IPEndPoint; // 获取客户端IP和端口
            Console.WriteLine(ip.Port);
            Client client = new Client(); // 创建客户端对象
            client.socket = socket; // 保存客户端Socket
            client.port = ip.Port; // 保存客户端端口
            allusers.Add(client); // 添加到客户端列表
            socket.BeginReceive(client.data, 0, client.data.Length, SocketFlags.None, AsyReceiveCall, client); // 开始异步接收数据
            main_socket.BeginAccept(AsyAcceptCall, null); // 继续监听其他客户端连接
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex); // 输出异常信息
        }
    }

    /// <summary>
    /// 异步接收数据回调方法
    /// </summary>
    /// <param name="ar">异步结果</param>
    private void AsyReceiveCall(IAsyncResult ar)
    {
        try
        {
            Client client = ar.AsyncState as Client; // 获取客户端对象
            int len = client.socket.EndReceive(ar); // 获取接收到的数据长度
            if (len > 0)
            {
                byte[] rdata = new byte[len]; // 存储接收到的数据
                Buffer.BlockCopy(client.data, 0, rdata, 0, len); // 复制数据到临时数组
                while (rdata.Length > 4)
                {
                    int bodylen = BitConverter.ToInt32(rdata, 0); // 获取消息体长度
                    byte[] bodydata = new byte[bodylen]; // 存储消息体数据
                    Buffer.BlockCopy(rdata, 4, bodydata, 0, bodylen); // 复制消息体数据

                    int msgid = BitConverter.ToInt32(bodydata, 0); // 获取消息ID
                    byte[] info = new byte[bodydata.Length - 4]; // 消息内容
                    Buffer.BlockCopy(bodydata, 4, info, 0, bodydata.Length - 4);

                    MsgData msgData = new MsgData(); // 创建消息数据对象
                    msgData.data = info;
                    msgData.client = client;
                    MessageManager<MsgData>.Instance.OnBroadCast(msgid, msgData); // 广播消息

                    int sylen = rdata.Length - 4 - bodylen; // 剩余数据长度
                    byte[] sydata = new byte[sylen]; // 剩余数据
                    Buffer.BlockCopy(rdata, 4 + bodylen, sydata, 0, sylen);
                    rdata = sydata; // 更新数据
                }
            }

            if (client.socket.Connected)
            {
                client.socket.BeginReceive(client.data, 0, client.data.Length, SocketFlags.None, AsyReceiveCall, client); // 继续接收数据
            }
            else
            {
                client.socket.Shutdown(SocketShutdown.Both); // 关闭Socket连接
                client.socket.Close(); // 关闭Socket
                allusers.Remove(client); // 从客户端列表中移除
                Console.WriteLine(client.port + "已下线");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex); // 输出异常信息
        }
    }

    /// <summary>
    /// 异步发送数据
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <param name="data">要发送的数据</param>
    /// <param name="client">客户端对象</param>
    public void OnSendAsy(int id, byte[] data, Client client)
    {
        int head = 4 + data.Length; // 头部长度
        byte[] enddata = new byte[0]; // 创建空数组
        enddata = enddata.Concat(BitConverter.GetBytes(head)).Concat(BitConverter.GetBytes(id)).Concat(data).ToArray(); // 拼接头部和数据
        client.socket.BeginSend(enddata, 0, enddata.Length, SocketFlags.None, AsySendCall, client); // 异步发送数据
    }

    /// <summary>
    /// 异步发送数据回调方法
    /// </summary>
    /// <param name="ar">异步结果</param>
    private void AsySendCall(IAsyncResult ar)
    {
        Client client = ar.AsyncState as Client; // 获取客户端对象
        int len = client.socket.EndSend(ar); // 完成发送
    }
}

