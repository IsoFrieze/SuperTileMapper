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
        public static bool changes = false;
        public static VRAMEditor vram;
        public static CGRAMEditor cgram;
        public static OAMEditor oam;
        public static PPURegEditor2 ppu2;
        public static TilemapEditor tmap;
        public static OBJEditor obj;

        public SuperTileMapper()
        {
            InitializeComponent();
            
            try
            {
                string testdata = "C:\\Users\\Alex\\Documents\\Visual Studio 2017\\SuperTileMapper\\testdata\\smw\\";

                byte[] cgram = File.ReadAllBytes(testdata + "cgram.bin");
                byte[] vram = File.ReadAllBytes(testdata + "vram.bin");
                byte[] oam = File.ReadAllBytes(testdata + "oam.bin");
                byte[] ppu = File.ReadAllBytes(testdata + "ppuregs.bin");

                for (int i = 0; i < cgram.Length; i++)
                    Data.SetCGRAMByte(i, cgram[i]);
                for (int i = 0; i < vram.Length; i++)
                    Data.SetVRAMByte(i, vram[i]);
                for (int i = 0; i < oam.Length; i++)
                    Data.SetOAMByte(i, oam[i]);
                for (int i = 0; i < ppu.Length; i+=2)
                {
                    Data.SetPPURegBits(i/2, 0xFF, ppu[i]);
                    Data.SetPPURegBits(i/2, 0xFF00, ppu[i + 1] << 8);
                }
            }
            catch (Exception)
            {

            }
            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SNESGraphics.UpdateAll();

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

            //Data.PPURegs[0x00] = 0x0F;
            //Data.PPURegs[0x1B] = 0x100;
            //Data.PPURegs[0x1E] = 0x100;
            //Data.PPURegs[0x30] = 0x30;
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

        private bool continueUnsavedChanges()
        {
            return true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void revertUnsavedChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
