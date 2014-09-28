namespace Aktores.Core.Tests.Communication
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Aktores.Core.Communication;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TypeSerializerTests
    {
        [TestMethod]
        public void GetPersonTypeFullName()
        {
            var serializer = new TypeSerializer(typeof(Person));

            Assert.AreEqual("Aktores.Core.Tests.Person", serializer.TypeName);
        }

        [TestMethod]
        public void GetPersonProperties()
        {
            var serializer = new TypeSerializer(typeof(Person));

            var props = serializer.Properties;

            Assert.IsNotNull(props);
            Assert.AreEqual(3, props.Count);

            Assert.AreEqual("Id", props[0].Name);
            Assert.AreEqual(Types.Integer, props[0].Type);
            Assert.IsNull(props[0].TypeName);

            Assert.AreEqual("FirstName", props[1].Name);
            Assert.AreEqual(Types.String, props[1].Type);
            Assert.IsNull(props[1].TypeName);

            Assert.AreEqual("LastName", props[2].Name);
            Assert.AreEqual(Types.String, props[2].Type);
            Assert.IsNull(props[2].TypeName);
        }

        [TestMethod]
        public void CreateSerializerByFullNameAndProperties()
        {
            IList<PropertyType> properties = new List<PropertyType>();

            properties.Add(new PropertyType()
            {
                Name = "Id",
                Type = Types.Integer
            });

            properties.Add(new PropertyType()
            {
                Name = "FirstName",
                Type = Types.String
            });

            var serializer = new TypeSerializer("Aktores.Core.Tests.Person", properties);

            Assert.AreEqual("Aktores.Core.Tests.Person", serializer.TypeName);

            var props = serializer.Properties;

            Assert.IsNotNull(props);
            Assert.AreEqual(2, props.Count);

            Assert.AreEqual("Id", props[0].Name);
            Assert.AreEqual(Types.Integer, props[0].Type);
            Assert.IsNull(props[0].TypeName);

            Assert.AreEqual("FirstName", props[1].Name);
            Assert.AreEqual(Types.String, props[1].Type);
            Assert.IsNull(props[1].TypeName);
        }
    }
}
