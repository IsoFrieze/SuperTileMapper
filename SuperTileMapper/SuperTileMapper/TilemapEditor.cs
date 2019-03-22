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
    public partial class TilemapEditor : Form
    {
        private int[,] BGBitDepths = new int[,] {
            {0,  0,  0,  0}, // mode 0
            {1,  1,  0, -1}, // mode 1
            {1,  1, -2, -1}, // mode 2
            {2,  1, -1, -1}, // mode 3
            {2,  0, -2, -1}, // mode 4
            {1,  0, -1, -1}, // mode 5
            {1, -1, -2, -1}, // mode 6
            {3, -1, -1, -1}, // mode 7
        };

        int bgOfInterest = 1;
        int tilemapZoom = 1;
        int transparency = 0;
        int priority = 0;

        int pickerZoom = 1;
        bool pickerFlipH = false;
        bool pickerFlipV = false;
        bool pickerPriority = false;
        int pickerPalette = 0;
        int pickerAcross = 0x20;

        int pickerTile = 0;
        bool updatingDetails = false;

        public TilemapEditor()
        {
            InitializeComponent();
            pictureSelTile.Image = new Bitmap(64, 64);
            UpdateDetails();
            RedrawAll();
        }

        private void TilemapEditor_Load(object sender, EventArgs e)
        {

        }

        public void RedrawAll()
        {
            RedrawPicker();
            RedrawTilemap();
            RedrawSelectedTile();
        }

        private void RedrawPicker()
        {
            if (pictureBox2.Image != null) pictureBox2.Image.Dispose();

            int mode = Data.PPURegs[0x05] & 0x7;
            int tileCount = mode == 7 ? 0x100 : 0x400;

            Bitmap img = new Bitmap(pickerZoom * 8 * pickerAcross, pickerZoom * 8 * (tileCount / pickerAcross));

            for (int ty = 0; ty < tileCount / pickerAcross; ty++)
            {
                for (int tx = 0; tx < pickerAcross; tx++)
                {
                    DrawTile(ty * pickerAcross + tx, pickerFlipH, pickerFlipV, pickerPalette, img, 8 * tx, 8 * ty, pickerZoom, 0);
                }
            }

            pictureBox2.Image = img;
            pictureBox2.Width = img.Width;
            pictureBox2.Height = img.Height;
        }

        private void RedrawTilemap()
        {
            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();

            int mode = Data.PPURegs[0x05] & 0x7;
            
            if (mode == 7)
            {
                Bitmap img = new Bitmap(tilemapZoom * 8 * 0x80, tilemapZoom * 8 * 0x80);

                for (int ty = 0; ty < 0x80; ty++)
                {
                    for (int tx = 0; tx < 0x80; tx++)
                    {
                        int tile = Data.VRAM[2 * (0x80 * ty + tx)];

                        DrawTile(tile, false, false, 0, img, 8 * tx, 8 * ty, tilemapZoom, 0);
                    }
                }

                pictureBox1.Image = img;
                pictureBox1.Width = img.Width;
                pictureBox1.Height = img.Height;
            } else
            {
                int bg = bgOfInterest - 1;
                int sizeX = 1 + (Data.PPURegs[0x07 + bg] & 0x01);
                int sizeY = 1 + ((Data.PPURegs[0x07 + bg] & 0x02) >> 1);
                int tilemapBase = ((Data.PPURegs[0x07 + bg] & 0xFC) << 8) & 0xFC00;
                int charSize = 1 + ((Data.PPURegs[0x05] >> (4 + bg)) & 1);
                int charX = mode == 6 ? 2 : charSize, charY = mode == 6 ? 1 : charSize;

                Bitmap img = new Bitmap(tilemapZoom * 8 * 0x20 * sizeX * charX, tilemapZoom * 8 * 0x20 * sizeY * charY);

                int screenNo = 0;
                for (int scy = 0; scy < sizeY; scy++)
                {
                    for (int scx = 0; scx < sizeX; scx++)
                    {
                        for (int ty = 0; ty < 0x20; ty++)
                        {
                            for (int tx = 0; tx < 0x20; tx++)
                            {
                                int tileI = screenNo * 0x20 * 0x20 + ty * 0x20 + tx;
                                int tileLow = Data.VRAM[0xFFFF & ((tilemapBase + tileI) * 2)];
                                int tileHigh = Data.VRAM[0xFFFF & ((tilemapBase + tileI) * 2 + 1)];

                                int tile = ((tileHigh & 0x3) << 8) | tileLow;
                                bool v = (tileHigh & 0x80) != 0;
                                bool h = (tileHigh & 0x40) != 0;
                                bool p = (tileHigh & 0x20) != 0;
                                int c = (tileHigh & 0x1C) >> 2;

                                if (p || priority != 2)
                                {
                                    for (int cy = 0; cy < charY; cy++)
                                    {
                                        for (int cx = 0; cx < charX; cx++)
                                        {
                                            int character = (tile + 0x10 * cy + cx) & 0x3FF;
                                            int x = (scx * 0x20 + tx) * charX + (charX == 2 && h ? 1 - cx : cx);
                                            int y = (scy * 0x20 + ty) * charY + (charY == 2 && v ? 1 - cy : cy);

                                            //TODO: implement priority focus
                                            DrawTile(character, h, v, c, img, 8 * x, 8 * y, tilemapZoom, transparency);
                                        }
                                    }
                                }
                            }
                        }
                        screenNo++;
                    }
                }

                pictureBox1.Image = img;
                pictureBox1.Width = img.Width;
                pictureBox1.Height = img.Height;
            }
        }

        private void RedrawSelectedTile()
        {
            int mode = Data.PPURegs[0x05] & 0x7;
            int charSize = 1 + ((Data.PPURegs[0x05] >> (4 + bgOfInterest - 1)) & 1);
            int charX = mode == 6 ? 2 : charSize, charY = mode == 6 ? 1 : charSize;

            Bitmap img = (Bitmap)pictureSelTile.Image;
            for (int cy = 0; cy < charY; cy++)
            {
                for (int cx = 0; cx < charX; cx++)
                {
                    int tile = (pickerTile + 0x10 * cy + cx) & 0x3FF;
                    DrawTile(tile, pickerFlipH, pickerFlipV, pickerPalette, img, 8 * (charX == 2 && pickerFlipH ? 1 - cx : cx), 8 * (charX == 2 && pickerFlipV ? 1 - cy : cy), 8 / charX, 0);
                }
            }
            pictureSelTile.Image = img;
        }

        private void DrawTile(int tile, bool h, bool v, int c, Bitmap img, int x, int y, int zoom, int t)
        {
            int bg = bgOfInterest - 1;
            int bpp = BGBitDepths[Data.PPURegs[0x05] & 0x7, bg];
            if (bpp >= 0)
            {
                if (bpp == 3)
                {
                    int vram = 0x80 * tile;
                    Util.Draw8x8Tile(vram, bpp, h, v, 0, img, x, y, zoom, t);
                } else
                {
                    int nameBase = 0xE000 & (Data.PPURegs[0x0B + (bg / 2)] << (((bg & 1) == 0) ? 13 : 9));
                    int tileOffset = tile * (bpp == 0 ? 0x10 : (bpp == 1 ? 0x20 : 0x40));

                    int vram = nameBase + tileOffset;
                    int cgram = (bpp > 1 ? 0 : c) * (bpp == 0 ? 4 : 0x10);
                    Util.Draw8x8Tile(vram, bpp, h, v, cgram, img, x, y, zoom, t);
                }
            }
        }

        private void updateCheckboxes()
        {
            layerBG1.Checked = (bgOfInterest == 1);
            layerBG2.Checked = (bgOfInterest == 2);
            layerBG3.Checked = (bgOfInterest == 3);
            layerBG4.Checked = (bgOfInterest == 4);
            tilemapZoom100.Checked = (tilemapZoom == 1);
            tilemapZoom200.Checked = (tilemapZoom == 2);
            tilemapZoom300.Checked = (tilemapZoom == 3);
            tilemapZoom400.Checked = (tilemapZoom == 4);
            pickerZoom100.Checked = (pickerZoom == 1);
            pickerZoom200.Checked = (pickerZoom == 2);
            pickerZoom300.Checked = (pickerZoom == 3);
            pickerZoom400.Checked = (pickerZoom == 4);
            pickerWidth32.Checked = (pickerAcross == 0x20);
            pickerWidth16.Checked = (pickerAcross == 0x10);
            transLocal0.Checked = (transparency == 0);
            transColor00.Checked = (transparency == 1);
            transBlack.Checked = (transparency == 2);
            transWhite.Checked = (transparency == 3);
            prioAll.Checked = (priority == 0);
            prioFocus.Checked = (priority == 1);
            prioOnly.Checked = (priority == 2);
            RedrawAll();
        }

        private void layerBG1_Click(object sender, EventArgs e)
        {
            bgOfInterest = 1;
            updateCheckboxes();
        }

        private void layerBG2_Click(object sender, EventArgs e)
        {
            bgOfInterest = 2;
            updateCheckboxes();
        }

        private void layerBG3_Click(object sender, EventArgs e)
        {
            bgOfInterest = 3;
            updateCheckboxes();
        }

        private void layerBG4_Click(object sender, EventArgs e)
        {
            bgOfInterest = 4;
            updateCheckboxes();
        }

        private void tilemapZoom100_Click(object sender, EventArgs e)
        {
            tilemapZoom = 1;
            updateCheckboxes();
        }

        private void tilemapZoom200_Click(object sender, EventArgs e)
        {
            tilemapZoom = 2;
            updateCheckboxes();
        }

        private void tilemapZoom300_Click(object sender, EventArgs e)
        {
            tilemapZoom = 3;
            updateCheckboxes();
        }

        private void tilemapZoom400_Click(object sender, EventArgs e)
        {
            tilemapZoom = 4;
            updateCheckboxes();
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

        private void pickerWidth32_Click(object sender, EventArgs e)
        {
            pickerAcross = 0x20;
            updateCheckboxes();
        }

        private void pickerWidth16_Click(object sender, EventArgs e)
        {
            pickerAcross = 0x10;
            updateCheckboxes();
        }

        private void transLocal0_Click(object sender, EventArgs e)
        {
            transparency = 0;
            updateCheckboxes();
        }

        private void transColor00_Click(object sender, EventArgs e)
        {
            transparency = 1;
            updateCheckboxes();
        }

        private void transBlack_Click(object sender, EventArgs e)
        {
            transparency = 2;
            updateCheckboxes();
        }

        private void transWhite_Click(object sender, EventArgs e)
        {
            transparency = 3;
            updateCheckboxes();
        }

        private void prioAll_Click(object sender, EventArgs e)
        {
            priority = 0;
            updateCheckboxes();
        }

        private void prioFocus_Click(object sender, EventArgs e)
        {
            priority = 1;
            updateCheckboxes();
        }

        private void prioOnly_Click(object sender, EventArgs e)
        {
            priority = 2;
            updateCheckboxes();
        }

        private void UpdateDetails()
        {
            updatingDetails = true;

            labelTileNo.Text = "Tile $" + Util.DecToHex(pickerTile, 3);
            textPalette.Text = "$" + Util.DecToHex(pickerPalette, 2);
            checkFlipH.Checked = pickerFlipH;
            checkFlipV.Checked = pickerFlipV;
            checkPriority.Checked = pickerPriority;

            int mode = Data.PPURegs[0x05] & 0x7;
            textPalette.Enabled = (mode != 7);
            checkFlipH.Enabled = (mode != 7);
            checkFlipV.Enabled = (mode != 7);
            checkPriority.Enabled = (mode != 7);

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

        private void checkPriority_CheckedChanged(object sender, EventArgs e)
        {
            if (!updatingDetails)
            {
                pickerPriority = checkPriority.Checked;
                UpdateDetails();
                RedrawPicker();
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
            pickerTile = tx + pickerAcross * ty;
            UpdateDetails();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int bg = bgOfInterest - 1;
            int mode = Data.PPURegs[0x05] & 0x7;

            if (mode == 7)
            {
                int tx = e.X / (8 * tilemapZoom);
                int ty = e.Y / (8 * tilemapZoom);
                int tileI = ty * 0x80 + tx;

                int tile = Data.VRAM[tileI * 2];

                pickerTile = tile;
                pickerFlipV = false;
                pickerFlipH = false;
                pickerPriority = false;
                pickerPalette = 0;
            } else
            {
                int sizeX = 1 + (Data.PPURegs[0x07 + bg] & 0x01);
                int sizeY = 1 + ((Data.PPURegs[0x07 + bg] & 0x02) >> 1);
                int tilemapBase = ((Data.PPURegs[0x07 + bg] & 0xFC) << 8) & 0xFC00;
                int charSize = 1 + ((Data.PPURegs[0x05] >> (4 + bg)) & 1);
                int charX = mode == 6 ? 2 : charSize, charY = mode == 6 ? 1 : charSize;

                int rx = e.X / (8 * tilemapZoom * charX);
                int ry = e.Y / (8 * tilemapZoom * charY);
                int sx = rx / 0x20, sy = ry / 0x20;
                int tx = rx % 0x20, ty = ry % 0x20;
                int scNo = sy * sizeX + sx;
                int tileI = scNo * 0x20 * 0x20 + ty * 0x20 + tx;

                int tileLow = Data.VRAM[0xFFFF & ((tilemapBase + tileI) * 2)];
                int tileHigh = Data.VRAM[0xFFFF & ((tilemapBase + tileI) * 2 + 1)];

                pickerTile = ((tileHigh & 0x3) << 8) | tileLow;
                pickerFlipV = (tileHigh & 0x80) != 0;
                pickerFlipH = (tileHigh & 0x40) != 0;
                pickerPriority = (tileHigh & 0x20) != 0;
                pickerPalette = (tileHigh & 0x1C) >> 2;
            }
            UpdateDetails();
        }
    }
}
