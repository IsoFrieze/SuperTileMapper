using Be.Windows.Forms;
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
        bool showHexEditor = false;
        int showDetails = -1;
        bool updatingDetails = false;

        int bpp = 0;
        int pal = 0;
        int zoom = 1;
        int across = 0x40;

        public VRAMEditor()
        {
            InitializeComponent();
            RedrawAll();
            ResizeMe();
            hexBox1.ByteProvider = new DynamicByteProvider(Data.VRAM);
            pictureBox2.Image = new Bitmap(64, 64);
        }

        private void VRAMEditor_Load(object sender, EventArgs e)
        {

        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportData import = new ImportData("VRAM", Data.VRAM);
            DialogResult result = import.ShowDialog();
            if (result == DialogResult.OK)
            {
                RedrawAll();
                if (showDetails >= 0) UpdateDetails();
                UpdateHexEditor(0);
            }
        }

        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportData export = new ExportData("VRAM", Data.VRAM);
            DialogResult result = export.ShowDialog();
        }

        private void SetSize(Size s)
        {
            this.MaximumSize = s;
            this.MinimumSize = s;
            this.Size = s;
        }
        public void ResizeMe()
        {
            hexEditorToolStripMenuItem.Checked = showHexEditor;
            tileDetailsToolStripMenuItem.Checked = (showDetails >= 0);
            if (showHexEditor && (showDetails >= 0))
            {
                SetSize(new Size(956, 675));
                hexBox1.Height = 612;
            }
            else if (showHexEditor)
            {
                SetSize(new Size(956, 575));
                hexBox1.Height = 512;
            }
            else if ((showDetails >= 0))
            {
                SetSize(new Size(528, 675));
            }
            else
            {
                SetSize(new Size(528, 575));
            }
            hexBox1.Visible = showHexEditor;
            panel2.Visible = (showDetails >= 0);
        }

        public void RedrawAll()
        {
            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();

            int tileCount = bpp == 0 ? 0x1000 : bpp == 1 ? 0x800 : bpp == 2 ? 0x400 : 0x100;

            Bitmap img = new Bitmap(8 * across * zoom, 8 * zoom * tileCount / across);
            pictureBox1.Image = img;

            for (int i = 0; i < tileCount; i++) Redraw(i);

            pictureBox1.Width = img.Width;
            pictureBox1.Height = img.Height;

            if (showDetails >= 0) UpdateDetails();
        }

        public void Redraw(int tile)
        {
            Bitmap img = (Bitmap)pictureBox1.Image;
            DrawTile(tile, img, 8 * (tile % across), 8 * (tile / across), zoom);
            pictureBox1.Image = img;
        }

        private void DrawTile(int tile, Bitmap img, int x, int y, int zoom)
        {
            int vram = ((0x10 * tile) * (bpp == 0 ? 1 : (bpp == 1 ? 2 : (bpp == 2 ? 4 : 8))));
            int cgram = pal * (bpp == 0 ? 4 : 0x10);
            Util.Draw8x8Tile(vram, bpp, false, false, cgram, img, x, y, zoom, 0);
        }

        private void UpdateDetails()
        {
            updatingDetails = true;
            label1.Text = "Tile $" + Util.DecToHex(showDetails, 3);

            Bitmap img = (Bitmap)pictureBox2.Image;
            DrawTile(showDetails, img, 0, 0, 8);
            pictureBox2.Image = img;

            updatingDetails = false;
        }

        private void UpdateHexEditor(int tile)
        {
            int b = bpp == 0 ? 0x10 : (bpp == 1 ? 0x20 : (bpp == 2 ? 0x40 : 0x80));
            hexBox1.ByteProvider = new DynamicByteProvider(Data.VRAM);
            hexBox1.SelectionStart = b * tile;
            hexBox1.SelectionLength = b;
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
            tilesToolStripMenuItem.Checked = (across == 0x40);
            tilesToolStripMenuItem1.Checked = (across == 0x20);
            tilesToolStripMenuItem2.Checked = (across == 0x10);
            RedrawAll();
            if (showDetails >= 0)
            {
                showDetails = Util.clamp(showDetails, 0, bpp == 0 ? 0x1000 : (bpp == 1 ? 0x800 : 0x400));
                UpdateDetails();
            }
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

        private void tilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            across = 0x40;
            updateCheckboxes();
        }

        private void tilesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            across = 0x20;
            updateCheckboxes();
        }

        private void tilesToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            across = 0x10;
            updateCheckboxes();
        }

        private void hexEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showHexEditor = !showHexEditor;
            ResizeMe();
        }

        private void tileDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showDetails = showDetails >= 0 ? -1 : 0;
            if (showDetails >= 0) UpdateDetails();
            ResizeMe();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int tx = e.X / (8 * zoom);
            int ty = e.Y / (8 * zoom);
            int tile = tx + across * ty;
            if (showDetails >= 0)
            {
                showDetails = tile;
                UpdateDetails();
            }
            UpdateHexEditor(tile);
        }

        private void hexBox1_CurrentPositionInLineChanged(object sender, EventArgs e)
        {
            int i = Util.clamp((int)hexBox1.SelectionStart - 1, 0, Data.VRAM.Length - 1);
            Data.VRAM[i] = hexBox1.ByteProvider.ReadByte(i);
            
            int b = bpp == 0 ? 0x10 : (bpp == 1 ? 0x20 : 0x40);
            Redraw(i / b);
            if (showDetails == i / b) UpdateDetails();

            // TODO: this can be reduced to only redraw when an oam-used tile is modified
            if (SuperTileMapper.oam != null) SuperTileMapper.oam.RedrawAll();
        }
    }
}
