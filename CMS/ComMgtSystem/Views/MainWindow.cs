using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ComMgtSystem.Views
{
    public partial class MainWindow : Form,IView
    {
        Presenters.presenter p;

        public MainWindow()
        {
            InitializeComponent();
            this.p = new Presenters.presenter(this, new Models.model());
        }

        private void OK_Click(object sender, EventArgs e)
        {
            p.calculate();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int numberA
        {
            get { return Convert.ToInt32(this.textBox1.Text); }
        }

        public int numberB
        {
            get { return Convert.ToInt32(this.textBox2.Text); }
        }

        public int result
        {
            set { this.textBox3.Text = value.ToString(); }
        }

    }
}
