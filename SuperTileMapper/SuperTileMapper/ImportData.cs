using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTileMapper
{
    public partial class ImportData : Form
    {
        string name;
        byte[] destination;
        byte[] source;
        int destOffset = 0, srcOffset = 0;
        int len;
        int defLen;

        public ImportData(String loc, byte[] arr, int amount = -1, int step = -1, int fileStep = -1, int fileOffset = -1, int arrStep = -1, int arrOffset = -1, int endian = -1)
        {
            name = loc;
            destination = arr;
            defLen = amount;

            InitializeComponent();
            groupBox1.Text = groupBox1.Text.Replace("<x>", loc);
            label3.Text = label3.Text.Replace("<x>", loc);

            textBox2.Text = "$" + Util.DecToHex(defLen == -1 ? 0 : defLen, 0);
            comboBox1.SelectedIndex = step == -1 ? 0 : step;
            comboBox2.SelectedIndex = fileStep == -1 ? 0 : fileStep;
            textBox3.Text = "$" + Util.DecToHex(fileOffset == -1 ? 0 : fileOffset, 4);
            comboBox3.SelectedIndex = arrStep == -1 ? 1 : arrStep;
            textBox4.Text = "$" + Util.DecToHex(arrOffset == -1 ? 0 : arrOffset, 4);
            comboBox4.SelectedIndex = endian == -1 ? 0 : endian;
        }

        private void ImportData_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int transfer = comboBox1.SelectedIndex;
                int endian = comboBox4.SelectedIndex;

                if (transfer >= 8) throw new Exception("Must insert 'bytes' or 'words' only!");
                if (comboBox2.SelectedIndex >= 2) throw new Exception("Must access 'bytes' or 'words' only from file!");
                if (comboBox3.SelectedIndex >= 2) throw new Exception("Must access 'bytes' or 'words' only from " + name + "!");
                if (endian >= 2) throw new Exception("Must use 'little endian' or 'big endian' only!");
                if (!Util.TryHexOrDecToDec(textBox2.Text, out len)) throw new Exception("Unknown value for number of bytes or words to insert!");
                if (!Util.TryHexOrDecToDec(textBox3.Text, out srcOffset)) throw new Exception("Unknown value for file offset!");
                if (!Util.TryHexOrDecToDec(textBox4.Text, out destOffset)) throw new Exception("Unknown value for " + name + " offset!");

                if (transfer >= 3) len *= 2;
                if (comboBox2.SelectedIndex == 1) srcOffset *= 2;
                if (comboBox3.SelectedIndex == 1) destOffset *= 2;

                if (len > destination.Length) throw new Exception("Amount of data to insert exceeds the size of " + name + "!");
                if (srcOffset >= source.Length) throw new Exception("The source address exceeds the size of the file!");
                if (destOffset >= destination.Length) throw new Exception("The destination address exceeds the size of " + name + "!");
                if (srcOffset + len + ((endian == 1 && len % 2 == 1) ? 1 : 0) > source.Length) throw new Exception("The amount of data to insert exceeds the size of the file!");
                // I don't know why anyone would import an odd amount of bytes via big endian but who knows?

                // Trust me on this voodoo, I used k-maps
                int srcStep = transfer >= 4 ? 2 : 1;
                int destStep = (transfer % 3 >= 1) && (transfer < 6) ? 2 : 1;
                bool endianFlip = (endian == 1) && (transfer == 0 || transfer == 3);
                if (transfer == 4 || transfer == 6 + endian) srcOffset++;
                if (transfer % 3 == endian + 1 && transfer < 6) destOffset++;

                for (int i = 0, j = 0; i < len; i += srcStep, j += destStep)
                {
                    destination[(destOffset + j) % destination.Length] = source[srcOffset + i + (endianFlip ? Util.Endianness[i % 2] : 0)];
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                try
                {
                    source = File.ReadAllBytes(file);

                    if (defLen < 0)
                    {
                        len = source.Length >= destination.Length ? destination.Length : source.Length;
                        textBox2.Text = "$" + Util.DecToHex(len, 0);
                    }

                    textBox1.Text = file;
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    textBox4.Enabled = true;
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = true;
                    comboBox4.Enabled = true;
                    button2.Enabled = true;
                } catch (Exception)
                {

                }
            }
        }
    }
}
