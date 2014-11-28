namespace ENCWorker
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
            this.btnToggleTimer = new System.Windows.Forms.Button();
            this.chkTimerEnabled = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.timerProcessQueue = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnToggleTimer
            // 
            this.btnToggleTimer.Location = new System.Drawing.Point(34, 8);
            this.btnToggleTimer.Name = "btnToggleTimer";
            this.btnToggleTimer.Size = new System.Drawing.Size(103, 23);
            this.btnToggleTimer.TabIndex = 0;
            this.btnToggleTimer.Text = "Toggle Timer";
            this.btnToggleTimer.UseVisualStyleBackColor = true;
            this.btnToggleTimer.Click += new System.EventHandler(this.btnToggleTimer_Click);
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
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(172, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 37);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(356, 393);
            this.txtLog.TabIndex = 3;
            // 
            // timerProcessQueue
            // 
            this.timerProcessQueue.Interval = 1000;
            this.timerProcessQueue.Tick += new System.EventHandler(this.timerProcessQueue_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 442);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.chkTimerEnabled);
            this.Controls.Add(this.btnToggleTimer);
            this.Name = "frmMain";
            this.Text = "ENC Worker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnToggleTimer;
        private System.Windows.Forms.CheckBox chkTimerEnabled;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Timer timerProcessQueue;
    }
}

