namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorRef
    {
        private Actor actor;
        private Mailbox queue;

        internal ActorRef(Actor actor, Mailbox queue)
        {
            this.actor = actor;
            this.queue = queue;
        }

        public ActorState State { get { return this.actor.State; } } 

        internal Actor Actor { get { return this.actor; } }

        public void Tell(object message, ActorRef sender = null)
        {
            this.queue.Add(new ActorMessage(this.actor, message, sender));
        }
    }
}
