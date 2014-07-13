namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorContext : ActorRefFactory
    {
        ActorSystem system;
        ActorRefFactory parent;

        internal ActorContext(ActorSystem system)
        {
            this.system = system;
            this.parent = system;
        }

        internal override ActorRef CreateActorRef(Actor actor)
        {
            return this.system.CreateActorRef(actor);
        }

        internal override ActorContext CreateActorContext()
        {
            throw new NotImplementedException();
        }

        public override void Stop(ActorRef actorref)
        {
            this.system.Stop(actorref);
        }
    }
}
