using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTileMapper
{
    public partial class SuperTileMapper : Form
    {
        public static VRAMEditor vram;
        public static CGRAMEditor cgram;
        public static OAMEditor oam;
        public static PPURegEditor2 ppu2;
        public static TilemapEditor tmap;
        public static OBJEditor obj;

        public SuperTileMapper()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tmap = new TilemapEditor();
            tmap.Close();
            obj = new OBJEditor();
            obj.Close();
            vram = new VRAMEditor();
            vram.Close();
            cgram = new CGRAMEditor();
            cgram.Close();
            oam = new OAMEditor();
            oam.Close();
            ppu2 = new PPURegEditor2();
            ppu2.Close();

            Project.NewProject();
            RedrawAllWindows();
        }

        private void vRAMEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vram.IsDisposed)
            {
                vram = new VRAMEditor();
            }
            vram.Show();
        }

        private void cGRAMEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cgram.IsDisposed)
            {
                cgram = new CGRAMEditor();
            }
            cgram.Show();
        }

        private void oAMEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (oam.IsDisposed)
            {
                oam = new OAMEditor();
            }
            oam.Show();
        }

        private void pPURegistersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ppu2.IsDisposed)
            {
                ppu2 = new PPURegEditor2();
            }
            ppu2.Show();
        }

        private void tilemapEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tmap.IsDisposed)
            {
                tmap = new TilemapEditor();
            }
            tmap.Show();
        }

        private void oBJEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (obj.IsDisposed)
            {
                obj = new OBJEditor();
            }
            obj.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ContinueUnsavedChanges())
            {
                Application.Exit();
            }
        }

        private void SuperTileMapper_Resize(object sender, EventArgs e)
        {
            bool vis = (WindowState != FormWindowState.Minimized);
            if (!vram.IsDisposed && vis) vram.Show();
            if (!vram.IsDisposed && !vis) vram.Hide();
            if (!cgram.IsDisposed && vis) cgram.Show();
            if (!cgram.IsDisposed && !vis) cgram.Hide();
            if (!oam.IsDisposed && vis) oam.Show();
            if (!oam.IsDisposed && !vis) oam.Hide();
            if (!ppu2.IsDisposed && vis) ppu2.Show();
            if (!ppu2.IsDisposed && !vis) ppu2.Hide();
            if (!tmap.IsDisposed && vis) tmap.Show();
            if (!tmap.IsDisposed && !vis) tmap.Hide();
            if (!obj.IsDisposed && vis) obj.Show();
            if (!obj.IsDisposed && !vis) obj.Hide();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void RedrawAllWindows()
        {
            SNESGraphics.UpdateAll();
            if (vram != null && vram.Visible) vram.RedrawAll();
            if (cgram != null && cgram.Visible) cgram.RedrawAll();
            if (oam != null && oam.Visible) oam.RedrawAll();
            if (ppu2 != null && ppu2.Visible) ppu2.RedrawAll();
            if (tmap != null && tmap.Visible) tmap.RedrawAll();
            if (obj != null && obj.Visible) obj.RedrawAll();
        }

        private bool ContinueUnsavedChanges()
        {
            if (Project.unsavedChanges)
            {
                DialogResult confirm = MessageBox.Show("You have unsaved changes. They will be lost if you continue.", "Unsaved Changes", MessageBoxButtons.OKCancel);
                return confirm == DialogResult.OK;
            }
            return true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ContinueUnsavedChanges())
            {
                Project.NewProject();
                saveProjectToolStripMenuItem.Enabled = false;
                RedrawAllWindows();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ContinueUnsavedChanges())
            {
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (Project.TryOpenProject(openFileDialog1.FileName))
                    {
                        saveProjectToolStripMenuItem.Enabled = true;
                        RedrawAllWindows();
                    }
                }
            }
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.SaveProject(Project.currentFile);
        }

        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                Project.SaveProject(saveFileDialog1.FileName);
                saveProjectToolStripMenuItem.Enabled = true;
            }
        }

        private void revertUnsavedChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ContinueUnsavedChanges())
            {
                if (Project.currentFile == null)
                {
                    Project.NewProject();
                } else
                {
                    Project.TryOpenProject(Project.currentFile);
                }
                RedrawAllWindows();
            }
        }
    }
}
