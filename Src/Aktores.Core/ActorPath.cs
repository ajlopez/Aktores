namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorPath
    {
        private ActorPath parent;
        private string name;

        public ActorPath(string name)
            : this(null, name)
        {
        }

        public ActorPath(ActorPath parent, string name)
        {
            this.parent = parent;
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public override string ToString()
        {
            return (this.parent == null ? string.Empty : this.parent.ToString()) + "/" + name;
        }

        public static bool IsName(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return true;

            if (path.Contains("/"))
                return false;

            return true;
        }

        public static bool IsRelative(string path)
        {
            if (IsName(path))
                return true;

            if (path.StartsWith("."))
                return true;

            return false;
        }
    }
}
