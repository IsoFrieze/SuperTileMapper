using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTileMapper
{
    public static class Util
    {
        public static int[,] Endianness = new int[,] { { 0, 0 }, { 1, -1 } };

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

        public static string DecToHex(int val)
        {
            return val.ToString("X");
        }
    }
}
