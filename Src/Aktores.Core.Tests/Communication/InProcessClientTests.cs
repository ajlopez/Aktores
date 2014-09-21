namespace Aktores.Core.Tests.Communication
{
    using System;
    using System.Threading;
    using Aktores.Core.Communication;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InProcessClientTests
    {
        [TestMethod]
        public void SendMessageToActor()
        {
            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);

            Actor actor = new LambdaActor(c => { total += (int)c; wait.Set(); });

            ActorSystem system = new ActorSystem();
            var client = new InProcessClient(system);

            var result = system.ActorOf(actor, "myactor");
            var path = result.Path.ToString();

            client.Tell(path, 1);

            wait.WaitOne();

            Assert.AreEqual(1, total);
            system.Shutdown();
        }
    }
}
