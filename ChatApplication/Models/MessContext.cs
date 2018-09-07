using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ChatApplication.Models
{
    public class MessContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
    }
}