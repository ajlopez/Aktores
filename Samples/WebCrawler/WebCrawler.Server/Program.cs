namespace WebCrawler.Server
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

            ActorSystem system = new ActorSystem();
            TcpServer host = new TcpServer(address.HostName, address.Port, system);
            host.Start();

            Resolver resolver = new Resolver();
            Harvester harvester = new Harvester();

            RouterActor router = new RouterActor();

            ActorRef resolverref = system.ActorOf(resolver, "resolver");
            ActorRef downloaderref = system.ActorOf(router, "downloader");
            ActorRef harvesterref = system.ActorOf(harvester, "harvester");

            for (int k = 1; k <= 10; k++)
            {
                Downloader downloader = new Downloader();
                downloader.Harvester = harvesterref;
                ActorRef dlref = system.ActorOf(downloader);
                router.Register(dlref);
            }

            resolver.Downloader = downloaderref;
            harvester.Resolver = resolverref;

            foreach (var arg in args.Skip(1))
                resolverref.Tell(arg);
        }
    }
}
