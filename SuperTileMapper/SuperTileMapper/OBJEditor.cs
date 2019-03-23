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
    public partial class OBJEditor : Form
    {
        bool updatingDetails = false;

        int pickerZoom = 1;
        int pickerTile = 0;
        bool pickerFlipH = false;
        bool pickerFlipV = false;
        bool pickerSize = false;
        int pickerPriority = 0;
        int pickerPalette = 0;

        int viewerZoom = 1;
        int viewerAcross = 0x20;

        public OBJEditor()
        {
            InitializeComponent();
            pictureSelTile.Image = new Bitmap(64, 64);
            UpdateDetails();
            RedrawAll();
            UpdateScrollbars();
        }

        private void UpdateScrollbars()
        {
            bool extendTilemap = pictureBox1.Width > 512 || pictureBox1.Height > 512;
            if (!extendTilemap) panel1.AutoScroll = extendTilemap;
            panel1.HorizontalScroll.Visible = !extendTilemap;
            panel1.VerticalScroll.Visible = !extendTilemap;
            panel1.HorizontalScroll.Enabled = extendTilemap;
            panel1.VerticalScroll.Enabled = extendTilemap;
            if (extendTilemap) panel1.AutoScroll = extendTilemap;

            bool extendPicker = pictureBox2.Width > 256 || pictureBox2.Height > 256;
            if (!extendPicker) panel2.AutoScroll = extendPicker;
            panel2.HorizontalScroll.Visible = !extendPicker;
            panel2.VerticalScroll.Visible = !extendPicker;
            panel2.HorizontalScroll.Enabled = extendPicker;
            panel2.VerticalScroll.Enabled = extendPicker;
            if (extendPicker) panel2.AutoScroll = extendPicker;
        }

        public void RedrawAll()
        {
            RedrawPicker();
            RedrawViewer();
            UpdateScrollbars();
        }

        private void RedrawPicker()
        {
            if (pictureBox2.Image != null) pictureBox2.Image.Dispose();

            Bitmap img = new Bitmap(pickerZoom * 8 * 0x10, pickerZoom * 8 * 0x20);

            for (int ty = 0; ty < 0x20; ty++)
            {
                for (int tx = 0; tx < 0x10; tx++)
                {
                    DrawTile(ty * 0x10 + tx, pickerFlipH, pickerFlipV, pickerPalette, img, 8 * tx, 8 * ty, pickerZoom, 0);
                }
            }

            pictureBox2.Image = img;
            pictureBox2.Width = img.Width;
            pictureBox2.Height = img.Height;
        }

        private void RedrawViewer()
        {
            int objsize = (Data.PPURegs[0x01] & 0xE0) >> 5;
            int objw = Util.OBJsizes[objsize, 1, 0], objh = Util.OBJsizes[objsize, 1, 1];

            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
            Bitmap img = new Bitmap(viewerZoom * objw * viewerAcross, viewerZoom * objh * 0x80 / viewerAcross);
            pictureBox1.Image = img;

            for (int i = 0; i < 0x80; i++) Redraw(i);

            pictureBox1.Width = img.Width;
            pictureBox1.Height = img.Height;
        }

        public void Redraw(int obj)
        {
            Bitmap img = (Bitmap)pictureBox1.Image;

            int objsize = (Data.PPURegs[0x01] & 0xE0) >> 5;
            int objw = Util.OBJsizes[objsize, 1, 0], objh = Util.OBJsizes[objsize, 1, 1];

            int objLow = Data.OAM[4 * obj + 2];
            int objHigh = Data.OAM[4 * obj + 3];
            int objBits = (Data.OAM[0x200 + obj / 4] >> (2 * (obj % 4))) & 0x3;

            int t = ((objHigh & 0x01) << 8) | objLow;
            bool v = (objHigh & 0x80) != 0;
            bool h = (objHigh & 0x40) != 0;
            int c = (objHigh & 0x0E) >> 1;
            bool s = (objBits & 0x02) != 0;

            DrawOBJ(t, h, v, s, c, img, objw * (obj % viewerAcross), objh * (obj / viewerAcross), viewerZoom);

            pictureBox1.Image = img;
        }

        private void DrawTile(int tile, bool h, bool v, int c, Bitmap img, int x, int y, int zoom, int t)
        {
            int nameBase = 0xE000 & ((Data.PPURegs[0x01] & 0x7) << 14);
            int nameOffset = 0x6000 & ((Data.PPURegs[0x01] & 0x18) << 10);
            int tileOffset = tile * 0x20;

            int vram = 0xFFFF & (nameBase + (tile >= 0x100 ? nameOffset : 0) + tileOffset);
            int cgram = 0x80 + 0x10 * c;
            Util.Draw8x8Tile(vram, 1, h, v, cgram, img, x, y, zoom, t);
        }

        private void DrawOBJ(int tile, bool h, bool v, bool s, int c, Bitmap img, int x, int y, int zoom)
        {
            int objsize = (Data.PPURegs[0x01] & 0xE0) >> 5;
            int nameBase = 0xE000 & ((Data.PPURegs[0x01] & 0x7) << 14);
            int nameOffset = 0x6000 & ((Data.PPURegs[0x01] & 0x18) << 10);

            int bw = Util.OBJsizes[objsize, (s ? 1 : 0), 0] / 8, bh = Util.OBJsizes[objsize, (s ? 1 : 0), 1] / 8;
            for (int ty = 0; ty < bh; ty++)
            {
                for (int tx = 0; tx < bh; tx++)
                {
                    if (ty < bh && tx < bw)
                    {
                        int tileX = (tile + tx) & 0x0F;
                        int tileY = (tile + 0x10 * ty) & 0xF0;
                        int tileP = tile & 0x100;
                        int newTile = tileP | tileY | tileX;

                        // TODO: Correctly Y-flip OBJ sizes 6 and 7
                        int vram = 0xFFFF & (nameBase + (newTile >= 0x100 ? nameOffset : 0) + newTile * 0x20);
                        int cgram = 0x80 + 0x10 * c;
                        Util.Draw8x8Tile(vram, 1, h, v, cgram, img, x + 8 * (h ? bw - tx - 1 : tx), y + 8 * (v ? bh - ty - 1 : ty), zoom, 0);
                    } else
                    {
                        for (int zy = 0; zy < zoom; zy++)
                        {
                            for (int zx = 0; zx < zoom; zx++)
                            {
                                for (int py = 0; py < 8; py++)
                                {
                                    for (int px = 0; px < 8; px++)
                                    {
                                        img.SetPixel((x + 8 * tx + px) * zoom + zx, (y + 8 * ty + py) * zoom + zy, Color.Transparent);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void RedrawSelectedTile()
        {
            Bitmap img = (Bitmap)pictureSelTile.Image;

            int objsize = (Data.PPURegs[0x01] & 0xE0) >> 5;
            int objh = Util.OBJsizes[objsize, (pickerSize ? 1 : 0), 1];
            int zoom = 0x40 / objh;

            DrawOBJ(pickerTile, pickerFlipH, pickerFlipV, pickerSize, pickerPalette, img, 0, 0, zoom);

            pictureSelTile.Image = img;
        }

        private void updateCheckboxes()
        {
            pickerZoom100.Checked = (pickerZoom == 1);
            pickerZoom200.Checked = (pickerZoom == 2);
            pickerZoom300.Checked = (pickerZoom == 3);
            pickerZoom400.Checked = (pickerZoom == 4);
            viewerZoom100.Checked = (viewerZoom == 1);
            viewerZoom200.Checked = (viewerZoom == 2);
            viewerZoom300.Checked = (viewerZoom == 3);
            viewerZoom400.Checked = (viewerZoom == 4);
            viewerWidth32.Checked = (viewerAcross == 0x20);
            viewerWidth16.Checked = (viewerAcross == 0x10);
            viewerWidth8.Checked = (viewerAcross == 0x08);
            viewerWidth4.Checked = (viewerAcross == 0x04);
            RedrawAll();
        }

        private void pickerZoom100_Click(object sender, EventArgs e)
        {
            pickerZoom = 1;
            updateCheckboxes();
        }

        private void pickerZoom200_Click(object sender, EventArgs e)
        {
            pickerZoom = 2;
            updateCheckboxes();
        }

        private void pickerZoom300_Click(object sender, EventArgs e)
        {
            pickerZoom = 3;
            updateCheckboxes();
        }

        private void pickerZoom400_Click(object sender, EventArgs e)
        {
            pickerZoom = 4;
            updateCheckboxes();
        }

        private void viewerZoom100_Click(object sender, EventArgs e)
        {
            viewerZoom = 1;
            updateCheckboxes();
        }

        private void viewerZoom200_Click(object sender, EventArgs e)
        {
            viewerZoom = 2;
            updateCheckboxes();
        }

        private void viewerZoom300_Click(object sender, EventArgs e)
        {
            viewerZoom = 3;
            updateCheckboxes();
        }

        private void viewerZoom400_Click(object sender, EventArgs e)
        {
            viewerZoom = 4;
            updateCheckboxes();
        }

        private void viewerWidth32_Click(object sender, EventArgs e)
        {
            viewerAcross = 0x20;
            updateCheckboxes();
        }

        private void viewerWidth16_Click(object sender, EventArgs e)
        {
            viewerAcross = 0x10;
            updateCheckboxes();
        }

        private void viewerWidth8_Click(object sender, EventArgs e)
        {
            viewerAcross = 0x08;
            updateCheckboxes();
        }

        private void viewerWidth4_Click(object sender, EventArgs e)
        {
            viewerAcross = 0x04;
            updateCheckboxes();
        }

        private void UpdateDetails()
        {
            updatingDetails = true;

            labelTileNo.Text = "Tile $" + Util.DecToHex(pickerTile, 3);
            textPalette.Text = "$" + Util.DecToHex(pickerPalette, 2);
            checkFlipH.Checked = pickerFlipH;
            checkFlipV.Checked = pickerFlipV;
            checkSize.Checked = pickerSize;
            textPriority.Text = "$" + Util.DecToHex(pickerPriority, 2);

            RedrawSelectedTile();

            updatingDetails = false;
        }

        private void checkFlipH_CheckedChanged(object sender, EventArgs e)
        {
            if (!updatingDetails)
            {
                pickerFlipH = checkFlipH.Checked;
                UpdateDetails();
                RedrawPicker();
            }
        }

        private void checkFlipV_CheckedChanged(object sender, EventArgs e)
        {
            if (!updatingDetails)
            {
                pickerFlipV = checkFlipV.Checked;
                UpdateDetails();
                RedrawPicker();
            }
        }

        private void checkSize_CheckedChanged(object sender, EventArgs e)
        {
            if (!updatingDetails)
            {
                pickerSize = checkSize.Checked;
                UpdateDetails();
                RedrawPicker();
            }
        }

        private void textPriority_TextChanged(object sender, EventArgs e)
        {
            int val;
            if (Util.TryHexOrDecToDec(textPriority.Text, out val))
            {
                if (val >= 0 && val < 8)
                {
                    pickerPriority = val;
                    RedrawSelectedTile();
                    RedrawPicker();
                }
            }
        }

        private void textPalette_TextChanged(object sender, EventArgs e)
        {
            int val;
            if (Util.TryHexOrDecToDec(textPalette.Text, out val))
            {
                if (val >= 0 && val < 8)
                {
                    pickerPalette = val;
                    RedrawSelectedTile();
                    RedrawPicker();
                }
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            int tx = e.X / (8 * pickerZoom);
            int ty = e.Y / (8 * pickerZoom);
            pickerTile = tx + 0x10 * ty;
            UpdateDetails();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int objsize = (Data.PPURegs[0x01] & 0xE0) >> 5;
            int objw = Util.OBJsizes[objsize, 1, 0], objh = Util.OBJsizes[objsize, 1, 1];

            int ox = e.X / (objw * viewerZoom);
            int oy = e.Y / (objh * viewerZoom);
            int objI = oy * viewerAcross + ox;

            int objLow = Data.OAM[4 * objI + 2];
            int objHigh = Data.OAM[4 * objI + 3];
            int objBits = (Data.OAM[0x200 + objI / 4] >> (2 * (objI % 4))) & 0x3;

            pickerTile = ((objHigh & 0x01) << 8) | objLow;
            pickerFlipV = (objHigh & 0x80) != 0;
            pickerFlipH = (objHigh & 0x40) != 0;
            pickerSize = (objBits & 0x02) != 0;
            pickerPriority = (objHigh & 0x30) >> 4;
            pickerPalette = (objHigh & 0x0E) >> 1;

            UpdateDetails();
        }
    }
}
