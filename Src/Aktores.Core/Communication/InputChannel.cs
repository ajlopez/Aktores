namespace Aktores.Core.Communication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class InputChannel
    {
        private BinaryReader reader;

        public InputChannel(BinaryReader reader)
        {
            this.reader = reader;
        }

        public object Read()
        {
            byte type = this.reader.ReadByte();

            switch (type)
            {
                case (byte)Types.Null:
                    return null;
                case (byte)Types.Integer:
                    return this.reader.ReadInt32();
                case (byte)Types.Double:
                    return this.reader.ReadDouble();
                case (byte)Types.String:
                    return this.reader.ReadString();
                case (byte)Types.Byte:
                    return this.reader.ReadByte();
                case (byte)Types.Character:
                    return this.reader.ReadChar();
                case (byte)Types.Single:
                    return this.reader.ReadSingle();
                case (byte)Types.Short:
                    return this.reader.ReadInt16();
                case (byte)Types.Long:
                    return this.reader.ReadInt64();
                case (byte)Types.Decimal:
                    return this.reader.ReadDecimal();
            }

            throw new InvalidDataException();
        }
    }
}
