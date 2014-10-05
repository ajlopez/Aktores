namespace WebCrawler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Aktores.Core;

    public class Program
    {
        static void Main(string[] args)
        {
            ActorSystem system = new ActorSystem();

            Resolver resolver = new Resolver();
            Downloader downloader = new Downloader();
            Harvester harvester = new Harvester();

            ActorRef resolverref = system.ActorOf(resolver, "resolver");
            ActorRef downloaderref = system.ActorOf(downloader, "downloader");
            ActorRef harvesterref = system.ActorOf(harvester, "harvester");

            resolver.Downloader = downloaderref;
            downloader.Harvester = harvesterref;
            harvester.Resolver = resolverref;

            foreach (var arg in args)
                resolverref.Tell(arg);
        }
    }
}
