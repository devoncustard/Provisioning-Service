namespace Tramp
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
            this.timerProcessQueue = new System.Windows.Forms.Timer(this.components);
            this.buttonToggleTimer = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.chkTimerEnabled = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timerProcessQueue
            // 
            this.timerProcessQueue.Interval = 1000;
            this.timerProcessQueue.Tick += new System.EventHandler(this.timerProcessQueue_Tick);
            // 
            // buttonToggleTimer
            // 
            this.buttonToggleTimer.Location = new System.Drawing.Point(45, 8);
            this.buttonToggleTimer.Name = "buttonToggleTimer";
            this.buttonToggleTimer.Size = new System.Drawing.Size(126, 23);
            this.buttonToggleTimer.TabIndex = 5;
            this.buttonToggleTimer.Text = "Toggle Timer";
            this.buttonToggleTimer.UseVisualStyleBackColor = true;
            this.buttonToggleTimer.Click += new System.EventHandler(this.buttonToggleTimer_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(13, 37);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(504, 294);
            this.txtLog.TabIndex = 6;
            // 
            // chkTimerEnabled
            // 
            this.chkTimerEnabled.AutoSize = true;
            this.chkTimerEnabled.Location = new System.Drawing.Point(13, 13);
            this.chkTimerEnabled.Name = "chkTimerEnabled";
            this.chkTimerEnabled.Size = new System.Drawing.Size(15, 14);
            this.chkTimerEnabled.TabIndex = 7;
            this.chkTimerEnabled.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(442, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 345);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.chkTimerEnabled);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.buttonToggleTimer);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMain";
            this.Text = "Tramp";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonToggleTimer;
        private System.Windows.Forms.Timer timerProcessQueue;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.CheckBox chkTimerEnabled;
        private System.Windows.Forms.Button btnClear;
    }
}

