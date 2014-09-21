namespace Aktores.Core.Tests.Communication
{
    using System;
    using System.Net.Sockets;
    using System.Threading;
    using Aktores.Core.Communication;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TcpServerTests
    {
        [TestMethod]
        public void SendMessageToActor()
        {
            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);

            Actor actor = new LambdaActor(c => { total += (int)c; wait.Set(); });

            ActorSystem system = new ActorSystem();
            var server = new TcpServer("localhost", 3000, system);

            var result = system.ActorOf(actor, "myactor");
            var path = result.Path.ToString();

            server.Start();

            var client = new TcpClient();
            client.Connect("localhost", 3000);
            var channel = new OutputChannel(new System.IO.BinaryWriter(client.GetStream()));
            channel.Write(path);
            channel.Write(1);

            wait.WaitOne();

            Assert.AreEqual(1, total);

            client.Close();
            server.Stop();
            system.Shutdown();
        }
    }
}
