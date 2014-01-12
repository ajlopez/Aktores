namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorSystem
    {
        private IDictionary<string, ActorRef> actors = new Dictionary<string, ActorRef>();

        public ActorRef ActorOf(Type t, string name = null)
        {
            var actor = (Actor)Activator.CreateInstance(t);
            return this.ActorOf(actor, name);
        }

        public ActorRef ActorOf(Actor actor, string name = null)
        {
            var actorref = new ActorRef(actor);

            if (!string.IsNullOrWhiteSpace(name))
                this.actors[name] = actorref;

            actor.Self = actorref;
            actor.Context = this;

            actor.Initialize();
            actor.Start();

            return actorref;
        }

        public ActorRef ActorFor(string name)
        {
            if (this.actors.ContainsKey(name))
                return this.actors[name];

            return null;
        }
    }
}
