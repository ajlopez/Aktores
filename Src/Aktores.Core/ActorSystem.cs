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
                actors[name] = actorref;

            actor.Self = actorref;
            actor.Context = this;

            actor.Initialize();

            return actorref;
        }

        public ActorRef ActorFor(string name)
        {
            if (actors.ContainsKey(name))
                return actors[name];

            return null;
        }
    }
}
