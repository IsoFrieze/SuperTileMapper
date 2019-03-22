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
        public static int[,] Endianness = new int[,] { { 0, 0 }, { 1, -1 } };

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
            string hex = (val & (digits == 4 ? 0xFFFF : (digits == 3 ? 0xFFF : 0xFF))).ToString("X");
            int idx = digits - hex.Length;
            return "00000000".Substring(0, idx < 0 ? 0 : idx) + hex;
        }

        public static int clamp(int val, int min, int max)
        {
            return (val < min ? min : (val > max ? max : val));
        }

        public static void Draw8x8Tile(int vram, int bpp, bool h, bool v, int cgram, Bitmap img, int x, int y, int zoom, int transparency)
        {
            if (bpp == 3)
            {
                for (int py = 0; py < 8; py++)
                {
                    for (int px = 0; px < 8; px++)
                    {
                        int xx = Data.VRAM[(vram + 2 * (py * 8 + px) + 1) % Data.VRAM.Length];

                        for (int zy = 0; zy < zoom; zy++)
                        {
                            for (int zx = 0; zx < zoom; zx++)
                            {
                                img.SetPixel(
                                    (x + (h ? 7 - px : px)) * zoom + zx,
                                    (y + (v ? 7 - py : py)) * zoom + zy,
                                    Data.GetCGRAMColor(cgram + xx)
                                    );
                            }
                        }
                    }
                }
            } else
            {
                Color back = Color.White;
                switch (transparency)
                {
                    case 1: back = Data.GetCGRAMColor(0); break;
                    case 2: back = Color.Black; break;
                    case 3: back = Color.White; break;
                }

                for (int py = 0; py < 8; py++)
                {
                    for (int px = 0; px < 8; px++)
                    {
                        int b0 = 0x01 & Data.VRAM[(2 * py + 0x00 + vram + 0) % Data.VRAM.Length] >> (7 - px);
                        int b1 = 0x01 & Data.VRAM[(2 * py + 0x00 + vram + 1) % Data.VRAM.Length] >> (7 - px);
                        int b2 = 0x01 & Data.VRAM[(2 * py + 0x10 + vram + 0) % Data.VRAM.Length] >> (7 - px);
                        int b3 = 0x01 & Data.VRAM[(2 * py + 0x10 + vram + 1) % Data.VRAM.Length] >> (7 - px);
                        int b4 = 0x01 & Data.VRAM[(2 * py + 0x20 + vram + 0) % Data.VRAM.Length] >> (7 - px);
                        int b5 = 0x01 & Data.VRAM[(2 * py + 0x20 + vram + 1) % Data.VRAM.Length] >> (7 - px);
                        int b6 = 0x01 & Data.VRAM[(2 * py + 0x30 + vram + 0) % Data.VRAM.Length] >> (7 - px);
                        int b7 = 0x01 & Data.VRAM[(2 * py + 0x30 + vram + 1) % Data.VRAM.Length] >> (7 - px);

                        int xx = b0 + 2 * b1;
                        if (bpp > 0) xx += 4 * b2 + 8 * b3;
                        if (bpp > 1) xx += 0x10 * b4 + 0x20 * b5 + 0x40 * b6 + 0x80 * b7;

                        for (int zy = 0; zy < zoom; zy++)
                        {
                            for (int zx = 0; zx < zoom; zx++)
                            {
                                img.SetPixel(
                                    (x + (h ? 7 - px : px)) * zoom + zx,
                                    (y + (v ? 7 - py : py)) * zoom + zy,
                                    xx == 0 && transparency != 0 ? back : Data.GetCGRAMColor(cgram + xx)
                                    );
                            }
                        }
                    }
                }
            }
        }
    }
}
