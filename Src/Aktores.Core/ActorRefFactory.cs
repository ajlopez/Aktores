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

            this.Register(actorref, name);

            actor.Self = actorref;
            actor.Context = this.CreateActorContext(actorref.Path);

            actor.Initialize();

            lock (actor)
                actor.Start();

            return actorref;
        }

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

        internal abstract ActorRef CreateActorRef(Actor actor, string name);

        internal abstract ActorContext CreateActorContext(string path);
    }
}
