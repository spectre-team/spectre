using System;
using System.Threading;
using NUnit.Framework;
using Spectre.Service.Abstract;

namespace Spectre.Service.Tests
{
    [TestFixture]
    public class ConsoleCaptureServiceTest
    {
        private IServiceFactory _factory;

        [SetUp]
        public void SetUp()
        {
            _factory = new ServiceFactory();
        }

        [Test]
        public void WriteTest()
        {
            const string text = "blah bleh blash";
            using (var captureService = _factory.GetConsoleCaptureService(1.0))
            {
                Console.Write(text);
                Thread.Sleep(100);
                Assert.AreEqual(text, captureService.Content, "Text has not been captured.");
            }
        }

        [Test]
        public void WriteLineTest()
        {
            const string text = "blah bleh blash";
            using (var captureService = _factory.GetConsoleCaptureService(1.0))
            {
                Console.WriteLine(text);
                Thread.Sleep(100);
                Assert.AreEqual(text + "\r\n", captureService.Content, "Text has not been captured.");
            }
        }
    }
}
