using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApplication.Models
{
    public class Message
    {
        // ID сообщения
        public int Id { get; set; }
        // имя пользователя
        public string Name { get; set; }
        // сообщение
        public string Text { get; set; }
    }
}