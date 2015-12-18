using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExamAutoSel
{
    public partial class FormPfExam : Form
    {
        public FormPfExam()
        {
            InitializeComponent();
        }
        public string HostUrl { get; set; }
        private void FormPfExam_Load(object sender, EventArgs e)
        {
            this.textBoxHosUrl.Text = HostUrl;
        }

        string account = string.Empty;
        string password = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            this.HostUrl = this.textBoxHosUrl.Text.Trim();
            account = this.textBoxAccount.Text.Trim();
            password = this.textBoxPassword.Text.Trim();

            CExamPFLib libObj=new CExamPFLib(HostUrl,account,password);
            string hrString = libObj.CommitData(this.checkBox1.Checked, textBoxExamID.Text.Trim()); 

            MessageBox.Show(hrString);
        }
    }
}
