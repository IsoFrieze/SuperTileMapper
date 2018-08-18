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
            vram = new VRAMEditor();
            vram.Hide();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void vRAMEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vram.IsDisposed)
            {
                vram = new VRAMEditor();
            }
            vram.Show();
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

        private void SuperTileMapper_Resize(object sender, EventArgs e)
        {
            bool vis = (WindowState != FormWindowState.Minimized);
            if (!vram.IsDisposed && vis) vram.Show();
            if (!vram.IsDisposed && !vis) vram.Hide();
        }
    }
}
