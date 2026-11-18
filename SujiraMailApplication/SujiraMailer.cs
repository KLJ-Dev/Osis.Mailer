using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace SujiraMailer
{
    public partial class SujiraMailer :Form
    {
        private SimpleController _controller;
        public SujiraMailer()
        {
            InitializeComponent();
            combSMTPAccount.SelectedIndex = 0;
            _controller = new SimpleController(this);

        
        }
        public void TextBoxClear(TextBox txtBox)
        {
            txtBox.Text = "";
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void txtbox_Clicked(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox s = (System.Windows.Forms.TextBox)sender;
            if (s.Text == "필수" || s.Text == "제목을 입력해주세요." || s.Text == "이메일 내용을 입력해주세요.")
            {
                TextBoxClear(s);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Text = "필수";
            txtPW.Text = "필수";
            txtSenderEmail.Text = "필수";
            txtReciverEmail.Text = "필수";
            txtMsgBody.Text = "이메일 내용을 입력해주세요.";
            txtHead.Text = "제목을 입력해주세요.";
//            txtAttach.Text = "";
            lstAttach.Items.Clear();
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }

        private void btnAttach_Click(object sender, EventArgs e)
        {

        }

       
        
       

    }
}