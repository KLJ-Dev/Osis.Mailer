using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Web.Mail;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Specialized;



//구글과 네이버 에서 2차인증이 걸려있기 때문에
//일반 비밀번호로는 SMTP가 접근하지 못한다.
//따라서 앱비밀번호를 따로 만들어줘야 한다.
//구글 앱 비번: fpmdakyasllkwwey

//네이버 앱 비번: RZRBW1E22N2L

//////////2022-06-08
//////////EnableSSL -> 프로젝트 -> 속성 -> 서명 에 SSL 인증서를 등록해야 한다.
//////////그렇게 하면 TLS : PORT 587 은 되지만 SSL: PORT 465 에서 전송만 되는(혹은 전송된다고 착각하는) 현상은 해결 할 수도 있을듯
//////////SSL 방식의 SSL HandShake 프로토콜(서버와 클라이언트 간의 인증 확인)이 통과되어야 Timed out 오류 해결 가능할듯
//////////정민 피셜: 포트 465 써도 587 포트 이용 가능하다. 일단 SSL 인증서 먼저 유효한지 확인 해봐야 한다.
//////////먼저 무료 SSL 서명 적용해서 -> 잘 된다면 -> Sujira 프로그램에 장착하기 위해 정식 유료 SSL 장착해야 한다.
//////////그 SSL 로 수지라 홈페이지도 HTTPS 로 변경 가능하지 않을까? 연구해봐야 할듯

//////////https://blog.hapinus.com/2020/ssl-certificate-lets-encrypt/ SSL 받는 방법 + 다양한 정보 블로그
//////////Let’s Encrypt는 CA(Certificate Authority)기관으로 무료로 인증서를 제공해주고 있다. 
//////////인증만료일은 3개월(90일)이지만 자동으로 재갱신을 설정할 수 있어서 오히려 보안적으로 더 좋다고 할 수 있다.

////////// 응용프로그램용 SSL 발급받기 : https://gigong.tistory.com/94
//////////실패 테스트 SSL 인증서 발급 받았는데 587로 하면 Timed out 뜨고, 다시 방법 1 2 로 하면 보낸 메일함에 저장이 안된다.

//////////Imap 에 연결하여 전송 완료시 보낸메일함에 메세지를 넣어보는 방법 써보자.
//////////Limilabs IMAP.dll 을 참조 추가했다.
//////////http://www.toughman.pe.kr/2018/02/c-pop3-imap-%EB%A9%94%EC%9D%BC-%EC%9D%BD%EC%96%B4%EC%98%A4%EA%B8%B0/ 응용
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////2022-06-08
//////////Limilabs IMAP.dll 은 좀 근거 없어서
//////////인터넷에서 그나마 보이는 유명한 S22.imap.dll 을 참조 추가해서 사용할 것이다.
//////////https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=cysk_&logNo=220252708403 S22.imap 사용법

namespace SujiraMailApplication
{
    class SJRMail
    {
        //메일 객체 생성
        private string sID=null;
        private string sPW=null;
        private bool CreateMail = false;
        private bool AttachComplete = true;
        private bool SendComplete = false;

        private Imap Imapclient = new Imap();

        private XmlDocument xmlD = new XmlDocument();
        private XmlNodeList MailNodes = null;
        private XmlNodeList SmtpNodes = null;
        
        
        /// <summary>
        /// 메일 관련 XML 파일을 구성합니다.
        /// </summary>
        /// <param name="MailID">XML 파일에 추가할 메일의 ID입니다.</param>
        /// <returns></returns>
        public XmlDocument CreateMailXML(string MailID)
        {
            XmlNodeList xnl = xmlD.ChildNodes;
            if (xnl.Count!=0)
            {
                

                XmlNode root = xmlD["Mailer"];
                //xmlD.AppendChild(root);


                XmlNode mail = xmlD.CreateElement("mail");
                XmlAttribute id = xmlD.CreateAttribute("id");
                id.Value = MailID;
                mail.Attributes.Append(id);
                XmlAttribute create = xmlD.CreateAttribute("create");
                create.Value = "false";
                mail.Attributes.Append(create);
                XmlAttribute send = xmlD.CreateAttribute("send");
                send.Value = "false";
                mail.Attributes.Append(send);
               
                XmlNode to = xmlD.CreateElement("to");
                //name1.InnerText = "gudtkdml@naver.com, gudtkdml@kakao.com";
                mail.AppendChild(to);

                XmlNode from = xmlD.CreateElement("from");
                //name1.InnerText = "gudtkdml@naver.com";
                mail.AppendChild(from);

                XmlNode subject = xmlD.CreateElement("subject");
                //name1.InnerText = "제목입니다.";
                mail.AppendChild(subject);

                XmlNode attachment = xmlD.CreateElement("attachment");
                //name1.InnerText = "파일경로 등..";
                mail.AppendChild(attachment);

                XmlNode body = xmlD.CreateElement("body");
                //name1.InnerText = "본문입니다.";
                mail.AppendChild(body);

                XmlNode mailerrmsg = xmlD.CreateElement("errmsg");
                //name1.InnerText = "받는 사람 이메일이 형식에 맞지 않습니다.";
                mail.AppendChild(mailerrmsg);

                root.AppendChild(mail);
                
            }
            else
            {


            XmlNode root =xmlD.CreateElement("Mailer");
            xmlD.AppendChild(root);

            XmlNode app = xmlD.CreateElement("app");
            app.InnerText = "SujiraSoft";
            root.AppendChild(app);

            XmlNode datatype = xmlD.CreateElement("datatype");
            datatype.InnerText = "xml";
            root.AppendChild(datatype);
            

            XmlNode mail = xmlD.CreateElement("mail");

            XmlAttribute id = xmlD.CreateAttribute("id");
            id.Value = MailID;
            mail.Attributes.Append(id);
            XmlAttribute create = xmlD.CreateAttribute("create");
            create.Value = "false";
            mail.Attributes.Append(create);
            XmlAttribute send = xmlD.CreateAttribute("send");
            send.Value = "false";
            mail.Attributes.Append(send);
           
            XmlNode to = xmlD.CreateElement("to");
            //name1.InnerText = "gudtkdml@naver.com, gudtkdml@kakao.com";
            mail.AppendChild(to);

            XmlNode from = xmlD.CreateElement("from");
            //name1.InnerText = "gudtkdml@naver.com";
            mail.AppendChild(from);

            XmlNode subject = xmlD.CreateElement("subject");
            //name1.InnerText = "제목입니다.";
            mail.AppendChild(subject);

            XmlNode attachment = xmlD.CreateElement("attachment");
            //name1.InnerText = "파일경로 등..";
            mail.AppendChild(attachment);

            XmlNode body = xmlD.CreateElement("body");
            //name1.InnerText = "본문입니다.";
            mail.AppendChild(body);

            XmlNode mailerrmsg = xmlD.CreateElement("errmsg");
            //name1.InnerText = "받는 사람 이메일이 형식에 맞지 않습니다.";
            mail.AppendChild(mailerrmsg);

            root.AppendChild(mail);

            }

            return xmlD;
        }

        /// <summary>
        /// SMTP 관련 XML 파일을 구성합니다.
        /// </summary>
        /// <param name="MailNumber">XML 파일에 추가할 메일의 ID입니다.</param>
        /// <returns></returns>
        public XmlDocument CreateSmtpXML(string MailNumber)
        {
            //string n = mailArgs.GetValues("MailID")[0];
            XmlNode mail = xmlD["Mailer"].SelectSingleNode("mail[@id=" + MailNumber + "]");
           
            XmlNode smtp = xmlD.CreateElement("smtp");
            XmlAttribute connect = xmlD.CreateAttribute("connect");
            connect.Value = "false";
            smtp.Attributes.Append(connect);

            XmlNode host = xmlD.CreateElement("host");
            //host.InnerText = "smtp.google.com";
            smtp.AppendChild(host);
            XmlNode port = xmlD.CreateElement("port");
            //host.InnerText = "smtp.google.com";
            smtp.AppendChild(port);
            XmlNode id = xmlD.CreateElement("id");
            //id.InnerText = "smtp.google.com";
            smtp.AppendChild(id);
            XmlNode pw = xmlD.CreateElement("pw");
            //pw.InnerText = "******";
            smtp.AppendChild(pw);
            XmlNode send = xmlD.CreateElement("send");
            //send.InnerText = "complete";
            smtp.AppendChild(send);
            XmlNode smtperrmsg = xmlD.CreateElement("errmsg");
            //send.InnerText = "complete";
            smtp.AppendChild(smtperrmsg);

            mail.AppendChild(smtp);

           
            return xmlD;
        }


        /// <summary>
        /// 메일 관련하여 xml 문서를 작성합니다.
        /// </summary>
        /// <param name="message">587 포트 메일입니다. google, naver</param>
        /// <param name="mailArgs">메일의 정보입니다. </param>
        /// <param name="xmlD">작성할 XmlDocument 타입의 xml 문서입니다.</param>
        private void XmlMailInnerText(System.Net.Mail.MailMessage message, NameValueCollection mailArgs, XmlDocument xmlD)
        {
            string n = mailArgs.GetValues("MailID")[0];
            XmlNode mailnode = xmlD["Mailer"].SelectSingleNode("mail[@id=" + n + "]");
            XmlNodeList mailchildnodes = xmlD["Mailer"].SelectSingleNode("mail[@id=" + n + "]").ChildNodes;//속성 조건으로 노드 하나만 선택하는 코드
            mailchildnodes[0].InnerText = message.To.ToString();
            mailchildnodes[1].InnerText = message.From.ToString();
            mailchildnodes[2].InnerText = message.Subject;
            
            string attachments="";
            for(int i=0; i<message.Attachments.Count;i++)
            {
                if(i==message.Attachments.Count-1)
                {
                    attachments+=message.Attachments[i].Name.ToString();
                }
                else
                {
                    attachments+=message.Attachments[i].Name.ToString()+", ";
                }
            }
            mailchildnodes[3].InnerText = attachments;
            mailchildnodes[4].InnerText = message.Body;

            string[] mailArgsKeys = mailArgs.AllKeys;
            foreach (string key in mailArgsKeys)
            {
                if (key == "Create")
                {
                    mailnode.Attributes["create"].Value = mailArgs.GetValues(key)[0].ToString();
                }
                else if (key == "errMsg")
                {
                    mailchildnodes[5].InnerText = mailArgs.GetValues(key)[0];
                }
            }
        }

        /// <summary>
        /// 메일 관련하여 xml 문서를 작성합니다.
        /// </summary>
        /// <param name="message">465 포트 메일입니다. daum, kakao, hanmail</param>
        /// <param name="mailArgs">메일 관련 정보들입니다.</param>
        /// <param name="xmlD">작성할 XmlDocument 타입의 xml 문서입니다.</param>
        private void XmlMailInnerText(System.Web.Mail.MailMessage msg, NameValueCollection mailArgs, XmlDocument xmlD)
        {
            string n = mailArgs.GetValues("MailID")[0];
            XmlNode mailnode = xmlD["Mailer"].SelectSingleNode("mail[@id=" + n + "]");
            XmlNodeList mailchildnodes = xmlD["Mailer"].SelectSingleNode("mail[@id=" + n + "]").ChildNodes;//속성 조건으로 노드 하나만 선택하는 코드
            mailchildnodes[0].InnerText = msg.To;
            mailchildnodes[1].InnerText = msg.From;
            mailchildnodes[2].InnerText = msg.Subject;
            
            string attachments="";
            for (int i = 0; i < msg.Attachments.Count; i++)
            {
                if (i == msg.Attachments.Count - 1)
                {
                    attachments += msg.Attachments[i].ToString();
                }
                else
                {
                    attachments += msg.Attachments[i].ToString() + ", ";
                }
            }
            mailchildnodes[3].InnerText = attachments;
            mailchildnodes[4].InnerText = msg.Body; 
            string[] mailArgsKeys = mailArgs.AllKeys;
            foreach (string key in mailArgsKeys)
            {
                if (key == "Create")
                {
                    mailnode.Attributes["create"].Value = mailArgs.GetValues(key)[0].ToString();
                }
                else if (key == "errMsg")
                {
                    mailchildnodes[5].InnerText = mailArgs.GetValues(key)[0];
                }
            }
            
        }

        /// <summary>
        /// 메일 관련하여 xml 문서를 작성합니다.
        /// </summary>
        /// <param name="sFrom">From</param>
        /// <param name="sTo">To</param>
        /// <param name="sSubject">메일 제목</param>
        /// <param name="sBody">메일 본문</param>
        /// <param name="mailArgs">메일 정보</param>
        /// <param name="xmlD">출력 대상 xml</param>
        private void XmlMailInnerText(string sFrom, string sTo, string sSubject, string sBody, NameValueCollection mailArgs, XmlDocument xmlD)
        {
            string n = mailArgs.GetValues("MailID")[0];
            XmlNode mailnode = xmlD["Mailer"].SelectSingleNode("mail[@id=" + n + "]");
            XmlNodeList mailchildnodes = xmlD["Mailer"].SelectSingleNode("mail[@id="+n+"]").ChildNodes;//속성 조건으로 노드 하나만 선택하는 코드
            //XmlNodeList s = mailchildnodes.Item;
            mailchildnodes[0].InnerText = sTo;
            mailchildnodes[1].InnerText = sFrom;
            mailchildnodes[2].InnerText = sSubject;
            mailchildnodes[4].InnerText = sBody;

            string[] mailArgsKeys = mailArgs.AllKeys;
            foreach (string key in mailArgsKeys)
            {
                if (key == "Create")
                {
                    mailnode.Attributes["create"].Value = mailArgs.GetValues(key)[0].ToString();
                }
                else if (key == "errMsg")
                {
                    mailchildnodes[5].InnerText = mailArgs.GetValues(key)[0];
                }
            }
        }

        /// <summary>
        /// 메일의 첨부파일 관련하여 xml 문서를 작성합니다.
        /// </summary>
        /// <param name="msg">465포트를 이용하는 메일입니다.</param>
        /// <param name="mailArgss">메일의 정보입니다.</param>
        /// <param name="xmlD">출력 대상 xml</param>
        private void XmlAttachInnerText(System.Web.Mail.MailMessage msg, NameValueCollection mailArgss, XmlDocument xmlD)
        {
            NameValueCollection mailArgs = mailArgss;
            if (mailArgs.GetValues("Create")[0] == "true")
            {
                string n = mailArgs.GetValues("MailID")[0];
                XmlNode mailnode = xmlD["Mailer"].SelectSingleNode("mail[@id=" + n + "]");
                XmlNodeList mailchildnodes = xmlD["Mailer"].SelectSingleNode("mail[@id=" + n + "]").ChildNodes;//속성 조건으로 노드 하나만 선택하는 코드

                string attachments = "";
                for (int i = 0; i < msg.Attachments.Count; i++)
                {
                    if (i == msg.Attachments.Count - 1)
                    {
                        System.Web.Mail.MailAttachment at = (System.Web.Mail.MailAttachment)msg.Attachments[i];
                        attachments += at.Filename.ToString();
                    }
                    else
                    {
                        System.Web.Mail.MailAttachment at = (System.Web.Mail.MailAttachment)msg.Attachments[i];
                        attachments += at.Filename.ToString() + ", ";
                    }
                }
                mailchildnodes[3].InnerText = attachments;
            }
            
        }

        /// <summary>
        /// 메일의 첨부파일 관련하여 xml 문서를 작성합니다.
        /// </summary>
        /// <param name="msg">587포트를 이용하는 메일입니다.</param>
        /// <param name="mailArgss">메일의 정보입니다.</param>
        /// <param name="xmlD">출력 대상 xml</param>
        private void XmlAttachInnerText(System.Net.Mail.MailMessage message, NameValueCollection mailArgs, XmlDocument xmlD)
        {
            string n = mailArgs.GetValues("MailID")[0];
            XmlNode mailnode = xmlD["Mailer"].SelectSingleNode("mail[@id=" + n + "]");
            XmlNodeList mailchildnodes = xmlD["Mailer"].SelectSingleNode("mail[@id=" + n + "]").ChildNodes;//속성 조건으로 노드 하나만 선택하는 코드

            string attachments="";
            for (int i = 0; i < message.Attachments.Count; i++)
            {
                if (i == message.Attachments.Count - 1)
                {
                    attachments += message.Attachments[i].Name.ToString();
                }
                else
                {
                    attachments += message.Attachments[i].Name.ToString() + ", ";
                }
            }
            mailchildnodes[3].InnerText = attachments;

        }

        /// <summary>
        /// SMTP 관련 Xml을 작성합니다.
        /// </summary>
        /// <param name="sHost">smtp.naver.com 같은 호스트입니다.</param>
        /// <param name="sPort">smtp의 포트입니다.</param>
        /// <param name="sID">smtp인증을 위한 아이디입니다.</param>
        /// <param name="sPW">smtp인증을 위한 비밀번호입니다.</param>
        /// <param name="smtpArg">smtp관련 정보들입니다.</param>
        /// <param name="xmlD">출력 대상 xml</param>
        private void XmlSMTPInnerText(string sHost, string sPort, string sID, string sPW, NameValueCollection smtpArgs, NameValueCollection mailArgs, XmlDocument xmlD)
        {
            string n = mailArgs.GetValues("MailID")[0];
            XmlNode mailnode = xmlD["Mailer"].SelectSingleNode("mail[@id=" + n + "]");
            string[] w = smtpArgs.GetValues("send");
            //XmlNodeList mailchildnodes = xmlD["Mailer"].SelectSingleNode("mail[@id=" + n + "]")["smtp"].ChildNodes;//속성 조건으로 노드 하나만 선택하는 코드
            XmlNodeList smtpchildnodes = mailnode.SelectSingleNode("smtp").ChildNodes;
            smtpchildnodes[0].InnerText = sHost;
            smtpchildnodes[1].InnerText = sPort;
            smtpchildnodes[2].InnerText = sID;
            smtpchildnodes[3].InnerText = sPW;
            smtpchildnodes[4].InnerText = smtpArgs.GetValues("send")[0];
            mailnode.Attributes["send"].Value = smtpArgs.GetValues("send")[0];
            foreach (string key in smtpArgs)
            {   if(smtpArgs.GetValues("connect")[0]=="true")
                {
                    mailnode["smtp"].Attributes["connect"].Value = "true";
                }
                if (key == "errMsg")
                {
                    smtpchildnodes[5].InnerText = smtpArgs.GetValues("errMsg")[0];
                }
            }
            
        }
        
        /// <summary>
        /// 생성과 보내기 성공한 메일의 내용을 xml로 출력합니다.
        /// </summary>
        public XmlDocument XmlResult
        {
            get
            {
                return xmlD;
            }
        }

        /// <summary>
        /// 465포트를 이용하는 메일을 해당 imap서버에 등록합니다.
        /// </summary>
        /// <param name="sHost">보내는 호스트입니다.</param>
        /// <param name="sID">보내는 계정의 ID 입니다.</param>
        /// <param name="sPW">보내는 계정의 PW 입니다.</param>
        /// <param name="msg">보내는 메일 입니다.</param>
        /// <returns></returns>
        private void ImapSend(string sHost,string sID, string sPW, System.Web.Mail.MailMessage msg)
        {
           string imapH = sHost.Replace("smtp", "imap");
            Imapclient.Login(imapH, 993,sID.Trim(), sPW.Trim(), true);
                try
                {
                    Imapclient.MessageAppend("Sent", msg, true);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                }
                
                 SendComplete = true;
                
                    ///////////////////////////////////////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// 465포트 전용 메일 인스턴스입니다. daum, kakao, paran, hanmail
        /// </summary>
        /// <param name="sFrom">보내는 사람의 이메일 입니다.</param>
        /// <param name="sTo">받을 사람의 이메일 입니다.</param>
        /// <param name="sSubject">보내는 메일의 제목 입니다.</param>
        /// <param name="sBody">보내는 메일의 본문 입니다.</param>
        /// <param name="Clear">메일 인스턴스 초기화를 위한 변수입니다. 실행시 다른 변수들은 "0" or null 입력바랍니다.</param>
        /// <returns></returns>
        //원본public System.Web.Mail.MailMessage CDOPortMail(string sFrom, string sTo, string sSubject, string sBody,bool Clear)
        public object[] CDOPortMail(string sFrom, string sTo, string sSubject, string sBody, bool Clear)
        {
            string MailID = DateTime.Now.ToShortDateString().Replace("-", "") + DateTime.Now.TimeOfDay.ToString().Remove(8).Replace(":", "");
            CreateMailXML(MailID);
            System.Web.Mail.MailMessage msg = null;
            NameValueCollection MailArgs = new NameValueCollection();

            string[] sToA = CreateRecieverList(sTo);
            
            try
            {
                msg = new System.Web.Mail.MailMessage();//방법1
                if (Clear)
                {
                        msg.Fields.Clear();
                        msg.Headers.Clear();
                        msg.Body = null;
                }
                else
                {
                        
                            string sHost = MailHostSetting(sFrom.Trim());
                            SmtpMail.SmtpServer = sHost;

                            for (int i = 0; i < sToA.Length; i++)
                            {
                                if (i == sToA.Length - 1)
                                {
                                    msg.To += sToA[i].Trim();
                                }
                                else
                                {
                                    msg.To += sToA[i].Trim() + ", ";
                                }

                            }
                            //msg.To = gudtkdml@naver.com, gudtkdml@kakao.com 이렇게 해도 들어가네

                            msg.From = sFrom.Trim();

                            string h = msg.Headers.ToString();
                            ICollection FK = msg.Fields.Keys;

                            msg.Body = sBody;
                            msg.Subject = sSubject.Trim(); ;


                            

                            if (EmailFormatValidating(sToA) && EmailFormatValidating(sFrom))
                            {
                                if (MailStructureValidating(msg))
                                {
                                    CreateMail = true;
                                    MailArgs.Add("MailID", MailID.ToString());
                                    MailArgs.Add("Create", "true");
                                    XmlMailInnerText(msg, MailArgs, xmlD);
                                }
                                else
                                {
                                    throw new Exception("메일의 기본 구성요소가 누락되었습니다. 메일 생성을 취소합니다.");
                                }
                            }
                            else
                            {                          
                                throw new Exception("이메일 주소 형식이 올바르지 않습니다. 메일 생성을 취소합니다.");
                            }
                       
                    }                    
                }
            
            catch(Exception e)
            {
                
                CreateMail = false;
                MailArgs.Add("MailID", MailID.ToString());
                MailArgs.Add("Create","false");
                if (e.Message.Contains("개체 참조"))
                {
                    MailArgs.Add("errMsg", "메일의 기본 구성요소가 누락되었습니다. 메일 생성을 취소합니다.");
                    XmlMailInnerText(sFrom, sTo, sSubject, sBody, MailArgs, xmlD);
                }
                else
                {
                    MailArgs.Add("errMsg", e.Message.ToString());
                    XmlMailInnerText(sFrom, sTo, sSubject, sBody, MailArgs, xmlD);
                }

                msg.Fields.Clear();
                msg.Headers.Clear();
                msg.Body = null;
                msg = null;

            }
            object[] param = new object[2];
            param[0]=msg;
            param[1]=MailArgs;
            return param;
            //원본return msg;
        }
        
       
        /// <summary>
        /// 465 포트 메일 인스턴스를 초기화 합니다.
        /// </summary>
        public void CDOPortMailClear()
        {
            CDOPortMail("0","0","0","0", true);
        }
        
        /// <summary>
        /// 587 포트 전용 메일 인스턴스입니다. google, naver
        /// </summary>
        /// <param name="sFrom">보내는 사람의 이메일 입니다.</param>
        /// <param name="sTo">받을 사람의 이메일 입니다.</param>
        /// <param name="sSubject">보내는 메일의 제목 입니다.</param>
        /// <param name="sBody">보내는 메일의 본문 입니다.</param>
        /// <param name="Clear">메일 인스턴스 초기화를 위한 변수입니다. 실행시 다른 변수들은 "0" or null 입력바랍니다.</param>        
        /// <returns></returns>
        public object[] PortMail(string sFrom, string sTo, string sSubject, string sBody, bool Clear)
        {

            string MailID = DateTime.Now.ToShortDateString().Replace("-", "") + DateTime.Now.TimeOfDay.ToString().Remove(8).Replace(":", "");
            CreateMailXML(MailID);
            ////////////////////////////////////구글, 네이버 계정 전용 프로토콜 시작////////////////////////////////////////////////////////////////
            System.Net.Mail.MailMessage message=null;
            NameValueCollection MailArgs = new NameValueCollection();
            try
            {
            //본 메세지 코딩
                if (Clear)
                {
                    message = null;
                }
                else
                {
                    string[] sToA = CreateRecieverList(sTo);
                   
                        message = new System.Net.Mail.MailMessage();

                        message.From = new MailAddress(sFrom.Trim());
                        foreach (string to in sToA)
                        {
                            message.To.Add(new MailAddress(to.Trim()));
                        }
                        //메일 제목
                        message.Subject = sSubject.Trim();
                        message.SubjectEncoding = System.Text.Encoding.UTF8;

                        //메일 본문
                        message.Body = sBody;
                        message.Body += Environment.NewLine;
                        message.BodyEncoding = System.Text.Encoding.UTF8;

                        if (EmailFormatValidating(sToA) && EmailFormatValidating(sFrom))
                        {
                            if (MailStructureValidating(message))
                            {
                                CreateMail = true;
                                MailArgs.Add("MailID", MailID.ToString());
                                MailArgs.Add("Create", "true");
                                //MailArgs.Add("Create Time", DateTime.Now.ToString());
                                XmlMailInnerText(message, MailArgs, xmlD);
                            }
                            else
                            {
                                throw new Exception("메일의 기본 구성요소가 누락되었습니다. 메일 생성을 취소합니다.");
                            }
                        }
                        else
                        {
                            if (EmailFormatValidating(sFrom)==false)
                            {
                                throw new Exception("보내는 사람의 이메일 형식이 올바르지 않습니다. 메일 생성을 취소합니다.");
                            }
                            else if(EmailFormatValidating(sToA)==false)
                            {
                                throw new Exception("받는 사람의 이메일 형식이 올바르지 않습니다. 메일 생성을 취소합니다.");
                            }
                            
                        }
                    
                }
            }
            catch (Exception e)
            {
                
                CreateMail = false;
                MailArgs.Add("MailID", MailID.ToString());
                MailArgs.Add("Create", "false");
                if (e.Message.Contains("지정한 문자열"))
                {
                    MailArgs.Add("errMsg", "받는 혹은 보내는 이메일 형식이 올바르지 않습니다. 메일 생성을 취소합니다.");
                    XmlMailInnerText(sFrom, sTo, sSubject, sBody, MailArgs, xmlD);
                }
                else
                {
                    MailArgs.Add("errMsg", e.Message.ToString());
                    XmlMailInnerText(message, MailArgs, xmlD);
                }
                
                

            }

            object[] param = new object[2];
            param[0] = message;
            param[1] = MailArgs;
            return param;
            
            ////////////////////////////////////구글, 네이버 계정 전용 프로토콜 끝////////////////////////////////////////////////////////////////
        }
        
        /// <summary>
        /// 587 포트 메일 인스턴스를 초기화 합니다.
        /// </summary>
        public void PortMailClear()
        {
            PortMail("0","0","0","0", true);
        }
        
        /// <summary>
        /// 465 포트 전용 메일을 전송합니다. 전송 결과를 bool형으로 반환 합니다.
        /// </summary>
        /// <param name="sID">보내는 계정의 아이디입니다.</param>
        /// <param name="sPW">보내는 계정의 비밀번호입니다.</param>
        /// <param name="msg">보내는 메일입니다. CDOPortMail() 메서드를 이용해주세요.</param>
        ///<param name="msgArgs">메일의 정보입니다.</param>
        /// <returns></returns>
        //원본public bool SendCDOEmail(string sID, string sPW, System.Web.Mail.MailMessage msg, bool ConsolePrint)
        public bool SendCDOEmail(string sID, string sPW, System.Web.Mail.MailMessage msg, NameValueCollection msgArgs)
        {
            NameValueCollection smtpArgs = new NameValueCollection();
            string g = msgArgs.GetValues("MailID")[0];
            CreateSmtpXML(g);
            string sHost="";
            try
            {
                if (msgArgs.GetValues("Create")[0] == "true")
                {
                    if (msg.Fields["http://schemas.microsoft.com/cdo/configuration/sendusingmethod"] == null)
                    {
                        int cdoBasic = 1;

                        msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusingmethod", 2);
                        msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", true);
                        msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 465);
                        msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
                        msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", sID);
                        msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", sPW);
                    }
                    sHost = MailHostSetting(msg.From.ToString().Trim());
                    SmtpMail.Send(msg);//방법1
                    ImapSend(sHost.Trim(), sID.Trim(), sPW.Trim(), msg);
                    SendComplete = true;
                    smtpArgs.Add("connect", "true");
                    smtpArgs.Add("send", "true");
                    XmlSMTPInnerText(sHost, "465", sID, sPW, smtpArgs, msgArgs, xmlD);
                }
                else
                {
                    throw new Exception("메일이 생성되지 않았습니다.");
                }
            }
            catch (Exception e)
            {

                if (e.Message.Contains("0x800704217")||e.Message.Contains("0x80040217")||e.Message.Contains("0x800ccc0b"))
                {
                    e.Message.Replace(e.Message.ToString(), "서버에 로그인 할 수 없습니다. 아이디와 비밀번호를 확인해주십시오." + Environment.NewLine + "2차 인증 사용시 해제 하거나, 애플리케이션 비밀번호를 사용해주세요.");
                }
                else
                {
                    e.Message.ToString().Insert(e.Message.Length," 메일 전송이 중단됩니다.");
                }
                if((msgArgs.GetValues("Create")[0] == "true"))
                {
                    msg.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/sendusingmethod");
                    msg.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/smtpusessl");
                    msg.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/smtpserverport");
                    msg.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate");
                    msg.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/sendusername");
                    msg.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/sendpassword");

                    msg = null;
                }
                SendComplete = false;
                smtpArgs.Add("send", "false");
                smtpArgs.Add("errMsg",e.Message.ToString());
                smtpArgs.Add("connect", "true");
                XmlSMTPInnerText(sHost, "465", sID, sPW, smtpArgs, msgArgs, xmlD);

            }
            if(msg!=null)
            {
            msg.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/sendusingmethod");
            msg.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/smtpusessl");
            msg.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/smtpserverport");
            msg.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate");
            msg.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/sendusername");
            msg.Fields.Remove("http://schemas.microsoft.com/cdo/configuration/sendpassword");
            msg=null;
            }
            return SendComplete;

            
        }
        
        /// <summary>
        /// 587 포트 전용 메일을 전송합니다. 전송 결과를 bool형으로 반환 합니다.
        /// </summary>
        /// <param name="sPort">보내는 포트입니다. 587로 고정예정.</param>
        /// <param name="sPW">보내는 계정의 비밀번호입니다.</param>
        /// <param name="message">보내는 메일입니다. PortMail() 메서드를 이용해주세요.</param>
        ///<param name="msgArgs">메일의 정보입니다.</param>
        /// <returns></returns>
        public bool SendEmail(string sID, string sPW, System.Net.Mail.MailMessage message, NameValueCollection msgArgs)
        {

            //원본 매개변수: string sHost, int sPort, string sID, string sPW, string sFrom, string sTo, string sSubject, string sBody, ArrayList sAttach
            NameValueCollection smtpArgs = new NameValueCollection();
            string g = msgArgs.GetValues("MailID")[0];
            CreateSmtpXML(g);

            string sHost = MailHostSetting(message.From.ToString().Trim());
            int sPort = MailPortSetting(message.From.ToString().Trim());
            SmtpClient Client = new SmtpClient(sHost, sPort);
            //시스템에 설정된 인증 정보를 사용하지 않는다.
            Client.UseDefaultCredentials = false;
            //SSL을 사용한다.

            Client.EnableSsl = true;
            //Gmail에 인증을 받기 위한 설정
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //해당 호스트에 아이디와 패스워드 입력
            

            if (sHost == "smtp.gmail.com")
            {
                Client.Credentials = new System.Net.NetworkCredential(sID.ToString().Trim(), sPW.ToString().Trim());
            }
            else if (sHost == "smtp.naver.com")
            {
                Client.Credentials = new System.Net.NetworkCredential(sID.Trim(), sPW.ToString().Trim());
            }

            try
            {
                if (msgArgs.GetValues("Create")[0] == "true")
                {
                    
                        Client.Send(message);
                        SendComplete = true;
                        smtpArgs.Add("connect", "true");
                        smtpArgs.Add("send", "true");
                        XmlSMTPInnerText(sHost, "587", sID, sPW, smtpArgs, msgArgs, xmlD);
                 
                }
                else
                {
                    throw new Exception("메일이 생성되지 않았습니다.");
                }
            }
            catch(Exception e)
            {
                if (e.Message.Contains("0x800704217") || e.Message.Contains("SMTP 서버에 보안 연결이 필요하거나 클라이언트가 인증되지 않았습니다."))
                {
                    smtpArgs.Add("errMsg", "서버에 로그인 할 수 없습니다. 아이디와 비밀번호를 확인해주십시오." + Environment.NewLine + "2차 인증 사용시 해제 하거나, 애플리케이션 비밀번호를 사용해주세요.");
                }
                else
                {
                   smtpArgs.Add("errMsg",e.Message.ToString());//+" 메일 전송이 취소되었습니다."
                }
                SendComplete = false;
                smtpArgs.Add("send", "false");
                smtpArgs.Add("connect", "true");
                XmlSMTPInnerText(sHost, "587", sID, sPW, smtpArgs, msgArgs, xmlD);
            }

            if (SendComplete)
            {
                message.Dispose();
            }

            
        
            ////////////////////////////////////전송 프로토콜 끝////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            return SendComplete;
        }
        
        /// <summary>
        /// 텍스트박스의 이메일 형식 적정성을 검토합니다. TEXTBOX.Validating 이벤트 핸들러에 추가해줘야 합니다.
        /// </summary>
        /// <param name="txt">이메일 형식을 검토할 텍스트박스입니다.</param>
        public bool EmailFormatValidating(System.Windows.Forms.TextBox txtBox)
        {
                string[] address = CreateRecieverList(txtBox.Text);
                string[] result = new string[address.Length];
                for (int i = 0; i < address.Length; i++)
                {
                    if (!ValidEmailAddress(address[i].Trim()))
                    {
                        result[i] = "false";
                    }
                }
                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i] == "false")
                    {
                        return false;
                    }
                }
            
            return true;
        }

        /// <summary>
        /// 문자열의 이메일 형식 적정성을 검토합니다.
        /// </summary>
        /// <param name="address">이메일 형식을 검토할 문자열입니다.</param>
        public bool EmailFormatValidating(string address)
        {
            if (!ValidEmailAddress(address.Trim()))
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// 문자열의 이메일 형식 적정성을 검토합니다. 
        /// </summary>
        /// <param name="address">이메일 형식을 검토할 문자열입니다. CreateRecieverList() 메서드를 이용해주세요.</param>
        public bool EmailFormatValidating(string[] address)
        {
            string[] result= new string[address.Length];
            for(int i=0; i<address.Length;i++)
            {
                if (!ValidEmailAddress(address[i].Trim()))
                {
                    result[i] = "false";
                }
            }
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == "false")
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 이메일 필수 값의 위치 적정성을 반환합니다.
        /// </summary>
        /// <param name="address">대상 이메일 주소입니다.</param>
        /// <returns></returns>
        private bool EmailPosition(string address)
        {
            if (address.ToLower().IndexOf("gmail", address.IndexOf("@")) > address.IndexOf("@"))
            {
                if (address.ToLower().IndexOf(".", address.IndexOf("@")) > address.ToLower().IndexOf("gmail", address.IndexOf("@")))
                {
                    return true;
                }
                return false;
            } 
            if (address.ToLower().IndexOf("kakao", address.IndexOf("@")) > address.IndexOf("@"))
            {
                if (address.ToLower().IndexOf(".", address.IndexOf("@")) > address.ToLower().IndexOf("naver", address.IndexOf("@")))
                {
                    return true;
                }
                return false;
            }
            if (address.ToLower().IndexOf("naver", address.IndexOf("@")) > address.IndexOf("@"))
            {
                if (address.ToLower().IndexOf(".", address.IndexOf("@")) > address.ToLower().IndexOf("kakao", address.IndexOf("@")))
                {
                    return true;
                }
                return false;
            }
            if (address.ToLower().IndexOf("daum", address.IndexOf("@")) > address.IndexOf("@"))
            {
                if (address.ToLower().IndexOf(".", address.IndexOf("@")) > address.ToLower().IndexOf("daum", address.IndexOf("@")))
                {
                    return true;
                }
                return false;
            }
            if (address.ToLower().IndexOf("hanmail", address.IndexOf("@")) > address.IndexOf("@"))
            {
                if (address.ToLower().IndexOf(".", address.IndexOf("@")) > address.ToLower().IndexOf("hanmail", address.IndexOf("@")))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private bool ValidEmailAddress(string address)
        {
            if (address.Trim() == "")
            {
                //받는 사람 없으면 통과
                return true;
            }
            if (address.IndexOf("@") > -1)
            {
                if (address.IndexOf(".", address.IndexOf("@")) > address.IndexOf("@"))
                {
                    if (EmailPosition(address))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    if (address.Contains(".com") || address.Contains(".net") || address.Contains(".org"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
           
            return true;
        }
        
        /// <summary>
        /// 첨부파일 들을 arraylist 형식으로 반환합니다.
        /// </summary>
        /// <returns></returns>
        public ArrayList AttachFilesToList()
        {
            ArrayList sATTACH = null;
            try
            {
                OpenFileDialog openFileDlg = new OpenFileDialog();
                openFileDlg.Multiselect = true;
                openFileDlg.ShowDialog();
                if (openFileDlg.CheckFileExists || openFileDlg.CheckPathExists)
                {
                    if (openFileDlg.FileName.Trim().Contains(".exe"))
                    {

                        throw new Exception("실행파일은 메일에 첨부 할 수 없습니다.");

                    }
                    else
                    {
                        foreach (string filename in openFileDlg.FileNames)
                        {
                            sATTACH.Add(filename.Trim());
                        }
                    }
                }
                else
                {
                    if (openFileDlg.CheckFileExists)
                    {
                        throw new Exception("파일이 존재하지 않습니다.");
                    }
                    if (openFileDlg.CheckPathExists)
                    {
                        throw new Exception("경로가 존재하지 않습니다.");
                    }
                }
            }
            catch(Exception e)
            {
                //xml 에 출력
            }
            return sATTACH;
        }
        
        /// <summary>
        /// 첨부파일들을 메세지에 첨부합니다.
        /// </summary>
        /// <param name="msg">465 포트 메일입니다.</param>
        /// <param name="mailArgss">메일 정보입니다.</param>
        public void AttachFilesToMail(System.Web.Mail.MailMessage msg, NameValueCollection mailArgss)
        {
            NameValueCollection AttachArgs = new NameValueCollection();
            NameValueCollection mailArgs = (NameValueCollection)mailArgss;
            try
            {
                OpenFileDialog openFileDlg = new OpenFileDialog();
                openFileDlg.Multiselect = true;
                openFileDlg.ShowDialog();
                if (openFileDlg.CheckFileExists || openFileDlg.CheckPathExists)
                {
                    if (openFileDlg.FileName.Trim().Contains(".exe"))
                    {
                        throw new Exception("실행파일은 메일에 첨부 할 수 없습니다.");
                    }
                    else
                    {
                        foreach (string filename in openFileDlg.FileNames)
                        {
                            //msg.Attachments.Add(new Attachment(filename.Trim()));
                            msg.Attachments.Add(new MailAttachment(filename));
                        }
                        AttachComplete = true;
                        XmlAttachInnerText(msg, mailArgs, xmlD);
                        //XmlAttachInnerText(msg, mailArgs, xmlD);
                    }
                }
                else
                {
                    if (openFileDlg.CheckFileExists)
                    {
                        throw new Exception("파일이 존재하지 않습니다.");
                    }
                    if (openFileDlg.CheckPathExists)
                    {
                        throw new Exception("경로가 존재하지 않습니다.");
                    }
                }
            }
            catch(Exception e)
            {
                AttachComplete = false;
                mailArgs.Add("errMsg",e.Message.ToString());
                XmlAttachInnerText(msg, mailArgs, xmlD);
            }
        }
       
        /// <summary>
        /// 첨부파일들을 메세지에 첨부합니다.
        /// </summary>
        /// <param name="message">587 포트 메일입니다.</param>
        /// <param name="mailArgss">메일 정보입니다.</param>
        public void AttachFilesToMail(System.Net.Mail.MailMessage message, NameValueCollection mailArgss)
        {
            NameValueCollection AttachArgs = new NameValueCollection();
            NameValueCollection mailArgs = (NameValueCollection)mailArgss;
            try
            {
                OpenFileDialog openFileDlg = new OpenFileDialog();
                openFileDlg.Multiselect = true;
                openFileDlg.ShowDialog();
                if (openFileDlg.CheckFileExists && openFileDlg.CheckPathExists)
                {
                    if (openFileDlg.FileName.Trim().Contains(".exe"))
                    {
                        throw new Exception("실행파일은 메일에 첨부 할 수 없습니다.");
                    }
                    else
                    {
                        foreach (string filename in openFileDlg.FileNames)
                        {
                            message.Attachments.Add(new Attachment(filename.Trim()));
                        }
                        AttachComplete = true;
                        XmlAttachInnerText(message, mailArgs, xmlD);
                    }
                }
                else
                {
                    if (openFileDlg.CheckFileExists == false)
                    {
                        throw new Exception("파일이 존재하지 않습니다.");
                    }
                    if (openFileDlg.CheckPathExists == false)
                    {
                        throw new Exception("경로가 존재하지 않습니다.");
                    }
                }
            }
            catch(Exception e)
            {
                AttachComplete = false;
                mailArgs.Add("errMsg", e.Message.ToString());
                XmlAttachInnerText(message, mailArgs, xmlD);
            }
        }
        
        /// <summary>
        /// 예외를 Console, Forms.MessageBox 로 출력할지 정하는 메서드입니다.
        /// </summary>
        /// <param name="consolePrint">Console로 출력할지 여부를 설정합니다. false 일 경우 자동으로 Forms.MessageBox로 출력됩니다.</param>
        /// <param name="str">출력할 문자열 입니다.</param>
        private void ExceptionPrintFormat(bool consolePrint,string str)
        {
            if (consolePrint)
            {
                Console.WriteLine(str.Trim());
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(str.Trim());
            }
        }

        /// <summary>
        /// ","로 구별되는 받는 사람들의 이메일 주소들을 리스트로 만들어 줍니다.
        /// </summary>
        /// <param name="str">","로 구별되는 받는 사람들의 이메일 주소들 문자열입니다.</param>
        /// <returns>문자열들을 ","로 나눈 배열로 반환합니다.</returns>
        public string[] CreateRecieverList(string str)
        {
            string[] RecieverList=null;
           
            char[] s = ",".ToCharArray();
            RecieverList = str.Trim().Split(s);
            string[] vr = new string[RecieverList.Length];
           
            return RecieverList;
        }
        /// <summary>
        /// 보내는 사람의 이메일 주소에 따른 smtp 서버를 설정합니다.
        /// </summary>
        /// <param name="From">보내는 사람의 이메일 주소입니다.</param>
        /// <returns>smtp.host.com 형식으로 호스트 주소를 반환합니다.</returns>
        private string MailHostSetting(string From)
        {
            string sHost="";
            try
            {
                if (ValidEmailAddress(From))
                {
                    if (From.ToLower().Contains("gmail"))
                    {
                        sHost = "smtp.gmail.com";
                    }
                    else if (From.ToLower().Contains("naver"))
                    {
                        sHost = "smtp.naver.com";
                    }
                    else if (From.ToLower().Contains("daum"))
                    {
                        sHost = "smtp.daum.net";
                    }
                    else if (From.ToLower().Contains("kakao"))
                    {
                        sHost = "smtp.kakao.com";
                    }
                    else if (From.ToLower().Contains("hanmail"))
                    {
                        sHost = "smtp.hanmail.com";
                    }
                }
                else
                {
                    throw new Exception("MailHostSetting()에서 예외가 발생했습니다. 맞는 호스트를 찾을 수 없습니다.");
                }
            }
            catch (Exception e)
            {

            }

            return sHost;
        }
        /// <summary>
        /// 보내는 사람의 이메일 주소에 따른 smtp 포트를 설정합니다.
        /// </summary>
        /// <param name="From">보내는 사람의 이메일 주소입니다.</param>
        /// <returns>호스트 주소에 따른 포트 값을 반환합니다.</returns>
        private int MailPortSetting(string From)
        {
            int sPort = 0;
            try
            {
                if (ValidEmailAddress(From))
                {
                    if (From.ToLower().Contains("gmail"))
                    {
                        sPort = 587;
                    }
                    else if (From.ToLower().Contains("naver"))
                    {
                        sPort = 587;
                    }
                    else if (From.ToLower().Contains("daum"))
                    {
                        sPort = 465;
                    }
                    else if (From.ToLower().Contains("kakao"))
                    {
                        sPort = 465;
                    }
                    else if (From.ToLower().Contains("hanmail"))
                    {
                        sPort = 465;
                    }
                }
                else
                {
                    throw new Exception("MailPortSetting()에서 예외가 발생했습니다. 맞는 호스트를 찾을 수 없습니다.");
                }
            }
            catch (Exception e)
            {

            }
            return sPort;
        }
        
        /// <summary>
        /// 메일 구조의 적정성을 검토합니다.
        /// </summary>
        /// <param name="message">587포트를 이용하는 메일입니다.</param>
        private bool MailStructureValidating(System.Net.Mail.MailMessage message)
        {
            if(message.To.ToString().Trim()=="")
            {
                throw new Exception("받는 이메일을 작성해주세요.");
                return false;
            }
            if (message.From.ToString().Trim() == "")
            {
                throw new Exception("보내는 이메일을 작성해주세요.");
                return false;
            }
            if (message.Subject.ToString().Trim() == "")
            {
                throw new Exception("이메일 제목을 작성해주세요.");
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// /// 메일 구조의 적정성을 검토합니다.
        /// </summary>
        /// <param name="msg">465포트를 이용하는 메일입니다.</param>
        private bool MailStructureValidating(System.Web.Mail.MailMessage msg)
        {
            if (msg.To.ToString().Trim() == "")
            {
                throw new Exception("받는 이메일을 작성해주세요.");
                return false;
            }
            if (msg.From.ToString().Trim() == "")
            {
                throw new Exception("보내는 이메일을 작성해주세요.");
                return false;
            }
            if (msg.Subject.ToString().Trim() == "")
            {
                throw new Exception("이메일의 제목을 작성해주세요.");
                return false;
            }
            return true;
        }
      }
}
    


