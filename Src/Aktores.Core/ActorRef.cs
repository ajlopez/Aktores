namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class ActorRef
    {
        private ActorPath path;

        internal ActorRef(ActorPath path)
        {
            this.path = path;
        }

        public ActorPath Path { get { return this.path; } }

        public abstract void Tell(object message, ActorRef sender = null);
    }
}
