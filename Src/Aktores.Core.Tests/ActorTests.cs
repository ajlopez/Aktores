namespace Aktores.Core.Tests
{
    using System;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ActorTests
    {
        [TestMethod]
        public void SendOneMessage()
        {
            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);

            Actor actor = new LambdaActor(c => { total += (int)c; wait.Set(); });

            actor.SendMessage(1);

            wait.WaitOne();

            Assert.AreEqual(1, total);
        }

        [TestMethod]
        public void SendThreeMessages()
        {
            int total = 0;
            EventWaitHandle wait = new AutoResetEvent(false);

            Actor actor = new LambdaActor(c => { total += (int)c; if (total >= 6) wait.Set(); });

            actor.SendMessage(1);
            actor.SendMessage(2);
            actor.SendMessage(3);

            wait.WaitOne();

            Assert.AreEqual(6, total);
        }

        private class LambdaActor : Actor
        {
            private Action<object> fn;

            public LambdaActor(Action<object> fn)
            {
                this.fn = fn;
            }

            protected override void ReceiveMessage(object message)
            {
                this.fn(message);
            }
        }
    }
}
