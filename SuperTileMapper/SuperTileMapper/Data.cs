using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTileMapper
{
    public static class Data
    {
        public static byte[] VRAM = new byte[2*0x8000];
        public static byte[] CGRAM = new byte[2*0x100];
        public static byte[] OAM = new byte[2*0x110];
        public static short[] PPURegs = new short[0x34];

        public static byte[] lastVRAM = new byte[2*0x8000];
        public static byte[] lastCGRAM = new byte[2*0x100];
        public static byte[] lastOAM = new byte[2*0x110];
        public static short[] lastPPURegs = new short[0x34];

        public static Color GetCGRAMColor(int c)
        {
            int i = 2 * c;
            int r = (Data.CGRAM[i] & 0x1F) << 3;
            int g = ((Data.CGRAM[i] & 0xE0) >> 2) | ((Data.CGRAM[i + 1] & 0x03) << 6);
            int b = (Data.CGRAM[i + 1] & 0x7C) << 1;
            return Color.FromArgb(r, g, b);
        }

        public static void saveAll()
        {
            for (int i = 0; i < VRAM.Length; i++) lastVRAM[i] = VRAM[i];
            for (int i = 0; i < CGRAM.Length; i++) lastCGRAM[i] = CGRAM[i];
            for (int i = 0; i < OAM.Length; i++) lastOAM[i] = OAM[i];
            for (int i = 0; i < PPURegs.Length; i++) lastPPURegs[i] = PPURegs[i];
        }

        public static void revertAll()
        {
            for (int i = 0; i < VRAM.Length; i++) VRAM[i] = lastVRAM[i];
            for (int i = 0; i < CGRAM.Length; i++) CGRAM[i] = lastCGRAM[i];
            for (int i = 0; i < OAM.Length; i++) OAM[i] = lastOAM[i];
            for (int i = 0; i < PPURegs.Length; i++) PPURegs[i] = lastPPURegs[i];
        }
    }
}
