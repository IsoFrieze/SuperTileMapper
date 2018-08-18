using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTileMapper
{
    public static class Data
    {
        static int[] VRAM = new int[0x8000];
        static int[] CGRAM = new int[0x100];
        static int[] OAM = new int[0x110];
        static int[] PPURegs = new int[0x34];

        static int[] lastVRAM = new int[0x8000];
        static int[] lastCGRAM = new int[0x100];
        static int[] lastOAM = new int[0x110];
        static int[] lastPPURegs = new int[0x34];
    }
}
