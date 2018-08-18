using System;
using System.Collections.Generic;
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
    }
}
