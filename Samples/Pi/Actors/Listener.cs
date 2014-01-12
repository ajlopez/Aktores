namespace Pi.Actors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Aktores.Core;
    using Pi.Messages;

    public class Listener : Actor
    {
        protected override void Receive(object message)
        {
            double pi = ((PiAproximation)message).Value;

            Console.WriteLine(pi);
            this.Context.Shutdown();
        }
    }
}
