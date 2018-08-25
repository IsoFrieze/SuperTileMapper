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
    public partial class PPURegEditor : Form
    {
        public PPURegEditor()
        {
            InitializeComponent();
            Redraw();
        }

        private void PPURegEditor_Load(object sender, EventArgs e)
        {

        }

        private void Redraw()
        {
            reg2100.Text = "$" + Util.DecToHex(Data.PPURegs[0x00], 2);
            blanking.Checked = (Data.PPURegs[0x00] & 0x80) != 0x00;
            brightness.Text = (Data.PPURegs[0x00] & 0x0F).ToString();

            reg2101.Text = "$" + Util.DecToHex(Data.PPURegs[0x01], 2);
            objsize.Text = ((Data.PPURegs[0x01] & 0xE0) >> 5).ToString();
            objselect.Text = ((Data.PPURegs[0x01] & 0x18) >> 3).ToString();
            objbase.Text = (Data.PPURegs[0x01] & 0x07).ToString();

            reg2102.Text = "$" + Util.DecToHex(Data.PPURegs[0x02], 2);
            objpriority.Text = ((Data.PPURegs[0x02] & 0xFE) >> 1).ToString();

            reg2105.Text = "$" + Util.DecToHex(Data.PPURegs[0x05], 2);
            bgmode.Text = (Data.PPURegs[0x05] & 0x07).ToString();
            bg3priority.Checked = (Data.PPURegs[0x05] & 0x08) != 0x00;
            bg1chrsize.Checked = (Data.PPURegs[0x05] & 0x10) != 0x00;
            bg2chrsize.Checked = (Data.PPURegs[0x05] & 0x20) != 0x00;
            bg3chrsize.Checked = (Data.PPURegs[0x05] & 0x40) != 0x00;
            bg4chrsize.Checked = (Data.PPURegs[0x05] & 0x80) != 0x00;

            reg2106.Text = "$" + Util.DecToHex(Data.PPURegs[0x06], 2);
            mosaicsize.Text = ((Data.PPURegs[0x06] & 0xF0) >> 4).ToString();
            bg1mosaic.Checked = (Data.PPURegs[0x06] & 0x01) != 0x00;
            bg2mosaic.Checked = (Data.PPURegs[0x06] & 0x02) != 0x00;
            bg3mosaic.Checked = (Data.PPURegs[0x06] & 0x04) != 0x00;
            bg4mosaic.Checked = (Data.PPURegs[0x06] & 0x08) != 0x00;

            reg2107.Text = "$" + Util.DecToHex(Data.PPURegs[0x07], 2);
            bg1scbase.Text = ((Data.PPURegs[0x07] & 0xFC) >> 2).ToString();
            bg1scsize.Text = (Data.PPURegs[0x07] & 0x03).ToString();

            reg2108.Text = "$" + Util.DecToHex(Data.PPURegs[0x08], 2);
            bg2scbase.Text = ((Data.PPURegs[0x08] & 0xFC) >> 2).ToString();
            bg2scsize.Text = (Data.PPURegs[0x08] & 0x03).ToString();

            reg2109.Text = "$" + Util.DecToHex(Data.PPURegs[0x09], 2);
            bg3scbase.Text = ((Data.PPURegs[0x09] & 0xFC) >> 2).ToString();
            bg3scsize.Text = (Data.PPURegs[0x09] & 0x03).ToString();

            reg210A.Text = "$" + Util.DecToHex(Data.PPURegs[0x0A], 2);
            bg4scbase.Text = ((Data.PPURegs[0x0A] & 0xFC) >> 2).ToString();
            bg4scsize.Text = (Data.PPURegs[0x0A] & 0x03).ToString();

            reg210B.Text = "$" + Util.DecToHex(Data.PPURegs[0x0B], 2);
            bg1chrbase.Text = (Data.PPURegs[0x0B] & 0x0F).ToString();
            bg2chrbase.Text = ((Data.PPURegs[0x0B] & 0xF0) >> 4).ToString();

            reg210C.Text = "$" + Util.DecToHex(Data.PPURegs[0x0C], 2);
            bg3chrbase.Text = (Data.PPURegs[0x0C] & 0x0F).ToString();
            bg4chrbase.Text = ((Data.PPURegs[0x0C] & 0xF0) >> 4).ToString();

            reg210D.Text = "$" + Util.DecToHex(Data.PPURegs[0x0D], 4);
            bg1hofs.Text = (Data.PPURegs[0x0D] & 0x1FFF).ToString();

            reg210E.Text = "$" + Util.DecToHex(Data.PPURegs[0x0E], 4);
            bg1vofs.Text = (Data.PPURegs[0x0E] & 0x1FFF).ToString();

            reg210F.Text = "$" + Util.DecToHex(Data.PPURegs[0x0F], 4);
            bg2hofs.Text = (Data.PPURegs[0x0F] & 0x03FF).ToString();

            reg2110.Text = "$" + Util.DecToHex(Data.PPURegs[0x10], 4);
            bg2vofs.Text = (Data.PPURegs[0x10] & 0x03FF).ToString();

            reg2111.Text = "$" + Util.DecToHex(Data.PPURegs[0x11], 4);
            bg3hofs.Text = (Data.PPURegs[0x11] & 0x03FF).ToString();

            reg2112.Text = "$" + Util.DecToHex(Data.PPURegs[0x12], 4);
            bg3vofs.Text = (Data.PPURegs[0x12] & 0x03FF).ToString();

            reg2113.Text = "$" + Util.DecToHex(Data.PPURegs[0x13], 4);
            bg4hofs.Text = (Data.PPURegs[0x13] & 0x03FF).ToString();

            reg2114.Text = "$" + Util.DecToHex(Data.PPURegs[0x14], 4);
            bg4vofs.Text = (Data.PPURegs[0x14] & 0x03FF).ToString();

            reg211A.Text = "$" + Util.DecToHex(Data.PPURegs[0x1A], 2);
            m7screenover.Text = ((Data.PPURegs[0x1A] & 0xC0) >> 6).ToString();
            m7vflip.Checked = (Data.PPURegs[0x1A] & 0x02) != 0x00;
            m7hflip.Checked = (Data.PPURegs[0x1A] & 0x01) != 0x00;

            reg211B.Text = "$" + Util.DecToHex(Data.PPURegs[0x1B], 4);
            m7a.Text = (Data.PPURegs[0x1B] & 0xFFFF).ToString();

            reg211C.Text = "$" + Util.DecToHex(Data.PPURegs[0x1C], 4);
            m7b.Text = (Data.PPURegs[0x1C] & 0xFFFF).ToString();

            reg211D.Text = "$" + Util.DecToHex(Data.PPURegs[0x1D], 4);
            m7c.Text = (Data.PPURegs[0x1D] & 0xFFFF).ToString();

            reg211E.Text = "$" + Util.DecToHex(Data.PPURegs[0x1E], 4);
            m7d.Text = (Data.PPURegs[0x1E] & 0xFFFF).ToString();

            reg211F.Text = "$" + Util.DecToHex(Data.PPURegs[0x1F], 4);
            m7x.Text = (Data.PPURegs[0x1F] & 0xFFFF).ToString();

            reg2120.Text = "$" + Util.DecToHex(Data.PPURegs[0x20], 4);
            m7y.Text = (Data.PPURegs[0x20] & 0xFFFF).ToString();

            reg2123.Text = "$" + Util.DecToHex(Data.PPURegs[0x23], 2);
            bg2w2en.Checked = (Data.PPURegs[0x23] & 0x80) != 0x00;
            bg2w2io.Checked = (Data.PPURegs[0x23] & 0x40) != 0x00;
            bg2w1en.Checked = (Data.PPURegs[0x23] & 0x20) != 0x00;
            bg2w1io.Checked = (Data.PPURegs[0x23] & 0x10) != 0x00;
            bg1w2en.Checked = (Data.PPURegs[0x23] & 0x08) != 0x00;
            bg1w2io.Checked = (Data.PPURegs[0x23] & 0x04) != 0x00;
            bg1w1en.Checked = (Data.PPURegs[0x23] & 0x02) != 0x00;
            bg1w1io.Checked = (Data.PPURegs[0x23] & 0x01) != 0x00;

            reg2124.Text = "$" + Util.DecToHex(Data.PPURegs[0x24], 2);
            bg4w2en.Checked = (Data.PPURegs[0x24] & 0x80) != 0x00;
            bg4w2io.Checked = (Data.PPURegs[0x24] & 0x40) != 0x00;
            bg4w1en.Checked = (Data.PPURegs[0x24] & 0x20) != 0x00;
            bg4w1io.Checked = (Data.PPURegs[0x24] & 0x10) != 0x00;
            bg3w2en.Checked = (Data.PPURegs[0x24] & 0x08) != 0x00;
            bg3w2io.Checked = (Data.PPURegs[0x24] & 0x04) != 0x00;
            bg3w1en.Checked = (Data.PPURegs[0x24] & 0x02) != 0x00;
            bg3w1io.Checked = (Data.PPURegs[0x24] & 0x01) != 0x00;

            reg2125.Text = "$" + Util.DecToHex(Data.PPURegs[0x25], 2);
            colorw2en.Checked = (Data.PPURegs[0x25] & 0x80) != 0x00;
            colorw2io.Checked = (Data.PPURegs[0x25] & 0x40) != 0x00;
            colorw1en.Checked = (Data.PPURegs[0x25] & 0x20) != 0x00;
            colorw1io.Checked = (Data.PPURegs[0x25] & 0x10) != 0x00;
            objw2en.Checked = (Data.PPURegs[0x25] & 0x08) != 0x00;
            objw2io.Checked = (Data.PPURegs[0x25] & 0x04) != 0x00;
            objw1en.Checked = (Data.PPURegs[0x25] & 0x02) != 0x00;
            objw1io.Checked = (Data.PPURegs[0x25] & 0x01) != 0x00;

            reg2126.Text = "$" + Util.DecToHex(Data.PPURegs[0x26], 2);
            w1left.Text = (Data.PPURegs[0x26] & 0xFF).ToString();

            reg2127.Text = "$" + Util.DecToHex(Data.PPURegs[0x27], 2);
            w1right.Text = (Data.PPURegs[0x26] & 0xFF).ToString();

            reg2128.Text = "$" + Util.DecToHex(Data.PPURegs[0x28], 2);
            w2left.Text = (Data.PPURegs[0x26] & 0xFF).ToString();

            reg2129.Text = "$" + Util.DecToHex(Data.PPURegs[0x29], 2);
            w2right.Text = (Data.PPURegs[0x26] & 0xFF).ToString();

            reg212A.Text = "$" + Util.DecToHex(Data.PPURegs[0x2A], 2);
            bg1winlog.Text = (Data.PPURegs[0x2A] & 0x03).ToString();
            bg2winlog.Text = ((Data.PPURegs[0x2A] & 0x0C) >> 2).ToString();
            bg3winlog.Text = ((Data.PPURegs[0x2A] & 0x30) >> 4).ToString();
            bg4winlog.Text = ((Data.PPURegs[0x2A] & 0xC0) >> 6).ToString();

            reg212B.Text = "$" + Util.DecToHex(Data.PPURegs[0x2B], 2);
            objwinlog.Text = (Data.PPURegs[0x2B] & 0x03).ToString();
            colorwinlog.Text = ((Data.PPURegs[0x2B] & 0x0C) >> 2).ToString();

            reg212C.Text = "$" + Util.DecToHex(Data.PPURegs[0x2C], 2);
            objtm.Checked = (Data.PPURegs[0x2C] & 0x10) != 0x00;
            bg4tm.Checked = (Data.PPURegs[0x2C] & 0x08) != 0x00;
            bg3tm.Checked = (Data.PPURegs[0x2C] & 0x04) != 0x00;
            bg2tm.Checked = (Data.PPURegs[0x2C] & 0x02) != 0x00;
            bg1tm.Checked = (Data.PPURegs[0x2C] & 0x01) != 0x00;

            reg212D.Text = "$" + Util.DecToHex(Data.PPURegs[0x2D], 2);
            objts.Checked = (Data.PPURegs[0x2D] & 0x10) != 0x00;
            bg4ts.Checked = (Data.PPURegs[0x2D] & 0x08) != 0x00;
            bg3ts.Checked = (Data.PPURegs[0x2D] & 0x04) != 0x00;
            bg2ts.Checked = (Data.PPURegs[0x2D] & 0x02) != 0x00;
            bg1ts.Checked = (Data.PPURegs[0x2D] & 0x01) != 0x00;

            reg212E.Text = "$" + Util.DecToHex(Data.PPURegs[0x2E], 2);
            objtmw.Checked = (Data.PPURegs[0x2E] & 0x10) != 0x00;
            bg4tmw.Checked = (Data.PPURegs[0x2E] & 0x08) != 0x00;
            bg3tmw.Checked = (Data.PPURegs[0x2E] & 0x04) != 0x00;
            bg2tmw.Checked = (Data.PPURegs[0x2E] & 0x02) != 0x00;
            bg1tmw.Checked = (Data.PPURegs[0x2E] & 0x01) != 0x00;

            reg212F.Text = "$" + Util.DecToHex(Data.PPURegs[0x2F], 2);
            objtsw.Checked = (Data.PPURegs[0x2F] & 0x10) != 0x00;
            bg4tsw.Checked = (Data.PPURegs[0x2F] & 0x08) != 0x00;
            bg3tsw.Checked = (Data.PPURegs[0x2F] & 0x04) != 0x00;
            bg2tsw.Checked = (Data.PPURegs[0x2F] & 0x02) != 0x00;
            bg1tsw.Checked = (Data.PPURegs[0x2F] & 0x01) != 0x00;

            reg2130.Text = "$" + Util.DecToHex(Data.PPURegs[0x30], 2);
            colorwinmain.Text = ((Data.PPURegs[0x30] & 0xC0) >> 6).ToString();
            colorwinsub.Text = ((Data.PPURegs[0x30] & 0x30) >> 4).ToString();
            addsubfixed.Checked = (Data.PPURegs[0x30] & 0x02) != 0x00;
            directcolor.Checked = (Data.PPURegs[0x30] & 0x01) != 0x00;

            reg2131.Text = "$" + Util.DecToHex(Data.PPURegs[0x31], 2);
            addsubsel.Checked = (Data.PPURegs[0x31] & 0x80) != 0x00;
            halfsel.Checked = (Data.PPURegs[0x31] & 0x40) != 0x00;
            backaddsuben.Checked = (Data.PPURegs[0x31] & 0x20) != 0x00;
            objaddsuben.Checked = (Data.PPURegs[0x31] & 0x10) != 0x00;
            bg4addsuben.Checked = (Data.PPURegs[0x31] & 0x08) != 0x00;
            bg3addsuben.Checked = (Data.PPURegs[0x31] & 0x04) != 0x00;
            bg2addsuben.Checked = (Data.PPURegs[0x31] & 0x02) != 0x00;
            bg1addsuben.Checked = (Data.PPURegs[0x31] & 0x01) != 0x00;

            reg2132.Text = "$" + Util.DecToHex(Data.PPURegs[0x32], 4);
            fixedblue.Text = ((Data.PPURegs[0x32] & 0x7C00) >> 10).ToString();
            fixedgreen.Text = ((Data.PPURegs[0x32] & 0x03E0) >> 5).ToString();
            fixedred.Text = (Data.PPURegs[0x32] & 0x001F).ToString();

            reg2133.Text = "$" + Util.DecToHex(Data.PPURegs[0x33], 2);
            extsync.Checked = (Data.PPURegs[0x25] & 0x80) != 0x00;
            extbg.Checked = (Data.PPURegs[0x25] & 0x40) != 0x00;
            p512.Checked = (Data.PPURegs[0x25] & 0x08) != 0x00;
            overscan.Checked = (Data.PPURegs[0x25] & 0x04) != 0x00;
            objvdisp.Checked = (Data.PPURegs[0x25] & 0x02) != 0x00;
            interlace.Checked = (Data.PPURegs[0x25] & 0x01) != 0x00;
        }

        private void setRegBit(int idx, TextBox reg, int digits, CheckBox cbox, int bit)
        {
            Data.PPURegs[idx] = (short)((cbox.Checked ? bit : 0) | (Data.PPURegs[idx] & ~bit));
            reg.Text = "$" + Util.DecToHex(Data.PPURegs[idx], digits);
        }

        private void setRegBits(int idx, TextBox reg, int digits, TextBox tbox, int bits)
        {
            int val;
            if (Util.TryHexOrDecToDec(tbox.Text, out val))
            {
                int sh = 0, b = bits;
                while ((b & 1) == 0) { sh++; b >>= 1; }
                Data.PPURegs[idx] = (short)((Util.clamp(val, 0, bits >> sh) << sh) | (Data.PPURegs[idx] & ~bits));
                reg.Text = "$" + Util.DecToHex(Data.PPURegs[idx], digits);
            }
        }

        private void blanking_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x00, reg2100, 2, blanking, 0x80);
        }

        private void brightness_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x00, reg2100, 2, brightness, 0x0F);
        }

        private void objsize_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x01, reg2101, 2, objsize, 0xE0);
            SuperTileMapper.oam.RedrawAll();
        }

        private void objselect_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x01, reg2101, 2, objselect, 0x18);
            SuperTileMapper.oam.RedrawAll();
        }

        private void objbase_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x01, reg2101, 2, objbase, 0x07);
            SuperTileMapper.oam.RedrawAll();
        }

        private void objpriority_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x02, reg2102, 2, objpriority, 0xFE);
        }

        private void bg3priority_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x05, reg2105, 2, bg3priority, 0x08);
        }

        private void bgmode_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x05, reg2105, 2, bgmode, 0x07);
        }

        private void bg1chrsize_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x05, reg2105, 2, bg1chrsize, 0x10);
        }

        private void bg2chrsize_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x05, reg2105, 2, bg2chrsize, 0x20);
        }

        private void bg3chrsize_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x05, reg2105, 2, bg3chrsize, 0x40);
        }

        private void bg4chrsize_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x05, reg2105, 2, bg4chrsize, 0x80);
        }

        private void mosaicsize_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x06, reg2106, 2, mosaicsize, 0xF0);
        }

        private void bg1mosaic_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x06, reg2106, 2, bg1mosaic, 0x01);
        }

        private void bg2mosaic_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x06, reg2106, 2, bg2mosaic, 0x02);
        }

        private void bg3mosaic_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x06, reg2106, 2, bg3mosaic, 0x04);
        }

        private void bg4mosaic_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x06, reg2106, 2, bg4mosaic, 0x08);
        }

        private void bg1scbase_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x07, reg2107, 2, bg1scbase, 0xFC);
        }

        private void bg1scsize_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x07, reg2107, 2, bg1scsize, 0x03);
        }

        private void bg2scbase_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x08, reg2108, 2, bg2scbase, 0xFC);
        }

        private void bg2scsize_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x08, reg2108, 2, bg2scsize, 0x03);
        }

        private void bg3scbase_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x09, reg2109, 2, bg3scbase, 0xFC);
        }

        private void bg3scsize_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x09, reg2109, 2, bg3scsize, 0x03);
        }

        private void bg4scbase_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x0A, reg210A, 2, bg4scbase, 0xFC);
        }

        private void bg4scsize_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x0A, reg210A, 2, bg4scsize, 0x03);
        }

        private void bg1chrbase_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x0B, reg210B, 2, bg1chrbase, 0x0F);
        }

        private void bg2chrbase_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x0B, reg210B, 2, bg2chrbase, 0xF0);
        }

        private void bg3chrbase_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x0C, reg210C, 2, bg3chrbase, 0x0F);
        }

        private void bg4chrbase_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x0C, reg210C, 2, bg4chrbase, 0xF0);
        }

        private void bg1hofs_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x0D, reg210D, 4, bg1hofs, 0x1FFF);
        }

        private void bg1vofs_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x0E, reg210E, 4, bg1vofs, 0x1FFF);
        }

        private void bg2hofs_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x0F, reg210F, 4, bg2hofs, 0x03FF);
        }

        private void bg2vofs_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x10, reg2110, 4, bg2vofs, 0x03FF);
        }

        private void bg3hofs_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x11, reg2111, 4, bg3hofs, 0x03FF);
        }

        private void bg3vofs_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x12, reg2112, 4, bg3vofs, 0x03FF);
        }

        private void bg4hofs_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x13, reg2113, 4, bg4hofs, 0x03FF);
        }

        private void bg4vofs_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x14, reg2114, 4, bg4vofs, 0x03FF);
        }

        private void m7screenover_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x1A, reg211A, 2, m7screenover, 0xC0);
        }

        private void m7vflip_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x1A, reg211A, 2, m7vflip, 0x02);
        }

        private void m7hflip_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x1A, reg211A, 2, m7hflip, 0x01);
        }

        private void m7a_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x1B, reg211B, 4, m7a, 0xFFFF);
        }

        private void m7b_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x1C, reg211C, 4, m7b, 0xFFFF);
        }

        private void m7c_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x1D, reg211D, 4, m7c, 0xFFFF);
        }

        private void m7d_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x1E, reg211E, 4, m7d, 0xFFFF);
        }

        private void m7x_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x1F, reg211F, 4, m7x, 0xFFFF);
        }

        private void m7y_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x20, reg2120, 4, m7y, 0xFFFF);
        }

        private void bg1w2en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x23, reg2123, 2, bg1w2en, 0x08);
        }

        private void bg1w2io_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x23, reg2123, 2, bg1w2io, 0x04);
        }

        private void bg1w1en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x23, reg2123, 2, bg1w1en, 0x02);
        }

        private void bg1w1io_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x23, reg2123, 2, bg1w1io, 0x01);
        }

        private void bg2w2en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x23, reg2123, 2, bg2w2en, 0x80);
        }

        private void bg2w2io_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x23, reg2123, 2, bg2w2io, 0x40);
        }

        private void bg2w1en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x23, reg2123, 2, bg2w1en, 0x20);
        }

        private void bg2w1io_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x23, reg2123, 2, bg2w1io, 0x10);
        }

        private void bg3w2en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x24, reg2124, 2, bg3w2en, 0x08);
        }

        private void bg3w2io_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x24, reg2124, 2, bg3w2io, 0x04);
        }

        private void bg3w1en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x24, reg2124, 2, bg3w1en, 0x02);
        }

        private void bg3w1io_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x24, reg2124, 2, bg3w1io, 0x01);
        }

        private void bg4w2en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x24, reg2124, 2, bg4w2en, 0x80);
        }

        private void bg4w2io_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x24, reg2124, 2, bg4w2io, 0x40);
        }

        private void bg4w1en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x24, reg2124, 2, bg4w1en, 0x20);
        }

        private void bg4w1io_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x24, reg2124, 2, bg4w1io, 0x10);
        }

        private void objw2en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x25, reg2125, 2, objw2en, 0x08);
        }

        private void objw2io_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x25, reg2125, 2, objw2io, 0x04);
        }

        private void objw1en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x25, reg2125, 2, objw1en, 0x02);
        }

        private void objw1io_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x25, reg2125, 2, objw1io, 0x01);
        }

        private void colorw2en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x25, reg2125, 2, colorw2en, 0x80);
        }

        private void colorw2io_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x25, reg2125, 2, colorw2io, 0x40);
        }

        private void colorw1en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x25, reg2125, 2, colorw1en, 0x20);
        }

        private void colorw1io_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x25, reg2125, 2, colorw1io, 0x10);
        }

        private void w1left_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x26, reg2126, 2, w1left, 0xFF);
        }

        private void w1right_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x27, reg2127, 2, w1right, 0xFF);
        }

        private void w2left_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x28, reg2128, 2, w2left, 0xFF);
        }

        private void w2right_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x29, reg2129, 2, w2right, 0xFF);
        }

        private void bg1winlog_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x2A, reg212A, 2, bg1winlog, 0x03);
        }

        private void bg2winlog_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x2A, reg212A, 2, bg2winlog, 0x0C);
        }

        private void bg3winlog_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x2A, reg212A, 2, bg3winlog, 0x30);
        }

        private void bg4winlog_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x2A, reg212A, 2, bg4winlog, 0xC0);
        }

        private void colorwinlog_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x2B, reg212B, 2, colorwinlog, 0x0C);
        }

        private void objwinlog_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x2B, reg212B, 2, objwinlog, 0x03);
        }

        private void objtm_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2C, reg212C, 2, objtm, 0x10);
        }

        private void bg4tm_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2C, reg212C, 2, bg4tm, 0x08);
        }

        private void bg3tm_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2C, reg212C, 2, bg3tm, 0x04);
        }

        private void bg2tm_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2C, reg212C, 2, bg2tm, 0x02);
        }

        private void bg1tm_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2C, reg212C, 2, bg1tm, 0x01);
        }

        private void objts_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2D, reg212D, 2, objts, 0x10);
        }

        private void bg4ts_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2D, reg212D, 2, bg4ts, 0x08);
        }

        private void bg3ts_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2D, reg212D, 2, bg3ts, 0x04);
        }

        private void bg2ts_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2D, reg212D, 2, bg2ts, 0x02);
        }

        private void bg1ts_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2D, reg212D, 2, bg1ts, 0x01);
        }

        private void objtmw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2E, reg212E, 2, objtmw, 0x10);
        }

        private void bg4tmw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2E, reg212E, 2, bg4tmw, 0x08);
        }

        private void bg3tmw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2E, reg212E, 2, bg3tmw, 0x04);
        }

        private void bg2tmw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2E, reg212E, 2, bg2tmw, 0x02);
        }

        private void bg1tmw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2E, reg212E, 2, bg1tmw, 0x01);
        }

        private void objtsw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2F, reg212F, 2, objtsw, 0x10);
        }

        private void bg4tsw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2F, reg212F, 2, bg4tsw, 0x08);
        }

        private void bg3tsw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2F, reg212F, 2, bg3tsw, 0x04);
        }

        private void bg2tsw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2F, reg212F, 2, bg2tsw, 0x02);
        }

        private void bg1tsw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2F, reg212F, 2, bg1tsw, 0x01);
        }

        private void colorwinmain_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x30, reg2130, 2, colorwinmain, 0xC0);
        }

        private void colorwinsub_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x30, reg2130, 2, colorwinsub, 0x30);
        }

        private void addsubfixed_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x30, reg2130, 2, addsubfixed, 0x02);
        }

        private void directcolor_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x30, reg2130, 2, directcolor, 0x01);
        }

        private void addsubsel_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x31, reg2131, 2, addsubsel, 0x80);
        }

        private void halfsel_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x31, reg2131, 2, halfsel, 0x40);
        }

        private void backaddsuben_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x31, reg2131, 2, backaddsuben, 0x20);
        }

        private void objaddsuben_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x31, reg2131, 2, objaddsuben, 0x10);
        }

        private void bg4addsuben_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x31, reg2131, 2, bg4addsuben, 0x08);
        }

        private void bg3addsuben_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x31, reg2131, 2, bg3addsuben, 0x04);
        }

        private void bg2addsuben_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x31, reg2131, 2, bg2addsuben, 0x02);
        }

        private void bg1addsuben_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x31, reg2131, 2, bg1addsuben, 0x01);
        }

        private void fixedblue_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x32, reg2132, 4, fixedblue, 0x7C00);
        }

        private void fixedgreen_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x32, reg2132, 4, fixedgreen, 0x03E0);
        }

        private void fixedred_TextChanged(object sender, EventArgs e)
        {
            setRegBits(0x32, reg2132, 4, fixedred, 0x001F);
        }

        private void extsync_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x33, reg2133, 2, extsync, 0x80);
        }

        private void extbg_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x33, reg2133, 2, extbg, 0x40);
        }

        private void p512_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x33, reg2133, 2, p512, 0x08);
        }

        private void overscan_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x33, reg2133, 2, overscan, 0x04);
        }

        private void objvdisp_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x33, reg2133, 2, objvdisp, 0x02);
        }

        private void interlace_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x33, reg2133, 2, interlace, 0x01);
        }
    }
}
