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
    public partial class ExportData : Form
    {
        string name;
        byte[] source;
        string destination;
        int srcOffset = 0;
        int len;
        int defLen;

        public ExportData(String loc, byte[] arr, int amount = -1, int step = -1, int arrStep = -1, int arrOffset = -1, int endian = -1)
        {
            name = loc;
            source = arr;
            defLen = amount;

            InitializeComponent();
            groupBox1.Text = groupBox1.Text.Replace("<x>", loc);
            label2.Text = label2.Text.Replace("<x>", loc);

            textBox2.Text = "$" + Util.DecToHex(defLen == -1 ? 0 : defLen, 0);
            comboBox1.SelectedIndex = step == -1 ? 0 : step;
            comboBox2.SelectedIndex = arrStep == -1 ? 1 : arrStep;
            textBox3.Text = "$" + Util.DecToHex(arrOffset == -1 ? 0 : arrOffset, 4);
            comboBox3.SelectedIndex = endian == -1 ? 0 : endian;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                destination = saveFileDialog1.FileName;
                try
                {
                    if (defLen < 0)
                    {
                        len = source.Length;
                        textBox2.Text = "$" + Util.DecToHex(len, 0);
                    }

                    textBox1.Text = destination;
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = true;
                    button2.Enabled = true;
                }
                catch (Exception)
                {

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int transfer = comboBox1.SelectedIndex;
                int endian = comboBox3.SelectedIndex;

                if (transfer >= 8) throw new Exception("Must insert 'bytes' or 'words' only!");
                if (comboBox2.SelectedIndex >= 2) throw new Exception("Must access 'bytes' or 'words' only from " + name + "!");
                if (endian >= 2) throw new Exception("Must use 'little endian' or 'big endian' only!");
                if (!Util.TryHexOrDecToDec(textBox2.Text, out len)) throw new Exception("Unknown value for number of bytes or words to extract!");
                if (!Util.TryHexOrDecToDec(textBox3.Text, out srcOffset)) throw new Exception("Unknown value for " + name + " offset!");

                if (transfer >= 3) len *= 2;
                if (comboBox2.SelectedIndex == 1) srcOffset *= 2;

                if (len > source.Length) throw new Exception("Amount of data to extract exceeds the size of " + name + "!");
                if (srcOffset >= source.Length) throw new Exception("The source address exceeds the size of " + name + "!");

                // Trust me on this voodoo, I used k-maps
                int srcStep = transfer >= 4 ? 2 : 1;
                int destStep = (transfer % 3 >= 1) && (transfer < 6) ? 2 : 1;
                bool endianFlip = (endian == 1) && (transfer == 0 || transfer == 3);
                if (transfer == 4 || transfer == 6 + endian) srcOffset++;
                int destOffset = (transfer % 3 == endian + 1 && transfer < 6) ? 1 : 0;
                int destSize = len * (transfer == 1 || transfer == 2 ? 4 : (transfer >= 6 ? 1 : 2)) / 2 + ((endian == 1 && transfer % 3 == 0 && len % 2 == 1) ? 1 : 0);

                byte[] destArr = new byte[destSize];
                for (int i = 0, j = 0; i < len; i += srcStep, j += destStep)
                {
                    destArr[j + (endianFlip ? Util.Endianness[j % 2] : 0) + destOffset] = source[(srcOffset + i) % source.Length];
                }
                File.WriteAllBytes(destination, destArr);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportData_Load(object sender, EventArgs e)
        {

        }
    }
}
