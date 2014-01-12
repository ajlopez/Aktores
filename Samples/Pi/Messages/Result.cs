namespace Pi.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Result
    {
        private double value;

        public Result(double value)
        {
            this.value = value;
        }

        public double Value { get { return this.value; } }
    }
}
