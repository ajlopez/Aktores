namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class ActorRefFactory
    {
        private IDictionary<string, ActorRef> actors = new Dictionary<string, ActorRef>();

        internal IEnumerable<ActorRef> ActorRefs { get { return this.actors.Values; } }

        public abstract ActorRef ActorOf(Type t, string name = null);

        public abstract ActorRef ActorOf(Actor actor, string name = null);

        public abstract void Stop(ActorRef actorref);

        public ActorRef ActorFor(string name)
        {
            if (name.StartsWith("/"))
                name = name.Substring(1);

            if (this.actors.ContainsKey(name))
                return this.actors[name];

            return null;
        }

        internal void Register(ActorRef actor, string name)
        {
            this.actors[name] = actor;
        }
    }
}
