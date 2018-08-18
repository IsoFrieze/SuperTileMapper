using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTileMapper
{
    public static class Data
    {
        public static int[] VRAM = new int[0x8000];
        public static int[] CGRAM = new int[0x100];
        public static int[] OAM = new int[0x110];
        public static int[] PPURegs = new int[0x34];

        public static int[] lastVRAM = new int[0x8000];
        public static int[] lastCGRAM = new int[0x100];
        public static int[] lastOAM = new int[0x110];
        public static int[] lastPPURegs = new int[0x34];
    }
}
