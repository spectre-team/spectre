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
        public void ThrowsOnTooSmallUpdateInterval()
        {
            Assert.Throws<TooSmallUpdateIntervalException>(code: () => _factory.GetConsoleCaptureService(updateInterval: 1));
        }

        [Test]
        public void CreatesSeamlesslyWithGreaterInterval()
        {
            Assert.DoesNotThrow(code: () => _factory.GetConsoleCaptureService(updateInterval: 100));
        }

        [Test]
        public void WriteTest()
        {
            const string text = "blah bleh blash";
            using (var captureService = _factory.GetConsoleCaptureService(updateInterval: 100.0))
            {
                Console.Write(text);
                Thread.Sleep(millisecondsTimeout: 500);
                Assert.AreEqual(text, captureService.Content, message: "Text has not been captured.");
            }
        }

        [Test]
        public void WriteLineTest()
        {
            const string text = "blah bleh blash";
            using (var captureService = _factory.GetConsoleCaptureService(updateInterval: 100.0))
            {
                Console.WriteLine(text);
                Thread.Sleep(millisecondsTimeout: 500);
                Assert.AreEqual(expected: text + "\r\n",
                    actual: captureService.Content,
                    message: "Text has not been captured.");
            }
        }
    }
}
