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
    public partial class CGRAMEditor : Form
    {
        bool showHexEditor = false;
        int showDetails = 0;
        bool updatingDetails = false;

        public CGRAMEditor()
        {
            InitializeComponent();
            RedrawAll();
            ResizeMe();
            hexBox1.ByteProvider = new DynamicByteProvider(Data.GetCGRAMArray());
        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportData import = new ImportData("CGRAM", Data.GetCGRAMArray());
            DialogResult result = import.ShowDialog();
            if (result == DialogResult.OK)
            {
                SNESGraphics.UpdateAllPalettes();
                RedrawAll();
                UpdateDetails();
                UpdateHexEditor(0);
                RedrawOtherWindows();
            }
        }

        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportData export = new ExportData("CGRAM", Data.GetCGRAMArray());
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
            SetSize(new Size(showHexEditor ? 700 : 272, 419));
            hexBox1.Visible = showHexEditor;
        }

        public void RedrawAll()
        {
            for (int i = 0; i < 0x100; i++) Redraw(i);
            UpdateDetails();
        }

        public void Redraw(int color)
        {
            Bitmap curImg = (Bitmap)pictureBox1.Image;
            Bitmap img = curImg == null ? new Bitmap(256, 256) : curImg;
            int cx = color % 0x10, cy = color / 0x10;
            for (int py = 0; py < 16; py++)
            {
                for (int px = 0; px < 16; px++)
                {
                    img.SetPixel(16 * cx + px, 16 * cy + py, Data.GetCGRAMColor(cy * 0x10 + cx));
                }
            }
            pictureBox1.Image = img;

            for (int bpp = 0; bpp < 4; bpp++)
            {
                SNESGraphics.UpdateColor(bpp, showDetails);
            }
        }

        private void RedrawOtherWindows()
        {
            if (SuperTileMapper.oam != null && SuperTileMapper.oam.Visible) SuperTileMapper.oam.RedrawAll();
            if (SuperTileMapper.vram != null && SuperTileMapper.vram.Visible) SuperTileMapper.vram.RedrawAll();
            if (SuperTileMapper.tmap != null && SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
            if (SuperTileMapper.obj != null && SuperTileMapper.obj.Visible) SuperTileMapper.obj.RedrawAll();
        }

        private void UpdateDetails()
        {
            updatingDetails = true;
            label1.Text = "Color $" + Util.DecToHex(showDetails, 2);
            Color color = Data.GetCGRAMColor(showDetails);
            int val = Data.GetCGRAMWord(showDetails);
            textBox1.Text = "$" + Util.DecToHex(val, 4);
            textBox2.Text = "$" + Util.DecToHex(color.R >> 3, 2);
            textBox3.Text = "$" + Util.DecToHex(color.G >> 3, 2);
            textBox4.Text = "$" + Util.DecToHex(color.B >> 3, 2);
            trackBar3.Value = color.R >> 3;
            trackBar1.Value = color.G >> 3;
            trackBar2.Value = color.B >> 3;
            panel3.BackColor = color;
            updatingDetails = false;
        }

        private void UpdateHexEditor(int color)
        {
            hexBox1.ByteProvider = new DynamicByteProvider(Data.GetCGRAMArray());
            hexBox1.SelectionStart = 2 * color;
            hexBox1.SelectionLength = 2;
        }

        private void CGRAMEditor_Load(object sender, EventArgs e)
        {
        }

        private void hexEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showHexEditor = !showHexEditor;
            ResizeMe();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int color = 0x10 * (e.Y / 16) + (e.X / 16);
            if (showDetails >= 0)
            {
                showDetails = color;
                UpdateDetails();
            }
            UpdateHexEditor(color);
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Data.GetCGRAMColor(showDetails);
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Color c = colorDialog1.Color;
                int cg = ((c.B & 0xF8) << 7) | ((c.G & 0xF8) << 2) | ((c.R & 0xF8) >> 3);
                Data.SetCGRAMWord(showDetails, cg);
                UpdateDetails();
                Redraw(showDetails);
                RedrawOtherWindows();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int val;
            if (!updatingDetails && Util.TryHexOrDecToDec(textBox1.Text, out val))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Data.SetCGRAMWord(showDetails, val);
                    UpdateDetails();
                    Redraw(showDetails);
                    UpdateHexEditor(showDetails);
                    RedrawOtherWindows();
                } else if (e.KeyCode == Keys.Escape)
                {
                    textBox1_Leave(sender, e);
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            int val;
            if (!updatingDetails && Util.TryHexOrDecToDec(textBox2.Text, out val))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int emptyRed = Data.GetCGRAMWord(showDetails) & ~0x001F;
                    Data.SetCGRAMWord(showDetails, emptyRed | (val & 0x1F));
                    UpdateDetails();
                    Redraw(showDetails);
                    UpdateHexEditor(showDetails);
                    RedrawOtherWindows();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    textBox2_Leave(sender, e);
                }
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            int val;
            if (!updatingDetails && Util.TryHexOrDecToDec(textBox3.Text, out val))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int emptyGreen = Data.GetCGRAMWord(showDetails) & ~0x03E0;
                    Data.SetCGRAMWord(showDetails, emptyGreen | ((val & 0x1F) << 5));
                    UpdateDetails();
                    Redraw(showDetails);
                    UpdateHexEditor(showDetails);
                    RedrawOtherWindows();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    textBox3_Leave(sender, e);
                }
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            int val;
            if (!updatingDetails && Util.TryHexOrDecToDec(textBox4.Text, out val))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int emptyBlue = Data.GetCGRAMWord(showDetails) & ~0x7C00;
                    Data.SetCGRAMWord(showDetails, emptyBlue | ((val & 0x1F) << 10));
                    UpdateDetails();
                    Redraw(showDetails);
                    UpdateHexEditor(showDetails);
                    RedrawOtherWindows();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    textBox4_Leave(sender, e);
                }
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (!updatingDetails)
            {
                int emptyRed = Data.GetCGRAMWord(showDetails) & ~0x001F;
                Data.SetCGRAMWord(showDetails, emptyRed | (trackBar3.Value & 0x1F));
                UpdateDetails();
                Redraw(showDetails);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (!updatingDetails)
            {
                int emptyGreen = Data.GetCGRAMWord(showDetails) & ~0x03E0;
                Data.SetCGRAMWord(showDetails, emptyGreen | ((trackBar1.Value & 0x1F) << 5));
                UpdateDetails();
                Redraw(showDetails);
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (!updatingDetails)
            {
                int emptyBlue = Data.GetCGRAMWord(showDetails) & ~0x7C00;
                Data.SetCGRAMWord(showDetails, emptyBlue | ((trackBar2.Value & 0x1F) << 10));
                UpdateDetails();
                Redraw(showDetails);
            }
        }

        private void trackBar3_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateHexEditor(showDetails);
            RedrawOtherWindows();
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateHexEditor(showDetails);
            RedrawOtherWindows();
        }

        private void trackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateHexEditor(showDetails);
            RedrawOtherWindows();
        }

        private void hexBox1_KeyDown(object sender, KeyEventArgs e)
        {
            hexBox1.SelectionLength = 0;
        }

        private void hexBox1_CurrentPositionInLineChanged(object sender, EventArgs e)
        {
            int i = Util.Clamp((int)hexBox1.SelectionStart - 1, 0, Data.CGRAM_SIZE - 1);
            Data.SetCGRAMByte(i, hexBox1.ByteProvider.ReadByte(i));
            Redraw(i / 2);
            if (showDetails == i / 2)
            {
                UpdateDetails();
            }
            RedrawOtherWindows();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            updatingDetails = true;
            int oldVal = Data.GetCGRAMWord(showDetails);
            textBox1.Text = "$" + Util.DecToHex(oldVal, 4);
            updatingDetails = false;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            updatingDetails = true;
            int oldVal = Data.GetCGRAMWord(showDetails) & 0x001F;
            textBox2.Text = "$" + Util.DecToHex(oldVal, 2);
            updatingDetails = false;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            updatingDetails = true;
            int oldVal = (Data.GetCGRAMWord(showDetails) & 0x03E0) >> 5;
            textBox3.Text = "$" + Util.DecToHex(oldVal, 2);
            updatingDetails = false;
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            updatingDetails = true;
            int oldVal = (Data.GetCGRAMWord(showDetails) & 0x7C00) >> 10;
            textBox4.Text = "$" + Util.DecToHex(oldVal, 2);
            updatingDetails = false;
        }
    }
}
