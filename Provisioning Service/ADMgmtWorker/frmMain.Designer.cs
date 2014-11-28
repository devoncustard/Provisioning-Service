namespace ADMgmtWorker
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
            this.txtLog = new System.Windows.Forms.TextBox();
            this.buttonToggleTimer = new System.Windows.Forms.Button();
            this.timerProcessQueue = new System.Windows.Forms.Timer(this.components);
            this.chkTimerEnabled = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(13, 38);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(306, 282);
            this.txtLog.TabIndex = 0;
            // 
            // buttonToggleTimer
            // 
            this.buttonToggleTimer.Location = new System.Drawing.Point(34, 9);
            this.buttonToggleTimer.Name = "buttonToggleTimer";
            this.buttonToggleTimer.Size = new System.Drawing.Size(75, 23);
            this.buttonToggleTimer.TabIndex = 1;
            this.buttonToggleTimer.Text = "Toggle Timer";
            this.buttonToggleTimer.UseVisualStyleBackColor = true;
            this.buttonToggleTimer.Click += new System.EventHandler(this.buttonToggleTimer_Click);
            // 
            // timerProcessQueue
            // 
            this.timerProcessQueue.Tick += new System.EventHandler(this.timerProcessNameRequests_Tick);
            // 
            // chkTimerEnabled
            // 
            this.chkTimerEnabled.AutoSize = true;
            this.chkTimerEnabled.Enabled = false;
            this.chkTimerEnabled.Location = new System.Drawing.Point(13, 14);
            this.chkTimerEnabled.Name = "chkTimerEnabled";
            this.chkTimerEnabled.Size = new System.Drawing.Size(15, 14);
            this.chkTimerEnabled.TabIndex = 2;
            this.chkTimerEnabled.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(244, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 332);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.chkTimerEnabled);
            this.Controls.Add(this.buttonToggleTimer);
            this.Controls.Add(this.txtLog);
            this.Name = "frmMain";
            this.Text = "ADMgmt Worker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button buttonToggleTimer;
        private System.Windows.Forms.Timer timerProcessQueue;
        private System.Windows.Forms.CheckBox chkTimerEnabled;
        private System.Windows.Forms.Button btnClear;
    }
}

