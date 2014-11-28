using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Messaging;
using System.Diagnostics;



namespace Dashboard
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnToggleTimer_Click(object sender, EventArgs e)
        {
            chkEnableTimer.Checked = (chkEnableTimer.Checked == true) ? false : true;
            timUpdateUI.Enabled = chkEnableTimer.Checked;
        }

        private void timUpdateUI_Tick(object sender, EventArgs e)
        {
            txtComplete.Text = CountMessages(@"FormatName:direct=OS:provsvc\private$\complete").ToString();
            txtENCRequest.Text= CountMessages(@"FormatName:direct=OS:provsvc\private$\encrequest").ToString();
            txtNameRequest.Text = CountMessages(@"FormatName:direct=OS:provsvc\private$\namerequest").ToString();
            txtFailed.Text = CountMessages(@"FormatName:direct=OS:provsvc\private$\failed").ToString();

            txtRemoteInvoke.Text = CountMessages(@"FormatName:direct=OS:provsvc\private$\remoteinvoke").ToString();

            txtProvision.Text = CountMessages(@"FormatName:direct=OS:provsvc\private$\provision").ToString();

            txtVagrantProvider.Text = CountMessages(@"FormatName:direct=OS:provsvc\private$\vagprovider").ToString();
        }
        private int CountMessages( string q)
        {
            int i = 0;
            try
            {
                MessageQueue rq = new MessageQueue(q);
                MessageEnumerator me = rq.GetMessageEnumerator2();
                while (me.MoveNext())
                {
                    i++;
                }
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return i ;

        }
    

    }
}
