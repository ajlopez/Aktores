namespace Aktores.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class LambdaActor : Actor
    {
        private Action<object> fn;

        public LambdaActor(Action<object> fn)
        {
            this.fn = fn;
        }

        public override void Receive(object message)
        {
            this.fn(message);
        }
    }
}
