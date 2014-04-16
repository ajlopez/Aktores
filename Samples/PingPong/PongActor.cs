namespace PingPong
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Aktores.Core;

    public class PongActor : Actor
    {
        private int messagecount;

        public int MessageCount { get { return this.messagecount; } }

        public override void Receive(object message)
        {
            messagecount++;
            this.Sender.Tell("pong", this.Self);
        }
    }
}
