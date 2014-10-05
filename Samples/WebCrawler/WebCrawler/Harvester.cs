namespace WebCrawler
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using System.Globalization;
    using System.Text.RegularExpressions;
    using Aktores.Core;

    public class Harvester : Actor
    {
        public ActorRef Resolver { get; set; }

        public void Process(string body)
        {            
            IEnumerable<string> links = HarvestUrls(body);
            foreach (string link in links)
                this.Resolver.Tell(link);
        }

        public override void Receive(object message)
        {
            this.Process((string)message);
        }

        private static List<string> HarvestUrls(string content)
        {
            string regexp = @"href=\s*""([^&""]*)""";

            MatchCollection matches = Regex.Matches(content, regexp);
            List<string> links = new List<string>();

            foreach (Match m in matches)
            {
                if (!links.Contains(m.Groups[1].Value))
                {
                    links.Add(m.Groups[1].Value);
                }
            }

            return links;
        }
    }
}
