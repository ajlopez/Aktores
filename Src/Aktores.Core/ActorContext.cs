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

        internal ActorContext(ActorSystem system, string path)
            : base(path)
        {
            this.system = system;
            this.parent = system;
        }

        internal ActorContext(ActorContext parent, string path)
            : base(path)
        {
            this.system = parent.system;
            this.parent = parent;
        }

        public override void Stop(ActorRef actorref)
        {
            this.system.Stop(actorref);
        }

        internal override ActorRef CreateActorRef(Actor actor, string name)
        {
            string newpath = this.Prefix + name;

            return new ActorRef(actor, new Mailbox(this.system), newpath);
        }

        internal override ActorContext CreateActorContext(string path)
        {
            return new ActorContext(this, path);
        }
    }
}
