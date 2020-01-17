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
    public partial class ColorControl : Form
    {
        private Color selected;

        public Color Value
        {
            get { return selected; }
        }

        public ColorControl()
        {
            InitializeComponent();
        }

        private void colorB1_Click(object sender, EventArgs e) { selected = Color.FromArgb(255, 0, 0); }
        private void colorB2_Click(object sender, EventArgs e) { selected = Color.FromArgb(255, 128, 0); }
        private void colorB3_Click(object sender, EventArgs e) { selected = Color.FromArgb(255, 255, 0); }
        private void colorB4_Click(object sender, EventArgs e) { selected = Color.FromArgb(128, 0, 0); }
        private void colorB5_Click(object sender, EventArgs e) { selected = Color.FromArgb(0, 0, 192); }
        private void colorB6_Click(object sender, EventArgs e) { selected = Color.FromArgb(25, 255, 255); }
        private void colorB8_Click(object sender, EventArgs e) { selected = Color.FromArgb(128, 128, 255); }
        private void colorB9_Click(object sender, EventArgs e) { selected = Color.FromArgb(102, 255, 102); }
        private void colorB13_Click(object sender, EventArgs e) { selected = Color.FromArgb(0, 0, 0); }
        private void colorB14_Click(object sender, EventArgs e) { selected = Color.FromArgb(255, 255, 255); }

        private void submit_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
