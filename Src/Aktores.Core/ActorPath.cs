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
        {
            int p = name.LastIndexOf('/');

            if (p < 0)
            {
                this.parent = null;
                this.name = name;
            }
            else if (p == 0)
            {
                this.parent = null;
                this.name = name.Substring(1);
            }
            else
            {
                string parentname = name.Substring(0, p);
                name = name.Substring(p + 1);

                if (!string.IsNullOrWhiteSpace(parentname))
                    this.parent = new ActorPath(parentname);
                else
                    this.parent = null;

                this.name = name;
            }
        }

        public ActorPath(ActorPath parent, string name)
        {
            this.parent = parent;
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public ActorPath Parent { get { return this.parent; } }

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

        public override string ToString()
        {
            return (this.parent == null ? string.Empty : this.parent.ToString()) + "/" + this.name;
        }
    }
}
