using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;

namespace FormAuthenticationTest
{
    public class MyHub : Hub
    {
        public void Join(string room)
        {
            Groups.Add(Context.ConnectionId, room);
            Clients.Caller.addMessage(room, "欢迎来到聊天室： " + room);
        }

        public void CreateChatRoom(string room)
        {
            if (!ChatRooms.Exists(room))
            {
                ChatRooms.Add(room);
                Clients.All.addChatRoom(room);
            }
        }

        public void Send(string room, string message)
        {
            var info = "服务端接收的额外消息"+Clients.Caller.ExtraInfo;
            Clients.All.addMessage(room, message+info);
        }

        public void SendMessageToAll(string userName, string message)
        {
            // Broad cast message
            Clients.All.messageReceived(userName, message);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            
            foreach (var room in ChatRooms.GetAll())
            {
                //给新连接的客户端的聊天室列表添加所有聊天室
                Clients.Caller.addChatRoom(room);
            }
            return base.OnConnected();
        }
        
    }

    #region 聊天室类

    public class ChatRooms
    {
        private static List<string> _rooms = new List<string>();

        static ChatRooms()
        {
            _rooms.Add("默认聊天室1");
        }

        public static void Add(string name)
        {
            _rooms.Add(name);
        }

        public static bool Exists(string name)
        {
            return _rooms.Contains(name);
        }

        public static IEnumerable<string> GetAll()
        {
            return _rooms;
        }
    }

    #endregion

    #region 在Application_Start方法中注册SignalR
    public class SignalRConfiguration
    {
        public static void RegisterHub(RouteCollection routes)
        {
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableCrossDomain = true;
            hubConfiguration.EnableDetailedErrors = true;
            hubConfiguration.EnableJavaScriptProxies = true;
            routes.MapHubs("/signalr", hubConfiguration);
        }
    }
    #endregion
}