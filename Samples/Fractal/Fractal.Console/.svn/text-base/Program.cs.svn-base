using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fractal.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[4].Equals("*"))
                args[4] = "0";

            SectorInfo sectorinfo = new SectorInfo()
            {
                RealMinimum = Convert.ToDouble(args[0]),
                ImgMinimum = Convert.ToDouble(args[1]),
                Delta = Convert.ToDouble(args[2]),
                FromX = Convert.ToInt32(args[3]),
                FromY = Convert.ToInt32(args[4]),
                Width = Convert.ToInt32(args[5]),
                Height = Convert.ToInt32(args[6]),
                MaxIterations = Convert.ToInt32(args[7]),
                MaxValue = Convert.ToInt32(args[8])
            };

            Calculator calculator = new Calculator();

            Sector sector = calculator.CalculateSector(sectorinfo);

            SectorSerializer serializer = new SectorSerializer();

            string filename = string.Format("{0}-{1}-{2}-{3}-{4}.bin", args[9], sectorinfo.FromX, sectorinfo.FromY, sectorinfo.Width, sectorinfo.Height);

            serializer.Serialize(sector, filename);
        }
    }
}
