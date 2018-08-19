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

        public ExportData(String loc, byte[] arr)
        {
            name = loc;
            source = arr;
            InitializeComponent();
            groupBox1.Text = groupBox1.Text.Replace("<x>", loc);
            label2.Text = label2.Text.Replace("<x>", loc);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 0;
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
                    len = source.Length;

                    textBox1.Text = destination;
                    textBox2.Text = len.ToString();
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
                if (comboBox1.SelectedIndex > 1) throw new Exception("Must insert 'bytes' or 'words' only!");
                if (comboBox2.SelectedIndex > 1) throw new Exception("Must access 'bytes' or 'words' only from " + name + "!");
                if (comboBox3.SelectedIndex > 1) throw new Exception("Must use 'little endian' or 'big endian' only!");
                if (!Util.TryHexOrDecToDec(textBox2.Text, out len)) throw new Exception("Unknown value for number of bytes or words to extract!");
                if (!Util.TryHexOrDecToDec(textBox3.Text, out srcOffset)) throw new Exception("Unknown value for " + name + " offset!");

                if (comboBox1.SelectedIndex == 1) len *= 2;
                if (comboBox2.SelectedIndex == 1) srcOffset *= 2;

                if (len > source.Length) throw new Exception("Amount of data to extract exceeds the size of " + name + "!");
                if (srcOffset >= source.Length) throw new Exception("The source address exceeds the size of " + name + "!");

                byte[] destArr = new byte[len + ((comboBox3.SelectedIndex == 1 && len % 2 == 1) ? 1 : 0)];
                for (int i = 0; i < len; i++)
                {
                    destArr[i + Util.Endianness[comboBox3.SelectedIndex, i % 2]] = source[(srcOffset + i) % source.Length];
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
