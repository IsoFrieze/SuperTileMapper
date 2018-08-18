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
        public VRAMEditor()
        {
            InitializeComponent();
        }

        private void VRAMEditor_Load(object sender, EventArgs e)
        {

        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportData import = new ImportData("VRAM", Data.VRAM);
            import.Show();
        }

        private void exportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
