using System;
using System.Collections.Generic;
using System.IO;
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
                    data.Length == 0x4375B
                    )
                {
                    return ImportZSNES(data);
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
            Data.SetPPURegBits(0x03, 0xFE, data[0x65] << 1);
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

        private static void ImportSNES9x(byte[] data)
        {

        }

        private static void ImportBSNES(byte[] data)
        {

        }

        private static void ImportBizhawk(byte[] data)
        {

        }

        private static void ImportLSNES(byte[] data)
        {

        }
    }
}
