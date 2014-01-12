namespace Aktores.Core.Tests
{
    using System;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ActorTests
    {
        [TestMethod]
        public void SendMessage()
        {
            int total = 0;
            AutoResetEvent wait = new AutoResetEvent(false);

            Actor actor = new LambdaActor(c => { total += (int)c; wait.Set(); });

            actor.SendMessage(1);

            wait.WaitOne();

            Assert.AreEqual(1, total);
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
