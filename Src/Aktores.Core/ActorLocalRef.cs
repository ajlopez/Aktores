namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class ActorLocalRef : ActorRef
    {
        private Actor actor;
        private Mailbox mailbox;

        internal ActorLocalRef(Actor actor, Mailbox mailbox, ActorPath path)
            : base(path)
        {
            this.actor = actor;
            this.mailbox = mailbox;
        }

        internal Actor Actor { get { return this.actor; } }

        public override void Tell(object message, ActorRef sender = null)
        {
            this.mailbox.Add(this.actor, new ActorMessage(message, sender));
        }
    }
}
