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

        public CGRAMEditor()
        {
            InitializeComponent();
            Redraw();
            ResizeMe();
        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportData import = new ImportData("CGRAM", Data.CGRAM);
            DialogResult result = import.ShowDialog();
            if (result == DialogResult.OK) Redraw();
        }

        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportData export = new ExportData("CGRAM", Data.CGRAM);
            DialogResult result = export.ShowDialog();
            if (result == DialogResult.OK) Redraw();
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
                SetSize(new Size(672, 419));
                panel1.Height = 356;
            } else if (showHexEditor)
            {
                SetSize(new Size(672, 319));
                panel1.Height = 256;
            } else if ((showDetails >= 0))
            {
                SetSize(new Size(272, 419));
            } else
            {
                SetSize(new Size(272, 319));
            }
            panel1.Visible = showHexEditor;
            panel2.Visible = (showDetails >= 0);
        }

        public void Redraw()
        {
            Bitmap img = new Bitmap(256, 256);
            for (int cy = 0; cy < 16; cy++)
            {
                for (int cx = 0; cx < 16; cx++)
                {
                    for (int py = 0; py < 16; py++)
                    {
                        for (int px = 0; px < 16; px++)
                        {
                            img.SetPixel(16 * cx + px, 16 * cy + py, Data.GetCGRAMColor(cy * 0x10 + cx));
                        }
                    }
                }
            }
            pictureBox1.Image = img;
        }

        private void UpdateDetails()
        {
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
            if (showDetails >= 0)
            {
                UpdateDetails();
            }
            ResizeMe();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (showDetails >= 0)
            {
                showDetails = 0x10 * (e.Y / 16) + (e.X / 16);
                UpdateDetails();
            }
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
                Redraw();
            }
        }
    }
}
