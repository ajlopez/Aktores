namespace Aktores.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AddressTests
    {
        [TestMethod]
        public void SplitInFullPathInParts()
        {
            var path = new Address("aktores.tcp://sys@localhost:3000");

            Assert.AreEqual("aktores.tcp", path.Protocol);
            Assert.AreEqual("sys", path.SystemName);
            Assert.AreEqual("localhost", path.HostName);
            Assert.AreEqual(3000, path.Port);
        }

        [TestMethod]
        public void SplitPartialPathInParts()
        {
            var path = new Address("//sys2");

            Assert.AreEqual("aktores", path.Protocol);
            Assert.AreEqual("sys2", path.SystemName);
            Assert.IsNull(path.HostName);
            Assert.AreEqual(0, path.Port);
        }
    }
}
