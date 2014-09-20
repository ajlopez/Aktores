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
        public void ActorForUsingSlash()
        {
            ActorSystem system = new ActorSystem();

            var result = system.ActorOf(typeof(MyActor), "myactor");

            Assert.IsNotNull(result);

            var result2 = system.ActorFor("/myactor");

            Assert.IsNotNull(result2);
            Assert.AreSame(result, result2);
        }

        [TestMethod]
        public void CreateActorAndChildActor()
        {
            Actor actor = new MyActor();
            ActorSystem system = new ActorSystem();

            var actorref = system.ActorOf(actor, "myactor");
            var childactorref = actor.Context.ActorOf(typeof(MyActor), "mychildactor");

            Assert.IsNotNull(actorref);
            Assert.IsNotNull(childactorref);
            Assert.AreEqual("/myactor", actorref.Path.ToString());
            Assert.AreEqual("/myactor/mychildactor", childactorref.Path.ToString());
        }

        [TestMethod]
        public void ActorForFromChild()
        {
            Actor actor = new MyActor();
            Actor child = new MyActor();
            ActorSystem system = new ActorSystem();

            var actorref = system.ActorOf(actor, "myactor");
            var childactorref = actor.Context.ActorOf(child, "mychildactor");

            Assert.IsNotNull(actorref);
            Assert.IsNotNull(childactorref);

            var result = child.Context.ActorFor("/myactor");

            Assert.IsNotNull(result);
            Assert.AreEqual("/myactor", result.Path.ToString());
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

            result.Tell(1);

            wait.WaitOne();

            Assert.AreEqual(1, total);
            system.Stop(result);

            // TODO better time management
            Thread.Sleep(1000);

            Assert.AreEqual(ActorState.Stopped, result.State);
            Assert.AreEqual(ActorState.Stopped, actor.State);
        }

        [TestMethod]
        public void CreateActorRefUsingActorAndSendThreeMessages()
        {
            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);

            Actor actor = new LambdaActor(c => { total += (int)c; if (total >= 6) wait.Set(); });

            ActorSystem system = new ActorSystem();

            var result = system.ActorOf(actor);

            Assert.IsNotNull(result);
            Assert.AreSame(result, actor.Self);

            result.Tell(1);
            result.Tell(2);
            result.Tell(3);

            wait.WaitOne();

            Assert.AreEqual(6, total);
            system.Stop(result);

            // TODO better time management
            Thread.Sleep(1000);

            Assert.AreEqual(ActorState.Stopped, result.State);
            Assert.AreEqual(ActorState.Stopped, actor.State);
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

            forwarder.Receive(1);

            wait.WaitOne();

            Assert.AreEqual(1, total);
        }

        [TestMethod]
        public void CreateActorsAndSystemShutdown()
        {
            ActorSystem system = new ActorSystem();
            Actor actor1 = new MyActor();
            Actor actor2 = new MyActor();

            Assert.AreEqual(ActorState.Created, actor1.State);
            Assert.AreEqual(ActorState.Created, actor2.State);

            var ref1 = system.ActorOf(actor1);
            var ref2 = system.ActorOf(actor2);

            Assert.AreEqual(ActorState.Running, ref1.State);
            Assert.AreEqual(ActorState.Running, ref2.State);
            Assert.AreEqual(ActorState.Running, actor1.State);
            Assert.AreEqual(ActorState.Running, actor2.State);

            system.Shutdown();

            // TODO better time management
            Thread.Sleep(1000);

            Assert.AreEqual(ActorState.Stopped, ref1.State);
            Assert.AreEqual(ActorState.Stopped, ref2.State);
            Assert.AreEqual(ActorState.Stopped, actor1.State);
            Assert.AreEqual(ActorState.Stopped, actor2.State);
        }

        private class MyActor : Actor
        {
            public override void Receive(object message)
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

            public override void Receive(object message)
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

            public override void Receive(object message)
            {
                this.actorref.Tell(message, this.Sender);
            }
        }
    }
}
