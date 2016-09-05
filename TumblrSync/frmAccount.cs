using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TumblrSync
{
    public partial class frmAccount : Form
    {
        public tAccount newAcc = new tAccount();
        public frmAccount()
        {
            InitializeComponent();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            newAcc.AccountSystemPath = txtALP.Text;
            newAcc.AccountWebPath = txtAWP.Text;
            newAcc.ConsumerKey = txtConKey.Text;
            newAcc.ConsumerSecret = txtConSec.Text;
            newAcc.password = txtpassword.Text;
            newAcc.systemDrive = txtSD.Text;
            newAcc.username = txtUsername.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
