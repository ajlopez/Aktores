namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorSystem : ActorRefFactory
    {
        private TaskQueue queue = new TaskQueue();
        private int nworkers;

        public ActorSystem(int nworkers = 10)
        {
            this.nworkers = nworkers;

            for (int k = 0; k < nworkers; k++)
                (new Worker(this.queue)).Start();
        }

        public override void Stop(ActorRef actorref)
        {
            actorref.Actor.Stop();
        }

        public void Shutdown()
        {
            for (int k = 0; k < this.nworkers; k++)
                this.queue.Add(null);

            foreach (var actorref in this.ActorRefs)
                this.Stop(actorref);
        }

        public override ActorRef ActorFor(string name)
        {
            if (name.StartsWith("/"))
                return base.ActorFor(name.Substring(1));

            return base.ActorFor(name);
        }

        internal void AddTask(ActorTask task)
        {
            this.queue.Add(task);
        }

        internal override ActorRef CreateActorRef(Actor actor, string name)
        {
            return new ActorRef(actor, new Mailbox(this), new ActorPath(name));
        }

        internal override ActorContext CreateActorContext(ActorRef self)
        {
            return new ActorContext(this, self);
        }
    }
}
