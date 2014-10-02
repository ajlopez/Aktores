using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fractal.GUIFiles
{
    public partial class FractalForm : Form
    {
        private int nfile = 0;
        private bool mousePressed = false;
        private int mouseX;
        private int mouseY;

        private double realCenter = 0;
        private double imgCenter = 0;

        private double realDelta = 0.01;
        private double imgDelta = 0.01;

        private double realWidth;
        private double imgHeight;

        private double realMin;
        private double imgMin;

        private Random random = new Random();

        Color[] colors = new Color[2000];

        public FractalForm()
        {
            InitializeComponent();

            ResetColors();
        }

        private void ResetColors() 
        {
            for (int k = 0; k < colors.Length; k++)
                colors[k] = Color.FromArgb(k % 256, (k + 100) % 256, (k + 200) % 256);
        }

        private void ResetImage()
        {
            realCenter = 0;
            imgCenter = 0;
            imgDelta = 0.01;
            realDelta = 0.01;
        }

        private void ChangeColors()
        {
            int adjust = random.Next(128) + 64;
            int adjust2 = random.Next(256);

            for (int k = 0; k < colors.Length; k++)
                colors[k] = Color.FromArgb((k+adjust2) % 256, (k + adjust) % 256, (k + adjust*2) % 256);
        }

        public void DrawValues(int fromx, int fromy, int width, int height, int [] values) 
        {
            Bitmap bitmap;

            if (pcbFractal.Image == null)
                pcbFractal.Image = new Bitmap(pcbFractal.Width, pcbFractal.Height);

            bitmap = (Bitmap) pcbFractal.Image;

            int maxheight = bitmap.Height;
            int maxwidth = bitmap.Width;

            for (int x = 0; x < width && x+fromx < maxwidth; x++)
                for (int y = 0; y < height && y + fromy < maxheight; y++)
                    bitmap.SetPixel(x + fromx, y + fromy, colors[values[y * width + x]]);
                
            pcbFractal.Refresh();
        }

        private void pcbFractal_MouseDown(object sender, MouseEventArgs e)
        {
            mousePressed = true;
            mouseX = e.X;
            mouseY = e.Y;
        }

        private void pcbFractal_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pcbFractal_MouseUp(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                mousePressed = false;

                int newWidth = Math.Abs(mouseX - e.X);
                int newHeight = Math.Abs(mouseY - e.Y);

                if (newWidth == 0 || newHeight == 0)
                    return;

                realWidth = realDelta * pcbFractal.Width;
                imgHeight = imgDelta * pcbFractal.Height;

                realMin = realCenter - realWidth / 2;
                imgMin = imgCenter - imgHeight / 2;

                double newRealCenter = realMin + realDelta * (Math.Min(e.X, mouseX) + newWidth / 2);
                double newImgCenter = imgMin + imgDelta * (Math.Min(e.Y, mouseY) + newHeight / 2);

                realDelta = realDelta * newWidth / pcbFractal.Width;
                imgDelta = realDelta;

                realCenter = newRealCenter;
                imgCenter = newImgCenter;

                Calculate();
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void Calculate()
        {
            Bitmap bitmap = new Bitmap(pcbFractal.Width, pcbFractal.Height);
            pcbFractal.Image = bitmap;
            pcbFractal.Refresh();

            realWidth = realDelta * pcbFractal.Width;
            imgHeight = imgDelta * pcbFractal.Height;

            realMin = realCenter - realWidth / 2;
            imgMin = imgCenter - imgHeight / 2;

            int width = pcbFractal.Width;
            int height = pcbFractal.Height;

            SectorInfo sectorinfo = new SectorInfo()
            {
                FromX = 0,
                FromY = 0,
                Width = width,
                Height = height,
                RealMinimum = realMin,
                ImgMinimum = imgMin,
                Delta = realDelta,
                MaxIterations = colors.Length,
                MaxValue = 4
            };

            Calculator calculator = new Calculator();
            Sector sector = calculator.CalculateSector(sectorinfo);
            SectorSerializer serializer = new SectorSerializer();

            serializer.Serialize(sector, string.Format("sector{0}.bin", nfile++));

            this.DrawValues(sector.FromX, sector.FromY, sector.Width, sector.Height, sector.Values);
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            realDelta /= 2;
            imgDelta /= 2;
            Calculate();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            realDelta *= 2;
            imgDelta *= 2;
            Calculate();
        }

        private void btnResetColors_Click(object sender, EventArgs e)
        {
            ResetColors();
            Calculate();
        }

        private void btnResetPosition_Click(object sender, EventArgs e)
        {
            ResetImage();
            ResetColors();
            Calculate();
        }

        private void btnChangeColors_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeColors();
                Calculate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.openSectorDialog.ShowDialog() != DialogResult.OK)
                    return;

                SectorSerializer serializer = new SectorSerializer();

                foreach (string filename in this.openSectorDialog.FileNames)
                {
                    Sector sector = serializer.Deserialize(filename);

                    this.DrawValues(sector.FromX, sector.FromY, sector.Width, sector.Height, sector.Values);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
