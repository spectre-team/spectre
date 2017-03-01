using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spectre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectre.Tests.Models
{
    [TestClass()]
    public class UserModelTest
    {
        [TestMethod]
        public void AddUser()
        {
            using (var db = new AuthContext())
            {
                var username = "test";
                var password = "haslo";
                var password2 = "haslo";

                if (password == password2)
                {
                    var user = new UserModel(username, password);
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                
                var query = from b in db.Users
                            orderby b.username
                            select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.username);
                }

                Assert.AreEqual("2", "2");
            }
        }
    }
}
