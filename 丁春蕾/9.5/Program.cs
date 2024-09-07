using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.StaticFiles;

namespace Game_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSignalR();

            var app = builder.Build();

            //使用中心;
            app.MapHub<GameHub>("/mainhub");
            app.Run();
        }
    }
    public class GameHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            string sendMsg = $"服务的收到你的消息，user={user},msg={message}";

            Console.WriteLine(sendMsg);
             //服务端推送给所有客户端
             await Clients.All.SendAsync("ReceiveMessage", sendMsg);
            //服务端推送给 除自已以外 的所有客户端
            //await Clients.Others.SendAsync("ReceiveMessage", sendMsg);
        }

        #region 待学代码;
        ////客户端成功连接时，会触发此方法
        //public override Task OnConnectedAsync()
        //{
        //    return Task.CompletedTask;
        //}

        ////客户端断开连接时，会触发此方法
        //public override Task OnDisconnectedAsync(Exception? exception)
        //{
        //    return Task.CompletedTask;

        //}
        //public async Task SendMessageToCaller(string user, string message)
        //    => await Clients.Caller.SendAsync("ReceiveMessage", user, message);

        //public async Task SendMessageToGroup(string user, string message)
        //    => await Clients.Group("SignalR Users").SendAsync("ReceiveMessage", user, message);

        #endregion

        public async Task SendConnectionInfo(string id)
        {
            await Console.Out.WriteLineAsync($"玩家{id}登录");
            await Clients.All.SendAsync("ReceiveConnectionInfo", id);
        }

        public async Task SendDisconnectionInfo(string id)
        {
            await Console.Out.WriteLineAsync($"玩家{id}下线");
            await Clients.All.SendAsync("ReceiveDisconnectionInfo", id);
        }

        public async Task SendPosition(float x, float y, float z, string id)
        {
            await Console.Out.WriteLineAsync($"玩家{id}当前位置{x + " " + y + " " + z}");
            await Clients.AllExcept(id).SendAsync("ReceivePosition", x, y, z, id);
        }

    }


    public class ChatHub : Hub
    {
        public async Task<string> WaitForMessage(string connectionId)
        {
            var message = await Clients.Client(connectionId).InvokeAsync<string>(
                "GetMessage", CancellationToken.None);
            return message;
        }

        //调用方法时，客户端应使用此名称，而不是.NET 方法名称
        [HubMethodName("SendMessageToUser")]
        public async Task DirectMessage(string user, string message)
     => await Clients.User(user).SendAsync("ReceiveMessage", user, message);
    }


}
