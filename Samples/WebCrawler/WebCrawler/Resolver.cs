namespace WebCrawler
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Aktores.Core;

    public class Resolver : Actor
    {
        private List<Uri> downloadedAddresses;

        public Resolver() 
        {
            this.downloadedAddresses = new List<Uri>();
        }

        public ActorRef Downloader { get; set; }

        public void Process(string url)
        {
            Console.WriteLine("[Resolver] processing " + url);
            
            Uri target = new Uri(url);

            if ((target.Scheme != Uri.UriSchemeHttp) &&
                (target.Scheme != Uri.UriSchemeHttps))
            {                
                Console.WriteLine(
                    string.Format(CultureInfo.InvariantCulture, "URL rejected {0}: unsupported protocol", url));
                return;
            }

            if (this.downloadedAddresses.Contains(target))
                return;

            this.downloadedAddresses.Add(target);
                    
            this.Downloader.Tell(url);
        }

        public override void Receive(object message)
        {
            this.Process((string)message);
        }
    }
}