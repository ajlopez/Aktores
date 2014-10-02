using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Fractal
{
    public class Calculator
    {
        public Sector CalculateSector(SectorInfo si)
        {
            int[] values = new int[si.Width * si.Height];

            Parallel.For(0, si.Width, x =>
            {
                for (int y = 0; y < si.Height; y++)
                {
                    values[y * si.Width + x] = GetValue(si.RealMinimum + (x + si.FromX) * si.Delta, si.ImgMinimum + (y + si.FromY) * si.Delta, si.MaxIterations - 1, si.MaxValue);
                }
            });

            Sector sector = new Sector()
            {
                FromX = si.FromX,
                FromY = si.FromY,
                Width = si.Width,
                Height = si.Height,
                Values = values
            };

            return sector;
        }

        private int GetValue(double RealC, double ImaginaryC, int maxIter, int maxMagSquared)
        {
            double RealZ = 0;
            double ImaginaryZ = 0;
            double RealZ2 = 0;
            double ImaginaryZ2 = 0;
            int Value = 0;

            while ((Value < maxIter) && (RealZ2 + ImaginaryZ2 < maxMagSquared))
            {
                RealZ2 = RealZ * RealZ;
                ImaginaryZ2 = ImaginaryZ * ImaginaryZ;
                ImaginaryZ = 2 * ImaginaryZ * RealZ + ImaginaryC;
                RealZ = RealZ2 - ImaginaryZ2 + RealC;
                Value++;
            }
            return Value;
        }
    }
}
