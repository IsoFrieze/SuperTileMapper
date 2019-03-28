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
    public partial class PPURegEditor2 : Form
    {
        public PPURegEditor2()
        {
            InitializeComponent();
            ResizeMe();
            Redraw();
        }

        private void SetSize(Size s)
        {
            this.MaximumSize = s;
            this.MinimumSize = s;
            this.Size = s;
        }

        private void ResizeMe()
        {
            SetSize(new Size(432, tabControl2.SelectedIndex == 0 ? 438 : 573));
        }

        private void Redraw()
        {
            reg2100.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x00), 2);
            blanking.Checked = (Data.GetPPUReg(0x00) & 0x80) != 0x00;
            brightness.Text = (Data.GetPPUReg(0x00) & 0x0F).ToString();

            reg2101.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x01), 2);
            objsize.SelectedIndex = (Data.GetPPUReg(0x01) & 0xE0) >> 5;
            objselect.SelectedIndex = (Data.GetPPUReg(0x01) & 0x18) >> 3;
            objbase.SelectedIndex = (Data.GetPPUReg(0x01) & 0x07);

            reg2102.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x02), 2);
            objpriority.Text = ((Data.GetPPUReg(0x02) & 0xFE) >> 1).ToString();

            reg2105.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x05), 2);
            bgmode.SelectedIndex = (Data.GetPPUReg(0x05) & 0x07);
            bg3priority.Checked = (Data.GetPPUReg(0x05) & 0x08) != 0x00;
            bg1chrsize.SelectedIndex = (Data.GetPPUReg(0x05) & 0x10) >> 4;
            bg2chrsize.SelectedIndex = (Data.GetPPUReg(0x05) & 0x20) >> 5;
            bg3chrsize.SelectedIndex = (Data.GetPPUReg(0x05) & 0x40) >> 6;
            bg4chrsize.SelectedIndex = (Data.GetPPUReg(0x05) & 0x80) >> 7;

            reg2106.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x06), 2);
            mosaicsize.Text = ((Data.GetPPUReg(0x06) & 0xF0) >> 4).ToString();
            bg1mosaic.Checked = (Data.GetPPUReg(0x06) & 0x01) != 0x00;
            bg2mosaic.Checked = (Data.GetPPUReg(0x06) & 0x02) != 0x00;
            bg3mosaic.Checked = (Data.GetPPUReg(0x06) & 0x04) != 0x00;
            bg4mosaic.Checked = (Data.GetPPUReg(0x06) & 0x08) != 0x00;

            reg2107.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x07), 2);
            bg1scbase.SelectedIndex = (Data.GetPPUReg(0x07) & 0xFC) >> 2;
            bg1scsize.SelectedIndex = (Data.GetPPUReg(0x07) & 0x03);

            reg2108.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x08), 2);
            bg2scbase.SelectedIndex = (Data.GetPPUReg(0x08) & 0xFC) >> 2;
            bg2scsize.SelectedIndex = (Data.GetPPUReg(0x08) & 0x03);

            reg2109.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x09), 2);
            bg3scbase.SelectedIndex = (Data.GetPPUReg(0x09) & 0xFC) >> 2;
            bg3scsize.SelectedIndex = (Data.GetPPUReg(0x09) & 0x03);

            reg210A.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x0A), 2);
            bg4scbase.SelectedIndex = (Data.GetPPUReg(0x0A) & 0xFC) >> 2;
            bg4scsize.SelectedIndex = (Data.GetPPUReg(0x0A) & 0x03);

            reg210B.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x0B), 2);
            bg1chrbase.SelectedIndex = (Data.GetPPUReg(0x0B) & 0x0F);
            bg2chrbase.SelectedIndex = (Data.GetPPUReg(0x0B) & 0xF0) >> 4;

            reg210C.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x0C), 2);
            bg3chrbase.SelectedIndex = (Data.GetPPUReg(0x0C) & 0x0F);
            bg4chrbase.SelectedIndex = (Data.GetPPUReg(0x0C) & 0xF0) >> 4;

            reg210D.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x0D), 4);
            bg1hofs.Text = (Data.GetPPUReg(0x0D) & 0x1FFF).ToString();

            reg210E.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x0E), 4);
            bg1vofs.Text = (Data.GetPPUReg(0x0E) & 0x1FFF).ToString();

            reg210F.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x0F), 4);
            bg2hofs.Text = (Data.GetPPUReg(0x0F) & 0x03FF).ToString();

            reg2110.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x10), 4);
            bg2vofs.Text = (Data.GetPPUReg(0x10) & 0x03FF).ToString();

            reg2111.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x11), 4);
            bg3hofs.Text = (Data.GetPPUReg(0x11) & 0x03FF).ToString();

            reg2112.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x12), 4);
            bg3vofs.Text = (Data.GetPPUReg(0x12) & 0x03FF).ToString();

            reg2113.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x13), 4);
            bg4hofs.Text = (Data.GetPPUReg(0x13) & 0x03FF).ToString();

            reg2114.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x14), 4);
            bg4vofs.Text = (Data.GetPPUReg(0x14) & 0x03FF).ToString();

            reg211A.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x1A), 2);
            int outOfArea = (Data.GetPPUReg(0x1A) & 0xC0) >> 6;
            m7screenover.SelectedIndex = outOfArea == 0 ? 0 : outOfArea - 1;
            m7vflip.Checked = (Data.GetPPUReg(0x1A) & 0x02) != 0x00;
            m7hflip.Checked = (Data.GetPPUReg(0x1A) & 0x01) != 0x00;

            reg211B.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x1B), 4);
            m7a.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x1B), 4);

            reg211C.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x1C), 4);
            m7b.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x1C), 4);

            reg211D.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x1D), 4);
            m7c.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x1D), 4);

            reg211E.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x1E), 4);
            m7d.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x1E), 4);

            reg211F.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x1F), 4);
            m7x.Text = Data.GetPPUReg(0x1F).ToString();

            reg2120.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x20), 4);
            m7y.Text = Data.GetPPUReg(0x20).ToString();

            reg2123.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x23), 2);
            bg2w2en.Checked = (Data.GetPPUReg(0x23) & 0x80) != 0x00;
            bg2w2io.SelectedIndex = (Data.GetPPUReg(0x23) & 0x40) >> 6;
            bg2w1en.Checked = (Data.GetPPUReg(0x23) & 0x20) != 0x00;
            bg2w1io.SelectedIndex = (Data.GetPPUReg(0x23) & 0x10) >> 4;
            bg1w2en.Checked = (Data.GetPPUReg(0x23) & 0x08) != 0x00;
            bg1w2io.SelectedIndex = (Data.GetPPUReg(0x23) & 0x04) >> 2;
            bg1w1en.Checked = (Data.GetPPUReg(0x23) & 0x02) != 0x00;
            bg1w1io.SelectedIndex = (Data.GetPPUReg(0x23) & 0x01);

            reg2124.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x24), 2);
            bg4w2en.Checked = (Data.GetPPUReg(0x24) & 0x80) != 0x00;
            bg4w2io.SelectedIndex = (Data.GetPPUReg(0x24) & 0x40) >> 6;
            bg4w1en.Checked = (Data.GetPPUReg(0x24) & 0x20) != 0x00;
            bg4w1io.SelectedIndex = (Data.GetPPUReg(0x24) & 0x10) >> 4;
            bg3w2en.Checked = (Data.GetPPUReg(0x24) & 0x08) != 0x00;
            bg3w2io.SelectedIndex = (Data.GetPPUReg(0x24) & 0x04) >> 2;
            bg3w1en.Checked = (Data.GetPPUReg(0x24) & 0x02) != 0x00;
            bg3w1io.SelectedIndex = (Data.GetPPUReg(0x24) & 0x01);

            reg2125.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x25), 2);
            colorw2en.Checked = (Data.GetPPUReg(0x25) & 0x80) != 0x00;
            colorw2io.SelectedIndex = (Data.GetPPUReg(0x25) & 0x40) >> 6;
            colorw1en.Checked = (Data.GetPPUReg(0x25) & 0x20) != 0x00;
            colorw1io.SelectedIndex = (Data.GetPPUReg(0x25) & 0x10) >> 4;
            objw2en.Checked = (Data.GetPPUReg(0x25) & 0x08) != 0x00;
            objw2io.SelectedIndex = (Data.GetPPUReg(0x25) & 0x04) >> 2;
            objw1en.Checked = (Data.GetPPUReg(0x25) & 0x02) != 0x00;
            objw1io.SelectedIndex = (Data.GetPPUReg(0x25) & 0x01);

            reg2126.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x26), 2);
            w1left.Text = (Data.GetPPUReg(0x26) & 0xFF).ToString();

            reg2127.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x27), 2);
            w1right.Text = (Data.GetPPUReg(0x26) & 0xFF).ToString();

            reg2128.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x28), 2);
            w2left.Text = (Data.GetPPUReg(0x26) & 0xFF).ToString();

            reg2129.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x29), 2);
            w2right.Text = (Data.GetPPUReg(0x26) & 0xFF).ToString();

            reg212A.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x2A), 2);
            bg1winlog.SelectedIndex = (Data.GetPPUReg(0x2A) & 0x03);
            bg2winlog.SelectedIndex = (Data.GetPPUReg(0x2A) & 0x0C) >> 2;
            bg3winlog.SelectedIndex = (Data.GetPPUReg(0x2A) & 0x30) >> 4;
            bg4winlog.SelectedIndex = (Data.GetPPUReg(0x2A) & 0xC0) >> 6;

            reg212B.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x2B), 2);
            objwinlog.SelectedIndex = (Data.GetPPUReg(0x2B) & 0x03);
            colorwinlog.SelectedIndex = (Data.GetPPUReg(0x2B) & 0x0C) >> 2;

            reg212C.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x2C), 2);
            objtm.Checked = (Data.GetPPUReg(0x2C) & 0x10) != 0x00;
            bg4tm.Checked = (Data.GetPPUReg(0x2C) & 0x08) != 0x00;
            bg3tm.Checked = (Data.GetPPUReg(0x2C) & 0x04) != 0x00;
            bg2tm.Checked = (Data.GetPPUReg(0x2C) & 0x02) != 0x00;
            bg1tm.Checked = (Data.GetPPUReg(0x2C) & 0x01) != 0x00;

            reg212D.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x2D), 2);
            objts.Checked = (Data.GetPPUReg(0x2D) & 0x10) != 0x00;
            bg4ts.Checked = (Data.GetPPUReg(0x2D) & 0x08) != 0x00;
            bg3ts.Checked = (Data.GetPPUReg(0x2D) & 0x04) != 0x00;
            bg2ts.Checked = (Data.GetPPUReg(0x2D) & 0x02) != 0x00;
            bg1ts.Checked = (Data.GetPPUReg(0x2D) & 0x01) != 0x00;

            reg212E.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x2E), 2);
            objtmw.Checked = (Data.GetPPUReg(0x2E) & 0x10) != 0x00;
            bg4tmw.Checked = (Data.GetPPUReg(0x2E) & 0x08) != 0x00;
            bg3tmw.Checked = (Data.GetPPUReg(0x2E) & 0x04) != 0x00;
            bg2tmw.Checked = (Data.GetPPUReg(0x2E) & 0x02) != 0x00;
            bg1tmw.Checked = (Data.GetPPUReg(0x2E) & 0x01) != 0x00;

            reg212F.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x2F), 2);
            objtsw.Checked = (Data.GetPPUReg(0x2F) & 0x10) != 0x00;
            bg4tsw.Checked = (Data.GetPPUReg(0x2F) & 0x08) != 0x00;
            bg3tsw.Checked = (Data.GetPPUReg(0x2F) & 0x04) != 0x00;
            bg2tsw.Checked = (Data.GetPPUReg(0x2F) & 0x02) != 0x00;
            bg1tsw.Checked = (Data.GetPPUReg(0x2F) & 0x01) != 0x00;

            reg2130.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x30), 2);
            colormainswitch.SelectedIndex = (Data.GetPPUReg(0x30) & 0xC0) >> 6;
            colorsubswitch.SelectedIndex = (Data.GetPPUReg(0x30) & 0x30) >> 4;
            addsubfixed.SelectedIndex = (Data.GetPPUReg(0x30) & 0x02) >> 1;
            directcolor.Checked = (Data.GetPPUReg(0x30) & 0x01) != 0x00;

            reg2131.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x31), 2);
            colormathsel.SelectedIndex = (Data.GetPPUReg(0x31) & 0xC0) >> 6;
            backaddsuben.Checked = (Data.GetPPUReg(0x31) & 0x20) != 0x00;
            objaddsuben.Checked = (Data.GetPPUReg(0x31) & 0x10) != 0x00;
            bg4addsuben.Checked = (Data.GetPPUReg(0x31) & 0x08) != 0x00;
            bg3addsuben.Checked = (Data.GetPPUReg(0x31) & 0x04) != 0x00;
            bg2addsuben.Checked = (Data.GetPPUReg(0x31) & 0x02) != 0x00;
            bg1addsuben.Checked = (Data.GetPPUReg(0x31) & 0x01) != 0x00;

            reg2132.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x32), 4);
            Color f = Data.GetFixedColor();
            fixedblue.Text = (f.B >> 3).ToString();
            fixedgreen.Text = (f.G >> 3).ToString();
            fixedred.Text = (f.R >> 3).ToString();
            fixedcolor.BackColor = Data.GetFixedColor();

            reg2133.Text = "$" + Util.DecToHex(Data.GetPPUReg(0x33), 2);
            extsync.Checked = (Data.GetPPUReg(0x25) & 0x80) != 0x00;
            extbg.Checked = (Data.GetPPUReg(0x25) & 0x40) != 0x00;
            p512.Checked = (Data.GetPPUReg(0x25) & 0x08) != 0x00;
            overscan.SelectedIndex = (Data.GetPPUReg(0x25) & 0x04) >> 2;
            objvdisp.SelectedIndex = (Data.GetPPUReg(0x25) & 0x02) >> 1;
            interlace.Checked = (Data.GetPPUReg(0x25) & 0x01) != 0x00;
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResizeMe();
        }

        private void setRegBit(int idx, TextBox reg, int digits, bool option, int bit)
        {
            Data.SetPPURegBits(idx, bit, option ? bit : 0);
            reg.Text = "$" + Util.DecToHex(Data.GetPPUReg(idx), digits);
        }

        private void setRegBits(int idx, TextBox reg, int digits, int option, int bits)
        {
            int sh = 0, b = bits;
            while ((b & 1) == 0) { sh++; b >>= 1; }
            Data.SetPPURegBits(idx, bits, Util.Clamp(option, 0, bits >> sh) << sh);
            reg.Text = "$" + Util.DecToHex(Data.GetPPUReg(idx), digits);
        }

        private void setRegBitsDouble(int idx, TextBox reg, int digits, string option, int bits)
        {
            if (option.Contains("."))
            {
                if (double.TryParse(option, out double val))
                {
                    double db = Util.Clamp(val, -128.0, 127.99609375);
                    setRegBits(idx, reg, digits, (int)(256 * (db < 0 ? db + 256.0 : db)), bits);
                }
            }
            else
            {
                if (Util.TryHexOrDecToDec(option, out int val))
                {
                    setRegBits(idx, reg, digits, val < 0 ? val + 0x10000 : val, bits);
                }
            }
        }

        private void blanking_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x00, reg2100, 2, blanking.Checked, 0x80);
        }

        private void brightness_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(brightness.Text, out int val))
            {
                setRegBits(0x00, reg2100, 2, val, 0x0F);
            }
        }

        private void extsync_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x33, reg2133, 2, extsync.Checked, 0x80);
        }

        private void extbg_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x33, reg2133, 2, extbg.Checked, 0x40);
        }

        private void p512_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x33, reg2133, 2, p512.Checked, 0x08);
        }

        private void interlace_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x33, reg2133, 2, interlace.Checked, 0x01);
        }

        private void overscan_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x33, reg2133, 2, overscan.SelectedIndex, 0x04);
            if (SuperTileMapper.oam.Visible) SuperTileMapper.oam.RedrawAll();
        }

        private void objvdisp_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x33, reg2133, 2, objvdisp.SelectedIndex, 0x02);
        }

        private void objbase_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x01, reg2101, 2, objbase.SelectedIndex, 0x07);
            if (SuperTileMapper.oam.Visible) SuperTileMapper.oam.RedrawAll();
            if (SuperTileMapper.obj.Visible) SuperTileMapper.obj.RedrawAll();
        }

        private void objselect_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x01, reg2101, 2, objselect.SelectedIndex, 0x18);
            if (SuperTileMapper.oam.Visible) SuperTileMapper.oam.RedrawAll();
            if (SuperTileMapper.obj.Visible) SuperTileMapper.obj.RedrawAll();
        }

        private void objsize_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x01, reg2101, 2, objsize.SelectedIndex, 0xE0);
            if (SuperTileMapper.oam.Visible) SuperTileMapper.oam.RedrawAll();
            if (SuperTileMapper.obj.Visible) SuperTileMapper.obj.RedrawAll();
        }

        private void objpriority_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(objpriority.Text, out int val))
            {
                setRegBits(0x02, reg2102, 2, val, 0xFE);
                if (SuperTileMapper.oam.Visible) SuperTileMapper.oam.RedrawAll();
            }
        }

        private void bgmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x05, reg2105, 2, bgmode.SelectedIndex, 0x07);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg3priority_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x05, reg2105, 2, bg3priority.Checked, 0x08);
        }

        private void bg1chrsize_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x05, reg2105, 2, bg1chrsize.SelectedIndex, 0x10);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg2chrsize_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x05, reg2105, 2, bg2chrsize.SelectedIndex, 0x20);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg3chrsize_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x05, reg2105, 2, bg3chrsize.SelectedIndex, 0x40);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg4chrsize_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x05, reg2105, 2, bg4chrsize.SelectedIndex, 0x80);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg1mosaic_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x06, reg2106, 2, bg1mosaic.Checked, 0x01);
        }

        private void bg2mosaic_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x06, reg2106, 2, bg2mosaic.Checked, 0x02);
        }

        private void bg3mosaic_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x06, reg2106, 2, bg3mosaic.Checked, 0x04);
        }

        private void bg4mosaic_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x06, reg2106, 2, bg4mosaic.Checked, 0x08);
        }

        private void mosaicsize_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(mosaicsize.Text, out int val))
            {
                setRegBits(0x06, reg2106, 2, val, 0xF0);
            }
        }

        private void bg1scbase_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x07, reg2107, 2, bg1scbase.SelectedIndex, 0xFC);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg2scbase_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x08, reg2108, 2, bg2scbase.SelectedIndex, 0xFC);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg3scbase_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x09, reg2109, 2, bg3scbase.SelectedIndex, 0xFC);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg4scbase_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x0A, reg210A, 2, bg4scbase.SelectedIndex, 0xFC);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg1chrbase_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x0B, reg210B, 2, bg1chrbase.SelectedIndex, 0x0F);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg2chrbase_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x0B, reg210B, 2, bg2chrbase.SelectedIndex, 0xF0);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg3chrbase_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x0C, reg210C, 2, bg3chrbase.SelectedIndex, 0x0F);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg4chrbase_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x0C, reg210C, 2, bg4chrbase.SelectedIndex, 0xF0);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg1scsize_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x07, reg2107, 2, bg1scsize.SelectedIndex, 0x03);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg2scsize_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x08, reg2108, 2, bg2scsize.SelectedIndex, 0x03);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg3scsize_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x09, reg2109, 2, bg3scsize.SelectedIndex, 0x03);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg4scsize_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x0A, reg210A, 2, bg4scsize.SelectedIndex, 0x03);
            if (SuperTileMapper.tmap.Visible) SuperTileMapper.tmap.RedrawAll();
        }

        private void bg1hofs_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(bg1hofs.Text, out int val))
            {
                setRegBits(0x0D, reg210D, 4, val, 0x1FFF);
            }
        }

        private void bg1vofs_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(bg1vofs.Text, out int val))
            {
                setRegBits(0x0E, reg210E, 4, val, 0x1FFF);
            }
        }

        private void bg2hofs_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(bg2hofs.Text, out int val))
            {
                setRegBits(0x0F, reg210F, 4, val, 0x03FF);
            }
        }

        private void bg2vofs_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(bg2vofs.Text, out int val))
            {
                setRegBits(0x10, reg2110, 4, val, 0x03FF);
            }
        }

        private void bg3hofs_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(bg3hofs.Text, out int val))
            {
                setRegBits(0x11, reg2111, 4, val, 0x03FF);
            }
        }

        private void bg3vofs_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(bg3vofs.Text, out int val))
            {
                setRegBits(0x12, reg2112, 4, val, 0x03FF);
            }
        }

        private void bg4hofs_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(bg4hofs.Text, out int val))
            {
                setRegBits(0x13, reg2113, 4, val, 0x03FF);
            }
        }

        private void bg4vofs_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(bg4vofs.Text, out int val))
            {
                setRegBits(0x14, reg2114, 4, val, 0x03FF);
            }
        }

        private void m7screenover_SelectedIndexChanged(object sender, EventArgs e)
        {
            int outOfArea = m7screenover.SelectedIndex;
            setRegBits(0x1A, reg211A, 2, outOfArea == 0 ? 0 : outOfArea + 1, 0xC0);
        }

        private void m7hflip_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x1A, reg211A, 2, m7hflip.Checked, 0x01);
        }

        private void m7vflip_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x1A, reg211A, 2, m7vflip.Checked, 0x02);
        }

        private void m7a_TextChanged(object sender, EventArgs e)
        {
            setRegBitsDouble(0x1B, reg211B, 4, m7a.Text, 0xFFFF);
        }

        private void m7b_TextChanged(object sender, EventArgs e)
        {
            setRegBitsDouble(0x1C, reg211C, 4, m7b.Text, 0xFFFF);
        }

        private void m7c_TextChanged(object sender, EventArgs e)
        {
            setRegBitsDouble(0x1D, reg211D, 4, m7c.Text, 0xFFFF);
        }

        private void m7d_TextChanged(object sender, EventArgs e)
        {
            setRegBitsDouble(0x1E, reg211E, 4, m7d.Text, 0xFFFF);
        }

        private void m7x_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(m7x.Text, out int val))
            {
                setRegBits(0x1F, reg211F, 4, val, 0x1FFF);
            }
        }

        private void m7y_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(m7y.Text, out int val))
            {
                setRegBits(0x20, reg2120, 4, val, 0x1FFF);
            }
        }

        private void bg1w1en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x23, reg2123, 2, bg1w1en.Checked, 0x02);
        }

        private void bg2w1en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x23, reg2123, 2, bg2w1en.Checked, 0x20);
        }

        private void bg3w1en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x24, reg2124, 2, bg3w1en.Checked, 0x02);
        }

        private void bg4w1en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x24, reg2124, 2, bg4w1en.Checked, 0x20);
        }

        private void objw1en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x25, reg2125, 2, objw1en.Checked, 0x02);
        }

        private void colorw1en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x25, reg2125, 2, colorw1en.Checked, 0x20);
        }

        private void bg1w1io_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x23, reg2123, 2, bg1w1io.SelectedIndex, 0x01);
        }

        private void bg2w1io_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x23, reg2123, 2, bg2w1io.SelectedIndex, 0x10);
        }

        private void bg3w1io_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x24, reg2124, 2, bg3w1io.SelectedIndex, 0x01);
        }

        private void bg4w1io_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x24, reg2124, 2, bg4w1io.SelectedIndex, 0x10);
        }

        private void objw1io_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x25, reg2125, 2, objw1io.SelectedIndex, 0x01);
        }

        private void colorw1io_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x25, reg2125, 2, colorw1io.SelectedIndex, 0x10);
        }

        private void bg1w2en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x23, reg2123, 2, bg1w2en.Checked, 0x08);
        }

        private void bg2w2en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x23, reg2123, 2, bg2w2en.Checked, 0x80);
        }

        private void bg3w2en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x24, reg2124, 2, bg3w2en.Checked, 0x08);
        }

        private void bg4w2en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x24, reg2124, 2, bg4w2en.Checked, 0x80);
        }

        private void objw2en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x25, reg2125, 2, objw2en.Checked, 0x08);
        }

        private void colorw2en_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x25, reg2125, 2, colorw2en.Checked, 0x80);
        }

        private void bg1w2io_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x23, reg2123, 2, bg1w2io.SelectedIndex, 0x04);
        }

        private void bg2w2io_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x23, reg2123, 2, bg2w2io.SelectedIndex, 0x40);
        }

        private void bg3w2io_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x24, reg2124, 2, bg3w2io.SelectedIndex, 0x04);
        }

        private void bg4w2io_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x24, reg2124, 2, bg4w2io.SelectedIndex, 0x40);
        }

        private void objw2io_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x25, reg2125, 2, objw2io.SelectedIndex, 0x04);
        }

        private void colorw2io_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x25, reg2125, 2, colorw2io.SelectedIndex, 0x40);
        }

        private void bg1winlog_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x2A, reg212A, 2, bg1winlog.SelectedIndex, 0x03);
        }

        private void bg2winlog_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x2A, reg212A, 2, bg2winlog.SelectedIndex, 0x0C);
        }

        private void bg3winlog_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x2A, reg212A, 2, bg3winlog.SelectedIndex, 0x30);
        }

        private void bg4winlog_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x2A, reg212A, 2, bg4winlog.SelectedIndex, 0xC0);
        }

        private void objwinlog_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x2B, reg212B, 2, objwinlog.SelectedIndex, 0x03);
        }

        private void colorwinlog_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x2B, reg212B, 2, colorwinlog.SelectedIndex, 0x0C);
        }

        private void w1left_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(w1left.Text, out int val))
            {
                setRegBits(0x26, reg2126, 2, val, 0xFF);
            }
        }

        private void w1right_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(w1right.Text, out int val))
            {
                setRegBits(0x27, reg2127, 2, val, 0xFF);
            }
        }

        private void w2left_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(w2left.Text, out int val))
            {
                setRegBits(0x28, reg2128, 2, val, 0xFF);
            }
        }

        private void w2right_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(w2right.Text, out int val))
            {
                setRegBits(0x29, reg2129, 2, val, 0xFF);
            }
        }

        private void bg1tm_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2C, reg212C, 2, bg1tm.Checked, 0x01);
        }

        private void bg2tm_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2C, reg212C, 2, bg2tm.Checked, 0x02);
        }

        private void bg3tm_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2C, reg212C, 2, bg3tm.Checked, 0x04);
        }

        private void bg4tm_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2C, reg212C, 2, bg4tm.Checked, 0x08);
        }

        private void objtm_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2C, reg212C, 2, objtm.Checked, 0x10);
        }

        private void bg1ts_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2D, reg212D, 2, bg1ts.Checked, 0x01);
        }

        private void bg2ts_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2D, reg212D, 2, bg2ts.Checked, 0x02);
        }

        private void bg3ts_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2D, reg212D, 2, bg3ts.Checked, 0x04);
        }

        private void bg4ts_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2D, reg212D, 2, bg4ts.Checked, 0x08);
        }

        private void objts_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2D, reg212D, 2, objts.Checked, 0x10);
        }

        private void bg1tmw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2E, reg212E, 2, bg1tmw.Checked, 0x01);
        }

        private void bg2tmw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2E, reg212E, 2, bg2tmw.Checked, 0x02);
        }

        private void bg3tmw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2E, reg212E, 2, bg3tmw.Checked, 0x04);
        }

        private void bg4tmw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2E, reg212E, 2, bg4tmw.Checked, 0x08);
        }

        private void objtmw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2E, reg212E, 2, objtmw.Checked, 0x10);
        }

        private void bg1tsw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2F, reg212F, 2, bg1tsw.Checked, 0x01);
        }

        private void bg2tsw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2F, reg212F, 2, bg2tsw.Checked, 0x02);
        }

        private void bg3tsw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2F, reg212F, 2, bg3tsw.Checked, 0x04);
        }

        private void bg4tsw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2F, reg212F, 2, bg4tsw.Checked, 0x08);
        }

        private void objtsw_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x2F, reg212F, 2, objtsw.Checked, 0x10);
        }

        private void directcolor_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x30, reg2130, 2, directcolor.Checked, 0x01);
        }

        private void colormainswitch_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x30, reg2130, 2, colormainswitch.SelectedIndex, 0xC0);
        }

        private void colorsubswitch_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x30, reg2130, 2, colorsubswitch.SelectedIndex, 0x30);
        }

        private void addsubfixed_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x30, reg2130, 2, addsubfixed.SelectedIndex, 0x02);
        }

        private void colormathsel_SelectedIndexChanged(object sender, EventArgs e)
        {
            setRegBits(0x31, reg2131, 2, colormathsel.SelectedIndex, 0xC0);
        }

        private void bg1addsuben_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x31, reg2131, 2, bg1addsuben.Checked, 0x01);
        }

        private void bg2addsuben_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x31, reg2131, 2, bg2addsuben.Checked, 0x02);
        }

        private void bg3addsuben_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x31, reg2131, 2, bg3addsuben.Checked, 0x04);
        }

        private void bg4addsuben_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x31, reg2131, 2, bg4addsuben.Checked, 0x08);
        }

        private void objaddsuben_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x31, reg2131, 2, objaddsuben.Checked, 0x10);
        }

        private void backaddsuben_CheckedChanged(object sender, EventArgs e)
        {
            setRegBit(0x31, reg2131, 2, backaddsuben.Checked, 0x20);
        }

        private void fixedred_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(fixedred.Text, out int val))
            {
                setRegBits(0x32, reg2132, 4, val, 0x001F);
                fixedcolor.BackColor = Data.GetFixedColor();
            }
        }

        private void fixedgreen_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(fixedgreen.Text, out int val))
            {
                setRegBits(0x32, reg2132, 4, val, 0x03E0);
                fixedcolor.BackColor = Data.GetFixedColor();
            }
        }

        private void fixedblue_TextChanged(object sender, EventArgs e)
        {
            if (Util.TryHexOrDecToDec(fixedblue.Text, out int val))
            {
                setRegBits(0x32, reg2132, 4, val, 0x7C00);
                fixedcolor.BackColor = Data.GetFixedColor();
            }
        }

        private void fixedcolor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = Data.GetFixedColor();
            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Color c = colorDialog1.Color;
                fixedred.Text = (c.R >> 3).ToString();
                fixedgreen.Text = (c.G >> 3).ToString();
                fixedblue.Text = (c.B >> 3).ToString();
            }
        }
    }
}
