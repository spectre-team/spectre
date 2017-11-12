using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Spectre.Data.RoiIo;

namespace Spectre.Data.Tests
{
    [TestFixture]
    public class ReadTest
    {
        [Test]
        public void WorkingTest()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            RoiUtilities service = new RoiUtilities();

            var names = service.ListRoisFromDirectory();
        }
    }
}
