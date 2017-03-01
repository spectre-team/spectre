using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Spectre.Models;

namespace Spectre.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public virtual List<UserModel> Users { get; set; }

        public UserModel(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}

public class AuthContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }
}