namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorSystem : ActorRefFactory
    {
        private IList<ActorRef> actorrefs = new List<ActorRef>();

        public override ActorRef ActorOf(Type t, string name = null)
        {
            var actor = (Actor)Activator.CreateInstance(t);
            return this.ActorOf(actor, name);
        }

        public override ActorRef ActorOf(Actor actor, string name = null)
        {
            var actorref = new ActorRef(actor);

            if (!string.IsNullOrWhiteSpace(name))
                this.Register(actorref, name);

            actor.Self = actorref;
            actor.Context = this;

            actor.Initialize();
            actor.Start();

            this.actorrefs.Add(actorref);

            return actorref;
        }

        public override void Stop(ActorRef actorref)
        {
            actorref.Actor.Stop();
        }

        public void Shutdown()
        {
            foreach (var actorref in this.actorrefs)
                this.Stop(actorref);
        }
    }
}
