namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Aktores.Core.Communication;

    internal class ActorRemoteRef : ActorRef
    {
        private TcpClient remote;
        private string pathstring;

        internal ActorRemoteRef(TcpClient remote, ActorPath path)
            : base(path)
        {
            this.remote = remote;
            this.pathstring = path.ToString();
        }

        public override void Tell(object message, ActorRef sender = null)
        {
            this.remote.Tell(this.pathstring, message);
        }
    }
}
