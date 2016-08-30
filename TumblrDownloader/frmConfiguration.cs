using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TumblrDownloader
{
    public partial class frmConfiguration : Form
    {

        String Server = "";
        String Database = "";
        String User = "";
        String Pass = "";
        bool Integrated = false;

        public frmConfiguration()
        {
            InitializeComponent();

            Server = ConfigurationManager.AppSettings["Server"];
            Database = ConfigurationManager.AppSettings["Database"];
            User = ConfigurationManager.AppSettings["Username"];
            Pass = ConfigurationManager.AppSettings["Password"];
            Integrated = Convert.ToBoolean(ConfigurationManager.AppSettings["Integrated"]);

            cbIntegrated.Checked = Integrated;
            txtServer.Text = Server;
            txtDatabase.Text = Database;

            // assumes a connectionString name in .config of MyDbEntities
            var selectedDb = new TumblrModel();

            if (Integrated)
            {
                selectedDb.ChangeDatabase
                    (
                        initialCatalog: Database,
                    //userId: "hfdhanekom",
                    //password: "nomoresecrets",
                        dataSource: Server // could be ip address 120.273.435.167 etc
                        , integratedSecuity: Integrated
                    );
            }
            else
            {
                selectedDb.ChangeDatabase
                   (
                       initialCatalog: Database,
                        userId: "hfdhanekom",
                        password: "nomoresecrets",
                       dataSource: Server // could be ip address 120.273.435.167 etc
                       , integratedSecuity: false
                   );

            }
        }

        private void cbIntegrated_CheckedChanged(object sender, EventArgs e)
        {
            gbLogin.Visible = !cbIntegrated.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings["Server"] = Server;
            ConfigurationManager.AppSettings["Database"] = Database;
            ConfigurationManager.AppSettings["Username"] = User;
            ConfigurationManager.AppSettings["Password"] = Pass;
            ConfigurationManager.AppSettings["Integrated"] = Integrated.ToString();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            TumblrModel mod = new TumblrModel();
            try
            {
                mod.Database.Connection.Open();
                mod.Database.Connection.Close();
                MessageBox.Show("Connection was Succesful.");
            }
            catch
            {
                MessageBox.Show("Connection Failed.");
            }
        }
    }
}
