namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    internal class Worker
    {
        private ActorMessageQueue queue;

        public Worker(ActorMessageQueue queue)
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
                var message = this.queue.Take();

                if (message == null)
                    return;

                lock(message.Target) 
                {
                    message.Target.Sender = message.Sender;
                    message.Target.Receive(message.Message);
                }
            }
        }
    }
}
