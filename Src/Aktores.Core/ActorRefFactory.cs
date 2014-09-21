namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class ActorRefFactory
    {
        private IDictionary<string, ActorRef> actorrefs = new Dictionary<string, ActorRef>();
        private IDictionary<string, ActorContext> contexts = new Dictionary<string, ActorContext>();

        internal IEnumerable<ActorRef> ActorRefs { get { return this.actorrefs.Values; } }

        internal IEnumerable<ActorContext> ActorContexts { get { return this.contexts.Values; } }

        public ActorRef ActorOf(Type t, string name = null)
        {
            var actor = (Actor)Activator.CreateInstance(t);
            return this.ActorOf(actor, name);
        }

        public ActorRef ActorOf(Actor actor, string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = Guid.NewGuid().ToString();

            var actorref = this.CreateActorRef(actor, name);
            var actorctx = this.CreateActorContext(actorref);

            this.Register(actorref, actorctx, name);

            actor.Self = actorref;
            actor.Context = actorctx;

            actor.Initialize();

            lock (actor)
                actor.Start();

            return actorref;
        }

        public virtual ActorRef ActorFor(string name)
        {
            int p = name.IndexOf('/');

            if (p > 0)
            {
                string topname = name.Substring(0, p);
                name = name.Substring(p + 1);

                if (this.contexts.ContainsKey(topname))
                    return this.contexts[topname].ActorFor(name);

                return null;
            }

            if (this.actorrefs.ContainsKey(name))
                return this.actorrefs[name];

            return null;
        }

        internal void Register(ActorRef actorref, ActorContext context, string name)
        {
            this.actorrefs[name] = actorref;
            this.contexts[name] = context;
        }

        internal abstract ActorRef CreateActorRef(Actor actor, string name);

        internal abstract ActorContext CreateActorContext(ActorRef self);
    }
}
