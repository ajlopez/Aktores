namespace Aktores.Core.Tests.Communication
{
    using System;
    using System.Threading;
    using Aktores.Core.Communication;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RemoteTests
    {
        [TestMethod]
        public void SendMessageToRemoteActor()
        {
            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);

            Actor actor = new LambdaActor(c => { total += (int)c; wait.Set(); });

            ActorSystem remotesystem = new ActorSystem();
            var server = new TcpServer("localhost", 3004, remotesystem);

            var result = remotesystem.ActorOf(actor, "myactor");
            var path = result.Path.ToString();

            server.Start();

            var system = new ActorSystem();
            var remoteref = system.ActorSelect("aktores.tcp://sys@localhost:3004/myactor");

            remoteref.Tell(1);

            wait.WaitOne();

            Assert.AreEqual(1, total);

            system.Shutdown();
            server.Stop();
            remotesystem.Shutdown();
        }

        [TestMethod]
        public void SendMessagesToRemoteActor()
        {
            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);

            Actor actor = new LambdaActor(c => { total += (int)c; if (total >= 6) wait.Set(); });

            ActorSystem remotesystem = new ActorSystem();
            var server = new TcpServer("localhost", 3005, remotesystem);

            var result = remotesystem.ActorOf(actor, "myactor");
            var path = result.Path.ToString();

            server.Start();

            var system = new ActorSystem();
            var remoteref = system.ActorSelect("aktores.tcp://sys@localhost:3005/myactor");

            remoteref.Tell(1);
            remoteref.Tell(2);
            remoteref.Tell(3);

            wait.WaitOne();

            Assert.AreEqual(6, total);

            system.Shutdown();
            server.Stop();
            remotesystem.Shutdown();
        }
    }
}
