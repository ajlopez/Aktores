namespace Aktores.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActorContext : ActorRefFactory
    {
        public ActorContext(ActorRefFactory parent)
        {
        }

        public override ActorRef ActorOf(Type t, string name = null)
        {
            throw new NotImplementedException();
        }

        public override ActorRef ActorOf(Actor actor, string name = null)
        {
            throw new NotImplementedException();
        }

        public override void Stop(ActorRef actorref)
        {
            throw new NotImplementedException();
        }
    }
}
