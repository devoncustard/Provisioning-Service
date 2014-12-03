namespace CertWorker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txtLog = new System.Windows.Forms.TextBox();
            this.chkTimerEnabled = new System.Windows.Forms.CheckBox();
            this.btnEnableTimer = new System.Windows.Forms.Button();
            this.timerProcessQueue = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(12, 37);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(360, 312);
            this.txtLog.TabIndex = 0;
            // 
            // chkTimerEnabled
            // 
            this.chkTimerEnabled.AutoSize = true;
            this.chkTimerEnabled.Location = new System.Drawing.Point(13, 13);
            this.chkTimerEnabled.Name = "chkTimerEnabled";
            this.chkTimerEnabled.Size = new System.Drawing.Size(15, 14);
            this.chkTimerEnabled.TabIndex = 1;
            this.chkTimerEnabled.UseVisualStyleBackColor = true;
            // 
            // btnEnableTimer
            // 
            this.btnEnableTimer.Location = new System.Drawing.Point(34, 8);
            this.btnEnableTimer.Name = "btnEnableTimer";
            this.btnEnableTimer.Size = new System.Drawing.Size(116, 23);
            this.btnEnableTimer.TabIndex = 2;
            this.btnEnableTimer.Text = "toggleTimer";
            this.btnEnableTimer.UseVisualStyleBackColor = true;
            this.btnEnableTimer.Click += new System.EventHandler(this.btnEnableTimer_Click);
            // 
            // timerProcessQueue
            // 
            this.timerProcessQueue.Tick += new System.EventHandler(this.timerProcessQueue_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(223, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnEnableTimer);
            this.Controls.Add(this.chkTimerEnabled);
            this.Controls.Add(this.txtLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Cert Worker";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.CheckBox chkTimerEnabled;
        private System.Windows.Forms.Button btnEnableTimer;
        private System.Windows.Forms.Timer timerProcessQueue;
        private System.Windows.Forms.Button button1;
    }
}

