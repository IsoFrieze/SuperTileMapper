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
        int showDetails = -1;
        bool updatingDetails = false;

        public CGRAMEditor()
        {
            InitializeComponent();
            RedrawAll();
            ResizeMe();
            hexBox1.ByteProvider = new DynamicByteProvider(Data.CGRAM);
        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportData import = new ImportData("CGRAM", Data.CGRAM);
            DialogResult result = import.ShowDialog();
            if (result == DialogResult.OK)
            {
                RedrawAll();
                if (showDetails >= 0) UpdateDetails();
                UpdateHexEditor(0);
                SuperTileMapper.oam.RedrawAll();
                SuperTileMapper.vram.RedrawAll();
            }
        }

        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportData export = new ExportData("CGRAM", Data.CGRAM);
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
            colorPropertiesToolStripMenuItem.Checked = (showDetails>=0);
            if (showHexEditor && (showDetails >= 0))
            {
                SetSize(new Size(700, 419));
                hexBox1.Height = 356;
            } else if (showHexEditor)
            {
                SetSize(new Size(700, 319));
                hexBox1.Height = 256;
            } else if ((showDetails >= 0))
            {
                SetSize(new Size(272, 419));
            } else
            {
                SetSize(new Size(272, 319));
            }
            hexBox1.Visible = showHexEditor;
            panel2.Visible = (showDetails >= 0);
        }

        public void RedrawAll()
        {
            for (int i = 0; i < 0x100; i++) Redraw(i);
            if (showDetails >= 0) UpdateDetails();
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
        }

        private void UpdateDetails()
        {
            updatingDetails = true;
            label1.Text = "Color $" + Util.DecToHex(showDetails, 2);
            int val = (Data.CGRAM[2 * showDetails] | (Data.CGRAM[2 * showDetails + 1] << 8));
            textBox1.Text = "$" + Util.DecToHex(val, 4);
            textBox2.Text = "$" + Util.DecToHex(val & 0x001F, 2);
            textBox3.Text = "$" + Util.DecToHex((val & 0x03E0) >> 5, 2);
            textBox4.Text = "$" + Util.DecToHex((val & 0x7C00) >> 10, 2);
            trackBar3.Value = val & 0x001F;
            trackBar1.Value = (val & 0x03E0) >> 5;
            trackBar2.Value = (val & 0x7C00) >> 10;
            panel3.BackColor = Data.GetCGRAMColor(showDetails);
            updatingDetails = false;
        }

        private void UpdateHexEditor(int color)
        {
            hexBox1.ByteProvider = new DynamicByteProvider(Data.CGRAM);
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

        private void colorPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showDetails = showDetails >= 0 ? -1 : 0;
            if (showDetails >= 0) UpdateDetails();
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
                Data.CGRAM[2 * showDetails] = (byte)(cg & 0x00FF);
                Data.CGRAM[2 * showDetails + 1] = (byte)((cg & 0x7F00) >> 8);
                UpdateDetails();
                Redraw(showDetails);
                SuperTileMapper.oam.RedrawAll();
                SuperTileMapper.vram.RedrawAll();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int val;
            if (!updatingDetails && Util.TryHexOrDecToDec(textBox1.Text, out val))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Data.CGRAM[2 * showDetails] = (byte)(val & 0x00FF);
                    Data.CGRAM[2 * showDetails + 1] = (byte)((val & 0x7F00) >> 8);
                    UpdateDetails();
                    Redraw(showDetails);
                    UpdateHexEditor(showDetails);
                    SuperTileMapper.oam.RedrawAll();
                    SuperTileMapper.vram.RedrawAll();
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
                    Data.CGRAM[2 * showDetails] = (byte)((Data.CGRAM[2 * showDetails] & 0xE0) | (val & 0x1F));
                    UpdateDetails();
                    Redraw(showDetails);
                    UpdateHexEditor(showDetails);
                    SuperTileMapper.oam.RedrawAll();
                    SuperTileMapper.vram.RedrawAll();
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
                    Data.CGRAM[2 * showDetails] = (byte)((Data.CGRAM[2 * showDetails] & 0x1F) | ((val & 0x07) << 5));
                    Data.CGRAM[2 * showDetails + 1] = (byte)((Data.CGRAM[2 * showDetails + 1] & 0xFC) | ((val & 0x18) >> 3));
                    UpdateDetails();
                    Redraw(showDetails);
                    UpdateHexEditor(showDetails);
                    SuperTileMapper.oam.RedrawAll();
                    SuperTileMapper.vram.RedrawAll();
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
                    Data.CGRAM[2 * showDetails + 1] = (byte)((Data.CGRAM[2 * showDetails + 1] & 0x03) | ((val & 0x1F) << 2));
                    UpdateDetails();
                    Redraw(showDetails);
                    UpdateHexEditor(showDetails);
                    SuperTileMapper.oam.RedrawAll();
                    SuperTileMapper.vram.RedrawAll();
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
                Data.CGRAM[2 * showDetails] = (byte)((Data.CGRAM[2 * showDetails] & 0xE0) | (trackBar3.Value & 0x1F));
                UpdateDetails();
                Redraw(showDetails);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (!updatingDetails)
            {
                Data.CGRAM[2 * showDetails] = (byte)((Data.CGRAM[2 * showDetails] & 0x1F) | ((trackBar1.Value & 0x07) << 5));
                Data.CGRAM[2 * showDetails + 1] = (byte)((Data.CGRAM[2 * showDetails + 1] & 0xFC) | ((trackBar1.Value & 0x18) >> 3));
                UpdateDetails();
                Redraw(showDetails);
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (!updatingDetails)
            {
                Data.CGRAM[2 * showDetails + 1] = (byte)((Data.CGRAM[2 * showDetails + 1] & 0x03) | ((trackBar2.Value & 0x1F) << 2));
                UpdateDetails();
                Redraw(showDetails);
            }
        }

        private void trackBar3_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateHexEditor(showDetails);
            SuperTileMapper.oam.RedrawAll();
            SuperTileMapper.vram.RedrawAll();
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateHexEditor(showDetails);
            SuperTileMapper.oam.RedrawAll();
            SuperTileMapper.vram.RedrawAll();
        }

        private void trackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateHexEditor(showDetails);
            SuperTileMapper.oam.RedrawAll();
            SuperTileMapper.vram.RedrawAll();
        }

        private void hexBox1_KeyDown(object sender, KeyEventArgs e)
        {
            hexBox1.SelectionLength = 0;
        }

        private void hexBox1_CurrentPositionInLineChanged(object sender, EventArgs e)
        {
            int i = Util.clamp((int)hexBox1.SelectionStart - 1, 0, Data.CGRAM.Length-1);
            Data.CGRAM[i] = hexBox1.ByteProvider.ReadByte(i);
            Redraw(i / 2);
            if (showDetails == i / 2)
            {
                UpdateDetails();
            }
            if (SuperTileMapper.oam != null) SuperTileMapper.oam.RedrawAll();
            if (SuperTileMapper.vram != null) SuperTileMapper.vram.RedrawAll();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            updatingDetails = true;
            int oldVal = (Data.CGRAM[2 * showDetails] | (Data.CGRAM[2 * showDetails + 1] << 8));
            textBox1.Text = "$" + Util.DecToHex(oldVal, 4);
            updatingDetails = false;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            updatingDetails = true;
            int oldVal = Data.CGRAM[2 * showDetails] & 0x1F;
            textBox2.Text = "$" + Util.DecToHex(oldVal, 2);
            updatingDetails = false;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            updatingDetails = true;
            int oldVal = (((Data.CGRAM[2 * showDetails] & 0xE0) >> 5) | ((Data.CGRAM[2 * showDetails + 1] & 0x03) << 3));
            textBox3.Text = "$" + Util.DecToHex(oldVal, 2);
            updatingDetails = false;
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            updatingDetails = true;
            int oldVal = (Data.CGRAM[2 * showDetails + 1] & 0x7C) >> 2;
            textBox4.Text = "$" + Util.DecToHex(oldVal, 2);
            updatingDetails = false;
        }
    }
}
