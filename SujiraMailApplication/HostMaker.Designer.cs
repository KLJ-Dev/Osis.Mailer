namespace SujiraMailer
{
    partial class HostMaker
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
            this.btnClose = new System.Windows.Forms.Button();
            this.cbdaum = new System.Windows.Forms.CheckBox();
            this.cbhanmail = new System.Windows.Forms.CheckBox();
            this.cbkakao = new System.Windows.Forms.CheckBox();
            this.cbnaver = new System.Windows.Forms.CheckBox();
            this.cbgmail = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(344, 120);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(91, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // cbdaum
            // 
            this.cbdaum.AutoSize = true;
            this.cbdaum.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbdaum.Location = new System.Drawing.Point(269, 27);
            this.cbdaum.Name = "cbdaum";
            this.cbdaum.Size = new System.Drawing.Size(69, 23);
            this.cbdaum.TabIndex = 1;
            this.cbdaum.Text = "다음";
            this.cbdaum.UseVisualStyleBackColor = true;
            // 
            // cbhanmail
            // 
            this.cbhanmail.AutoSize = true;
            this.cbhanmail.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbhanmail.Location = new System.Drawing.Point(118, 67);
            this.cbhanmail.Name = "cbhanmail";
            this.cbhanmail.Size = new System.Drawing.Size(88, 23);
            this.cbhanmail.TabIndex = 2;
            this.cbhanmail.Text = "한메일";
            this.cbhanmail.UseVisualStyleBackColor = true;
            // 
            // cbkakao
            // 
            this.cbkakao.AutoSize = true;
            this.cbkakao.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbkakao.Location = new System.Drawing.Point(225, 67);
            this.cbkakao.Name = "cbkakao";
            this.cbkakao.Size = new System.Drawing.Size(88, 23);
            this.cbkakao.TabIndex = 4;
            this.cbkakao.Text = "카카오";
            this.cbkakao.UseVisualStyleBackColor = true;
            // 
            // cbnaver
            // 
            this.cbnaver.AutoSize = true;
            this.cbnaver.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbnaver.Location = new System.Drawing.Point(175, 27);
            this.cbnaver.Name = "cbnaver";
            this.cbnaver.Size = new System.Drawing.Size(88, 23);
            this.cbnaver.TabIndex = 6;
            this.cbnaver.Text = "네이버";
            this.cbnaver.UseVisualStyleBackColor = true;
            // 
            // cbgmail
            // 
            this.cbgmail.AutoSize = true;
            this.cbgmail.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbgmail.Location = new System.Drawing.Point(81, 27);
            this.cbgmail.Name = "cbgmail";
            this.cbgmail.Size = new System.Drawing.Size(69, 23);
            this.cbgmail.TabIndex = 7;
            this.cbgmail.Text = "구글";
            this.cbgmail.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(247, 120);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(91, 25);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "확인";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // HostMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 157);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbgmail);
            this.Controls.Add(this.cbnaver);
            this.Controls.Add(this.cbkakao);
            this.Controls.Add(this.cbhanmail);
            this.Controls.Add(this.cbdaum);
            this.Controls.Add(this.btnClose);
            this.Name = "HostMaker";
            this.Text = "이메일 추가하기";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox cbdaum;
        internal System.Windows.Forms.CheckBox cbhanmail;
        internal System.Windows.Forms.CheckBox cbkakao;
        internal System.Windows.Forms.CheckBox cbnaver;
        internal System.Windows.Forms.CheckBox cbgmail;
        internal System.Windows.Forms.Button btnAdd;
        internal System.Windows.Forms.Button btnClose;
    }
}