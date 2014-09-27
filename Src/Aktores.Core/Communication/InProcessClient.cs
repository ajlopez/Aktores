namespace Aktores.Core.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class InProcessClient
    {
        private ActorSystem system;

        public InProcessClient(ActorSystem system)
        {
            this.system = system;
        }

        public void Tell(string path, object message, ActorRef sender = null)
        {
            var actorref = this.system.ActorSelect(path);
            actorref.Tell(message, sender);
        }
    }
}
