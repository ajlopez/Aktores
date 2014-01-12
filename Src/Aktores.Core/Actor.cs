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
        private BlockingCollection<ActorMessage> queue = new BlockingCollection<ActorMessage>();
        private ActorRef sender;
        private ActorSystem context;

        public ActorRef Sender { get { return this.sender; } }

        public ActorRef Self { get; internal set; }

        public ActorSystem Context { get; internal set; }

        public virtual void Initialize()
        {
        }

        public void Tell(object message, ActorRef sender = null)
        {
            if (!this.started)
                lock(this)
                    this.Start();

            this.queue.Add(new ActorMessage(message, sender));
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

        protected abstract void Receive(object message);

        private void Run()
        {
            while (true)
            {
                var message = this.queue.Take();

                this.sender = message.Sender;
                this.Receive(message.Message);
            }
        }
    }
}
