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
        private string path;

        internal ActorContext(ActorSystem system, string path)
        {
            this.system = system;
            this.parent = system;
            this.path = path;
        }

        internal ActorContext(ActorContext parent, string path)
        {
            this.system = parent.system;
            this.parent = parent;
            this.path = path;
        }

        public override void Stop(ActorRef actorref)
        {
            this.system.Stop(actorref);
        }

        internal override ActorRef CreateActorRef(Actor actor, string name)
        {
            string newpath = this.path;

            if (!newpath.EndsWith("/"))
                newpath += "/";

            newpath += name;

            return new ActorRef(actor, new Mailbox(this.system), newpath);
        }

        internal override ActorContext CreateActorContext(string path)
        {
            return new ActorContext(this, path);
        }
    }
}
