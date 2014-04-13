namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class ActorTask
    {
        private Actor actor;
        private Mailbox mailbox;

        public ActorTask(Actor actor, Mailbox mailbox)
        {
            this.actor = actor;
            this.mailbox = mailbox;
        }

        public Actor Actor { get { return this.actor; } }

        public Mailbox Mailbox { get { return this.mailbox; } }
    }
}
