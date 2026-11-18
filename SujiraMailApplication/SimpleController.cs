using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace SujiraMailer
{
    internal class SimpleController
    {
        private SMTPModel _model = new SMTPModel();
        private SujiraMailer _view;
        private HostMaker _viewHostMaker;
        private System.Windows.Forms.ComboBox _combsSMTPAccounts;
        //SendEmail() 인수..... _SendButton_Click 에서 사용한다.
        private string sHost;
        private int sPort;
        private string sID;
        private string sPW;
        private string sFrom;
        //private string sTo;
        private string[] sToA;
        private string sSubject;
        private string sBody;
        private ArrayList sAttach = new ArrayList();

        //컨트롤러용 변수
        private System.Windows.Forms.TextBox _txtServerN;
        private System.Windows.Forms.TextBox _txtPortN;
        private System.Windows.Forms.TextBox _txtID;
        private System.Windows.Forms.TextBox _txtPW;
        private System.Windows.Forms.Button _btnPW;
        private System.Windows.Forms.TextBox _txtSenderEmail;
        private System.Windows.Forms.TextBox _txtRecieverEmail;
        private System.Windows.Forms.TextBox _txtSubject;
        private System.Windows.Forms.TextBox _txtBody;
        //private System.Windows.Forms.TextBox _txtAttach;
        private System.Windows.Forms.ListBox _lstAttach;
        private System.Windows.Forms.Button _SendButton;
        private System.Windows.Forms.Button _AttachButton;
        private System.Windows.Forms.Button _btnAttachClear;
        private System.Windows.Forms.Button _btnAttachDelete;
        private System.Windows.Forms.Button _btnClear;
        private System.Windows.Forms.Button _btnHelp;
        private System.Windows.Forms.Button _btnHAdd;
        private System.Windows.Forms.Button _btnHDelete;
        private System.Windows.Forms.ErrorProvider _ErrorProvider;
        private System.Windows.Forms.Label _lblEmailError;
        private System.Windows.Forms.Label _lblEmailError2;
        private System.Windows.Forms.TextBox _txtCC;
        private System.Windows.Forms.TextBox _txtBCC;
        // private System.Windows.Forms.CheckBox _chbAuto;

        //HostMaker 용 변수
        private System.Windows.Forms.Button _btnHMAdd;
        private System.Windows.Forms.Button _btnHMClose;
        private System.Windows.Forms.CheckBox _cbgmail;
        private System.Windows.Forms.CheckBox _cbnaver;
        private System.Windows.Forms.CheckBox _cbdaum;
        // private System.Windows.Forms.CheckBox _cbkakao;
        //private System.Windows.Forms.CheckBox _cbnate;
        // private System.Windows.Forms.CheckBox _cbparan;
        private System.Windows.Forms.CheckBox _cbhanmail;
        // private System.Windows.Forms.TextBox _cbcustom;

        //Mail 객체
        //private System.Web.Mail.MailMessage msg;
        //private System.Net.Mail.MailMessage message;
        bool SendComplete = false;

        public SimpleController(SujiraMailer view)
        {
            _view = view;
            ControllerEventReference();
        }
       
        public SimpleController(HostMaker viewh)
        {
            _viewHostMaker = viewh;
            ControllerEventReference();
        }
        public void ControllerEventReference()
        {
            //////////////////SujiraEmailManager.cs EventReference////////////////////////
            _combsSMTPAccounts = _view.combSMTPAccount;
            _combsSMTPAccounts.SelectedIndexChanged += new EventHandler(_combsSMTPAccounts_SelectedIndexChanged);

            _txtServerN = _view.txtServerName;
            _txtPortN = _view.txtPortNumber;
            _combsSMTPAccounts_initialize();

            _txtID = _view.txtID;
            _txtID.TextChanged += new EventHandler(_txtID_TextChanged);
            // _txtID.LostFocus += new EventHandler((LostIDFocus));
            _txtID.Validating += new System.ComponentModel.CancelEventHandler(IDtextBox_Validating);
            _txtID.Validated += new EventHandler(IDtextBox_Validated);
            _txtPW = _view.txtPW;
            _txtPW.TextChanged += new EventHandler(_txtPW_TextChanged);
            _btnPW = _view.btnPW;
            _btnPW.Click += new EventHandler(_btnPW_Click);
            // _txtPW.LostFocus += new EventHandler(LostPWFocus);
            _txtSenderEmail = _view.txtSenderEmail;
            _txtSenderEmail.TextChanged += new EventHandler(_txtSenderEmail_TextChanged);
            _txtSenderEmail.LostFocus += new EventHandler(LostSenderEmailFocus);
            _txtSenderEmail.Validating += new System.ComponentModel.CancelEventHandler(textBox_Validating);
            _txtSenderEmail.Validated += new EventHandler(textBox_Validated);
            _txtRecieverEmail = _view.txtReciverEmail;
            _txtRecieverEmail.TextChanged += new EventHandler(_txtRecieverEmail_TextChanged);
            // _txtRecieverEmail.LostFocus += new EventHandler(LostRecieverEmailFocus);
            _txtRecieverEmail.Validating += new System.ComponentModel.CancelEventHandler(textBox_Validating);
            _txtRecieverEmail.Validated += new EventHandler(textBox_Validated);
            _txtSubject = _view.txtHead;
            _txtSubject.TextChanged += new EventHandler(_txtSubject_TextChanged);
            // _txtSubject.LostFocus += new EventHandler(LostSubjectFocus);
            _txtBody = _view.txtMsgBody;
            _txtBody.TextChanged += new EventHandler(_txtBody_TextChanged);
            //_txtBody.LostFocus += new EventHandler(LostMessageFocus);
            _SendButton = _view.btnSend;
            _SendButton.Click += new EventHandler(_SendButton_Click);
            _AttachButton = _view.btnAttach;
            _AttachButton.Click += new EventHandler(_AttachButton_Click);
            //_txtAttach = _view.txtAttach;
            _lstAttach = _view.lstAttach;
            _btnAttachClear = _view.btnAttachClear;
            _btnAttachClear.Click += new EventHandler(_btnAttachClear_Click);
            _btnAttachDelete = _view.btnAttachDelete;
            _btnAttachDelete.Click += new EventHandler(_btnAttachDelete_Click);
            _btnClear = _view.btnClear;
            _btnClear.Click += new EventHandler(_btnClear_Click);
            _btnHelp = _view.btnHelp;
           // _btnHelp.Click += new EventHandler(_btnHelp_Click);
            _btnHAdd = _view.btnHAdd;
            _btnHAdd.Click += new EventHandler(_btnHAdd_Click);
            _btnHDelete = _view.btnHDelete;
            _btnHDelete.Click += new EventHandler(_btnHDelete_Click);
            _ErrorProvider = _view.EmailErrorProvider;
            _lblEmailError = _view.lblError;
            _lblEmailError2 = _view.lblError2;

            _txtCC = _view.txtCC;
            _txtBCC = _view.txtBCC;
            //_chbAuto = _view.chbAuto;
            //_chbAuto.Click+= new EventHandler(_chbAuto_CheckedChanged);
            //_chbAuto.CheckedChanged += new EventHandler(_chbAuto_CheckedChanged);
        }

        void _btnPW_Click(object sender, EventArgs e)
        {

            if (_txtPW.PasswordChar == '\0')
            {
                _txtPW.PasswordChar = '*';
            }
            else
            {
                _txtPW.PasswordChar = '\0';
            }


        }

        //void _chbAuto_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (_chbAuto.Checked)
        //    {
        //        _txtID.LostFocus +=new EventHandler(LostIDFocusChecked);
        //    }
        //    else
        //    {
        //        _txtID.LostFocus += new EventHandler(LostIDFocusUnChecked);
        //    }
        //}



        //추가 버튼 -> HostMaker 를 실행시킨다.
        void _btnHAdd_Click(object sender, EventArgs e)
        {
            HostMaker hm = new HostMaker();
            HMControllerEventReference(hm);


            //_combsSMTPAccounts 와의 연동 설정 해야 한다.

            foreach (string HostName in _combsSMTPAccounts.Items)
            {
                //Console.WriteLine(HostName + Environment.NewLine);
                //switch(HostName)
                //{
                //    case "": _cbgmail.Checked = true;
                //        break;
                //}
                //if ("_cb"+HosName)
                //{

                //}
                foreach (System.Windows.Forms.CheckBox controls in _viewHostMaker.Controls.Find("cb" + HostName, true))
                {
                    if ("cb" + HostName == controls.Name)
                    {
                        controls.Checked = true;
                    }
                    else
                    {
                        controls.Checked = false;
                    }
                }
            }
            _viewHostMaker.Show();
        }
        ///////////HostMaker Controller////////////////////////////////////////
        //////////////////////HostMaker.cs EventReference///////////////////////////
        public void HMControllerEventReference(HostMaker viewhm)
        {
            _viewHostMaker = viewhm;
            _btnHMAdd = _viewHostMaker.btnAdd;
            _btnHMAdd.Click += new EventHandler(_btnHMAdd_Click);
            _btnHMClose = _viewHostMaker.btnClose;
            _btnHMClose.Click += new EventHandler(_btnHMClose_Click);
            _cbgmail = _viewHostMaker.cbgmail;
            _cbdaum = _viewHostMaker.cbdaum;
            _cbnaver = _viewHostMaker.cbnaver;
            //_cbnate = _viewHostMaker.cbnate;
            _cbhanmail = _viewHostMaker.cbhanmail;
        }
        //_combsSMTPAccounts 에 호스트를 추가해준다.
        void _btnHMAdd_Click(object sender, EventArgs e)
        {
            //foreach (string HostName in _combsSMTPAccounts.Items)
            //{

            //    foreach (System.Windows.Forms.CheckBox controls in _viewHostMaker.Controls.Find("cb" + HostName, true))
            //    {
            //        if ("cb" + HostName == controls.Name)
            //        {
            //            controls.Checked = true;
            //        }
            //        else
            //        {
            //            controls.Checked = false;
            //        }
            //    }
            //}
            System.Windows.Forms.Control.ControlCollection cc = _viewHostMaker.Controls;
            System.Windows.Forms.Control c;
            ArrayList v1Hosts = new ArrayList();

            try
            {


                foreach (string v1Host in _combsSMTPAccounts.Items)
                {
                    v1Hosts.Add(v1Host);
                }
                //foreach(string v2Host in cc)
                //{
                //    v2Hosts.Add();
                //}

                for (int i = 0; i < cc.Count; i++)
                {
                    Console.WriteLine(cc[i]);
                    c = cc[i];
                    System.Type type = c.GetType();

                    if (type.Name == "CheckBox")
                    {
                        System.Windows.Forms.CheckBox CB = (System.Windows.Forms.CheckBox)c;
                        string oHost = CB.Name.Remove(0, 2);
                        if (CB.Checked == true)
                        {
                            if (v1Hosts.Contains(oHost))
                            {
                                continue;
                            }
                            else
                            {
                                _combsSMTPAccounts.Items.Add(oHost);
                            }
                        }
                        else
                        {
                            if (v1Hosts.Contains(oHost))
                            {
                                if (_combsSMTPAccounts.Items.Count == 1)
                                {
                                    MessageBox.Show("최소 1개의 환경이 있어야 합니다.");
                                    continue;
                                }
                                else
                                {
                                    _combsSMTPAccounts.Items.Remove(oHost);
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

                }

            }
            catch (Exception he)
            {
                MessageBox.Show(he.ToString());
            }
            finally
            {
                _combsSMTPAccounts.SelectedIndex = 0;
                v1Hosts.Clear();
                _viewHostMaker.Close();
            }

            //object o = cc.GetEnumerator().Current;
            //foreach( System.Windows.Forms.Control[] controls in cc["CheckBox"].Controls.GetEnumerator())
            //{
            //    Console.WriteLine(controls);
            //}
            //Object Controls = _viewHostMaker.Controls["CheckBox"].Controls.GetEnumerator().Current;
            ////foreach(System.Windows.Forms.Control controls in Controls)
            ////{

            //}


        }

        void _btnHMClose_Click(object sender, EventArgs e)
        {
            _viewHostMaker.Close();
        }

        void _btnHDelete_Click(object sender, EventArgs e)
        {
            //_combsSMTPAccounts.Items[_combsSMTPAccounts.SelectedIndex];
            if (_combsSMTPAccounts.Items.Count == 1)
            {
                MessageBox.Show("최소 1개의 환경이 있어야 합니다.");

            }
            else
            {
                _combsSMTPAccounts.Items.Remove(_combsSMTPAccounts.Items[_combsSMTPAccounts.SelectedIndex]);
            }
            _combsSMTPAccounts.SelectedIndex = 0;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////


        //public void _btnHelp_Click(object sender, EventArgs e)
        //{
        //    HelpPage hp = new HelpPage();
        //    hp.StartPosition = FormStartPosition.CenterParent;


        //    hp.lblHelp.Text = Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        //    hp.lblHelp.Text += "1. 계정에 2차 인증이 설정되어 있지 않는 경우" + Environment.NewLine + Environment.NewLine;
        //    hp.lblHelp.Text += "기존의 아이디, 패스워드를 입력하면 됩니다." + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        //    hp.lblHelp.Text += "2. 계정에 2차 인증이 설정되어 있는 경우" + Environment.NewLine + Environment.NewLine;
        //    hp.lblHelp.Text += "계정의 보안 페이지에 가서 앱 비밀번호를 생성한 후 " + Environment.NewLine + Environment.NewLine;
        //    hp.lblHelp.Text += "아이디에는 기존의 아이디," + Environment.NewLine + Environment.NewLine;
        //    hp.lblHelp.Text += "패스워드에는 생성한 앱 비밀번호를 입력하면 됩니다." + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        //    hp.lblHelp.Text += "하단의 '사용방법' 버튼을 클릭하여 다시 볼 수 있습니다." + Environment.NewLine + Environment.NewLine;

        //    hp.Show();
        //}



        void _SendButton_Click(object sender, EventArgs e)
        {

            //public void SendEmail(string sHost, int sPort, string sID, string sPW, string sFrom, string sTo,string sSubject,string sBody)

            try
            {
                if (sHost == "" || sID == "" || sPW == "" || sFrom == "" || sToA == null || sSubject == "")
                {
                    if (sHost == "")
                    {

                    }
                    else if (sID == "")
                    {
                        MessageBox.Show("아이디를 입력해주세요.");
                    }
                    else if (sPW == "")
                    {
                        MessageBox.Show("비밀번호를 입력해주세요.");
                    }
                    else if (sFrom == "")
                    {
                        MessageBox.Show("보낸 사람을 입력해주세요.");
                    }
                    else if (sToA == null)
                    {
                        MessageBox.Show("받는 사람을 입력해주세요.");
                    }
                    else if (sSubject == "")
                    {
                        MessageBox.Show("제목을 입력해주세요.");
                    }

                }
                //else if (sID == "필수" || sFrom == "필수" || sToA == null || sSubject == "제목을 입력해주세요." || sBody == "이메일 내용을 입력해주세요.")
                //{
                //    MessageBox.Show("필수 값을 입력해주세요.");
                //}
                else if (_lblEmailError.Visible == true || _lblEmailError2.Visible == true)
                {
                    MessageBox.Show("이메일 형식을 완성해주세요.");
                }
                else
                {
                    System.Xml.XmlDocument xml=null;
                    SujiraMail.SJRMail sm = new SujiraMail.SJRMail();
                    string ssTO = ""; ;
                    for(int i=0;i<sToA.Length;i++)
                    {
                        if (i != sToA.Length-1)
                        {
                            ssTO += sToA[i] + ",";
                        }
                        else
                        {
                            ssTO += sToA[i];
                        }
                    }
                    List<string> at = new List<string>();
                    
                    if (sAttach != null)
                    {

                        for (int i = 0; i < sAttach.Count; i++)
                        {
                            at.Add(sAttach[i].ToString());
                        }

                    }
                    else
                    {
                        at = null;
                    }
                    if (_txtCC.Text != "" || _txtBCC.Text != "")
                    {
                        xml = sm.SendMail(sID, sPW, sFrom, ssTO,_txtCC.Text.Trim(),_txtBCC.Text.Trim(), sSubject, sBody, at);
                    }
                    else
                    {
                         xml = sm.SendMail(sID, sPW, sFrom, ssTO,sSubject, sBody, at);
                    }
                    

                    System.IO.StringWriter stringWriter = new System.IO.StringWriter();
                    System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(stringWriter);

                    xml.WriteTo(xmlTextWriter);

                    MessageBox.Show(xml.OuterXml);

                    //if (sPort == 587)
                    //{

                    //    SendComplete = _model.SendEmail(sHost, sPort, sID, sPW, sFrom, _model.PortMail(sHost, sPort, sID, sPW, sFrom, sToA, sSubject, sBody, sAttach, false));
                    //}
                    //else
                    //{
                    //    SendComplete = _model.SendEmail(sHost, sID, sPW, _model.CDOPortMail(sHost, sPort, sID, sPW, sFrom, sToA, sSubject, sBody, sAttach, false));
                    //}

                }
            }
            catch (Exception ex)
            {

                if (sPort == 587)
                {
                    _model.PortMailClear();
                    SendComplete = false;
                }
                else
                {
                    _model.CDOPortMailClear();
                    SendComplete = false;
                }
                MessageBox.Show(ex.ToString());
            }

        }


        /////////////////////////호스트, 포트, 아이디, 비밀번호 에 대한 메서드///////////////////
        private void _combsSMTPAccounts_initialize()
        {
            MailHostSetting(_combsSMTPAccounts.SelectedIndex);
            MailPortSetting(_combsSMTPAccounts.SelectedIndex);
            _txtServerN.Text = sHost.Remove(0, 5);
            _txtPortN.Text = sPort.ToString();
        }
        void _combsSMTPAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {

            MailHostSetting(_combsSMTPAccounts.SelectedIndex);
            MailPortSetting(_combsSMTPAccounts.SelectedIndex);
            _txtServerN.Text = sHost.Remove(0, 5).Trim();
            _txtPortN.Text = sPort.ToString();

            //if (_txtSenderEmail.Text != "")
            //{
            //    _txtSenderEmail.Text = sID + "@" + _txtServerN.Text;
            //}


        }

        private string MailHostSetting(int si)
        {
            switch (_combsSMTPAccounts.Items[si].ToString())
            {
                case "gmail":
                    sHost = "smtp.gmail.com";
                    break;
                case "daum":
                    sHost = "smtp.daum.net";
                    break;
                case "naver":
                    sHost = "smtp.naver.com";
                    break;
                //case "nate":
                //    sHost = "smtp.mail.nate.com";
                //break;
                case "kakao":
                    sHost = "smtp.kakao.com";
                    break;
                case "hanmail":
                    sHost = "smtp.hanmail.net";
                    break;

            }

            return sHost;
        }
        private int MailPortSetting(int si)
        {
            switch (_combsSMTPAccounts.Items[si].ToString())
            {
                case "gmail":
                    sPort = 587;//587
                    break;
                case "daum":
                    sPort = 465;
                    break;
                case "naver":
                    sPort = 587;
                    break;
                //case "nate":
                //    sPort = 465;
                //    break;
                case "kakao":
                    sPort = 465;
                    break;
                case "hanmail":
                    sPort = 465;
                    break;

            }

            return sPort;
        }
        void _txtPW_TextChanged(object sender, EventArgs e)
        {
            TextBox txtbox = (TextBox)sender;

            txtbox.PasswordChar = '*';


            MailPW(_txtPW.Text);

        }

        void _txtID_TextChanged(object sender, EventArgs e)
        {
            MailID(_txtID.Text);
        }
        private string MailID(string id)
        {


            sID = id.Trim();


            return sID;
        }
        private string MailPW(string PW)
        {
            sPW = PW.Trim();
            return sPW;
        }
        /////////////////////////////////////////////////////////////////////////////////////
        //////////////////////메일 본문 관련 메서드/////////////////////////////////////////
        void _txtRecieverEmail_TextChanged(object sender, EventArgs e)
        {
            MailToAddress(_txtRecieverEmail.Text);
        }

        void _txtSenderEmail_TextChanged(object sender, EventArgs e)
        {
            MailFromAddress(_txtSenderEmail.Text);
        }
        void _txtBody_TextChanged(object sender, EventArgs e)
        {
            MailBody(_txtBody.Text);
        }

        void _txtSubject_TextChanged(object sender, EventArgs e)
        {
            MailSubject(_txtSubject.Text);
        }

        void _AttachButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Multiselect = true;
            //openFileDlg.DefaultExt = "db";
            //openFileDlg.Filter = "Excel Files(*.xlsx;*.db)|*.xlsx;*.db";
            openFileDlg.ShowDialog();
            if (openFileDlg.FileName.Length > 0)
            {
                //List<string> l = _lstAttach.Items;
            //    SujiraMail.SJRMail s = new SujiraMail.SJRMail();
            
            //System.Xml.XmlDocument sgg = s.SendMail("gudtkdml","2jung$kakao","gudtkdml@kakao.com","gudtkdml@kakao.com","g","g",l);
                foreach (string filename in openFileDlg.FileNames)
                {
                    // _txtAttach.Text += filename+Environment.NewLine;
                    _lstAttach.Items.Add(filename);
                    
                    AttachFile(filename);
                }
            }
        }
        void _btnAttachDelete_Click(object sender, EventArgs e)
        {
            RemoveAttachFile();
            if (_lstAttach.SelectedIndex != -1)
            {
                _lstAttach.Items.Remove(_lstAttach.Items[_lstAttach.SelectedIndex]);
            }
        }

        void _btnAttachClear_Click(object sender, EventArgs e)
        {
            ClearAttachFile();
            //_txtAttach.Text = "";
            _lstAttach.Items.Clear();
        }
        void _btnClear_Click(object sender, EventArgs e)
        {
            _txtID.Text = "";
            _txtPW.Text = "";
            _txtSenderEmail.Text = "";
            _txtRecieverEmail.Text = "";
            _txtBody.Text = "";
            _txtSubject.Text = "";
            ClearAttachFile();
            _lblEmailError.Visible = false;
            _lblEmailError2.Visible = false;
            if (SendComplete)
            {
                if (sPort == 587)
                {
                    _model.PortMailClear();
                }
                else
                {
                    _model.CDOPortMailClear();
                }
            }
        }
        private ArrayList AttachFile(string filepath)
        {

            sAttach.Add(filepath);
            return sAttach;
        }
        private ArrayList ClearAttachFile()
        {
            sAttach.Clear();
            return sAttach;
        }
        private ArrayList RemoveAttachFile()
        {
            if (sAttach.Count != 0)
            {
                sAttach.RemoveAt(sAttach.Count - 1);
            }
            return sAttach;
        }

        private string[] MailToAddress(string address)
        {
            
            char[] s = ",".ToCharArray();
            sToA = address.Split(s);
            return sToA;
        }

        private string MailFromAddress(string address)
        {
            sFrom = address;
            return sFrom;
        }
        private string MailSubject(string subject)
        {
            sSubject = subject;
            return sSubject;
        }
        private string MailBody(string body)
        {
            sBody = body;
            return sBody;
        }
        private void LostIDFocus(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text != "")
            {
                _txtSenderEmail.Text = sID + "@" + _txtServerN.Text;
            }

            //if (_txtSenderEmail.Text == "필수" || _txtSenderEmail.Text == "")
            //{
            //    _lblEmailException1.Visible = false;
            //}
            //if (_txtRecieverEmail.Text == "필수" || _txtRecieverEmail.Text == "")
            //{
            //    _lblEmailException2.Visible = false;
            //}
        }
        private void LostIDFocusUnChecked(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == "")
            {
                txt.Text = "";

            }
        }
        private void LostPWFocus(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            char[] ca = string.Empty.ToCharArray();
            if (txt.Text == "")
            {

                txt.Text = "";
            }
        }
        private void LostFocus(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == "")
            {
                txt.Text = "";
            }
            //if (_txtSenderEmail.Text == "필수" || _txtSenderEmail.Text == "")
            //{
            //    _lblEmailException1.Visible = false;
            //}
            //if (_txtRecieverEmail.Text == "필수" || _txtRecieverEmail.Text == "")
            //{
            //    _lblEmailException2.Visible = false;
            //}
        }
        private void LostSubjectFocus(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == "")
            {
                txt.Text = "";
            }
            //if (_txtSenderEmail.Text == "필수" || _txtSenderEmail.Text == "")
            //{
            //    _lblEmailException1.Visible = false;
            //}
            //if (_txtRecieverEmail.Text == "필수" || _txtRecieverEmail.Text == "")
            //{
            //    _lblEmailException2.Visible = false;
            //}
        }
        private void LostMessageFocus(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == "")
            {
                txt.Text = "";
            }
            //if (_txtSenderEmail.Text == "필수" || _txtSenderEmail.Text == "")
            //{
            //    _lblEmailException1.Visible = false;
            //}
            //if (_txtRecieverEmail.Text == "필수" || _txtRecieverEmail.Text == "")
            //{
            //    _lblEmailException2.Visible = false;
            //}
        }
        private void LostSenderEmailFocus(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == "")
            {
                txt.Text = "";
            }
            //else if (txt.Text.ToLower().Contains("@")==false || txt.Text.ToLower().Contains(".")==false)
            //{
            //    _lblEmailException1.Visible = true;
            //   // MessageBox.Show("이메일 형식에 맞지 않습니다.");
            //}
            //else if ((txt.Text.ToLower().Contains("@") == true && txt.Text.ToLower().Contains(".") == true))
            //{
            //    _lblEmailException1.Visible = false;
            //}
            //else if(txt.Text=="필수"||txt.Text=="")
            //{
            //    _lblEmailException1.Visible = false;
            //}

            //if (_txtRecieverEmail.Text == "필수" || _txtRecieverEmail.Text == "")
            //{
            //    _lblEmailException2.Visible = false;
            //}
        }
        private void LostRecieverEmailFocus(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == "")
            {
                txt.Text = "";
            }
            //else if (txt.Text.ToLower().Contains("@") == false || txt.Text.ToLower().Contains(".") == false)
            //{
            //    _lblEmailException2.Visible = true;
            //    // MessageBox.Show("이메일 형식에 맞지 않습니다.");
            //}
            //else if ((txt.Text.ToLower().Contains("@") == true && txt.Text.ToLower().Contains(".") == true))
            //{
            //    _lblEmailException2.Visible = false;
            //}
            //else if (txt.Text == "필수"||txt.Text=="")
            //{
            //    _lblEmailException2.Visible = false;
            //}

            //if (_txtSenderEmail.Text == "필수" || _txtSenderEmail.Text == "")
            //{
            //    _lblEmailException1.Visible = false;
            //}
        }
        string errorMsg;
        //ID TextBox Validating
        private void IDtextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox Sender = (TextBox)sender;
            TextBox textBox = _txtSenderEmail;
            if (textBox.Name == "txtSenderEmail")
            {
                if (!ValidEmailAddress(textBox.Text, out errorMsg))
                {
                    // Cancel the event and select the text to be corrected by the user.
                    //e.Cancel = true;
                    _lblEmailError.Visible = true;
                    //textBox.Select(0, textBox.Text.Length);

                    // Set the ErrorProvider error with the text to display. 
                    this._ErrorProvider.SetError(textBox, errorMsg);
                    this._ErrorProvider.GetError(textBox);
                }
            }
        }
        private void IDtextBox_Validated(object sender, System.EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            // If all conditions have been met, clear the ErrorProvider of errors.
            this._ErrorProvider.SetError(textBox, "");

        }

        //Sender, Reciever TextBox Validating
        private void textBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Name == "txtSenderEmail")
            {
                if (!ValidEmailAddress(textBox.Text, out errorMsg))
                {
                    // Cancel the event and select the text to be corrected by the user.
                    //e.Cancel = true;
                    _lblEmailError.Visible = true;
                    //textBox.Select(0, textBox.Text.Length);

                    // Set the ErrorProvider error with the text to display. 
                    this._ErrorProvider.SetError(textBox, errorMsg);
                    this._ErrorProvider.GetError(textBox);
                }
            }
            if (textBox.Name == "txtReciverEmail")
            {
                if (!ValidEmailAddress2(textBox.Text, out errorMsg))
                {
                    // Cancel the event and select the text to be corrected by the user.
                    //e.Cancel = true;
                    _lblEmailError2.Visible = true;
                    //textBox.Select(0, textBox.Text.Length);

                    // Set the ErrorProvider error with the text to display. 
                    this._ErrorProvider.SetError(textBox, errorMsg);
                    this._ErrorProvider.GetError(textBox);
                }
            }
        }
        private void textBox_Validated(object sender, System.EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            // If all conditions have been met, clear the ErrorProvider of errors.
            this._ErrorProvider.SetError(textBox, "");

        }
        public bool ValidEmailAddress(string emailAddress, out string errorMessage)
        {
            // Confirm that the email address string is not empty.
            if (emailAddress == "")
            {
                _lblEmailError.Visible = false;
                errorMessage = "";
                return true;
            }

            // Confirm that there is an "@" and a "." in the email address, and in the correct order.
            if (emailAddress.IndexOf("@") > -1)
            {
                if (emailAddress.IndexOf(".", emailAddress.IndexOf("@")) > emailAddress.IndexOf("@"))
                {
                    _lblEmailError.Visible = false;
                    errorMessage = "";
                    return true;
                }
            }

            errorMessage = "이메일 주소 형식으로 작성해야 합니다. 예) example@naver.com";
            return false;
        }
        public bool ValidEmailAddress2(string emailAddress, out string errorMessage)
        {
            char[] s = ",".ToCharArray();
            sToA = emailAddress.Split(s);
            string[] vr = new string[sToA.Length];
            bool result = true;
            // Confirm that the email address string is not empty.
            if (emailAddress == "")
            {
                _lblEmailError2.Visible = false;
                errorMessage = "";
                return true;
            }
            for (int i = 0; i < sToA.Length; i++)
            {
                // Confirm that there is an "@" and a "." in the email address, and in the correct order.
                if (sToA[i].IndexOf("@") > -1)
                {
                    if (sToA[i].IndexOf(".", sToA[i].IndexOf("@")) > sToA[i].IndexOf("@"))
                    {
                        _lblEmailError2.Visible = false;
                        errorMessage = "";
                        //return true;
                        vr[i] = "true";
                    }
                    else
                    {
                        errorMessage = "";
                        vr[i] = "false";
                    }

                }
                else
                {
                    errorMessage = "";
                    vr[i] = "false";
                }
            }
            errorMessage = "이메일 주소 형식으로 작성해야 합니다. 예) example@naver.com";
            for (int i = 0; i < vr.Length; i++)
            {
                if (vr[i] == "false")
                {
                    result = false;
                }

            }
            if (result == false)
            {
                return false;

            }
            return true;
        }



        ////////////////////////////////////////////////////////////////////////////////////
    }
}
