namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorContext : ActorRefFactory
    {
        private ActorSystem system;
        private ActorRefFactory parent;

        internal ActorContext(ActorSystem system)
        {
            this.system = system;
            this.parent = system;
        }

        internal ActorContext(ActorContext parent)
        {
            this.system = parent.system;
            this.parent = parent;
        }

        public override void Stop(ActorRef actorref)
        {
            this.system.Stop(actorref);
        }

        internal override ActorRef CreateActorRef(Actor actor)
        {
            return this.system.CreateActorRef(actor);
        }

        internal override ActorContext CreateActorContext()
        {
            return new ActorContext(this);
        }
    }
}
