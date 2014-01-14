namespace Pi.Actors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Aktores.Core;
    using Pi.Messages;

    public class Worker : Actor
    {
        public override void Receive(object message)
        {
            if (message is Work) 
            {
                Work work = (Work)message;
                Console.WriteLine(string.Format("start {0}; no.elements {1}", work.Start, work.NoElements));
                this.Sender.Tell(new Result(this.CalculatePiFor(work.Start, work.NoElements)));
                return;
            }

            throw new NotImplementedException();
        }

        public double CalculatePiFor(int start, int noelements)
        {
            double acc = 0;

            for (int i = start; i < start + noelements; i++)
                acc += 4.0 * (1 - (i % 2) * 2) / (2 * i + 1);

            return acc;
        }
    }
}

