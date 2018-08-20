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
    public partial class OAMEditor : Form
    {
        int[,,] sizes = new int[,,] {
            { {8,8}, {16,16} },
            { {8,8}, {32,32} },
            { {8,8}, {64,64} },
            { {16,16}, {32,32} },
            { {16,16}, {64,64} },
            { {32,32}, {64,64} },
            { {16,32}, {32,64} },
            { {16,32}, {32,32} }
        };
        int zoom = 1;
        public OAMEditor()
        {
            InitializeComponent();
            Redraw();
        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportData import = new ImportData("OAM", Data.OAM);
            DialogResult result = import.ShowDialog();
            if (result == DialogResult.OK) Redraw();
        }

        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportData export = new ExportData("OAM", Data.OAM);
            DialogResult result = export.ShowDialog();
            if (result == DialogResult.OK) Redraw();
        }

        public void Redraw()
        {
            int objsize = (Data.PPURegs[0x01] & 0xE0) >> 5;
            int voffset = (Data.PPURegs[0x01] & 0x03) * 0x4000;
            int vselect = ((Data.PPURegs[0x01] & 0x18) >> 3) * 0x100;
            int objw = sizes[objsize, 1, 0], objh = sizes[objsize, 1, 1];
            Bitmap img = new Bitmap(zoom * 8 * objw, zoom * 16 * objh);
            for (int by = 0; by < 16; by++)
            {
                for (int bx = 0; bx < 8; bx++)
                {
                    int bi = by * 8 + bx;
                    int bs = (Data.OAM[0x200 + bi / 4] >> (2 * (bi % 4) + 1)) & 0x01;
                    int bw = sizes[objsize, bs, 0], bh = sizes[objsize, bs, 1];
                    bool xflip = (Data.OAM[4 * bi + 3] & 0x40) != 0, yflip = (Data.OAM[4 * bi + 3] & 0x80) != 0;
                    for (int ty = 0; ty < bh/8; ty++)
                    {
                        for (int tx = 0; tx < bw/8; tx++)
                        {
                            for (int py = 0; py < 8; py++)
                            {
                                for (int px = 0; px < 8; px++)
                                {
                                    int tile = (Data.OAM[4 * bi + 2] | ((Data.OAM[4 * bi + 3] & 0x01) << 8)) + 0x10 * ty + tx;
                                    if (tile >= 0x100) tile += vselect;
                                    int i = voffset + 0x20 * tile + 2 * py;
                                    int b0 = 0x01 & Data.VRAM[(0x00 + i + 0) % Data.VRAM.Length] >> (7 - px);
                                    int b1 = 0x01 & Data.VRAM[(0x00 + i + 1) % Data.VRAM.Length] >> (7 - px);
                                    int b2 = 0x01 & Data.VRAM[(0x10 + i + 0) % Data.VRAM.Length] >> (7 - px);
                                    int b3 = 0x01 & Data.VRAM[(0x10 + i + 1) % Data.VRAM.Length] >> (7 - px);
                                    int x = b0 + 2 * b1 + 4 * b2 + 8 * b3;
                                    int c = 0x80 + 0x10 * ((Data.OAM[4 * bi + 3] & 0x0E) >> 1);
                                    for (int zy = 0; zy < zoom; zy++)
                                    {
                                        for (int zx = 0; zx < zoom; zx++)
                                        {
                                            img.SetPixel(
                                                (objw * bx + 8 * (xflip ? bw/8 - tx - 1 : tx) + (xflip ? 7-px : px)) * zoom + zx,
                                                (objh * by + 8 * (yflip ? bh/8 - ty - 1 : ty) + (yflip ? 7-py : py)) * zoom + zy,
                                                Data.GetCGRAMColor(c + x));
                                        }
                                    }
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

        private void OAMEditor_Load(object sender, EventArgs e)
        {

        }

        private void updateCheckboxes()
        {
            toolStripMenuItem2.Checked = (zoom == 1);
            toolStripMenuItem3.Checked = (zoom == 2);
            toolStripMenuItem4.Checked = (zoom == 3);
            toolStripMenuItem5.Checked = (zoom == 4);
            Redraw();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            zoom = 1;
            updateCheckboxes();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            zoom = 2;
            updateCheckboxes();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            zoom = 3;
            updateCheckboxes();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            zoom = 4;
            updateCheckboxes();
        }
    }
}
