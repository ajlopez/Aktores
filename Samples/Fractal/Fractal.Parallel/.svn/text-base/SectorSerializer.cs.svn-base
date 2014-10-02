using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Fractal
{
    public class SectorSerializer
    {
        public void Serialize(Sector sector, string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Create(filename);
            formatter.Serialize(stream, sector);
            stream.Close();
        }

        public Sector Deserialize(string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.OpenRead(filename);
            Sector sector = (Sector)formatter.Deserialize(stream);
            stream.Close();
            return sector;
        }
    }
}
