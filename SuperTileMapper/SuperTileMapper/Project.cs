using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTileMapper
{
    public static class Project
    {
        public static string currentFile = null;
        public static bool unsavedChanges = false;
        public static byte[] watermark = new byte[] { 0x53, 0x75, 0x70, 0x65, 0x72, 0x54, 0x69, 0x6C, 0x65, 0x4D, 0x61, 0x70, 0x70, 0x65, 0x72 };

        public static void NewProject()
        {
            for (int i = 0; i < Data.VRAM_SIZE; i++) Data.SetVRAMByte(i, 0);
            for (int i = 0; i < Data.CGRAM_SIZE; i++) Data.SetCGRAMByte(i, 0);
            for (int i = 0; i < Data.OAM_SIZE; i++) Data.SetOAMByte(i, 0);
            for (int i = 0; i < 0x34; i++) Data.SetPPURegBits(i, 0xFFFF, 0);

            Data.SetPPURegBits(0x00, 0x000F, 0x000F); // full brightness
            Data.SetPPURegBits(0x1B, 0xFFFF, 0x0100); // identity
            Data.SetPPURegBits(0x1E, 0xFFFF, 0x0100); // matrix
            Data.SetPPURegBits(0x30, 0x0030, 0x0030); // disable color math

            unsavedChanges = false;
            currentFile = null;
        }

        public static void SaveProject(string filename)
        {
            try
            {
                int headerSize = 0x100;
                byte[] data = SaveVersion0();
                byte[] everything = new byte[headerSize + data.Length];
                everything[0] = 0; // version
                watermark.CopyTo(everything, 1);
                data.CopyTo(everything, headerSize);

                File.WriteAllBytes(filename, everything);
                unsavedChanges = false;
                currentFile = filename;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static byte[] SaveVersion0()
        {
            byte[] data = new byte[Data.VRAM_SIZE + Data.CGRAM_SIZE + Data.OAM_SIZE + 2 * 0x34];

            int locationVRAM = 0;
            int locationCGRAM = locationVRAM + Data.VRAM_SIZE;
            int locationOAM = locationCGRAM + Data.CGRAM_SIZE;
            int locationPPUReg = locationOAM + Data.OAM_SIZE;

            data[0x10] = (byte)locationVRAM;
            data[0x11] = (byte)(locationVRAM >> 8);
            data[0x12] = (byte)(locationVRAM >> 16);
            data[0x13] = (byte)(locationVRAM >> 24);
            data[0x14] = (byte)locationCGRAM;
            data[0x15] = (byte)(locationCGRAM >> 8);
            data[0x16] = (byte)(locationCGRAM >> 16);
            data[0x17] = (byte)(locationCGRAM >> 24);
            data[0x18] = (byte)locationOAM;
            data[0x19] = (byte)(locationOAM >> 8);
            data[0x1A] = (byte)(locationOAM >> 16);
            data[0x1B] = (byte)(locationOAM >> 24);
            data[0x1C] = (byte)locationPPUReg;
            data[0x1D] = (byte)(locationPPUReg >> 8);
            data[0x1E] = (byte)(locationPPUReg >> 16);
            data[0x1F] = (byte)(locationPPUReg >> 24);

            Data.GetVRAMArray().CopyTo(data, locationVRAM);
            Data.GetCGRAMArray().CopyTo(data, locationCGRAM);
            Data.GetOAMArray().CopyTo(data, locationOAM);
            for (int i = 0; i < 0x34; i++)
            {
                int val = Data.GetPPUReg(i);
                data[locationPPUReg + 2 * i + 0] = (byte)val;
                data[locationPPUReg + 2 * i + 1] = (byte)(val >> 8);
            }

            return data;
        }

        public static bool TryOpenProject(string filename)
        {
            try
            {
                byte[] raw = File.ReadAllBytes(filename);

                for (int i = 0; i < watermark.Length; i++)
                {
                    if (raw[i + 1] != watermark[i])
                    {
                        throw new Exception("This is not a valid SuperTileMapper file!");
                    }
                }

                byte version = raw[0];

                switch (version)
                {
                    case 0: OpenVersion0(raw); break;
                    default: throw new Exception("This is not a valid SuperTileMapper file!");
                }

                unsavedChanges = false;
                currentFile = filename;
                return true;
            } catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static void OpenVersion0(byte[] raw)
        {
            int headerSize = 0x100;
            int locationVRAM = headerSize;
            int locationCGRAM = locationVRAM + Data.VRAM_SIZE;
            int locationOAM = locationCGRAM + Data.CGRAM_SIZE;
            int locationPPUReg = locationOAM + Data.OAM_SIZE;

            if (raw.Length != locationPPUReg + 2 * 0x34)
            {
                throw new Exception("This is not a valid SuperTileMapper file!");
            }

            for (int i = 0; i < Data.VRAM_SIZE; i++) Data.SetVRAMByte(i, raw[locationVRAM + i]);
            for (int i = 0; i < Data.CGRAM_SIZE; i++) Data.SetCGRAMByte(i, raw[locationCGRAM + i]);
            for (int i = 0; i < Data.OAM_SIZE; i++) Data.SetOAMByte(i, raw[locationOAM + i]);
            for (int i = 0; i < 0x34; i++)
            {
                int regLow = 0xFF & raw[locationPPUReg + 2 * i + 0];
                int regHigh = 0xFF & raw[locationPPUReg + 2 * i + 1];
                int regWord = (regHigh << 8) | regLow;
                Data.SetPPURegBits(i, 0xFFFF, regWord);
            }
        }
    }
}
