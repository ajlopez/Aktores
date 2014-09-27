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

        internal override ActorRef ActorSelectLocal(string name)
        {
            if (name.StartsWith("/"))
                return this.system.ActorSelectLocal(name);

            return base.ActorSelectLocal(name);
        }

        internal void Shutdown()
        {
            foreach (var childctx in this.ActorContexts)
                childctx.Shutdown();

            this.self.Actor.Stop();
        }

        internal override ActorRef ActorSelectRemote(Address address, ActorPath path)
        {
            throw new NotImplementedException();
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
