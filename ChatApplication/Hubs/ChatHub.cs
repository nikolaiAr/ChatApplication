using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ChatApplication.Models;

namespace ChatApplication.Hubs
{
    public class ChatHub : Hub
    {
        static List<User> Users = new List<User>();
        MessContext db = new MessContext();
        

        // Отправка сообщений
        public void Send(string name, string message)
        {
            Message mess = new Message();
            mess.Name = name;
            mess.Text = message;
            db.Messages.Add(mess);
            db.SaveChanges();
            Clients.All.addMessage(name, message);
        }

        // Подключение нового пользователя
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;


            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new User { ConnectionId = id, Name = userName });

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, userName, Users);

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }
            if (0 == Users.Count)
            {
                foreach (Message current in db.Messages)
                    db.Messages.Remove(current);
                db.SaveChanges();
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}