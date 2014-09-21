namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorRef
    {
        private Actor actor;
        private Mailbox mailbox;
        private ActorPath path;

        internal ActorRef(Actor actor, Mailbox mailbox, ActorPath path)
        {
            this.actor = actor;
            this.mailbox = mailbox;
            this.path = path;
        }

        public ActorPath Path { get { return this.path; } }

        internal Actor Actor { get { return this.actor; } }

        public void Tell(object message, ActorRef sender = null)
        {
            this.mailbox.Add(this.actor, new ActorMessage(message, sender));
        }
    }
}
