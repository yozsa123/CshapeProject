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
    public partial class YesNoForm : Form
    {
        BM_Reg_001 reg = null;
        string nUserIdn = "";

        public YesNoForm (string _bid)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            question.Text = "[" + _bid + "] 를 삭제하시겠습니까?";

            yes.Click += new EventHandler(clickYes);
            no.Click += new EventHandler(clickNo);
        }

        private void clickYes (object sender, EventArgs e)
        {
            Gloval.deleteOK = true;
            this.Close();
        }

        private void clickNo (object sender, EventArgs e)
        {
            Gloval.deleteOK = false;
            this.Close();
        }


    }
}
