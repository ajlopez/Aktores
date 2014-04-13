namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    internal class Worker
    {
        private TaskQueue queue;

        public Worker(TaskQueue queue)
        {
            this.queue = queue;
        }

        public void Start()
        {
            ThreadStart ts = new ThreadStart(this.Run);
            Thread thread = new Thread(ts);
            thread.IsBackground = false;
            thread.Start();
        }

        private void Run()
        {
            while (true)
            {
                var task = this.queue.Take();

                if (task == null)
                    return;

                if (Monitor.TryEnter(task.Actor))
                    try
                    {
                        if (task.Actor.State == ActorState.Stopped)
                            continue;

                        var message = task.Mailbox.Take();

                        task.Actor.Sender = message.Sender;
                        task.Actor.Receive(message.Message);
                    }
                    finally
                    {
                        Monitor.Exit(task.Actor);
                    }
                else
                    this.queue.Add(task);
            }
        }
    }
}
