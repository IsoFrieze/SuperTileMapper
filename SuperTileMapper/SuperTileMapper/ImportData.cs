using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTileMapper
{
    public partial class ImportData : Form
    {
        int[] data;

        public ImportData(String loc, int[] arr)
        {
            data = arr;
            InitializeComponent();
            groupBox1.Text = groupBox1.Text.Replace("<x>", loc);
            label3.Text = label3.Text.Replace("<x>", loc);
        }

        private void ImportData_Load(object sender, EventArgs e)
        {

        }
    }
}
