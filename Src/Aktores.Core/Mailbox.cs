namespace Aktores.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class Mailbox
    {
        private BlockingCollection<ActorMessage> queue = new BlockingCollection<ActorMessage>();

        public void Add(ActorMessage message)
        {
            this.queue.Add(message);
        }

        public ActorMessage Take()
        {
            return this.queue.Take();
        }
    }
}
