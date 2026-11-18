namespace SujiraMailer
{
    partial class SujiraMailer
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPW = new System.Windows.Forms.Button();
            this.lstAttach = new System.Windows.Forms.ListBox();
            this.btnAttachClear = new System.Windows.Forms.Button();
            this.btnAttachDelete = new System.Windows.Forms.Button();
            this.lblError2 = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnHDelete = new System.Windows.Forms.Button();
            this.btnHAdd = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.txtPW = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPortNumber = new System.Windows.Forms.TextBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.combSMTPAccount = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSenderEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReciverEmail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHead = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAttach = new System.Windows.Forms.Button();
            this.txtMsgBody = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.EmailErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtCC = new System.Windows.Forms.TextBox();
            this.lblCC = new System.Windows.Forms.Label();
            this.txtBCC = new System.Windows.Forms.TextBox();
            this.lblBCC = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmailErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtCC);
            this.panel1.Controls.Add(this.lblCC);
            this.panel1.Controls.Add(this.txtBCC);
            this.panel1.Controls.Add(this.lblBCC);
            this.panel1.Controls.Add(this.btnPW);
            this.panel1.Controls.Add(this.lstAttach);
            this.panel1.Controls.Add(this.btnAttachClear);
            this.panel1.Controls.Add(this.btnAttachDelete);
            this.panel1.Controls.Add(this.lblError2);
            this.panel1.Controls.Add(this.lblError);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.btnHDelete);
            this.panel1.Controls.Add(this.btnHAdd);
            this.panel1.Controls.Add(this.btnHelp);
            this.panel1.Controls.Add(this.txtPW);
            this.panel1.Controls.Add(this.txtID);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtPortNumber);
            this.panel1.Controls.Add(this.txtServerName);
            this.panel1.Controls.Add(this.combSMTPAccount);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtSenderEmail);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtReciverEmail);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtHead);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnAttach);
            this.panel1.Controls.Add(this.txtMsgBody);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(959, 655);
            this.panel1.TabIndex = 7;
            // 
            // btnPW
            // 
            this.btnPW.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPW.Location = new System.Drawing.Point(612, 45);
            this.btnPW.Name = "btnPW";
            this.btnPW.Size = new System.Drawing.Size(75, 29);
            this.btnPW.TabIndex = 40;
            this.btnPW.Text = "비밀번호";
            this.btnPW.UseVisualStyleBackColor = true;
            // 
            // lstAttach
            // 
            this.lstAttach.FormattingEnabled = true;
            this.lstAttach.ItemHeight = 15;
            this.lstAttach.Location = new System.Drawing.Point(310, 361);
            this.lstAttach.Name = "lstAttach";
            this.lstAttach.Size = new System.Drawing.Size(577, 79);
            this.lstAttach.TabIndex = 38;
            // 
            // btnAttachClear
            // 
            this.btnAttachClear.Font = new System.Drawing.Font("굴림", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAttachClear.Location = new System.Drawing.Point(424, 326);
            this.btnAttachClear.Name = "btnAttachClear";
            this.btnAttachClear.Size = new System.Drawing.Size(106, 29);
            this.btnAttachClear.TabIndex = 37;
            this.btnAttachClear.Text = "비우기";
            this.btnAttachClear.UseVisualStyleBackColor = true;
            // 
            // btnAttachDelete
            // 
            this.btnAttachDelete.Font = new System.Drawing.Font("굴림", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAttachDelete.Location = new System.Drawing.Point(536, 326);
            this.btnAttachDelete.Name = "btnAttachDelete";
            this.btnAttachDelete.Size = new System.Drawing.Size(106, 29);
            this.btnAttachDelete.TabIndex = 36;
            this.btnAttachDelete.Text = "삭제";
            this.btnAttachDelete.UseVisualStyleBackColor = true;
            // 
            // lblError2
            // 
            this.lblError2.AutoSize = true;
            this.lblError2.BackColor = System.Drawing.SystemColors.Control;
            this.lblError2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblError2.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblError2.Location = new System.Drawing.Point(309, 157);
            this.lblError2.Name = "lblError2";
            this.lblError2.Size = new System.Drawing.Size(357, 15);
            this.lblError2.TabIndex = 34;
            this.lblError2.Text = "이메일형식이 아닙니다. 올바르게 작성해주세요.";
            this.lblError2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblError2.Visible = false;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.BackColor = System.Drawing.SystemColors.Control;
            this.lblError.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblError.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblError.Location = new System.Drawing.Point(309, 111);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(357, 15);
            this.lblError.TabIndex = 33;
            this.lblError.Text = "이메일형식이 아닙니다. 올바르게 작성해주세요.";
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblError.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.Location = new System.Drawing.Point(648, 332);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(256, 15);
            this.label10.TabIndex = 32;
            this.label10.Text = "첨부파일의 용량은 최대 2GB 입니다.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Location = new System.Drawing.Point(197, 330);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 31;
            this.label9.Text = "첨부파일";
            // 
            // btnHDelete
            // 
            this.btnHDelete.Location = new System.Drawing.Point(259, 620);
            this.btnHDelete.Name = "btnHDelete";
            this.btnHDelete.Size = new System.Drawing.Size(145, 28);
            this.btnHDelete.TabIndex = 30;
            this.btnHDelete.Text = "삭제";
            this.btnHDelete.UseVisualStyleBackColor = true;
            this.btnHDelete.Visible = false;
            // 
            // btnHAdd
            // 
            this.btnHAdd.Location = new System.Drawing.Point(344, 620);
            this.btnHAdd.Name = "btnHAdd";
            this.btnHAdd.Size = new System.Drawing.Size(143, 27);
            this.btnHAdd.TabIndex = 29;
            this.btnHAdd.Text = "편집";
            this.btnHAdd.UseVisualStyleBackColor = true;
            this.btnHAdd.Visible = false;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(730, 612);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 36);
            this.btnHelp.TabIndex = 28;
            this.btnHelp.Text = "사용방법";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Visible = false;
            // 
            // txtPW
            // 
            this.txtPW.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPW.Location = new System.Drawing.Point(693, 47);
            this.txtPW.Name = "txtPW";
            this.txtPW.PasswordChar = '*';
            this.txtPW.Size = new System.Drawing.Size(196, 25);
            this.txtPW.TabIndex = 3;
            this.txtPW.Click += new System.EventHandler(this.txtbox_Clicked);
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtID.Location = new System.Drawing.Point(369, 47);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(235, 25);
            this.txtID.TabIndex = 2;
            this.txtID.Click += new System.EventHandler(this.txtbox_Clicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(311, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 25;
            this.label7.Text = "아이디";
            // 
            // txtPortNumber
            // 
            this.txtPortNumber.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPortNumber.Location = new System.Drawing.Point(163, 620);
            this.txtPortNumber.Name = "txtPortNumber";
            this.txtPortNumber.ReadOnly = true;
            this.txtPortNumber.Size = new System.Drawing.Size(175, 25);
            this.txtPortNumber.TabIndex = 24;
            this.txtPortNumber.Visible = false;
            // 
            // txtServerName
            // 
            this.txtServerName.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtServerName.Location = new System.Drawing.Point(67, 620);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.ReadOnly = true;
            this.txtServerName.Size = new System.Drawing.Size(199, 25);
            this.txtServerName.TabIndex = 23;
            this.txtServerName.Visible = false;
            // 
            // combSMTPAccount
            // 
            this.combSMTPAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSMTPAccount.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.combSMTPAccount.FormattingEnabled = true;
            this.combSMTPAccount.Items.AddRange(new object[] {
            "naver",
            "gmail",
            "daum",
            "kakao",
            "hanmail"});
            this.combSMTPAccount.Location = new System.Drawing.Point(313, 12);
            this.combSMTPAccount.Name = "combSMTPAccount";
            this.combSMTPAccount.Size = new System.Drawing.Size(576, 23);
            this.combSMTPAccount.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(198, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 15);
            this.label4.TabIndex = 19;
            this.label4.Text = "메일 환경 설정";
            // 
            // txtSenderEmail
            // 
            this.txtSenderEmail.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EmailErrorProvider.SetIconAlignment(this.txtSenderEmail, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.txtSenderEmail.Location = new System.Drawing.Point(312, 83);
            this.txtSenderEmail.Name = "txtSenderEmail";
            this.txtSenderEmail.Size = new System.Drawing.Size(577, 25);
            this.txtSenderEmail.TabIndex = 4;
            this.txtSenderEmail.Click += new System.EventHandler(this.txtbox_Clicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(197, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "보낸 사람";
            // 
            // txtReciverEmail
            // 
            this.txtReciverEmail.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EmailErrorProvider.SetIconAlignment(this.txtReciverEmail, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.txtReciverEmail.Location = new System.Drawing.Point(312, 129);
            this.txtReciverEmail.Name = "txtReciverEmail";
            this.txtReciverEmail.Size = new System.Drawing.Size(577, 25);
            this.txtReciverEmail.TabIndex = 5;
            this.txtReciverEmail.Click += new System.EventHandler(this.txtbox_Clicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(197, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "받는 사람";
            // 
            // txtHead
            // 
            this.txtHead.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtHead.Location = new System.Drawing.Point(309, 295);
            this.txtHead.Name = "txtHead";
            this.txtHead.Size = new System.Drawing.Size(577, 25);
            this.txtHead.TabIndex = 6;
            this.txtHead.Click += new System.EventHandler(this.txtbox_Clicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(195, 294);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "제목";
            // 
            // btnAttach
            // 
            this.btnAttach.Font = new System.Drawing.Font("굴림", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAttach.Location = new System.Drawing.Point(312, 326);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(106, 29);
            this.btnAttach.TabIndex = 7;
            this.btnAttach.Text = "파일 찾기";
            this.btnAttach.UseVisualStyleBackColor = true;
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // txtMsgBody
            // 
            this.txtMsgBody.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtMsgBody.Location = new System.Drawing.Point(57, 446);
            this.txtMsgBody.Multiline = true;
            this.txtMsgBody.Name = "txtMsgBody";
            this.txtMsgBody.Size = new System.Drawing.Size(830, 160);
            this.txtMsgBody.TabIndex = 8;
            this.txtMsgBody.Click += new System.EventHandler(this.txtbox_Clicked);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(57, 344);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(119, 96);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "초기화";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(57, 15);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(120, 323);
            this.btnSend.TabIndex = 9;
            this.btnSend.Text = "보내기";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(811, 612);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 36);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // EmailErrorProvider
            // 
            this.EmailErrorProvider.BlinkRate = 400;
            this.EmailErrorProvider.ContainerControl = this;
            // 
            // txtCC
            // 
            this.txtCC.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EmailErrorProvider.SetIconAlignment(this.txtCC, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.txtCC.Location = new System.Drawing.Point(311, 175);
            this.txtCC.Name = "txtCC";
            this.txtCC.Size = new System.Drawing.Size(577, 25);
            this.txtCC.TabIndex = 41;
            // 
            // lblCC
            // 
            this.lblCC.AutoSize = true;
            this.lblCC.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCC.Location = new System.Drawing.Point(196, 178);
            this.lblCC.Name = "lblCC";
            this.lblCC.Size = new System.Drawing.Size(37, 15);
            this.lblCC.TabIndex = 44;
            this.lblCC.Text = "참조";
            // 
            // txtBCC
            // 
            this.txtBCC.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EmailErrorProvider.SetIconAlignment(this.txtBCC, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.txtBCC.Location = new System.Drawing.Point(311, 206);
            this.txtBCC.Name = "txtBCC";
            this.txtBCC.Size = new System.Drawing.Size(577, 25);
            this.txtBCC.TabIndex = 42;
            // 
            // lblBCC
            // 
            this.lblBCC.AutoSize = true;
            this.lblBCC.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblBCC.Location = new System.Drawing.Point(196, 209);
            this.lblBCC.Name = "lblBCC";
            this.lblBCC.Size = new System.Drawing.Size(72, 15);
            this.lblBCC.TabIndex = 43;
            this.lblBCC.Text = "숨은 참조";
            // 
            // SujiraMailer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 655);
            this.Controls.Add(this.panel1);
            this.Name = "SujiraMailer";
            this.Text = "Mail";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmailErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Button btnAttach;
        internal System.Windows.Forms.TextBox txtMsgBody;
        internal System.Windows.Forms.Button btnClear;
        internal System.Windows.Forms.Button btnSend;
        internal System.Windows.Forms.TextBox txtSenderEmail;
        internal System.Windows.Forms.TextBox txtReciverEmail;
        internal System.Windows.Forms.TextBox txtHead;
        internal System.Windows.Forms.TextBox txtServerName;
        internal System.Windows.Forms.ComboBox combSMTPAccount;
        internal System.Windows.Forms.TextBox txtPortNumber;
        internal System.Windows.Forms.TextBox txtPW;
        internal System.Windows.Forms.TextBox txtID;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Button btnHelp;
        internal System.Windows.Forms.Button btnHDelete;
        internal System.Windows.Forms.Button btnHAdd;
        internal System.Windows.Forms.ErrorProvider EmailErrorProvider;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label lblError;
        internal System.Windows.Forms.Label lblError2;
        internal System.Windows.Forms.Button btnAttachClear;
        internal System.Windows.Forms.Button btnAttachDelete;
        internal System.Windows.Forms.ListBox lstAttach;
        internal System.Windows.Forms.Button btnPW;
        internal System.Windows.Forms.TextBox txtCC;
        internal System.Windows.Forms.Label lblCC;
        internal System.Windows.Forms.TextBox txtBCC;
        internal System.Windows.Forms.Label lblBCC;

    }
}

