namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorContext : ActorRefFactory
    {
        private ActorSystem system;
        private ActorRef self;

        internal ActorContext(ActorSystem system, ActorRef self)
            : base()
        {
            this.system = system;
            this.self = self;
        }

        public override ActorRef ActorFor(string name)
        {
            if (name.StartsWith("/"))
                return this.system.ActorFor(name);

            return base.ActorFor(name);
        }

        internal void Shutdown()
        {
            foreach (var childctx in this.ActorContexts)
                childctx.Shutdown();

            this.self.Actor.Stop();
        }

        internal override ActorRef CreateActorRef(Actor actor, string name)
        {
            ActorPath path = new ActorPath(this.self.Path, name);

            return new ActorRef(actor, new Mailbox(this.system), path);
        }

        internal override ActorContext CreateActorContext(ActorRef self)
        {
            return new ActorContext(this.system, self);
        }
    }
}
