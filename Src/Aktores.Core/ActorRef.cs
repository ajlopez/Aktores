namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorRef
    {
        private ActorSystem system;
        private Actor actor;
        private Mailbox mailbox;

        internal ActorRef(ActorSystem system, Actor actor, Mailbox mailbox)
        {
            this.system = system;
            this.actor = actor;
            this.mailbox = mailbox;
        }

        public ActorState State { get { return this.actor.State; } } 

        internal Actor Actor { get { return this.actor; } }

        public void Tell(object message, ActorRef sender = null)
        {
            this.mailbox.Add(new ActorMessage(this.actor, message, sender));
            this.system.AddTask(new ActorTask(this.actor, this.mailbox));
        }
    }
}
