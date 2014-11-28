namespace ProvisioningWorker
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
            this.queProvision = new System.Messaging.MessageQueue();
            this.timerProcessQueue = new System.Windows.Forms.Timer(this.components);
            this.buttonToggleTimer = new System.Windows.Forms.Button();
            this.checkBoxTimerState = new System.Windows.Forms.CheckBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // queProvision
            // 
            this.queProvision.MessageReadPropertyFilter.LookupId = true;
            this.queProvision.SynchronizingObject = this;
            // 
            // timerProcessQueue
            // 
            this.timerProcessQueue.Interval = 1000;
            this.timerProcessQueue.Tick += new System.EventHandler(this.timerProcessProvisionQueue_Tick);
            // 
            // buttonToggleTimer
            // 
            this.buttonToggleTimer.Location = new System.Drawing.Point(33, 7);
            this.buttonToggleTimer.Name = "buttonToggleTimer";
            this.buttonToggleTimer.Size = new System.Drawing.Size(136, 23);
            this.buttonToggleTimer.TabIndex = 0;
            this.buttonToggleTimer.Text = "Toggle Timer";
            this.buttonToggleTimer.UseVisualStyleBackColor = true;
            this.buttonToggleTimer.Click += new System.EventHandler(this.buttonToggleTimer_Click);
            // 
            // checkBoxTimerState
            // 
            this.checkBoxTimerState.AutoSize = true;
            this.checkBoxTimerState.Enabled = false;
            this.checkBoxTimerState.Location = new System.Drawing.Point(12, 12);
            this.checkBoxTimerState.Name = "checkBoxTimerState";
            this.checkBoxTimerState.Size = new System.Drawing.Size(15, 14);
            this.checkBoxTimerState.TabIndex = 1;
            this.checkBoxTimerState.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(13, 42);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(335, 239);
            this.txtLog.TabIndex = 2;
            this.txtLog.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 293);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.checkBoxTimerState);
            this.Controls.Add(this.buttonToggleTimer);
            this.Name = "frmMain";
            this.Text = "Provisioning Decider";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Messaging.MessageQueue queProvision;
        private System.Windows.Forms.Timer timerProcessQueue;
        private System.Windows.Forms.CheckBox checkBoxTimerState;
        private System.Windows.Forms.Button buttonToggleTimer;
        private System.Windows.Forms.TextBox txtLog;
    }
}

