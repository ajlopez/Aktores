namespace Aktores.Core
{
    using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

    public abstract class Actor
    {
        private bool started = false;
        private Thread thread;
        private BlockingCollection<object> queue = new BlockingCollection<object>();

        public void SendMessage(object message)
        {
            if (!this.started)
                lock(this)
                    this.Start();

            this.queue.Add(message);
        }

        public void Start()
        {
            if (this.started)
                return;

            this.started = true;

            ThreadStart ts = new ThreadStart(this.Run);
            this.thread = new Thread(ts);
            this.thread.Start();
        }

        protected abstract void ReceiveMessage(object message);

        private void Run()
        {
            while (true)
                this.ReceiveMessage(this.queue.Take());
        }
    }
}
