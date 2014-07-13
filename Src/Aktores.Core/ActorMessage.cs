namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class ActorMessage
    {
        private object message;
        private ActorRef sender;

        public ActorMessage(object message, ActorRef sender = null)
        {
            this.message = message;
            this.sender = sender;
        }

        public object Message { get { return this.message; } }

        public ActorRef Sender { get { return this.sender; } }
    }
}
