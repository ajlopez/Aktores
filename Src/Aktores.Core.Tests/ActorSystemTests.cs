namespace Aktores.Core.Tests
{
    using System;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ActorSystemTests
    {
        [TestMethod]
        public void CreateActorRefUsingType()
        {
            ActorSystem system = new ActorSystem();

            var result = system.ActorOf(typeof(MyActor));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetNullActorRefForUnknownPath()
        {
            ActorSystem system = new ActorSystem();

            Assert.IsNull(system.ActorFor("unknown"));
        }

        [TestMethod]
        public void CreateActorRefUsingTypeAndName()
        {
            ActorSystem system = new ActorSystem();

            var result = system.ActorOf(typeof(MyActor), "myactor");

            Assert.IsNotNull(result);

            var result2 = system.ActorFor("myactor");

            Assert.IsNotNull(result2);
            Assert.AreSame(result, result2);
        }

        [TestMethod]
        public void CreateActorRefUsingActorAndSendMessage()
        {
            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);

            Actor actor = new LambdaActor(c => { total += (int)c; wait.Set(); });

            ActorSystem system = new ActorSystem();

            var result = system.ActorOf(actor);

            Assert.IsNotNull(result);
            Assert.AreSame(result, actor.Self);
            Assert.IsTrue(result.IsRunning());
            Assert.IsTrue(actor.IsRunning());

            result.Tell(1);

            wait.WaitOne();

            Assert.AreEqual(1, total);
            system.Stop(result);

            // TODO better time management
            Thread.Sleep(1000);
            Assert.IsFalse(result.IsRunning());
        }

        [TestMethod]
        public void CreateAndUseForwardActor()
        {
            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);

            Actor actor = new LambdaActor(c => { total += (int)c; wait.Set(); });

            ActorSystem system = new ActorSystem();

            var actorref = system.ActorOf(actor);
            Actor forwarder = new ForwardActor(actorref);

            forwarder.Tell(1);

            wait.WaitOne();

            Assert.AreEqual(1, total);
        }

        [TestMethod]
        public void CreateActorsAndSystemShutdown()
        {
            ActorSystem system = new ActorSystem();

            var ref1 = system.ActorOf(typeof(MyActor));
            var ref2 = system.ActorOf(typeof(MyActor));

            Assert.IsTrue(ref1.IsRunning());
            Assert.IsTrue(ref2.IsRunning());

            system.Shutdown();

            // TODO better time management
            Thread.Sleep(1000);

            Assert.IsFalse(ref1.IsRunning());
            Assert.IsFalse(ref2.IsRunning());
        }

        private class MyActor : Actor
        {
            protected override void Receive(object message)
            {
                throw new NotImplementedException();
            }
        }

        private class LambdaActor : Actor
        {
            private Action<object> fn;

            public LambdaActor(Action<object> fn)
            {
                this.fn = fn;
            }

            protected override void Receive(object message)
            {
                this.fn(message);
            }
        }

        private class ForwardActor : Actor
        {
            private ActorRef actorref;

            public ForwardActor(ActorRef actorref)
            {
                this.actorref = actorref;
            }

            protected override void Receive(object message)
            {
                this.actorref.Tell(message, this.Sender);
            }
        }
    }
}
