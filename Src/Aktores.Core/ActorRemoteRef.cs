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
        private bool failed;

        internal ActorRemoteRef(TcpClient remote, ActorPath path)
            : base(path)
        {
            this.remote = remote;
            this.pathstring = path.ToString();
        }

        public override void Tell(object message, ActorRef sender = null)
        {
            if (this.failed)
                return;

            try
            {
                this.remote.Tell(this.pathstring, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                this.failed = true;
            }
        }
    }
}
