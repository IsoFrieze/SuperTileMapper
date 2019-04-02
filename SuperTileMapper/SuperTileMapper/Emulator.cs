using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTileMapper
{
    public static class Emulator
    {
        public static bool TryImportEmulator(string filename)
        {
            try
            {
                byte[] data = File.ReadAllBytes(filename);

                if (
                    data[0] == 0x5A && data[1] == 0x53 && data[2] == 0x4E && data[3] == 0x45 && data[4] == 0x53 &&
                    data[0x17] == 0x31 && data[0x18] == 0x34 && data[0x19] == 0x33 && data[0x1A] == 0x1A && data[0x1B] == 0x8F &&
                    data.Length >= 0x30C13
                    )
                {
                    return ImportZSNES(data);
                } else
                {
                    byte[] unzipped = TryUnzip(data);
                    if (unzipped != null)
                    {
                        if (
                            unzipped[0] == 0x23 && unzipped[1] == 0x21 && unzipped[2] == 0x73 && unzipped[3] == 0x39 && unzipped[4] == 0x78 && unzipped[5] == 0x73 && unzipped[6] == 0x6E &&
                            unzipped[9] == 0x30 && unzipped[10] == 0x30 && unzipped[11] == 0x31 && unzipped[12] == 0x31
                            )
                        {
                            return ImportSNES9x(unzipped);
                        }
                    }
                }

                throw new Exception("This is not a save state file SuperTileMapper can import currently.");
            } catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static bool ImportZSNES(byte[] data)
        {
            Data.SetPPURegBits(0x00, 0x8F, (data[0x4E] & 0x0F) | (data[0x50] & 0x80));
            int objBase = (data[0x52] << 8) | (data[0x53] << 16);
            int objBase2 = (data[0x56] << 8) | (data[0x57] << 16);
            Data.SetPPURegBits(0x01, 0x07, objBase >> 14);
            int objOffset = (objBase2 - objBase) & 0x01FFFF;
            Data.SetPPURegBits(0x01, 0x18, objOffset >> 10);
            int objSmall = data[0x59], objLarge = data[0x5A];
            if (objSmall == 0x01 && objLarge == 0x10) Data.SetPPURegBits(0x01, 0xE0, 0x20);
            else if (objSmall == 0x01 && objLarge == 0x40) Data.SetPPURegBits(0x01, 0xE0, 0x40);
            else if (objSmall == 0x04 && objLarge == 0x10) Data.SetPPURegBits(0x01, 0xE0, 0x60);
            else if (objSmall == 0x04 && objLarge == 0x40) Data.SetPPURegBits(0x01, 0xE0, 0x80);
            else if (objSmall == 0x10 && objLarge == 0x40) Data.SetPPURegBits(0x01, 0xE0, 0xA0);
            else Data.SetPPURegBits(0x01, 0xE0, 0x00);
            Data.SetPPURegBits(0x02, 0xFE, data[0x65] << 1);
            Data.SetPPURegBits(0x05, 0x07, data[0x66]);
            Data.SetPPURegBits(0x05, 0x08, data[0x67] << 3);
            Data.SetPPURegBits(0x05, 0xF0, data[0x68] << 4);
            Data.SetPPURegBits(0x06, 0x0F, data[0x69]);
            Data.SetPPURegBits(0x06, 0xF0, data[0x6A] << 4);
            Data.SetPPURegBits(0x07, 0xFC, data[0x6C] >> 1);
            Data.SetPPURegBits(0x08, 0xFC, data[0x6E] >> 1);
            Data.SetPPURegBits(0x09, 0xFC, data[0x70] >> 1);
            Data.SetPPURegBits(0x0A, 0xFC, data[0x72] >> 1);
            Data.SetPPURegBits(0x07, 0x03, data[0x8B]);
            Data.SetPPURegBits(0x08, 0x03, data[0x8C]);
            Data.SetPPURegBits(0x09, 0x03, data[0x8D]);
            Data.SetPPURegBits(0x0A, 0x03, data[0x8E]);
            Data.SetPPURegBits(0x0B, 0x0F, data[0x90] >> 5);
            Data.SetPPURegBits(0x0B, 0xF0, data[0x92] >> 1);
            Data.SetPPURegBits(0x0C, 0x0F, data[0x94] >> 5);
            Data.SetPPURegBits(0x0C, 0xF0, data[0x96] >> 1);
            Data.SetPPURegBits(0x0D, 0x1FFF, (data[0x98] << 8) | data[0x97]);
            Data.SetPPURegBits(0x0F, 0x03FF, (data[0x9A] << 8) | data[0x99]);
            Data.SetPPURegBits(0x11, 0x03FF, (data[0x9C] << 8) | data[0x9B]);
            Data.SetPPURegBits(0x13, 0x03FF, (data[0x9E] << 8) | data[0x9D]);
            Data.SetPPURegBits(0x0E, 0x1FFF, (data[0xA2] << 8) | data[0xA1]);
            Data.SetPPURegBits(0x10, 0x03FF, (data[0xA4] << 8) | data[0xA3]);
            Data.SetPPURegBits(0x12, 0x03FF, (data[0xA6] << 8) | data[0xA5]);
            Data.SetPPURegBits(0x14, 0x03FF, (data[0xA8] << 8) | data[0xA7]);
            Data.SetPPURegBits(0x2C, 0x1F, data[0xB4]);
            Data.SetPPURegBits(0x2D, 0x1F, data[0xB5]);
            Data.SetPPURegBits(0x33, 0x04, data[0xB7] == 0xEF ? 0x04 : 0x00);
            Data.SetPPURegBits(0x26, 0xFF, data[0xC7]);
            Data.SetPPURegBits(0x27, 0xFF, data[0xC8]);
            Data.SetPPURegBits(0x28, 0xFF, data[0xC9]);
            Data.SetPPURegBits(0x29, 0xFF, data[0xCA]);
            Data.SetPPURegBits(0x23, 0x0F, data[0xCB]);
            Data.SetPPURegBits(0x23, 0xF0, data[0xCC] << 4);
            Data.SetPPURegBits(0x24, 0x0F, data[0xCD]);
            Data.SetPPURegBits(0x24, 0xF0, data[0xCE] << 4);
            Data.SetPPURegBits(0x25, 0x0F, data[0xCF]);
            Data.SetPPURegBits(0x25, 0xF0, data[0xD0] << 4);
            Data.SetPPURegBits(0x2A, 0xFF, data[0xD1]);
            Data.SetPPURegBits(0x2B, 0x0F, data[0xD2]);
            Data.SetPPURegBits(0x2E, 0x1F, data[0xD3]);
            Data.SetPPURegBits(0x2F, 0x1F, data[0xD4]);
            Data.SetPPURegBits(0x1A, 0xC3, data[0xD5]);
            Data.SetPPURegBits(0x1B, 0xFFFF, (data[0xD7] << 8) | data[0xD6]);
            Data.SetPPURegBits(0x1C, 0xFFFF, (data[0xD9] << 8) | data[0xD8]);
            Data.SetPPURegBits(0x1D, 0xFFFF, (data[0xDB] << 8) | data[0xDA]);
            Data.SetPPURegBits(0x1E, 0xFFFF, (data[0xDD] << 8) | data[0xDC]);
            Data.SetPPURegBits(0x1F, 0x1FFF, (data[0xDF] << 8) | data[0xDE]);
            Data.SetPPURegBits(0x20, 0x1FFF, (data[0xE1] << 8) | data[0xE0]);
            // hdma data here: data[0x171-0x208]
            Data.SetPPURegBits(0x32, 0x001F, data[0x20A]);
            Data.SetPPURegBits(0x32, 0x03E0, data[0x20B] << 5);
            Data.SetPPURegBits(0x32, 0x7C00, data[0x20C] << 10);
            Data.SetPPURegBits(0x30, 0xF3, data[0x20E]);
            Data.SetPPURegBits(0x31, 0xFF, data[0x20F]);
            Data.SetPPURegBits(0x33, 0xCB, 0);
            for (int i = 0; i < Data.OAM_SIZE; i++) Data.SetOAMByte(i, data[0x218 + i]);
            for (int i = 0; i < Data.CGRAM_SIZE; i++) Data.SetCGRAMByte(i, data[0x618 + i]);
            for (int i = 0; i < Data.VRAM_SIZE; i++) Data.SetVRAMByte(i, data[0x20C13 + i]);
            return true;
        }

        private static bool ImportSNES9x(byte[] data)
        {
            int ptr = 0x0E;
            while (ptr + 3 < data.Length)
            {
                char c1 = (char)data[ptr], c2 = (char)data[ptr + 1], c3 = (char)data[ptr + 2];
                int count = (data[ptr + 4] - 0x30) * 100000 + (data[ptr + 5] - 0x30) * 10000 +
                    (data[ptr + 6] - 0x30) * 1000 + (data[ptr + 7] - 0x30) * 100 +
                    (data[ptr + 8] - 0x30) * 10 + (data[ptr + 9] - 0x30);
                ptr += 11;
                if (c1 == 'V' && c2 == 'R' && c3 == 'A')
                {
                    for (int i = 0; i < Data.VRAM_SIZE; i++) Data.SetVRAMByte(i, data[ptr + i]);
                }
                else if (c1 == 'D' && c2 == 'M' && c3 == 'A')
                {
                    // hdma data here
                }
                else if (c1 == 'P' && c2 == 'P' && c3 == 'U')
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Data.SetPPURegBits(0x07 + i, 0xFC, data[ptr + 0x0E + 11 * i + 0]);
                        Data.SetPPURegBits(0x0D + 2 * i, 0x03FF, (data[ptr + 0x0E + 11 * i + 2] << 8) | data[ptr + 0x0E + 11 * i + 3]);
                        Data.SetPPURegBits(0x0E + 2 * i, 0x03FF, (data[ptr + 0x0E + 11 * i + 4] << 8) | data[ptr + 0x0E + 11 * i + 5]);
                        Data.SetPPURegBits(0x05, 0x10 << i, data[ptr + 0x0E + 11 * i + 6] == 0 ? 0 : 0xFF);
                        Data.SetPPURegBits(0x0B + i / 2, 0x0F << (4 * (i & 1)), data[ptr + 0x0E + 11 * i + 7] >> (4 * (~i & 1)));
                        Data.SetPPURegBits(0x07 + i, 0x03, data[ptr + 0x0E + 11 * i + 10]);
                    }
                    int mode = data[ptr + 0x3A];
                    Data.SetPPURegBits(0x05, 0x07, mode);
                    Data.SetPPURegBits(0x05, 0x08, data[ptr + 0x3B] << 3);
                    for (int i = 0; i < Data.CGRAM_SIZE; i++) Data.SetCGRAMByte(i, data[ptr + 0x40 + i + (i % 2 == 0 ? 1 : -1)]);

                    Data.SetPPURegBits(0x01, 0x03, data[ptr + 0x7C3] >> 6);
                    Data.SetPPURegBits(0x01, 0x18, data[ptr + 0x7C5] >> 2);
                    Data.SetPPURegBits(0x01, 0xE0, data[ptr + 0x7C7] << 5);

                    for (int i = 0; i < Data.OAM_SIZE; i++) Data.SetOAMByte(i, data[ptr + 0x7D3 + i]);
                    Data.SetPPURegBits(0x02, 0xFE, data[ptr + 0x9F3] << 1);

                    Data.SetPPURegBits(0x1A, 0x01, data[ptr + 0xA0A] != 0 ? 0x01 : 0);
                    Data.SetPPURegBits(0x1A, 0x02, data[ptr + 0xA0B] != 0 ? 0x02 : 0);
                    Data.SetPPURegBits(0x1A, 0xC0, data[ptr + 0xA0C] << 6);

                    Data.SetPPURegBits(0x1B, 0xFFFF, (data[ptr + 0xA0D] << 8) | (data[ptr + 0xA0E]));
                    Data.SetPPURegBits(0x1C, 0xFFFF, (data[ptr + 0xA0F] << 8) | (data[ptr + 0xA10]));
                    Data.SetPPURegBits(0x1D, 0xFFFF, (data[ptr + 0xA11] << 8) | (data[ptr + 0xA12]));
                    Data.SetPPURegBits(0x1E, 0xFFFF, (data[ptr + 0xA13] << 8) | (data[ptr + 0xA14]));
                    Data.SetPPURegBits(0x1F, 0x1FFF, (data[ptr + 0xA15] << 8) | (data[ptr + 0xA16]));
                    Data.SetPPURegBits(0x20, 0x1FFF, (data[ptr + 0xA17] << 8) | (data[ptr + 0xA18]));
                    if (mode == 7) Data.SetPPURegBits(0x0D, 0x1FFF, (data[ptr + 0xA19] << 8) | (data[ptr + 0xA1A]));
                    if (mode == 7) Data.SetPPURegBits(0x0E, 0x1FFF, (data[ptr + 0xA1B] << 8) | (data[ptr + 0xA1C]));

                    Data.SetPPURegBits(0x06, 0xF0, (data[ptr + 0xA1D] - 1) << 4);
                    Data.SetPPURegBits(0x06, 0x01, data[ptr + 0xA1F] != 0 ? 0x01 : 0);
                    Data.SetPPURegBits(0x06, 0x02, data[ptr + 0xA20] != 0 ? 0x02 : 0);
                    Data.SetPPURegBits(0x06, 0x04, data[ptr + 0xA21] != 0 ? 0x04 : 0);
                    Data.SetPPURegBits(0x06, 0x08, data[ptr + 0xA22] != 0 ? 0x08 : 0);

                    Data.SetPPURegBits(0x26, 0xFF, data[ptr + 0xA23]);
                    Data.SetPPURegBits(0x27, 0xFF, data[ptr + 0xA24]);
                    Data.SetPPURegBits(0x28, 0xFF, data[ptr + 0xA25]);
                    Data.SetPPURegBits(0x29, 0xFF, data[ptr + 0xA26]);

                    Data.SetPPURegBits(0x2A, 0x03, data[ptr + 0xA29]);
                    Data.SetPPURegBits(0x23, 0x02, data[ptr + 0xA2A] != 0 ? 0x02 : 0);
                    Data.SetPPURegBits(0x23, 0x08, data[ptr + 0xA2B] != 0 ? 0x08 : 0);
                    Data.SetPPURegBits(0x23, 0x01, data[ptr + 0xA2C] == 0 ? 0x01 : 0);
                    Data.SetPPURegBits(0x23, 0x04, data[ptr + 0xA2D] == 0 ? 0x04 : 0);
                    Data.SetPPURegBits(0x2A, 0x0C, data[ptr + 0xA2F] << 2);
                    Data.SetPPURegBits(0x23, 0x20, data[ptr + 0xA30] != 0 ? 0x20 : 0);
                    Data.SetPPURegBits(0x23, 0x80, data[ptr + 0xA31] != 0 ? 0x80 : 0);
                    Data.SetPPURegBits(0x23, 0x10, data[ptr + 0xA32] == 0 ? 0x10 : 0);
                    Data.SetPPURegBits(0x23, 0x40, data[ptr + 0xA33] == 0 ? 0x40 : 0);
                    Data.SetPPURegBits(0x2A, 0x30, data[ptr + 0xA35] << 4);
                    Data.SetPPURegBits(0x24, 0x02, data[ptr + 0xA36] != 0 ? 0x02 : 0);
                    Data.SetPPURegBits(0x24, 0x08, data[ptr + 0xA37] != 0 ? 0x08 : 0);
                    Data.SetPPURegBits(0x24, 0x01, data[ptr + 0xA38] == 0 ? 0x01 : 0);
                    Data.SetPPURegBits(0x24, 0x04, data[ptr + 0xA39] == 0 ? 0x04 : 0);
                    Data.SetPPURegBits(0x2A, 0xC0, data[ptr + 0xA3B] << 6);
                    Data.SetPPURegBits(0x24, 0x20, data[ptr + 0xA3C] != 0 ? 0x20 : 0);
                    Data.SetPPURegBits(0x24, 0x80, data[ptr + 0xA3D] != 0 ? 0x80 : 0);
                    Data.SetPPURegBits(0x24, 0x10, data[ptr + 0xA3E] == 0 ? 0x10 : 0);
                    Data.SetPPURegBits(0x24, 0x40, data[ptr + 0xA3F] == 0 ? 0x40 : 0);
                    Data.SetPPURegBits(0x2B, 0x03, data[ptr + 0xA41]);
                    Data.SetPPURegBits(0x25, 0x02, data[ptr + 0xA42] != 0 ? 0x02 : 0);
                    Data.SetPPURegBits(0x25, 0x08, data[ptr + 0xA43] != 0 ? 0x08 : 0);
                    Data.SetPPURegBits(0x25, 0x01, data[ptr + 0xA44] == 0 ? 0x01 : 0);
                    Data.SetPPURegBits(0x25, 0x04, data[ptr + 0xA45] == 0 ? 0x04 : 0);
                    Data.SetPPURegBits(0x2B, 0x0C, data[ptr + 0xA47] << 2);
                    Data.SetPPURegBits(0x25, 0x20, data[ptr + 0xA48] != 0 ? 0x20 : 0);
                    Data.SetPPURegBits(0x25, 0x80, data[ptr + 0xA49] != 0 ? 0x80 : 0);
                    Data.SetPPURegBits(0x25, 0x10, data[ptr + 0xA4A] == 0 ? 0x10 : 0);
                    Data.SetPPURegBits(0x25, 0x40, data[ptr + 0xA4B] == 0 ? 0x40 : 0);

                    Data.SetPPURegBits(0x00, 0x80, data[ptr + 0xA4C] << 7);
                    Data.SetPPURegBits(0x32, 0x001F, data[ptr + 0xA4D]);
                    Data.SetPPURegBits(0x32, 0x03E0, data[ptr + 0xA4E] << 5);
                    Data.SetPPURegBits(0x32, 0x7C00, data[ptr + 0xA4F] << 10);
                    Data.SetPPURegBits(0x00, 0x0F, data[ptr + 0xA50]);
                }
                else if (c1 == 'F' && c2 == 'I' && c3 == 'L')
                {
                    Data.SetPPURegBits(0x2C, 0x1F, data[ptr + 0x212C]);
                    Data.SetPPURegBits(0x2D, 0x1F, data[ptr + 0x212D]);
                    Data.SetPPURegBits(0x2E, 0x1F, data[ptr + 0x212E]);
                    Data.SetPPURegBits(0x2F, 0x1F, data[ptr + 0x212F]);

                    Data.SetPPURegBits(0x30, 0xF3, data[ptr + 0x2130]);
                    Data.SetPPURegBits(0x31, 0xFF, data[ptr + 0x2131]);
                    Data.SetPPURegBits(0x33, 0xCF, data[ptr + 0x2133]);
                }
                ptr += count;
            }
            return true;
        }

        private static bool ImportBSNES(byte[] data)
        {
            return false;
        }

        private static bool ImportBizhawk(byte[] data)
        {
            return false;
        }

        private static bool ImportLSNES(byte[] data)
        {
            return false;
        }

        // https://stackoverflow.com/questions/33119119/unzip-byte-array-in-c-sharp
        private static byte[] TryUnzip(byte[] data)
        {
            try
            {
                using (MemoryStream comp = new MemoryStream(data))
                using (GZipStream gzip = new GZipStream(comp, CompressionMode.Decompress))
                using (MemoryStream res = new MemoryStream())
                {
                    gzip.CopyTo(res);
                    return res.ToArray();
                }
            } catch (Exception e)
            {
                return null;
            }
        }
    }
}
