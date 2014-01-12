namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorSystem
    {
        private IDictionary<string, Actor> actors = new Dictionary<string, Actor>();

        public Actor ActorOf(Type t, string name = null)
        {
            var actor = (Actor)Activator.CreateInstance(t);

            if (!string.IsNullOrWhiteSpace(name))
                actors[name] = actor;

            return actor;
        }

        public Actor ActorFor(string name)
        {
            if (actors.ContainsKey(name))
                return actors[name];

            return null;
        }
    }
}
