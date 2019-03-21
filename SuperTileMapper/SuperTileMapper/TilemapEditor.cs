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
        }

        private void RedrawPicker()
        {
            if (pictureBox2.Image != null) pictureBox2.Image.Dispose();
            Bitmap img = new Bitmap(pickerZoom * 8 * pickerAcross, pickerZoom * 8 * (0x400 / pickerAcross));
            for (int ty = 0; ty < 0x400 / pickerAcross; ty++)
            {
                for (int tx = 0; tx < pickerAcross; tx++)
                {
                    DrawTile(ty * pickerAcross + tx, img, 8 * tx, 8 * ty, pickerZoom);
                }
            }
            pictureBox2.Image = img;
            pictureBox2.Width = img.Width;
            pictureBox2.Height = img.Height;
        }

        private void RedrawTilemap()
        {
            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();

            // TODO actually draw the background
            Bitmap img = new Bitmap(tilemapZoom * 8 * 0x40, tilemapZoom * 8 * 0x40);

            pictureBox1.Image = img;
            pictureBox1.Width = img.Width;
            pictureBox1.Height = img.Height;
        }

        private void DrawTile(int tile, Bitmap img, int x, int y, int zoom)
        {
            int bg = bgOfInterest - 1;
            int bpp = BGBitDepths[Data.PPURegs[0x05] & 0x7, bg];
            if (bpp >= 0)
            {
                int nameBase = 0xE000 & (Data.PPURegs[0x0B + (bg / 2)] << (((bg & 1) == 0) ? 13 : 9));
                int tileOffset = tile * (bpp == 0 ? 0x10 : (bpp == 1 ? 0x20 : 0x40));

                int vram = nameBase + tileOffset;
                int cgram = (bpp > 1 ? 0 : pickerPalette) * (bpp == 0 ? 4 : 0x10);
                Util.Draw8x8Tile(vram, bpp, pickerFlipH, pickerFlipV, cgram, img, x, y, zoom);
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

        private void UpdateDetails()
        {
            updatingDetails = true;

            labelTileNo.Text = "Tile $" + Util.DecToHex(pickerTile, 3);
            textPalette.Text = "$" + Util.DecToHex(pickerPalette, 2);
            checkFlipH.Checked = pickerFlipH;
            checkFlipV.Checked = pickerFlipV;
            checkPriority.Checked = pickerPriority;

            Bitmap img = (Bitmap)pictureSelTile.Image;
            DrawTile(pickerTile, img, 0, 0, 8);
            pictureSelTile.Image = img;

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
                    UpdateDetails();
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
    }
}
