using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTileMapper
{
    public static class Util
    {
        public static int[] Endianness = new int[] { 1, -1 };

        public static int[,,] OBJsizes = new int[,,] {
            { {8,8}, {16,16} },
            { {8,8}, {32,32} },
            { {8,8}, {64,64} },
            { {16,16}, {32,32} },
            { {16,16}, {64,64} },
            { {32,32}, {64,64} },
            { {16,32}, {32,64} },
            { {16,32}, {32,32} }
        };

        public static int HexOrDecToDec(string val)
        {
            if (val[0] == '$')
            {
                return int.Parse(val.Substring(1), System.Globalization.NumberStyles.HexNumber);
            } else
            {
                return int.Parse(val);
            }
        }

        public static bool TryHexOrDecToDec(string val, out int result)
        {
            try
            {
                result = HexOrDecToDec(val);
                return true;
            } catch (Exception)
            {
                result = -1;
                return false;
            }
        }

        public static string DecToHex(int val, int digits)
        {
            int mask = digits == 0 ? -1 : ~(-1 << (4 * digits));
            string hex = (val & mask).ToString("X");
            int idx = digits - hex.Length;
            return "00000000".Substring(0, idx < 0 ? 0 : idx) + hex;
        }

        public static int clamp(int val, int min, int max)
        {
            return (val < min ? min : (val > max ? max : val));
        }
    }
}
