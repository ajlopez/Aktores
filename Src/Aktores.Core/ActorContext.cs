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

        internal ActorContext(ActorSystem system, string name)
        {
            this.system = system;
            this.parent = system;
            this.path = "/" + name;
        }

        internal ActorContext(ActorContext parent, string name)
        {
            this.system = parent.system;
            this.parent = parent;
            this.path = parent.Path + "/" + name;
        }

        public string Path { get { return this.path; } }

        public override void Stop(ActorRef actorref)
        {
            this.system.Stop(actorref);
        }

        internal override ActorRef CreateActorRef(Actor actor)
        {
            return this.system.CreateActorRef(actor);
        }

        internal override ActorContext CreateActorContext(string name)
        {
            return new ActorContext(this, name);
        }
    }
}
