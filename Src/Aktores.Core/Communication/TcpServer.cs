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

    public class TcpServer
    {
        private TcpListener listener;
        private bool running;
        private ActorSystem system;

        public TcpServer(string host, int port, ActorSystem system)
        {
            ////IPAddress ipaddress = Dns.GetHostEntry(host).AddressList[0];
            IPAddress ipaddress = Dns.Resolve(host).AddressList[0];
            this.listener = new TcpListener(ipaddress, port);
            this.system = system;
        }

        public void Start()
        {
            this.listener.Start();
            this.running = true;

            (new Thread(delegate()
            {
                while (this.running) 
                {
                    try
                    {
                        var client = this.listener.AcceptTcpClient();

                        (new Thread(delegate()
                        {
                            this.ProcessClient(client);
                        })).Start();
                    }
                    catch
                    {
                    }
                }
            })).Start();
        }

        public void Stop()
        {
            this.running = false;
            this.listener.Stop();
        }

        private void ProcessClient(System.Net.Sockets.TcpClient client)
        {
            try
            {
                var inputchannel = new InputChannel(new BinaryReader(client.GetStream()));

                while (true)
                    this.ProcessMessage(inputchannel);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.StackTrace);
            }
        }

        private void ProcessMessage(InputChannel channel)
        {
            var path = (string)channel.Read();
            var message = channel.Read();
            var actorref = this.system.ActorSelect(path);
            actorref.Tell(message);
        }
    }
}
 