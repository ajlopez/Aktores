namespace Aktores.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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
            Assert.IsFalse(ActorPath.IsName("aktores.tcp://sys@localhost:3000/foo"));
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
            Assert.IsFalse(ActorPath.IsRelative("aktores.tcp://sys@localhost:3000/foo"));
        }

        [TestMethod]
        public void SplitInFullPathInParts()
        {
            var path = new ActorPath("aktores.tcp://sys@localhost:3000/foo/actor");

            Assert.AreEqual("aktores.tcp", path.Protocol);
            Assert.AreEqual("sys", path.SystemName);
            Assert.AreEqual("localhost", path.HostName);
            Assert.AreEqual(3000, path.Port);
            Assert.AreEqual("/foo/actor", path.ActorAddress);
        }

        [TestMethod]
        public void SplitLocalPathInParts()
        {
            var path = new ActorPath("/foo/actor");

            Assert.AreEqual("aktores", path.Protocol);
            Assert.AreEqual("sys", path.SystemName);
            Assert.IsNull(path.HostName);
            Assert.AreEqual(0, path.Port);
            Assert.AreEqual("/foo/actor", path.ActorAddress);
        }

        [TestMethod]
        public void SplitPartialPathInParts()
        {
            var path = new ActorPath("//sys2/foo/actor");

            Assert.AreEqual("aktores", path.Protocol);
            Assert.AreEqual("sys2", path.SystemName);
            Assert.IsNull(path.HostName);
            Assert.AreEqual(0, path.Port);
            Assert.AreEqual("/foo/actor", path.ActorAddress);
        }
    }
}
