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
        int bpp = 0;
        int pal = 0;
        int zoom = 1;

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
            ExportData export = new ExportData("VRAM", Data.VRAM);
            DialogResult result = export.ShowDialog();
            if (result == DialogResult.OK) Redraw();
        }

        public void Redraw()
        {
            Bitmap img = new Bitmap((bpp == 0 ? 512 : 256) * zoom, (bpp < 2 ? 512 : 256) * zoom);
            for (int ty = 0; ty < (bpp < 2 ? 0x40 : 0x20); ty++)
            {
                for (int tx = 0; tx < (bpp == 0 ? 0x40 : 0x20); tx++)
                {
                    int tile = (bpp == 0 ? 0x40 : 0x20) * ty + tx;
                    for (int py = 0; py < 8; py++)
                    {
                        for (int px = 0; px < 8; px++)
                        {
                            int i = ((0x10 * tile) * (bpp == 0 ? 1 : (bpp == 1 ? 2 : 4))) + 2 * py;
                            int b0 = 0x01 & Data.VRAM[(0x00 + i + 0) % Data.VRAM.Length] >> (7 - px);
                            int b1 = 0x01 & Data.VRAM[(0x00 + i + 1) % Data.VRAM.Length] >> (7 - px);
                            int b2 = 0x01 & Data.VRAM[(0x10 + i + 0) % Data.VRAM.Length] >> (7 - px);
                            int b3 = 0x01 & Data.VRAM[(0x10 + i + 1) % Data.VRAM.Length] >> (7 - px);
                            int b4 = 0x01 & Data.VRAM[(0x20 + i + 0) % Data.VRAM.Length] >> (7 - px);
                            int b5 = 0x01 & Data.VRAM[(0x20 + i + 1) % Data.VRAM.Length] >> (7 - px);
                            int b6 = 0x01 & Data.VRAM[(0x30 + i + 0) % Data.VRAM.Length] >> (7 - px);
                            int b7 = 0x01 & Data.VRAM[(0x30 + i + 1) % Data.VRAM.Length] >> (7 - px);
                            int x = b0 + 2 * b1;
                            if (bpp > 0) x += 4 * b2 + 8 * b3;
                            if (bpp > 1) x += 0x10 * b4 + 0x20 * b5 + 0x40 * b6 + 0x80 * b7;
                            int c = pal * (bpp == 0 ? 4 : 0x10);
                            for (int zy = 0; zy < zoom; zy++)
                            {
                                for (int zx = 0; zx < zoom; zx++)
                                {
                                    img.SetPixel((8 * tx + px) * zoom + zx, (8 * ty + py) * zoom + zy, Data.GetCGRAMColor(c + x));
                                }
                            }
                        }
                    }
                }
            }
            pictureBox1.Image = img;
            pictureBox1.Width = img.Width;
            pictureBox1.Height = img.Height;
        }

        private void updateCheckboxes()
        {
            palette00ToolStripMenuItem1.Checked = (pal == 0x00);
            palette01ToolStripMenuItem1.Checked = (pal == 0x01);
            palette02ToolStripMenuItem1.Checked = (pal == 0x02);
            palette03ToolStripMenuItem1.Checked = (pal == 0x03);
            palette04ToolStripMenuItem.Checked = (pal == 0x04);
            palette05ToolStripMenuItem.Checked = (pal == 0x05);
            palette06ToolStripMenuItem.Checked = (pal == 0x06);
            palette07ToolStripMenuItem.Checked = (pal == 0x07);
            palette08ToolStripMenuItem.Checked = (pal == 0x08);
            palette09ToolStripMenuItem.Checked = (pal == 0x09);
            palette0AToolStripMenuItem.Checked = (pal == 0x0A);
            palette0BToolStripMenuItem.Checked = (pal == 0x0B);
            palette0CToolStripMenuItem.Checked = (pal == 0x0C);
            palette0DToolStripMenuItem.Checked = (pal == 0x0D);
            palette0EToolStripMenuItem.Checked = (pal == 0x0E);
            palette0FToolStripMenuItem.Checked = (pal == 0x0F);
            palette10ToolStripMenuItem.Checked = (pal == 0x10);
            palette11ToolStripMenuItem.Checked = (pal == 0x11);
            palette12ToolStripMenuItem.Checked = (pal == 0x12);
            palette13ToolStripMenuItem.Checked = (pal == 0x13);
            palette14ToolStripMenuItem.Checked = (pal == 0x14);
            palette15ToolStripMenuItem.Checked = (pal == 0x15);
            palette16ToolStripMenuItem.Checked = (pal == 0x16);
            palette17ToolStripMenuItem.Checked = (pal == 0x17);
            palette18ToolStripMenuItem.Checked = (pal == 0x18);
            palette19ToolStripMenuItem.Checked = (pal == 0x19);
            palette1AToolStripMenuItem.Checked = (pal == 0x1A);
            palette1BToolStripMenuItem.Checked = (pal == 0x1B);
            palette1CToolStripMenuItem.Checked = (pal == 0x1C);
            palette1DToolStripMenuItem.Checked = (pal == 0x1D);
            palette1EToolStripMenuItem.Checked = (pal == 0x1E);
            palette1FToolStripMenuItem.Checked = (pal == 0x1F);
            bPPToolStripMenuItem.Checked = (bpp == 0);
            bPPToolStripMenuItem1.Checked = (bpp == 1);
            bPPToolStripMenuItem2.Checked = (bpp == 2);
            mode78BPPToolStripMenuItem.Checked = (bpp == 3);
            x1ToolStripMenuItem.Checked = (zoom == 1);
            toolStripMenuItem2.Checked = (zoom == 2);
            toolStripMenuItem3.Checked = (zoom == 3);
            toolStripMenuItem4.Checked = (zoom == 4);
            Redraw();
        }

        private void bPPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bpp = 0;
            updateCheckboxes();
            palette00ToolStripMenuItem.Enabled = true;
            palette01ToolStripMenuItem.Enabled = true;
            palette02ToolStripMenuItem.Enabled = true;
            palette03ToolStripMenuItem.Enabled = true;
        }

        private void bPPToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bpp = 1;
            pal &= 0x0F;
            updateCheckboxes();
            palette00ToolStripMenuItem.Enabled = true;
            palette01ToolStripMenuItem.Enabled = true;
            palette02ToolStripMenuItem.Enabled = false;
            palette03ToolStripMenuItem.Enabled = false;
        }

        private void bPPToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bpp = 2;
            pal = 0;
            updateCheckboxes();
            palette00ToolStripMenuItem.Enabled = false;
            palette01ToolStripMenuItem.Enabled = false;
            palette02ToolStripMenuItem.Enabled = false;
            palette03ToolStripMenuItem.Enabled = false;
        }

        private void mode78BPPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bpp = 3;
            pal = 0;
            updateCheckboxes();
            palette00ToolStripMenuItem.Enabled = false;
            palette01ToolStripMenuItem.Enabled = false;
            palette02ToolStripMenuItem.Enabled = false;
            palette03ToolStripMenuItem.Enabled = false;
        }

        private void palette00ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pal = 0x00;
            updateCheckboxes();
        }

        private void palette01ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pal = 0x01;
            updateCheckboxes();
        }

        private void palette02ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pal = 0x02;
            updateCheckboxes();
        }

        private void palette03ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pal = 0x03;
            updateCheckboxes();
        }

        private void palette04ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x04;
            updateCheckboxes();
        }

        private void palette05ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x05;
            updateCheckboxes();
        }

        private void palette06ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x06;
            updateCheckboxes();
        }

        private void palette07ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x07;
            updateCheckboxes();
        }

        private void palette08ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x08;
            updateCheckboxes();
        }

        private void palette09ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x09;
            updateCheckboxes();
        }

        private void palette0AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x0A;
            updateCheckboxes();
        }

        private void palette0BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x0B;
            updateCheckboxes();
        }

        private void palette0CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x0C;
            updateCheckboxes();
        }

        private void palette0DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x0D;
            updateCheckboxes();
        }

        private void palette0EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x0E;
            updateCheckboxes();
        }

        private void palette0FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x0F;
            updateCheckboxes();
        }

        private void palette10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x10;
            updateCheckboxes();
        }

        private void palette11ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x11;
            updateCheckboxes();
        }

        private void palette12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x12;
            updateCheckboxes();
        }

        private void palette13ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x13;
            updateCheckboxes();
        }

        private void palette14ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x14;
            updateCheckboxes();
        }

        private void palette15ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x15;
            updateCheckboxes();
        }

        private void palette16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x16;
            updateCheckboxes();
        }

        private void palette17ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x17;
            updateCheckboxes();
        }

        private void palette18ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x18;
            updateCheckboxes();
        }

        private void palette19ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x19;
            updateCheckboxes();
        }

        private void palette1AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x1A;
            updateCheckboxes();
        }

        private void palette1BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x1B;
            updateCheckboxes();
        }

        private void palette1CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x1C;
            updateCheckboxes();
        }

        private void palette1DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x1D;
            updateCheckboxes();
        }

        private void palette1EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x1E;
            updateCheckboxes();
        }

        private void palette1FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pal = 0x1F;
            updateCheckboxes();
        }

        private void x1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoom = 1;
            updateCheckboxes();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            zoom = 2;
            updateCheckboxes();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            zoom = 3;
            updateCheckboxes();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            zoom = 4;
            updateCheckboxes();
        }
    }
}
