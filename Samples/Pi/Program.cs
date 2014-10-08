namespace Pi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Aktores.Core;
    using Pi.Actors;
    using Pi.Messages;

    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem system = new ActorSystem();

            var listener = system.ActorOf(typeof(Listener));
            var master = system.ActorOf(new Master(10, 1000, 10, listener));

            master.Tell(new Calculate());
        }
    }
}
