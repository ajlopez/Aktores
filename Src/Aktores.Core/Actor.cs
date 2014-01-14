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
        private ActorState state = ActorState.Created;

        public ActorState State { get { return this.state; } }

        public ActorRef Sender { get; internal set; }

        public ActorRef Self { get; internal set; }

        public ActorSystem Context { get; internal set; }

        internal void Start()
        {
            this.state = ActorState.Running;
        }

        internal void Stop()
        {
            this.state = ActorState.Stopped;
        }

        public virtual void Initialize()
        {
        }

        public abstract void Receive(object message);
    }
}
