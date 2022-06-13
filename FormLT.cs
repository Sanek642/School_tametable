using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School_tametable
{
    public partial class FormLT : Form
    {
        MainForm form;
        public FormLT()
        {
            InitializeComponent();
        }

        public FormLT(MainForm f)
        {
            this.TopMost = true;
            form = f;
            InitializeComponent();
        }

        private void FormLT_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
            form.TopMost = true;
        }
    }
}
