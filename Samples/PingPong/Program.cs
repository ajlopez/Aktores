namespace PingPong
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Aktores.Core;
    using System.Threading;

    public class Program
    {
        public static void Main(string[] args)
        {
            ActorSystem system = new ActorSystem(2);
            var pingactor = new PingActor();
            var pongactor = new PongActor();
            
            var ping = system.ActorOf(pingactor);
            var pong = system.ActorOf(pongactor);

            for (int k = 0; k < 10; k++)
            {
                ping.Tell("pong", pong);
                pong.Tell("ping", ping);
            }

            int total = 0;

            while (true)
            {
                Thread.Sleep(10000);
                int newtotal = pingactor.MessageCount + pongactor.MessageCount;
                double average = (newtotal - total) / 10.0;
                Console.WriteLine("Total: {0}; Throughput: {1}/s", newtotal, average);
                total = newtotal;
            }
        }
    }
}
