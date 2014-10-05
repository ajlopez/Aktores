namespace WebCrawler.Core
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Aktores.Core;

    public class Resolver : Actor
    {
        private List<Uri> downloadedAddresses;
        private string host;

        public Resolver() 
        {
            this.downloadedAddresses = new List<Uri>();
        }

        public ActorRef Downloader { get; set; }

        public void Process(string url)
        {
            Uri target;
                
            try
            {
                target = new Uri(url);
            }
            catch (Exception ex) 
            {
                return;
            }

            if ((target.Scheme != Uri.UriSchemeHttp) &&
                (target.Scheme != Uri.UriSchemeHttps))
            {                
                return;
            }

            if (this.host == null)
                this.host = target.Host;
            else if (this.host != target.Host)
            {
                return;
            }

            if (this.downloadedAddresses.Contains(target))
                return;

            Console.WriteLine("[Resolver] processing " + url);

            this.downloadedAddresses.Add(target);
                    
            this.Downloader.Tell(url);
        }

        public override void Receive(object message)
        {
            this.Process((string)message);
        }
    }
}