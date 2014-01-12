namespace Pi.Actors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Aktores.Core;
    using Pi.Messages;

    public class Master : Actor
    {
        private int noworkers;
        private int nomessages;
        private int noelements;
        private int noresults;

        private ActorRef listener;
        private ActorRef[] workers;
        private int position;
        private double pi;

        public Master(int noworkers, int nomessages, int noelements, ActorRef listener)
        {
            this.noworkers = noworkers;
            this.nomessages = nomessages;
            this.noelements = noelements;
            this.listener = listener;

            this.workers = new ActorRef[this.noworkers];
        }

        public override void Initialize()
        {
            for (int k = 0; k < this.noworkers; k++)
                this.workers[k] = this.Context.ActorOf(typeof(Worker));
        }

        protected override void Receive(object message)
        {
            if (message is Calculate)
            {
                for (int k = 0; k < this.nomessages; k++)
                    this.workers[k % this.noworkers].Tell(new Work(k * this.noelements, this.noelements), this.Self);

                return;
            }

            if (message is Result)
            {
                var result = (Result)message;
                pi += result.Value;
                this.noresults++;

                if (this.noresults >= this.nomessages)
                {
                    this.listener.Tell(new PiAproximation(pi));
                }

                return;
            }

            throw new NotImplementedException();
        }
    }
}
