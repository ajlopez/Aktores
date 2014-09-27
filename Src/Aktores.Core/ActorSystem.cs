namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using Aktores.Core.Communication;

    public class ActorSystem : ActorRefFactory
    {
        private TaskQueue queue = new TaskQueue();
        private int nworkers;
        private IDictionary<string, TcpClient> remotes = new Dictionary<string, TcpClient>();

        public ActorSystem(int nworkers = 10)
        {
            this.nworkers = nworkers;

            for (int k = 0; k < nworkers; k++)
                (new Worker(this.queue)).Start();
        }

        public void Stop(ActorRef actorref)
        {
            ((ActorLocalRef)actorref).Actor.Stop();
        }

        public void Shutdown()
        {
            for (int k = 0; k < this.nworkers; k++)
                this.queue.Add(null);

            foreach (var childctx in this.ActorContexts)
                childctx.Shutdown();

            foreach (var remote in this.remotes.Values)
                remote.Stop();
        }

        internal override ActorRef ActorSelectRemote(Address address, ActorPath path)
        {
            var key = address.ToString();

            if (!this.remotes.ContainsKey(key))
            {
                var client = new TcpClient(address.HostName, address.Port);
                client.Start();
                this.remotes[key] = client;
            }

            return new ActorRemoteRef(this.remotes[key], path);
        }

        internal override ActorRef ActorSelectLocal(string name)
        {
            if (name.StartsWith("/"))
                return base.ActorSelectLocal(name.Substring(1));

            return base.ActorSelectLocal(name);
        }

        internal void AddTask(ActorTask task)
        {
            this.queue.Add(task);
        }

        internal override ActorLocalRef CreateActorRef(Actor actor, string name)
        {
            return new ActorLocalRef(actor, new Mailbox(this), new ActorPath(name));
        }

        internal override ActorContext CreateActorContext(ActorLocalRef self)
        {
            return new ActorContext(this, self);
        }
    }
}
