namespace Aktores.Core.Communication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class OutputChannel
    {
        private BinaryWriter writer;

        public OutputChannel(BinaryWriter writer)
        {
            this.writer = writer;
        }

        public void Write(object obj)
        {
            if (obj == null)
            {
                this.writer.Write((byte)Types.Null);
                return;
            }

            if (obj is int)
            {
                this.writer.Write((byte)Types.Integer);
                this.writer.Write((int)obj);
                return;
            }

            if (obj is short)
            {
                this.writer.Write((byte)Types.Short);
                this.writer.Write((short)obj);
                return;
            }

            if (obj is long)
            {
                this.writer.Write((byte)Types.Long);
                this.writer.Write((long)obj);
                return;
            }

            if (obj is char)
            {
                this.writer.Write((byte)Types.Char);
                this.writer.Write((char)obj);
                return;
            }

            if (obj is byte)
            {
                this.writer.Write((byte)Types.Byte);
                this.writer.Write((byte)obj);
                return;
            }

            if (obj is double)
            {
                this.writer.Write((byte)Types.Double);
                this.writer.Write((double)obj);
                return;
            }

            if (obj is float)
            {
                this.writer.Write((byte)Types.Float);
                this.writer.Write((float)obj);
                return;
            }

            if (obj is string)
            {
                this.writer.Write((byte)Types.String);
                this.writer.Write((string)obj);
                return;
            }
        }
    }
}
