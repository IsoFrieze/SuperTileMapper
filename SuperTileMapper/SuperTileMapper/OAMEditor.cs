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

        private void Redraw()
        {
            // TODO
            Bitmap img = new Bitmap(256, 256);
            pictureBox1.Image = img;
        }
    }
}
