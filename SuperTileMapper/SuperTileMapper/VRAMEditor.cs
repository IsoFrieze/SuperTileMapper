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
    public partial class VRAMEditor : Form
    {
        public VRAMEditor()
        {
            InitializeComponent();
            Redraw();
        }

        private void VRAMEditor_Load(object sender, EventArgs e)
        {

        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportData import = new ImportData("VRAM", Data.VRAM);
            DialogResult result = import.ShowDialog();
            if (result == DialogResult.OK) Redraw();
        }

        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Redraw()
        {
            Color[] colors = new Color[] { Color.Gray, Color.Black, Color.Green, Color.Cyan };
            Bitmap img = new Bitmap(512, 512);
            for (int ty = 0; ty < 64; ty++)
            {
                for (int tx = 0; tx < 64; tx++)
                {
                    for (int py = 0; py < 8; py++)
                    {
                        for (int px = 0; px < 8; px++)
                        {
                            int i = 0x400 * ty + 0x10 * tx + 2 * py;
                            int b0 = 0x01 & Data.VRAM[i] >> (7 - px);
                            int b1 = 0x01 & Data.VRAM[i + 1] >> (7 - px);
                            int x = b0 + 2 * b1;
                            img.SetPixel(8 * tx + px, 8 * ty + py, colors[x]);
                        }
                    }
                }
            }
            pictureBox1.Image = img;
        }
    }
}
