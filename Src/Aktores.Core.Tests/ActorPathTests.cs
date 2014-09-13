namespace Aktores.Core.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ActorPathTests
    {
        [TestMethod]
        public void IsName()
        {
            Assert.IsTrue(ActorPath.IsName(null));
            Assert.IsTrue(ActorPath.IsName(string.Empty));
            Assert.IsTrue(ActorPath.IsName("  "));

            Assert.IsTrue(ActorPath.IsName("foo"));

            Assert.IsFalse(ActorPath.IsName("./foo"));
            Assert.IsFalse(ActorPath.IsName("/foo"));
            Assert.IsFalse(ActorPath.IsName("//sys/foo"));
            Assert.IsFalse(ActorPath.IsName("aktores://sys/foo"));
            Assert.IsFalse(ActorPath.IsName("actores.tcp://sys@localhost:3000/foo"));
        }

        [TestMethod]
        public void IsRelative()
        {
            Assert.IsTrue(ActorPath.IsRelative(null));
            Assert.IsTrue(ActorPath.IsRelative(string.Empty));
            Assert.IsTrue(ActorPath.IsRelative("  "));

            Assert.IsTrue(ActorPath.IsRelative("foo"));

            Assert.IsTrue(ActorPath.IsRelative("./foo"));
            Assert.IsFalse(ActorPath.IsRelative("/foo"));
            Assert.IsFalse(ActorPath.IsRelative("//sys/foo"));
            Assert.IsFalse(ActorPath.IsRelative("aktores://sys/foo"));
            Assert.IsFalse(ActorPath.IsRelative("actores.tcp://sys@localhost:3000/foo"));
        }
    }
}
