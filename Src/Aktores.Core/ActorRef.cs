namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorRef
    {
        private Actor actor;

        internal ActorRef(Actor actor)
        {
            this.actor = actor;
        }

        public void Tell(object message)
        {
            this.actor.Tell(message);
        }
    }
}
