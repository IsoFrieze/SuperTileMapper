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
            RedrawAll();
            ResizeMe();
            hexBox1.ByteProvider = new DynamicByteProvider(Data.OAM);
            pictureBox2.Image = new Bitmap(64, 64);
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
            int objsize = (Data.PPURegs[0x01] & 0xE0) >> 5;
            int objw = sizes[objsize, 1, 0], objh = sizes[objsize, 1, 1];

            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
            Bitmap img = new Bitmap(zoom * 8 * objw, zoom * 16 * objh);
            pictureBox1.Image = img;

            Console.WriteLine(pictureBox1.Image.Size);

            for (int i = 0; i < 0x80; i++) Redraw(i);

            pictureBox1.Width = img.Width;
            pictureBox1.Height = img.Height;

            if (showDetails >= 0) UpdateDetails();
        }

        public void Redraw(int obj)
        {
            Bitmap img = (Bitmap)pictureBox1.Image;

            int objsize = (Data.PPURegs[0x01] & 0xE0) >> 5;
            int objw = sizes[objsize, 1, 0], objh = sizes[objsize, 1, 1];
            int x = objw * (obj % 8), y = objh * (obj / 8);
            DrawOBJ(obj, img, x, y, zoom, objw, objh);

            pictureBox1.Image = img;
        }

        private void DrawOBJ(int obj, Bitmap img, int x, int y, int zoom, int sx, int sy)
        {
            for (int i = 0; i < sx; i++)
            {
                for (int j = 0; j < sy; j++)
                {
                    for (int zy = 0; zy < zoom; zy++)
                    {
                        for (int zx = 0; zx < zoom; zx++)
                        {
                            img.SetPixel(zoom * (x + i) + zx, zoom * (y + j) + zy, Color.Transparent);
                        }
                    }
                }
            }

            int objsize = (Data.PPURegs[0x01] & 0xE0) >> 5;
            int voffset = (Data.PPURegs[0x01] & 0x03) * 0x4000;
            int vselect = ((Data.PPURegs[0x01] & 0x18) >> 3) * 0x100;

            int bs = (Data.OAM[0x200 + obj / 4] >> (2 * (obj % 4) + 1)) & 0x01;
            int bw = sizes[objsize, bs, 0], bh = sizes[objsize, bs, 1];
            bool xflip = (Data.OAM[4 * obj + 3] & 0x40) != 0, yflip = (Data.OAM[4 * obj + 3] & 0x80) != 0;
            for (int ty = 0; ty < bh / 8; ty++)
            {
                for (int tx = 0; tx < bw / 8; tx++)
                {
                    for (int py = 0; py < 8; py++)
                    {
                        for (int px = 0; px < 8; px++)
                        {
                            int tile = (Data.OAM[4 * obj + 2] | ((Data.OAM[4 * obj + 3] & 0x01) << 8)) + 0x10 * ty + tx;
                            if (tile >= 0x100) tile += vselect;
                            int i = voffset + 0x20 * tile + 2 * py;
                            int b0 = 0x01 & Data.VRAM[(0x00 + i + 0) % Data.VRAM.Length] >> (7 - px);
                            int b1 = 0x01 & Data.VRAM[(0x00 + i + 1) % Data.VRAM.Length] >> (7 - px);
                            int b2 = 0x01 & Data.VRAM[(0x10 + i + 0) % Data.VRAM.Length] >> (7 - px);
                            int b3 = 0x01 & Data.VRAM[(0x10 + i + 1) % Data.VRAM.Length] >> (7 - px);
                            int xx = b0 + 2 * b1 + 4 * b2 + 8 * b3;
                            int c = 0x80 + 0x10 * ((Data.OAM[4 * obj + 3] & 0x0E) >> 1);
                            //TODO: correctly y-flip OBJ in size mode 6 & 7 (the undocumented sizes)
                            for (int zy = 0; zy < zoom; zy++)
                            {
                                for (int zx = 0; zx < zoom; zx++)
                                {
                                    img.SetPixel(
                                        (x + 8 * (xflip ? bw / 8 - tx - 1 : tx) + (xflip ? 7 - px : px)) * zoom + zx,
                                        (y + 8 * (yflip ? bh / 8 - ty - 1 : ty) + (yflip ? 7 - py : py)) * zoom + zy,
                                        Data.GetCGRAMColor(c + xx));
                                }
                            }
                        }
                    }
                }
            }
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
            
            Bitmap img = (Bitmap)pictureBox2.Image;
            int maxSize = sizes[((Data.PPURegs[0x01] & 0xE0) >> 5), (bits & 0x02) >> 1, 1];
            DrawOBJ(showDetails, img, 0, 0, img.Height / maxSize, maxSize, maxSize);
            pictureBox2.Image = img;

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
            toolStripMenuItem2.Checked = (zoom == 1);
            toolStripMenuItem3.Checked = (zoom == 2);
            toolStripMenuItem4.Checked = (zoom == 3);
            toolStripMenuItem5.Checked = (zoom == 4);
            RedrawAll();
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

        private void hexEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showHexEditor = !showHexEditor;
            ResizeMe();
        }

        private void oBJDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showDetails = showDetails >= 0 ? -1 : 0;
            if (showDetails >= 0)
            {
                UpdateDetails();
            }
            ResizeMe();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int ox = e.X / (pictureBox1.Width / 8);
            int oy = e.Y / (pictureBox1.Height / 16);
            int obj = ox + 8 * oy;
            if (showDetails >= 0)
            {
                showDetails = obj;
                UpdateDetails();
            }
            UpdateHexEditor(obj);
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
                    Redraw(showDetails);
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
                    Redraw(showDetails);
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
                    Redraw(showDetails);
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
                    Redraw(showDetails);
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
                    Redraw(showDetails);
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
                Redraw(showDetails);
                UpdateHexEditor(showDetails);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!updatingDetails)
            {
                Data.OAM[4 * showDetails + 3] = (byte)((Data.OAM[4 * showDetails + 3] & ~0x80) | (checkBox2.Checked ? 0x80 : 0));
                UpdateDetails();
                Redraw(showDetails);
                UpdateHexEditor(showDetails);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!updatingDetails)
            {
                Data.OAM[0x200 + showDetails / 4] = (byte)((Data.OAM[0x200 + showDetails / 4] & ~(0x02 << (2 * (showDetails % 4)))) | ((checkBox3.Checked ? 0x02 : 0) << (2 * (showDetails % 4))));
                UpdateDetails();
                Redraw(showDetails);
                UpdateHexEditor(showDetails);
            }
        }

        private void hexBox1_CurrentPositionInLineChanged(object sender, EventArgs e)
        {
            int i = Util.clamp((int)hexBox1.SelectionStart - 1, 0, Data.OAM.Length - 1);
            Data.OAM[i] = hexBox1.ByteProvider.ReadByte(i);
            if (i < 0x200)
            {
                Redraw(i / 4);
                if (showDetails == i / 4) UpdateDetails();
            } else
            {
                for (int j = 0; j < 4; j++)
                {
                    int obj = (i - 0x200) * 4 + j;
                    Redraw(obj);
                    if (showDetails == obj) UpdateDetails();
                }
            }
        }
    }
}
