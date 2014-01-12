namespace Pi.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PiAproximation
    {
        private double value;

        public PiAproximation(double value)
        {
            this.value = value;
        }

        public double Value { get { return this.value; } }
    }
}
