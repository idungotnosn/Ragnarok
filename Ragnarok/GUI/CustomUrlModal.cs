using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ragnarok.GUI
{
    public partial class CustomUrlModal : Form
    {

        public String CustomResult { get; set; }
        private String url;

        public CustomUrlModal(String url)
        {
            this.url = url;
            InitializeComponent();
            this.button1.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = textBox1.TextLength > 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.CustomResult = textBox1.Text;
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.url);

        }
    }
}
