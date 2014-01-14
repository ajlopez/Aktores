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

            Actor actor = new LambdaActor(c => { total += (int)c; });

            actor.Receive(1);

            Assert.AreEqual(1, total);
        }

        [TestMethod]
        public void SendThreeMessages()
        {
            int total = 0;

            Actor actor = new LambdaActor(c => { total += (int)c; });

            actor.Receive(1);
            actor.Receive(2);
            actor.Receive(3);

            Assert.AreEqual(6, total);
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
    }
}
