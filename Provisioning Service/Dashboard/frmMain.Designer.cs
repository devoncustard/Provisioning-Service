namespace Dashboard
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.timUpdateUI = new System.Windows.Forms.Timer(this.components);
            this.chkEnableTimer = new System.Windows.Forms.CheckBox();
            this.btnToggleTimer = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtComplete = new System.Windows.Forms.TextBox();
            this.txtENCRequest = new System.Windows.Forms.TextBox();
            this.txtFailed = new System.Windows.Forms.TextBox();
            this.txtNameRequest = new System.Windows.Forms.TextBox();
            this.txtProvision = new System.Windows.Forms.TextBox();
            this.txtRemoteInvoke = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVagrantProvider = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timUpdateUI
            // 
            this.timUpdateUI.Interval = 300;
            this.timUpdateUI.Tick += new System.EventHandler(this.timUpdateUI_Tick);
            // 
            // chkEnableTimer
            // 
            this.chkEnableTimer.AutoSize = true;
            this.chkEnableTimer.Enabled = false;
            this.chkEnableTimer.Location = new System.Drawing.Point(13, 13);
            this.chkEnableTimer.Name = "chkEnableTimer";
            this.chkEnableTimer.Size = new System.Drawing.Size(15, 14);
            this.chkEnableTimer.TabIndex = 0;
            this.chkEnableTimer.UseVisualStyleBackColor = true;
            // 
            // btnToggleTimer
            // 
            this.btnToggleTimer.Location = new System.Drawing.Point(34, 8);
            this.btnToggleTimer.Name = "btnToggleTimer";
            this.btnToggleTimer.Size = new System.Drawing.Size(75, 23);
            this.btnToggleTimer.TabIndex = 1;
            this.btnToggleTimer.Text = "Toggle Timer";
            this.btnToggleTimer.UseVisualStyleBackColor = true;
            this.btnToggleTimer.Click += new System.EventHandler(this.btnToggleTimer_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtVagrantProvider);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtRemoteInvoke);
            this.panel1.Controls.Add(this.txtProvision);
            this.panel1.Controls.Add(this.txtNameRequest);
            this.panel1.Controls.Add(this.txtFailed);
            this.panel1.Controls.Add(this.txtENCRequest);
            this.panel1.Controls.Add(this.txtComplete);
            this.panel1.Location = new System.Drawing.Point(13, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(187, 244);
            this.panel1.TabIndex = 2;
            // 
            // txtComplete
            // 
            this.txtComplete.BackColor = System.Drawing.SystemColors.Menu;
            this.txtComplete.Location = new System.Drawing.Point(112, 20);
            this.txtComplete.Name = "txtComplete";
            this.txtComplete.Size = new System.Drawing.Size(38, 20);
            this.txtComplete.TabIndex = 3;
            // 
            // txtENCRequest
            // 
            this.txtENCRequest.BackColor = System.Drawing.SystemColors.Menu;
            this.txtENCRequest.Location = new System.Drawing.Point(112, 46);
            this.txtENCRequest.Name = "txtENCRequest";
            this.txtENCRequest.Size = new System.Drawing.Size(38, 20);
            this.txtENCRequest.TabIndex = 4;
            // 
            // txtFailed
            // 
            this.txtFailed.BackColor = System.Drawing.SystemColors.Menu;
            this.txtFailed.Location = new System.Drawing.Point(112, 72);
            this.txtFailed.Name = "txtFailed";
            this.txtFailed.Size = new System.Drawing.Size(38, 20);
            this.txtFailed.TabIndex = 5;
            // 
            // txtNameRequest
            // 
            this.txtNameRequest.BackColor = System.Drawing.SystemColors.Menu;
            this.txtNameRequest.Location = new System.Drawing.Point(112, 98);
            this.txtNameRequest.Name = "txtNameRequest";
            this.txtNameRequest.Size = new System.Drawing.Size(38, 20);
            this.txtNameRequest.TabIndex = 6;
            // 
            // txtProvision
            // 
            this.txtProvision.BackColor = System.Drawing.SystemColors.Menu;
            this.txtProvision.Location = new System.Drawing.Point(112, 124);
            this.txtProvision.Name = "txtProvision";
            this.txtProvision.Size = new System.Drawing.Size(38, 20);
            this.txtProvision.TabIndex = 7;
            // 
            // txtRemoteInvoke
            // 
            this.txtRemoteInvoke.BackColor = System.Drawing.SystemColors.Menu;
            this.txtRemoteInvoke.Location = new System.Drawing.Point(112, 150);
            this.txtRemoteInvoke.Name = "txtRemoteInvoke";
            this.txtRemoteInvoke.Size = new System.Drawing.Size(38, 20);
            this.txtRemoteInvoke.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Complete";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "ENCRequest";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Failed";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "VagrantProvider";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "RemoteInvoke";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Provision";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "NameRequest";
            // 
            // txtVagrantProvider
            // 
            this.txtVagrantProvider.BackColor = System.Drawing.SystemColors.Menu;
            this.txtVagrantProvider.Location = new System.Drawing.Point(112, 179);
            this.txtVagrantProvider.Name = "txtVagrantProvider";
            this.txtVagrantProvider.Size = new System.Drawing.Size(38, 20);
            this.txtVagrantProvider.TabIndex = 16;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 302);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnToggleTimer);
            this.Controls.Add(this.chkEnableTimer);
            this.Name = "frmMain";
            this.Text = "Dashboard";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timUpdateUI;
        private System.Windows.Forms.CheckBox chkEnableTimer;
        private System.Windows.Forms.Button btnToggleTimer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtVagrantProvider;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRemoteInvoke;
        private System.Windows.Forms.TextBox txtProvision;
        private System.Windows.Forms.TextBox txtNameRequest;
        private System.Windows.Forms.TextBox txtFailed;
        private System.Windows.Forms.TextBox txtENCRequest;
        private System.Windows.Forms.TextBox txtComplete;
    }
}

