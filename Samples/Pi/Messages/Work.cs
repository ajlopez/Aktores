namespace Pi.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Work
    {
        private int start;
        private int noelements;

        public Work(int start, int noelements)
        {
            this.start = start;
            this.noelements = noelements;
        }

        public int Start { get { return this.start; } }

        public int NoElements { get { return this.noelements; } }
    }
}

