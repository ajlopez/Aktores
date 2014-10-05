namespace WebCrawler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Aktores.Core;
    using Aktores.Core.Routing;

    public class Program
    {
        static void Main(string[] args)
        {
            ActorSystem system = new ActorSystem();

            Resolver resolver = new Resolver();
            //Downloader downloader = new Downloader();
            Harvester harvester = new Harvester();

            RouterActor router = new RouterActor();

            ActorRef resolverref = system.ActorOf(resolver, "resolver");
            //ActorRef downloaderref = system.ActorOf(downloader, "downloader");
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
            //downloader.Harvester = harvesterref;
            harvester.Resolver = resolverref;

            foreach (var arg in args)
                resolverref.Tell(arg);
        }
    }
}
