using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTileMapper
{
    public static class SNESGraphics
    {
        public static int[] totalTiles = new int[] { 0x1000, 0x800, 0x400, 0x100 };
        public static int[] bytesPerTile = new int[] { 0x10, 0x20, 0x40, 0x80 };
        public static int[] colorsPerPalette = new int[] { 4, 0x10, 0x100, 0x100 };
        public static int[] palettesPerBPP = new int[] { 0x20, 0x10, 1, 1 };

        static Bitmap[][] indexedGraphics = new Bitmap[][]
        {
            new Bitmap[] {
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[0] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            },
            new Bitmap[] {
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed),
                new Bitmap(8 * 0x10, 8 * totalTiles[1] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            },
            new Bitmap[] {
                new Bitmap(8 * 0x10, 8 * totalTiles[2] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            },
            new Bitmap[] {
                new Bitmap(8 * 0x10, 8 * totalTiles[3] / 0x10, System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            }
        };

        public static void UpdateTile(int bpp, int tile)
        {
            for (int i = 0; i < indexedGraphics[bpp].Length; i++)
            {
                Bitmap img = indexedGraphics[bpp][i];
                BitmapData data = img.LockBits(new Rectangle(8 * (tile % 0x10), 8 * (tile / 0x10), 8, 8), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

                int vram = tile * bytesPerTile[bpp];
                int stride = data.Stride < 0 ? -data.Stride : data.Stride;
                unsafe
                {
                    byte* ptr = (byte*)data.Scan0;

                    for (int py = 0; py < 8; py++)
                    {
                        for (int px = 0; px < 8; px++)
                        {
                            if (bpp == 3)
                            {
                                ptr[px] = (byte)Data.GetVRAMByte((vram + 2 * (py * 8 + px) + 1));
                            }
                            else
                            {
                                // TODO optimize this
                                int b0 = 0x01 & Data.GetVRAMByte(2 * py + 0x00 + vram + 0) >> (7 - px);
                                int b1 = 0x01 & Data.GetVRAMByte(2 * py + 0x00 + vram + 1) >> (7 - px);
                                int b2 = 0x01 & Data.GetVRAMByte(2 * py + 0x10 + vram + 0) >> (7 - px);
                                int b3 = 0x01 & Data.GetVRAMByte(2 * py + 0x10 + vram + 1) >> (7 - px);
                                int b4 = 0x01 & Data.GetVRAMByte(2 * py + 0x20 + vram + 0) >> (7 - px);
                                int b5 = 0x01 & Data.GetVRAMByte(2 * py + 0x20 + vram + 1) >> (7 - px);
                                int b6 = 0x01 & Data.GetVRAMByte(2 * py + 0x30 + vram + 0) >> (7 - px);
                                int b7 = 0x01 & Data.GetVRAMByte(2 * py + 0x30 + vram + 1) >> (7 - px);

                                int xx = b0 + 2 * b1;
                                if (bpp > 0) xx += 4 * b2 + 8 * b3;
                                if (bpp > 1) xx += 0x10 * b4 + 0x20 * b5 + 0x40 * b6 + 0x80 * b7;

                                ptr[px] = (byte)(xx);
                            }
                        }

                        ptr += stride;
                    }
                }
                img.UnlockBits(data);
            }
        }

        public static void UpdateColor(int bpp, int color)
        {
            int c = color / colorsPerPalette[bpp];
            if (c < palettesPerBPP[bpp])
            {
                ColorPalette pal = indexedGraphics[bpp][c].Palette;
                pal.Entries[color % colorsPerPalette[bpp]] = Color.FromArgb(0xFF, Data.GetCGRAMColor(color));
                indexedGraphics[bpp][c].Palette = pal;
            }
        }

        public static void UpdatePalette(int bpp, int palette)
        {
            for (int i = 0; i < colorsPerPalette[bpp]; i++)
            {
                UpdateColor(bpp, colorsPerPalette[bpp] * palette + i);
            }
        }

        public static Color GetTransparency(int cgram, int transparency)
        {
            switch (transparency)
            {
                case 0: return Data.GetCGRAMColor(cgram);
                case 1: return Data.GetCGRAMColor(0);
                case 2: return Color.Black;
                case 3: return Color.White;
            }
            return Color.Black;
        }

        public static void UpdateTransparency(int bpp, int cgram, int transparency)
        {
            Color back = GetTransparency(cgram, transparency);

            int c = cgram / colorsPerPalette[bpp];
            ColorPalette pal = indexedGraphics[bpp][c].Palette;
            pal.Entries[0] = Color.FromArgb(transparency < 0 ? 0 : 0xFF, back);
            indexedGraphics[bpp][c].Palette = pal;
        }

        public static void UpdateAllPalettes()
        {
            for (int bpp = 0; bpp < 4; bpp++)
            {
                for (int c = 0; c < palettesPerBPP[bpp]; c++)
                {
                    UpdatePalette(bpp, c);
                }
            }
        }

        public static void UpdateAllTiles()
        {
            for (int bpp = 0; bpp < 4; bpp++)
            {
                for (int i = 0; i < totalTiles[bpp]; i++)
                {
                    UpdateTile(bpp, i);
                }
            }
        }

        public static void UpdateAll()
        {
            UpdateAllPalettes();
            UpdateAllTiles();
        }

        public static void Clear8x8Tile(int cgram, Bitmap img, int x, int y, int zoom, int transparency)
        {
            Color color = GetTransparency(cgram, transparency);

            using (Graphics g = Graphics.FromImage(img))
            {
                g.FillRectangle(new SolidBrush(color), new Rectangle(x * zoom, y * zoom, 8 * zoom, 8 * zoom));
            }
        }

        public static void Draw8x8Tile(int vram, int bpp, bool h, bool v, int cgram, Bitmap img, int x, int y, int zoom, int transparency)
        {
            int c = cgram / colorsPerPalette[bpp];
            int tile = (vram % Data.VRAM_SIZE) / bytesPerTile[bpp];

            UpdateTransparency(bpp, cgram, transparency);

            using (Graphics g = Graphics.FromImage(img))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                g.DrawImage(
                    indexedGraphics[bpp][c],
                    new Rectangle((h ? x + 8 : x) * zoom, (v ? y + 8 : y) * zoom, (h ? -1 : 1) * 8 * zoom, (v ? -1 : 1) * 8 * zoom),
                    new Rectangle(8 * (tile % 0x10), 8 * (tile / 0x10), 8, 8),
                    GraphicsUnit.Pixel);
            }
        }

        public static void DrawObject(int vram, bool h, bool v, int bw, int bh, int cgram, Bitmap img, int x, int y, int zoom)
        {
            int s = bytesPerTile[1];
            for (int ty = 0; ty < bh; ty++)
            {
                for (int tx = 0; tx < bw; tx++)
                {
                    int tile = (0xE000 & vram) | (0x1FFF & (vram + ty * s * 0x10 + tx * s));
                    Draw8x8Tile(
                        tile, 1, h, v, cgram, img,
                        x + 8 * (h ? bw - tx - 1 : tx),
                        y + 8 * (v ? (bh - ty - 1 + (bw == bh ? 0 : bh / 2)) % bh : ty),
                        zoom, -1);
                }
            }
        }
    }
}
