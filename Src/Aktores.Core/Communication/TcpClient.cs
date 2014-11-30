namespace Aktores.Core.Communication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using Aktores.Core;

    public class TcpClient
    {
        private string host;
        private int port;
        private System.Net.Sockets.TcpClient client;
        private OutputChannel channel;

        public TcpClient(string host, int port)
        {
            this.host = host;
            this.port = port;
            this.client = new System.Net.Sockets.TcpClient();
        }

        public void Start()
        {
            this.client.Connect(this.host, this.port);
            this.channel = new OutputChannel(new BinaryWriter(this.client.GetStream()));
        }

        public void Stop()
        {
            this.client.Close();
        }

        public void Tell(string path, object message, ActorRef sender = null)
        {
            lock (this)
            {
                this.channel.Write(path);
                this.channel.Write(message);
            }
        }
    }
}
