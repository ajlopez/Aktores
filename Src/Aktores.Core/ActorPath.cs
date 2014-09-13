namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorPath
    {
        private string path;
        private string protocol;
        private string systemname;
        private string hostname;
        private int port;
        private string actoraddress;

        public ActorPath(string path)
        {
            this.path = path;

            int p = path.IndexOf(':');

            if (p > 0)
            {
                this.protocol = path.Substring(0, p);
                path = path.Substring(p + 1);
            }
            else
                this.protocol = "aktores";

            if (path.StartsWith("//"))
            {
                path = path.Substring(2);

                p = path.IndexOf("/");

                string location = path.Substring(0, p);
                path = path.Substring(p);

                p = location.IndexOf("@");

                if (p > 0)
                {
                    this.systemname = location.Substring(0, p);
                    location = location.Substring(p + 1);

                    p = location.IndexOf(":");

                    if (p > 0)
                    {
                        this.hostname = location.Substring(0, p);
                        this.port = int.Parse(location.Substring(p + 1));
                    }
                    else
                        this.hostname = location;
                }
                else
                    this.systemname = location;
            }
            else
                this.systemname = "sys";

            this.actoraddress = path;
        }

        public string Protocol { get { return this.protocol; } }

        public string SystemName { get { return this.systemname; } }

        public string HostName { get { return this.hostname; } }

        public int Port { get { return this.port; } }

        public string ActorAddress { get { return this.actoraddress; } }

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
