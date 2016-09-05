namespace TumblrSync
{
    partial class frmConfiguration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbIntegrated = new System.Windows.Forms.CheckBox();
            this.gbLogin = new System.Windows.Forms.GroupBox();
            this.txtpass = new System.Windows.Forms.TextBox();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.gbLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password:";
            // 
            // cbIntegrated
            // 
            this.cbIntegrated.AutoSize = true;
            this.cbIntegrated.Checked = true;
            this.cbIntegrated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIntegrated.Location = new System.Drawing.Point(16, 69);
            this.cbIntegrated.Name = "cbIntegrated";
            this.cbIntegrated.Size = new System.Drawing.Size(115, 17);
            this.cbIntegrated.TabIndex = 4;
            this.cbIntegrated.Text = "Integrated Security";
            this.cbIntegrated.UseVisualStyleBackColor = true;
            this.cbIntegrated.CheckedChanged += new System.EventHandler(this.cbIntegrated_CheckedChanged);
            // 
            // gbLogin
            // 
            this.gbLogin.Controls.Add(this.txtpass);
            this.gbLogin.Controls.Add(this.txtuser);
            this.gbLogin.Controls.Add(this.label3);
            this.gbLogin.Controls.Add(this.label4);
            this.gbLogin.Location = new System.Drawing.Point(16, 104);
            this.gbLogin.Name = "gbLogin";
            this.gbLogin.Size = new System.Drawing.Size(293, 100);
            this.gbLogin.TabIndex = 5;
            this.gbLogin.TabStop = false;
            this.gbLogin.Text = "Login Details";
            this.gbLogin.Visible = false;
            // 
            // txtpass
            // 
            this.txtpass.Location = new System.Drawing.Point(84, 63);
            this.txtpass.Name = "txtpass";
            this.txtpass.Size = new System.Drawing.Size(183, 20);
            this.txtpass.TabIndex = 5;
            // 
            // txtuser
            // 
            this.txtuser.Location = new System.Drawing.Point(84, 27);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(183, 20);
            this.txtuser.TabIndex = 4;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(98, 10);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(185, 20);
            this.txtServer.TabIndex = 6;
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(98, 37);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(185, 20);
            this.txtDatabase.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(234, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(12, 210);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 9;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // frmConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 244);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.gbLogin);
            this.Controls.Add(this.cbIntegrated);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmConfiguration";
            this.Text = "Configuration";
            this.gbLogin.ResumeLayout(false);
            this.gbLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbIntegrated;
        private System.Windows.Forms.GroupBox gbLogin;
        private System.Windows.Forms.TextBox txtpass;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnTest;
    }
}