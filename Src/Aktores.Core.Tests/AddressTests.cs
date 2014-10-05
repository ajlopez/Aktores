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
            var address = new Address("aktores.tcp://sys@localhost:3000");

            Assert.AreEqual("aktores.tcp", address.Protocol);
            Assert.AreEqual("sys", address.SystemName);
            Assert.AreEqual("localhost", address.HostName);
            Assert.AreEqual(3000, address.Port);
            Assert.AreEqual("aktores.tcp://sys@localhost:3000", address.ToString());
        }

        [TestMethod]
        public void SplitPartialPathInParts()
        {
            var address = new Address("//sys2");

            Assert.AreEqual("aktores.tcp", address.Protocol);
            Assert.AreEqual("sys2", address.SystemName);
            Assert.AreEqual("localhost", address.HostName);
            Assert.AreEqual(0, address.Port);
            Assert.AreEqual("aktores.tcp://sys2@localhost", address.ToString());
        }

        [TestMethod]
        public void GetAddressIsNullInLocalPath()
        {
            var result = Address.GetAddress("/foo/bar");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPathFromLocalPath()
        {
            var result = Address.GetActorPath("/foo/bar");

            Assert.IsNotNull(result);
            Assert.AreEqual("/foo/bar", result.ToString());
        }

        [TestMethod]
        public void GetAddressAndPathInRemotePath()
        {
            var fullname = "aktores.tcp://sys@localhost:3000/foo/bar";

            var address = Address.GetAddress(fullname);

            Assert.IsNotNull(address);
            Assert.AreEqual("aktores.tcp", address.Protocol);
            Assert.AreEqual("sys", address.SystemName);
            Assert.AreEqual("localhost", address.HostName);
            Assert.AreEqual(3000, address.Port);
            Assert.AreEqual("aktores.tcp://sys@localhost:3000", address.ToString());

            var path = Address.GetActorPath(fullname);

            Assert.IsNotNull(path);
            Assert.AreEqual("/foo/bar", path.ToString());
        }
    }
}
