namespace WebCrawler.Worker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Aktores.Core;
    using Aktores.Core.Communication;
    using Aktores.Core.Routing;
    using WebCrawler.Core;

    public class Program
    {
        static void Main(string[] args)
        {
            Address address = new Address(args[0]);
            string localaddress = address.ToString();

            ActorSystem system = new ActorSystem();
            TcpServer host = new TcpServer(address.HostName, address.Port, system);
            host.Start();

            Address serveraddress = new Address(args[1]);
            string remoteaddress = serveraddress.ToString();

            Harvester harvester = new Harvester();
            RouterActor router = new RouterActor();

            ActorRef resolverref = system.ActorSelect(remoteaddress + "/resolver");
            ActorRef remotedownloaderref = system.ActorSelect(remoteaddress + "/downloader");
            ActorRef harvesterref = system.ActorOf(harvester, "harvester");

            harvester.Resolver = resolverref;

            for (int k = 1; k <= 10; k++)
            {
                Downloader downloader = new Downloader();
                downloader.Harvester = harvesterref;
                ActorRef dlref = system.ActorOf(downloader);
                remotedownloaderref.Tell(new RegisterActorMessage() { ActorPath = localaddress + dlref.Path.ToString() });
            }
        }
    }
}
