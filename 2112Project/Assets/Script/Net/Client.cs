
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 面向对象  客户端类
/// </summary>
public class Client
{
    public Socket socket;
    public byte[] data = new byte[1024];
    public int port;
    // public Player player = new Player();
}

