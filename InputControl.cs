using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FractalEngine
{
    public partial class InputControl : Form
    {

        public string Value
        {
            get { return inputBoxText.Text; }
        }

        public InputControl()
        {
            InitializeComponent();
        }

        private void submit_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
