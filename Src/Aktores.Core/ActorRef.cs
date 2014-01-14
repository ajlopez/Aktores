namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorRef
    {
        private Actor actor;
        private ActorMessageQueue queue;

        internal ActorRef(Actor actor, ActorMessageQueue queue)
        {
            this.actor = actor;
            this.queue = queue;
        }

        internal Actor Actor { get { return this.actor; } }

        public ActorState State { get { return this.actor.State; } } 

        public void Tell(object message, ActorRef sender = null)
        {
            this.queue.Add(new ActorMessage(this.actor, message, sender));
        }
    }
}
