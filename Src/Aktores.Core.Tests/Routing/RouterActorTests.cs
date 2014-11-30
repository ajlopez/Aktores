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
            ActorSystem system = new ActorSystem();

            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);
            Actor actor = new LambdaActor(c => { total += (int)c; wait.Set(); });
            var actorref = system.ActorOf(actor, "myactor");

            var router = new RouterActor();
            router.Register(actorref);
            var routerref = system.ActorOf(router, "myrouter");

            routerref.Tell(3);

            Assert.IsTrue(wait.WaitOne(1000));
            Assert.AreEqual(3, total);

            system.Shutdown();
        }

        [TestMethod]
        public void CreateRouterAndSendMessageToActors()
        {
            ActorSystem system = new ActorSystem();

            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);

            var router = new RouterActor();
            var routerref = system.ActorOf(router, "myrouter");

            for (int k = 0; k < 100; k++)
            {
                Actor actor = new LambdaActor(c => 
                { 
                    lock (system) 
                    { 
                        total += (int)c; 
                    } 
                    
                    if (total >= 10) 
                        wait.Set(); 
                });

                var actorref = system.ActorOf(actor);
                router.Register(actorref);
            }

            routerref.Tell(1);
            routerref.Tell(2);
            routerref.Tell(3);
            routerref.Tell(4);

            Assert.IsTrue(wait.WaitOne(1000));
            Assert.AreEqual(10, total);

            system.Shutdown();
        }

        [TestMethod]
        public void SendRegisterMessageToRouter()
        {
            ActorSystem system = new ActorSystem();

            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);
            Actor actor = new LambdaActor(c => { total += (int)c; wait.Set(); });
            var actorref = system.ActorOf(actor, "myactor");

            var router = new RouterActor();
            var routerref = system.ActorOf(router, "myrouter");

            routerref.Tell(new RegisterActorMessage() { ActorPath = actorref.Path.ToString() });
            routerref.Tell(3);

            Assert.IsTrue(wait.WaitOne(1000));
            Assert.AreEqual(3, total);

            system.Shutdown();
        }
    }
}
