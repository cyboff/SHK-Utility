using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHK_Utility
{
    public partial class FormLogin : Form
    {

        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.Text.Equals("offset"))
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else if (textBoxPassword.Text.Equals("1122"))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(Parent, "Inivalid password!");
                textBoxPassword.Clear();
                textBoxPassword.Focus();
                //return;
            }
        }

        

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.buttonOK_Click(sender, e);
            }
        }

        private void FormLogin_Shown(object sender, EventArgs e)
        {
            this.textBoxPassword.Focus();
        }
    }
}
