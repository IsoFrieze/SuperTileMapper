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

        public ImportData(String loc, byte[] arr)
        {
            name = loc;
            destination = arr;
            InitializeComponent();
            groupBox1.Text = groupBox1.Text.Replace("<x>", loc);
            label3.Text = label3.Text.Replace("<x>", loc);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 1;
            comboBox4.SelectedIndex = 0;
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
                if (comboBox1.SelectedIndex > 1) throw new Exception("Must insert 'bytes' or 'words' only!");
                if (comboBox2.SelectedIndex > 1) throw new Exception("Must access 'bytes' or 'words' only from file!");
                if (comboBox3.SelectedIndex > 1) throw new Exception("Must access 'bytes' or 'words' only from " + name + "!");
                if (comboBox4.SelectedIndex > 1) throw new Exception("Must use 'little endian' or 'big endian' only!");
                if (!Util.TryHexOrDecToDec(textBox2.Text, out len)) throw new Exception("Unknown value for number of bytes or words to insert!");
                if (!Util.TryHexOrDecToDec(textBox3.Text, out srcOffset)) throw new Exception("Unknown value for file offset!");
                if (!Util.TryHexOrDecToDec(textBox4.Text, out destOffset)) throw new Exception("Unknown value for " + name + " offset!");

                if (comboBox1.SelectedIndex == 1) len *= 2;
                if (comboBox2.SelectedIndex == 1) srcOffset *= 2;
                if (comboBox3.SelectedIndex == 1) destOffset *= 2;

                if (len > destination.Length) throw new Exception("Amount of data to insert exceeds the size of " + name + "!");
                if (srcOffset >= source.Length) throw new Exception("The source address exceeds the size of the file!");
                if (destOffset >= destination.Length) throw new Exception("The destination address exceeds the size of " + name + "!");
                if (srcOffset + len + ((comboBox4.SelectedIndex == 1 && len % 2 == 1) ? 1 : 0) > source.Length) throw new Exception("The amount of data to insert exceeds the size of the file!");
                // I don't know why anyone would import an odd amount of bytes via big endian but who knows?

                for (int i = 0; i < len; i++)
                {
                    destination[(destOffset + i) % destination.Length] = source[srcOffset + i + Util.Endianness[comboBox4.SelectedIndex, i % 2]];
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
                    len = source.Length >= destination.Length ? destination.Length : source.Length;

                    textBox1.Text = file;
                    textBox2.Text = len.ToString();
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
