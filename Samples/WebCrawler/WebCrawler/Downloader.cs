namespace WebCrawler
{
    using System;
    using System.Globalization;
    using System.Net;
    using Aktores.Core;

    public class Downloader : Actor
    {
        public ActorRef Harvester { get; set; }

        public void Process(string url)
        {
            Console.WriteLine("[Downloader] Processing " + url);
            string content;

            try
            {
                WebClient client = new WebClient();
                content = client.DownloadString(url);

                Console.WriteLine(
                    string.Format(CultureInfo.InvariantCulture, "URL {0} downloaded", url));
            }
            catch (System.Net.WebException)
            {
                Console.WriteLine(
                   string.Format(
                   CultureInfo.InvariantCulture, 
                   "URL could not be downloaded", 
                   url));

                return;
            }

            this.Harvester.Tell(content);
        }

        public override void Receive(object message)
        {
            this.Process((string)message);
        }
    }
}
