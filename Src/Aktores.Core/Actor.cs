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

        public ActorContext Context { get; internal set; }

        public virtual void Initialize()
        {
        }

        public abstract void Receive(object message);

        internal void Start()
        {
            this.state = ActorState.Running;
        }

        internal void Stop()
        {
            this.state = ActorState.Stopped;
        }
    }
}
