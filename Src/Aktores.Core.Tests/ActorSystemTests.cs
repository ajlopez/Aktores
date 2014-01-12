namespace Aktores.Core.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ActorSystemTests
    {
        [TestMethod]
        public void CreateActorUsingType()
        {
            ActorSystem system = new ActorSystem();

            var result = system.ActorOf(typeof(MyActor));

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MyActor));
        }

        [TestMethod]
        public void CreateActorUsingTypeAndName()
        {
            ActorSystem system = new ActorSystem();

            var result = system.ActorOf(typeof(MyActor), "myactor");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MyActor));

            var result2 = system.ActorFor("myactor");

            Assert.IsNotNull(result2);
            Assert.AreSame(result, result2);
        }

        private class MyActor : Actor
        {
            protected override void Receive(object message)
            {
                throw new NotImplementedException();
            }
        }
    }
}
