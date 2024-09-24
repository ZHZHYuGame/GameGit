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

            //ʹ������;
            app.MapHub<GameHub>("/mainhub");
            app.Run();
        }
    }
    public class GameHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            string sendMsg = $"������յ������Ϣ��user={user},msg={message}";

            Console.WriteLine(sendMsg);
             //��������͸����пͻ���
             await Clients.All.SendAsync("ReceiveMessage", sendMsg);
            //��������͸� ���������� �����пͻ���
            //await Clients.Others.SendAsync("ReceiveMessage", sendMsg);
        }

        #region ��ѧ����;
        ////�ͻ��˳ɹ�����ʱ���ᴥ���˷���
        //public override Task OnConnectedAsync()
        //{
        //    return Task.CompletedTask;
        //}

        ////�ͻ��˶Ͽ�����ʱ���ᴥ���˷���
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
            await Console.Out.WriteLineAsync($"���{id}��¼");
            await Clients.All.SendAsync("ReceiveConnectionInfo", id);
        }

        public async Task SendDisconnectionInfo(string id)
        {
            await Console.Out.WriteLineAsync($"���{id}����");
            await Clients.All.SendAsync("ReceiveDisconnectionInfo", id);
        }

        public async Task SendPosition(float x, float y, float z, string id)
        {
            await Console.Out.WriteLineAsync($"���{id}��ǰλ��{x + " " + y + " " + z}");
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

        //���÷���ʱ���ͻ���Ӧʹ�ô����ƣ�������.NET ��������
        [HubMethodName("SendMessageToUser")]
        public async Task DirectMessage(string user, string message)
     => await Clients.User(user).SendAsync("ReceiveMessage", user, message);
    }


}
