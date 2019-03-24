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
    public partial class OAMEditor : Form
    {
        bool showHexEditor = false;
        int showDetails = -1;
        bool updatingDetails = false;

        int screenZoom = 1;
        int transparency = 1;
        int displayArea = 0;

        public OAMEditor()
        {
            InitializeComponent();
            pictureBox2.Image = new Bitmap(64, 64);
            RedrawAll();
            ResizeMe();
            hexBox1.ByteProvider = new DynamicByteProvider(Data.OAM);
            UpdateScrollbars();
        }

        private void UpdateScrollbars()
        {
            bool extend = pictureBox3.Width > 512 || pictureBox3.Height > 512;
            if (!extend) panel3.AutoScroll = extend;
            panel3.HorizontalScroll.Visible = !extend;
            panel3.VerticalScroll.Visible = !extend;
            panel3.HorizontalScroll.Enabled = extend;
            panel3.VerticalScroll.Enabled = extend;
            if (extend) panel3.AutoScroll = extend;
        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportData import = new ImportData("OAM", Data.OAM);
            DialogResult result = import.ShowDialog();
            if (result == DialogResult.OK)
            {
                RedrawAll();
                if (showDetails >= 0) UpdateDetails();
                UpdateHexEditor(0);
                RedrawOtherWindows();
            }
        }

        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportData export = new ExportData("OAM", Data.OAM);
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
            oBJDetailsToolStripMenuItem.Checked = (showDetails >= 0);
            if (showHexEditor && (showDetails >= 0))
            {
                SetSize(new Size(975, 692));
                hexBox1.Height = 629;
            }
            else if (showHexEditor)
            {
                SetSize(new Size(975, 592));
                hexBox1.Height = 529;
            }
            else if ((showDetails >= 0))
            {
                SetSize(new Size(545, 692));
            }
            else
            {
                SetSize(new Size(545, 592));
            }
            hexBox1.Visible = showHexEditor;
            panel2.Visible = (showDetails >= 0);
        }

        public void RedrawAll()
        {
            if (showDetails >= 0) RedrawSelectedOBJ();
            RedrawScreen();
            UpdateScrollbars();
        }

        private void RedrawScreen()
        {
            if (pictureBox3.Image != null) pictureBox3.Image.Dispose();
            Bitmap img = new Bitmap(512 * screenZoom, 256 * screenZoom);

            using (Graphics g = Graphics.FromImage(img))
            {
                Color back = SNESGraphics.GetTransparency(0, transparency);
                g.FillRectangle(new SolidBrush(back), new Rectangle(0, 0, img.Width, img.Height));

                int lines = ((Data.PPURegs[0x33] & 0x4) != 0) ? 239 : 224;
                if (displayArea  == 1)
                {
                    g.DrawRectangle(new Pen(Color.Red), new Rectangle(256 * screenZoom, 1 * screenZoom, 256 * screenZoom - 1, lines * screenZoom - 1));
                } else if (displayArea == 2)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(0x80,0x80,0x80,0x80)), new Rectangle(256 * screenZoom, 1 * screenZoom, 256 * screenZoom, lines * screenZoom));
                }
            }

            int rotation = Data.PPURegs[0x02] & 0x7F;
            for (int i = 0x7F; i >= 0; i--)
            {
                int j = (i + rotation) & 0x7F;

                int objLow = Data.OAM[4 * j + 0];
                int objHigh = Data.OAM[4 * j + 1];
                int objBits = (Data.OAM[0x200 + j / 4] >> ((j % 4) * 2)) & 0x03;
                int y = objHigh;
                int x = ((objBits << 8) & 0x100) | objLow;

                DrawOBJ(j, img, x + 0x100, y, screenZoom);
                DrawOBJ(j, img, x - 0x100, y, screenZoom);
                DrawOBJ(j, img, x + 0x100, y - 0x100, screenZoom);
                DrawOBJ(j, img, x - 0x100, y - 0x100, screenZoom);
            }

            pictureBox3.Image = img;
            pictureBox3.Width = img.Width;
            pictureBox3.Height = img.Height;
        }

        private void RedrawSelectedOBJ()
        {
            pictureBox2.Image.Dispose();
            Bitmap img = new Bitmap(64, 64);

            int objBits = (Data.OAM[0x200 + showDetails / 4] >> ((showDetails % 4) * 2)) & 0x03;
            int maxSize = Util.OBJsizes[((Data.PPURegs[0x01] & 0xE0) >> 5), (objBits & 0x02) >> 1, 1];
            int zoom = 0x40 / maxSize;

            DrawOBJ(showDetails, img, 0, 0, zoom);

            pictureBox2.Image = img;
        }

        private void DrawOBJ(int i, Bitmap img, int x, int y, int zoom)
        {
            int objsize = (Data.PPURegs[0x01] & 0xE0) >> 5;
            int objLow = Data.OAM[4 * i + 2];
            int objHigh = Data.OAM[4 * i + 3];
            int objBits = (Data.OAM[0x200 + i / 4] >> ((i % 4) * 2)) & 0x03;
            int tile = ((objHigh << 8) & 0x100) | objLow;
            bool v = (objHigh & 0x80) != 0;
            bool h = (objHigh & 0x40) != 0;
            bool s = (objBits & 0x02) != 0;
            int c = (objHigh >> 1) & 0x7;

            DrawObject(tile, h, v, s, c, img, x, y, zoom);
        }

        private void DrawObject(int tile, bool h, bool v, bool s, int c, Bitmap img, int x, int y, int zoom)
        {
            int objsize = (Data.PPURegs[0x01] & 0xE0) >> 5;
            int nameBase = 0xE000 & ((Data.PPURegs[0x01] & 0x7) << 14);
            int nameOffset = 0x6000 & ((Data.PPURegs[0x01] & 0x18) << 10);
            int vram = 0xFFFF & (nameBase + (tile >= 0x100 ? nameOffset : 0) + tile * 0x20);
            int cgram = 0x80 + SNESGraphics.colorsPerPalette[1] * c;
            int bw = Util.OBJsizes[objsize, (s ? 1 : 0), 0] / 8, bh = Util.OBJsizes[objsize, (s ? 1 : 0), 1] / 8;

            SNESGraphics.DrawObject(vram, h, v, bw, bh, cgram, img, x, y, zoom);
        }

        private void RedrawOtherWindows()
        {
            if (SuperTileMapper.obj != null && SuperTileMapper.obj.Visible) SuperTileMapper.obj.RedrawAll();
        }

        private void UpdateDetails()
        {
            updatingDetails = true;
            
            label1.Text = "OBJ $" + Util.DecToHex(showDetails, 2);
            int word1 = (Data.OAM[4 * showDetails] | (Data.OAM[4 * showDetails + 1] << 8));
            int word2 = (Data.OAM[4 * showDetails + 2] | (Data.OAM[4 * showDetails + 3] << 8));
            int bits = (Data.OAM[0x200 + showDetails / 4] >> ((showDetails % 4) * 2)) & 0x03;
            textBox1.Text = "$" + Util.DecToHex(word2 & 0x01FF, 3);
            textBox2.Text = "$" + Util.DecToHex((word1 & 0x00FF) | ((bits & 0x01) << 8), 3);
            textBox3.Text = "$" + Util.DecToHex((word1 & 0xFF00) >> 8, 2);
            textBox4.Text = "$" + Util.DecToHex((word2 & 0x0E00) >> 9, 2);
            textBox5.Text = "$" + Util.DecToHex((word2 & 0x3000) >> 12, 2);
            checkBox1.Checked = (word2 & 0x4000) != 0;
            checkBox2.Checked = (word2 & 0x8000) != 0;
            checkBox3.Checked = (bits & 0x02) != 0;

            RedrawSelectedOBJ();

            updatingDetails = false;
        }

        private void UpdateHexEditor(int obj)
        {
            hexBox1.ByteProvider = new DynamicByteProvider(Data.OAM);
            hexBox1.SelectionStart = 4 * obj;
            hexBox1.SelectionLength = 4;
        }

        private void OAMEditor_Load(object sender, EventArgs e)
        {
        }

        private void updateCheckboxes()
        {
            screenZoom100.Checked = (screenZoom == 1);
            screenZoom200.Checked = (screenZoom == 2);
            screenZoom300.Checked = (screenZoom == 3);
            screenZoom400.Checked = (screenZoom == 4);
            transColor00.Checked = (transparency == 1);
            transBlack.Checked = (transparency == 2);
            transWhite.Checked = (transparency == 3);
            displayHidden.Checked = (displayArea == 0);
            displayOutline.Checked = (displayArea == 1);
            displayShaded.Checked = (displayArea == 2);
            RedrawAll();
        }

        private void screenZoom100_Click(object sender, EventArgs e)
        {
            screenZoom = 1;
            updateCheckboxes();
        }

        private void screenZoom200_Click(object sender, EventArgs e)
        {
            screenZoom = 2;
            updateCheckboxes();
        }

        private void screenZoom300_Click(object sender, EventArgs e)
        {
            screenZoom = 3;
            updateCheckboxes();
        }

        private void screenZoom400_Click(object sender, EventArgs e)
        {
            screenZoom = 4;
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

        private void displayHidden_Click(object sender, EventArgs e)
        {
            displayArea = 0;
            updateCheckboxes();
        }

        private void displayOutline_Click(object sender, EventArgs e)
        {
            displayArea = 1;
            updateCheckboxes();
        }

        private void displayShaded_Click(object sender, EventArgs e)
        {
            displayArea = 2;
            updateCheckboxes();
        }

        private void hexEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showHexEditor = !showHexEditor;
            ResizeMe();
        }

        private void oBJDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showDetails = showDetails >= 0 ? -1 : 0;
            if (showDetails >= 0) UpdateDetails();
            ResizeMe();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int val;
            if (!updatingDetails && Util.TryHexOrDecToDec(textBox1.Text, out val))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Data.OAM[4 * showDetails + 2] = (byte)(val & 0x00FF);
                    Data.OAM[4 * showDetails + 3] = (byte)((Data.OAM[4 * showDetails + 3] & ~0x01) | ((val & 0x0100) >> 8));
                    UpdateDetails();
                    RedrawAll();
                    UpdateHexEditor(showDetails);
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    textBox1_Leave(sender, e);
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            updatingDetails = true;
            int oldVal = (Data.OAM[4 * showDetails + 2] | ((Data.OAM[4 * showDetails + 3] & 0x01) << 8));
            textBox1.Text = "$" + Util.DecToHex(oldVal, 3);
            updatingDetails = false;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            int val;
            if (!updatingDetails && Util.TryHexOrDecToDec(textBox2.Text, out val))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Data.OAM[4 * showDetails] = (byte)(val & 0x00FF);
                    Data.OAM[0x200 + showDetails / 4] = (byte)((Data.OAM[0x200 + showDetails / 4] & ~(0x01 << (2*(showDetails%4))) | ((val & 0x0100) >> 8)));
                    UpdateDetails();
                    RedrawAll();
                    UpdateHexEditor(showDetails);
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    textBox2_Leave(sender, e);
                }
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            updatingDetails = true;
            int oldVal = (Data.OAM[4 * showDetails] | ((Data.OAM[0x200 + showDetails / 4] & (0x01 << (2 * (showDetails % 4)))) << 8));
            textBox2.Text = "$" + Util.DecToHex(oldVal, 3);
            updatingDetails = false;
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            int val;
            if (!updatingDetails && Util.TryHexOrDecToDec(textBox3.Text, out val))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Data.OAM[4 * showDetails + 1] = (byte)val;
                    UpdateDetails();
                    RedrawAll();
                    UpdateHexEditor(showDetails);
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    textBox3_Leave(sender, e);
                }
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            updatingDetails = true;
            int oldVal = Data.OAM[4 * showDetails + 1];
            textBox3.Text = "$" + Util.DecToHex(oldVal, 2);
            updatingDetails = false;
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            int val;
            if (!updatingDetails && Util.TryHexOrDecToDec(textBox4.Text, out val))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Data.OAM[4 * showDetails + 3] = (byte)((Data.OAM[4 * showDetails + 3] & ~0x0E) | ((val & 0x07) << 1));
                    UpdateDetails();
                    RedrawAll();
                    UpdateHexEditor(showDetails);
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    textBox4_Leave(sender, e);
                }
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            updatingDetails = true;
            int oldVal = (Data.OAM[4 * showDetails + 3] & 0x0E) >> 1;
            textBox4.Text = "$" + Util.DecToHex(oldVal, 2);
            updatingDetails = false;
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            int val;
            if (!updatingDetails && Util.TryHexOrDecToDec(textBox5.Text, out val))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Data.OAM[4 * showDetails + 3] = (byte)((Data.OAM[4 * showDetails + 3] & ~0x30) | ((val & 0x03) << 4));
                    UpdateDetails();
                    RedrawAll();
                    UpdateHexEditor(showDetails);
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    textBox5_Leave(sender, e);
                }
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            updatingDetails = true;
            int oldVal = (Data.OAM[4 * showDetails + 3] & 0x30) >> 4;
            textBox5.Text = "$" + Util.DecToHex(oldVal, 2);
            updatingDetails = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!updatingDetails)
            {
                Data.OAM[4 * showDetails + 3] = (byte)((Data.OAM[4 * showDetails + 3] & ~0x40) | (checkBox1.Checked ? 0x40 : 0));
                UpdateDetails();
                RedrawAll();
                UpdateHexEditor(showDetails);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!updatingDetails)
            {
                Data.OAM[4 * showDetails + 3] = (byte)((Data.OAM[4 * showDetails + 3] & ~0x80) | (checkBox2.Checked ? 0x80 : 0));
                UpdateDetails();
                RedrawAll();
                UpdateHexEditor(showDetails);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!updatingDetails)
            {
                Data.OAM[0x200 + showDetails / 4] = (byte)((Data.OAM[0x200 + showDetails / 4] & ~(0x02 << (2 * (showDetails % 4)))) | ((checkBox3.Checked ? 0x02 : 0) << (2 * (showDetails % 4))));
                UpdateDetails();
                RedrawAll();
                UpdateHexEditor(showDetails);
            }
        }

        private void hexBox1_CurrentPositionInLineChanged(object sender, EventArgs e)
        {
            int i = Util.clamp((int)hexBox1.SelectionStart - 1, 0, Data.OAM.Length - 1);
            Data.OAM[i] = hexBox1.ByteProvider.ReadByte(i);
            if (i < 0x200)
            {
                if (showDetails == i / 4) UpdateDetails();
            } else
            {
                for (int j = 0; j < 4; j++)
                {
                    int obj = (i - 0x200) * 4 + j;
                    if (showDetails == obj) UpdateDetails();
                }
            }
            RedrawAll();
            RedrawOtherWindows();
        }
    }
}
