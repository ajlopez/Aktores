namespace Aktores.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class TaskQueue
    {
        private BlockingCollection<ActorTask> queue = new BlockingCollection<ActorTask>();

        public void Add(ActorTask task)
        {
            this.queue.Add(task);
        }

        public ActorTask Take()
        {
            return this.queue.Take();
        }
    }
}
