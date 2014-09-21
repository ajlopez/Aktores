namespace Aktores.Core.Tests.Communication
{
    using System;
    using System.Threading;
    using Aktores.Core.Communication;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TcpClientTests
    {
        [TestMethod]
        public void SendMessageToActor()
        {
            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);

            Actor actor = new LambdaActor(c => { total += (int)c; wait.Set(); });

            ActorSystem system = new ActorSystem();
            var server = new TcpServer("localhost", 3002, system);

            var result = system.ActorOf(actor, "myactor");
            var path = result.Path.ToString();

            server.Start();

            var client = new TcpClient("localhost", 3002);
            client.Start();

            client.Tell(path, 1);

            wait.WaitOne();

            Assert.AreEqual(1, total);

            client.Stop();
            server.Stop();
            system.Shutdown();
        }

        [TestMethod]
        public void SendMessagesToActor()
        {
            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);

            Actor actor = new LambdaActor(c => { total += (int)c; if (total >= 6) wait.Set(); });

            ActorSystem system = new ActorSystem();
            var server = new TcpServer("localhost", 3003, system);

            var result = system.ActorOf(actor, "myactor");
            var path = result.Path.ToString();

            server.Start();

            var client = new TcpClient("localhost", 3003);
            client.Start();
            client.Tell(path, 1);
            client.Tell(path, 2);
            client.Tell(path, 3);

            wait.WaitOne();

            Assert.AreEqual(6, total);

            client.Stop();
            server.Stop();
            system.Shutdown();
        }
    }
}
