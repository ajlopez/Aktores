namespace Aktores.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class Mailbox
    {
        private ActorSystem system;
        private BlockingCollection<ActorMessage> queue = new BlockingCollection<ActorMessage>();

        internal Mailbox(ActorSystem system)
        {
            this.system = system;
        }

        public void Add(Actor actor, ActorMessage message)
        {
            this.queue.Add(message);
            this.system.AddTask(new ActorTask(actor, this));
        }

        public ActorMessage Take()
        {
            return this.queue.Take();
        }
    }
}
