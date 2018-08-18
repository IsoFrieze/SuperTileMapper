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
    public partial class SuperTileMapper : Form
    {
        bool changes = false;
        VRAMEditor vram;

        public SuperTileMapper()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void vRAMEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vram == null || vram.IsDisposed)
            {
                vram = new VRAMEditor();
                vram.Show();
            } else
            {
                //vram.Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (changes)
            {
                // save?
            } else
            {
                Application.Exit();
            }
        }
    }
}
