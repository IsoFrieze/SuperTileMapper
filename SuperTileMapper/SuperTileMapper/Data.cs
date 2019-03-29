using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTileMapper
{
    public static class Data
    {
        public const int VRAM_SIZE = 2 * 0x8000;
        public const int CGRAM_SIZE = 2 * 0x100;
        public const int OAM_SIZE = 2 * 0x110;

        private static byte[] VRAM = new byte[VRAM_SIZE];
        private static byte[] CGRAM = new byte[CGRAM_SIZE];
        private static byte[] OAM = new byte[OAM_SIZE];
        private static short[] PPURegs = new short[0x34];

        public static byte[] GetVRAMArray()
        {
            return VRAM;
        }

        public static byte[] GetCGRAMArray()
        {
            return CGRAM;
        }

        public static byte[] GetOAMArray()
        {
            return OAM;
        }

        public static Color GetFixedColor()
        {
            int r = (PPURegs[0x32] & 0x001F) << 3;
            int g = (PPURegs[0x32] & 0x03E0) >> 2;
            int b = (PPURegs[0x32] & 0x7C00) >> 7;
            return Color.FromArgb(r, g, b);
        }

        public static Color GetCGRAMColor(int c)
        {
            int i = (2 * c) & 0x1FF;
            int r = (CGRAM[i] & 0x1F) << 3;
            int g = ((CGRAM[i] & 0xE0) >> 2) | ((CGRAM[i + 1] & 0x03) << 6);
            int b = (CGRAM[i + 1] & 0x7C) << 1;
            return Color.FromArgb(r, g, b);
        }

        public static void SetCGRAMColor(int c, Color color)
        {
            int i = (2 * c) & 0x1FF;
            int val = ((color.B >> 3) << 10) | ((color.G >> 3) << 5) | (color.R >> 3);
            CGRAM[i] = (byte)val;
            CGRAM[i + 1] = (byte)(val >> 8);
            Project.unsavedChanges = true;
        }

        public static int GetCGRAMByte(int b)
        {
            return 0xFF & CGRAM[b & 0x1FF];
        }

        public static int GetCGRAMWord(int w)
        {
            int b = (2 * w) & 0x1FF;
            return 0xFFFF & (CGRAM[b] | (CGRAM[b + 1] << 8));
        }

        public static void SetCGRAMByte(int b, int d)
        {
            CGRAM[b & 0x1FF] = (byte)d;
            Project.unsavedChanges = true;
        }

        public static void SetCGRAMWord(int w, int d)
        {
            int b = (2 * w) & 0x1FF;
            CGRAM[b] = (byte)d;
            CGRAM[b + 1] = (byte)(d >> 8);
            Project.unsavedChanges = true;
        }

        public static int GetVRAMByte(int b)
        {
            return 0xFF & VRAM[b & 0xFFFF];
        }

        public static int GetVRAMWord(int w)
        {
            int b = (2 * w) & 0xFFFF;
            return 0xFFFF & (VRAM[b] | (VRAM[b + 1] << 8));
        }

        public static void SetVRAMByte(int b, int d)
        {
            VRAM[b & 0xFFFF] = (byte)d;
            Project.unsavedChanges = true;
        }

        public static void SetVRAMWord(int w, int d)
        {
            int b = (2 * w) & 0xFFFF;
            VRAM[b] = (byte)d;
            VRAM[b + 1] = (byte)(d >> 8);
            Project.unsavedChanges = true;
        }

        public static int GetOAMByte(int b)
        {
            int i = b & 0x3FF;
            return 0xFF & OAM[i < 0x200 ? i : 0x200 + (i & 0x1F)];
        }

        public static int GetOAMWord(int w)
        {
            int i = (2 * w) & 0x3FF;
            int b = i < 0x200 ? i : 0x200 + (i & 0x1F);
            return 0xFFFF & (OAM[b] | (OAM[b + 1] << 8));
        }

        public static void SetOAMByte(int b, int d)
        {
            int i = b & 0x3FF;
            OAM[i < 0x200 ? i : 0x200 + (i & 0x1F)] = (byte)d;
            Project.unsavedChanges = true;
        }

        public static void SetOAMWord(int w, int d)
        {
            int i = (2 * w) & 0x3FF;
            int b = i < 0x200 ? i : 0x200 + (i & 0x1F);
            OAM[b] = (byte)d;
            OAM[b + 1] = (byte)(d >> 8);
            Project.unsavedChanges = true;
        }

        public static int GetPPUReg(int i)
        {
            return 0xFFFF & PPURegs[i];
        }

        public static void SetPPURegBits(int i, int mask, int d)
        {
            PPURegs[i] = (short)((PPURegs[i] & ~mask) | (mask & d));
            Project.unsavedChanges = true;
        }
    }
}
