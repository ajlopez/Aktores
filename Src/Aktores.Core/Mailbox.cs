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
        private bool stopped;

        public void Add(ActorMessage message)
        {
            if (this.stopped)
                throw new InvalidOperationException("System was stopped");

            this.queue.Add(message);
        }

        public ActorMessage Take()
        {
            while (true)
            {
                if (this.stopped)
                    return null;

                ActorMessage message;

                if (this.queue.TryTake(out message, 500))
                    return message;
            }
        }

        public void Stop()
        {
            this.stopped = true;
        }
    }
}
