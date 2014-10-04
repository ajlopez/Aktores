namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorContext : ActorRefFactory
    {
        private ActorSystem system;
        private ActorLocalRef self;

        internal ActorContext(ActorSystem system, ActorLocalRef self)
            : base()
        {
            this.system = system;
            this.self = self;
        }

        public ActorSystem ActorSystem { get { return this.system; } }

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
            return this.system.ActorSelectRemote(address, path);
        }

        internal override ActorLocalRef CreateActorRef(Actor actor, string name)
        {
            ActorPath path = new ActorPath(this.self.Path, name);

            return new ActorLocalRef(actor, new Mailbox(this.system), path);
        }

        internal override ActorContext CreateActorContext(ActorLocalRef self)
        {
            return new ActorContext(this.system, self);
        }
    }
}
