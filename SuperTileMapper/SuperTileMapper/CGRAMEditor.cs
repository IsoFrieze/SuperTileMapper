using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTileMapper
{
    public partial class CGRAMEditor : Form
    {
        public CGRAMEditor()
        {
            InitializeComponent();
            Redraw();
        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportData import = new ImportData("CGRAM", Data.CGRAM);
            DialogResult result = import.ShowDialog();
            if (result == DialogResult.OK) Redraw();
        }

        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportData export = new ExportData("CGRAM", Data.CGRAM);
            DialogResult result = export.ShowDialog();
            if (result == DialogResult.OK) Redraw();
        }

        private void Redraw()
        {
            Bitmap img = new Bitmap(256, 256);
            for (int cy = 0; cy < 16; cy++)
            {
                for (int cx = 0; cx < 16; cx++)
                {
                    for (int py = 0; py < 16; py++)
                    {
                        for (int px = 0; px < 16; px++)
                        {
                            int i = 2 * (cy * 0x10 + cx);
                            int r = (Data.CGRAM[i] & 0x1F) << 3;
                            int g = ((Data.CGRAM[i] & 0xE0) >> 2) | ((Data.CGRAM[i+1] & 0x03) << 6);
                            int b = (Data.CGRAM[i+1] & 0x7C) << 1;
                            img.SetPixel(16 * cx + px, 16 * cy + py, Color.FromArgb(r, g, b));
                        }
                    }
                }
            }
            pictureBox1.Image = img;
        }
    }
}
