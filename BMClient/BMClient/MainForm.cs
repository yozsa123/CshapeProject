using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMClient
{
    public partial class MainForm : Form
    {

        Form currentForm = null;

        

        public MainForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            reg.Click += new EventHandler(clickReg);
            result.Click += new EventHandler(clickResult);
            compare.Click += new EventHandler(clickCompare);
            
              
        }

        private void clickReg(object sender, EventArgs e)
        {

            System.Threading.TimerCallback tmp = new System.Threading.TimerCallback(doSomething);
            Gloval.timer = new System.Threading.Timer(tmp, null, 0, 0);

            this.Controls.Remove(currentForm);
            setForm(new BM_Reg_001());
            this.Controls.Add(currentForm);
        }

        private void clickResult (object sender, EventArgs e)
        {
            this.Controls.Remove(currentForm);
            setForm(new BM_Result ());
            this.Controls.Add(currentForm);
        }
        private void clickCompare (object sender, EventArgs e)
        {
            this.Controls.Remove(currentForm);
            setForm(new BM_Compare ());
            this.Controls.Add(currentForm);
        }

        public void setForm(Form _form)
        {
            currentForm = _form;
            currentForm.TopLevel = false;
            currentForm.Location = new Point(0, 30);
            currentForm.Show();

            Log.WriteLog("setForm ()");
        }

        private void doSomething(object sender)
        {
            Log.WriteLog("doSomething ().........................................................");
            Gloval.loading = new LoadingForm();
            Gloval.loading.ShowDialog();
        }


    }
}
