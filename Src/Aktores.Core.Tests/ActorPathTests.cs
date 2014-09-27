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
            Assert.IsFalse(ActorPath.IsName("/foo/bar"));
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
            Assert.IsFalse(ActorPath.IsRelative("/foo/bar"));
        }

        [TestMethod]
        public void ActorPathForName()
        {
            var path = new ActorPath("foo");

            Assert.AreEqual("/foo", path.ToString());
            Assert.AreEqual("foo", path.Name);
            Assert.IsNull(path.Parent);
        }

        [TestMethod]
        public void ActorPathForRootName()
        {
            var path = new ActorPath("/foo");

            Assert.AreEqual("/foo", path.ToString());
            Assert.AreEqual("foo", path.Name);
            Assert.IsNull(path.Parent);
        }

        [TestMethod]
        public void ActorPathForCompositeName()
        {
            var path = new ActorPath("foo/bar");

            Assert.IsNotNull(path.Parent);
            Assert.AreEqual("/foo/bar", path.ToString());
            Assert.AreEqual("bar", path.Name);
            Assert.AreEqual("/foo", path.Parent.ToString());
            Assert.AreEqual("foo", path.Parent.Name);
        }

        [TestMethod]
        public void ActorPathForChild()
        {
            var parent = new ActorPath("foo");
            var path = new ActorPath(parent, "bar");
            Assert.AreEqual("/foo/bar", path.ToString());
            Assert.AreEqual("bar", path.Name);
            Assert.AreSame(parent, path.Parent);
        }
    }
}
