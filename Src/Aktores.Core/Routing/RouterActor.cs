﻿namespace Aktores.Core.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RouterActor : Actor
    {
        private Random random = new Random();
        private IList<ActorRef> childrefs = new List<ActorRef>();

        public void Register(ActorRef childref)
        {
            this.childrefs.Add(childref);
        }

        public override void Receive(object message)
        {
            if (message is RegisterActorMessage)
            {
                var rmsg = (RegisterActorMessage)message;
                this.Register(this.Context.ActorSystem.ActorSelect(rmsg.ActorPath));
                return;
            }

            int n = 0;

            while (this.childrefs.Count > 0)
                try
                {
                    n = this.random.Next(this.childrefs.Count);
                    this.childrefs[n].Tell(message, this.Sender);
                    return;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.StackTrace);
                    this.childrefs.RemoveAt(n);
                }
        }
    }
}
