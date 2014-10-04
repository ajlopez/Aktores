namespace Aktores.Core.Tests.Routing
{
    using System;
    using System.Threading;
    using Aktores.Core.Routing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RouterActorTests
    {
        [TestMethod]
        public void CreateRouterAndSendMessageToActor()
        {
            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);

            Actor actor = new LambdaActor(c => { total += (int)c; wait.Set(); });

            ActorSystem system = new ActorSystem();

            var actorref = system.ActorOf(actor, "myactor");

            var router = new RouterActor();

            router.Register(actorref);

            var routerref = system.ActorOf(router, "myrouter");

            routerref.Tell(3);

            Assert.IsTrue(wait.WaitOne(1000));

            Assert.AreEqual(3, total);

            system.Shutdown();
        }
    }
}
