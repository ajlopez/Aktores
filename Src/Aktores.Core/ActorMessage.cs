namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class ActorMessage
    {
        private Actor target;
        private object message;
        private ActorRef sender;

        public ActorMessage(Actor target, object message, ActorRef sender = null)
        {
            this.target = target;
            this.message = message;
            this.sender = sender;
        }

        public Actor Target { get { return this.target; } }

        public object Message { get { return this.message; } }

        public ActorRef Sender { get { return this.sender; } }
    }
}
