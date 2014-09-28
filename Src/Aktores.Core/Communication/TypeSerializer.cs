namespace Aktores.Core.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TypeSerializer
    {
        private Type type;
        private IList<PropertyType> properties = new List<PropertyType>();

        public TypeSerializer(Type type)
        {
            this.type = type;

            foreach (var pi in type.GetProperties())
            {
                var prop = new PropertyType();
                prop.Name = pi.Name;
                var tp = pi.PropertyType;

                if (tp == typeof(string))
                    prop.Type = Types.String;
                else if (tp == typeof(int))
                    prop.Type = Types.Integer;
                else if (tp == typeof(double))
                    prop.Type = Types.Double;
                else
                {
                    prop.Type = Types.Object;
                    prop.TypeName = tp.FullName;
                }

                this.properties.Add(prop);
            }
        }

        public string TypeName { get { return this.type.FullName; } }

        public IList<PropertyType> Properties { get { return this.properties; } }   
    }
}
