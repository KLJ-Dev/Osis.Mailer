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
using FileCryptor;

namespace SujiraMail
{
    public class SJRMail
    {
        //메일 객체 생성
        //private string sID=null;
        //private string sPW=null;
       // private bool CreateMail;// = false;
       // private bool AttachComplete;// = true;
        private bool SendComplete = false;

        private Imap Imapclient = new Imap();

        public void PDFLock(string FilePath, string UserPW, string MasterPW)
        {
            FileCryptor.PDFEncryptor pdfencryptor = new FileCryptor.PDFEncryptor();
            pdfencryptor.EncryptPDF(FilePath,UserPW,MasterPW);
        }

       // private XmlDocument xmlD = new XmlDocument();
        //private XmlNodeList MailNodes = null;
        //private XmlNodeList SmtpNodes = null;
        
        
        /// <summary>
        /// 메일 관련 XML 파일을 구성합니다.
        /// </summary>
        /// <param name="MailID">XML 파일에 추가할 메일의 ID입니다.</param>
        /// <returns></returns>
        private XmlDocument CreateMailXML(string MailID)
        {
            
            //XmlNodeList xnl = xmlD.ChildNodes;
            //if (xnl.Count!=0)
            //{
                

            //    XmlNode root = xmlD["Mailer"];
            //    //xmlD.AppendChild(root);


            //    XmlNode mail = xmlD.CreateElement("mail");
            //    XmlAttribute id = xmlD.CreateAttribute("id");
            //    id.Value = MailID;
            //    mail.Attributes.Append(id);
            //    XmlAttribute create = xmlD.CreateAttribute("create");
            //    create.Value = "false";
            //    mail.Attributes.Append(create);
            //    XmlAttribute send = xmlD.CreateAttribute("send");
            //    send.Value = "false";
            //    mail.Attributes.Append(send);
               
            //    XmlNode to = xmlD.CreateElement("to");
            //    //name1.InnerText = "gudtkdml@naver.com, gudtkdml@kakao.com";
            //    mail.AppendChild(to);

            //    XmlNode from = xmlD.CreateElement("from");
            //    //name1.InnerText = "gudtkdml@naver.com";
            //    mail.AppendChild(from);

            //    XmlNode subject = xmlD.CreateElement("subject");
            //    //name1.InnerText = "제목입니다.";
            //    mail.AppendChild(subject);

            //    XmlNode attachment = xmlD.CreateElement("attachment");
            //    //name1.InnerText = "파일경로 등..";
            //    mail.AppendChild(attachment);

            //    XmlNode body = xmlD.CreateElement("body");
            //    //name1.InnerText = "본문입니다.";
            //    mail.AppendChild(body);

            //    XmlNode mailerrmsg = xmlD.CreateElement("errmsg");
            //    //name1.InnerText = "받는 사람 이메일이 형식에 맞지 않습니다.";
            //    mail.AppendChild(mailerrmsg);

            //    root.AppendChild(mail);
                
            //}
            //else
            //{

            XmlDocument XML = new XmlDocument();

            XmlNode root =XML.CreateElement("root");
            XML.AppendChild(root);

            XmlNode app = XML.CreateElement("app");
            app.InnerText = "SujiraSoft";
            root.AppendChild(app);

            XmlNode datatype = XML.CreateElement("datatype");
            datatype.InnerText = "xml";
            root.AppendChild(datatype);


            XmlNode mail = XML.CreateElement("mail");

            XmlAttribute id = XML.CreateAttribute("id");
            id.Value = MailID;
            mail.Attributes.Append(id);
            XmlAttribute create = XML.CreateAttribute("create");
            create.Value = "false";
            mail.Attributes.Append(create);
            XmlAttribute send = XML.CreateAttribute("send");
            send.Value = "false";
            mail.Attributes.Append(send);

            XmlNode to = XML.CreateElement("to");
            to.InnerText = "";
            mail.AppendChild(to);

            XmlNode from = XML.CreateElement("from");
            from.InnerText = "";
            mail.AppendChild(from);
            
            XmlNode cc = XML.CreateElement("cc");
            cc.InnerText = "";
            mail.AppendChild(cc);
            
            XmlNode bcc = XML.CreateElement("bcc");
            bcc.InnerText = "";
            mail.AppendChild(bcc);

            XmlNode subject = XML.CreateElement("subject");
            subject.InnerText = "";
            mail.AppendChild(subject);

            XmlNode attachment = XML.CreateElement("attachment");
            attachment.InnerText = "";
            mail.AppendChild(attachment);

            XmlNode body = XML.CreateElement("body");
            body.InnerText = "";
            mail.AppendChild(body);

            XmlNode mailerrmsg = XML.CreateElement("errmsg");
            mailerrmsg.InnerText = "";
            mail.AppendChild(mailerrmsg);

            root.AppendChild(mail);

            //}

            return XML;
        }

        /// <summary>
        /// SMTP 관련 XML 파일을 구성합니다.
        /// </summary>
        /// <param name="MailNumber">XML 파일에 추가할 메일의 ID입니다.</param>
        /// <returns></returns>
        private XmlDocument CreateSmtpXML(string MailNumber, XmlDocument xml)
        {
            XmlDocument XML = xml;
            //string n = mailArgs.GetValues("MailID")[0];
            XmlNode mail = XML["root"].SelectSingleNode("mail");

            XmlNode smtp = XML.CreateElement("smtp");
            XmlAttribute connect = XML.CreateAttribute("connect");
            connect.Value = "false";
            smtp.Attributes.Append(connect);

            XmlNode host = XML.CreateElement("host");
            host.InnerText = "";
            smtp.AppendChild(host);
            XmlNode port = XML.CreateElement("port");
            port.InnerText = "";
            smtp.AppendChild(port);
            XmlNode id = XML.CreateElement("id");
            id.InnerText = "";
            smtp.AppendChild(id);
            XmlNode pw = XML.CreateElement("pw");
            pw.InnerText = "";
            smtp.AppendChild(pw);
            XmlNode send = XML.CreateElement("send");
            send.InnerText = "";
            smtp.AppendChild(send);
            XmlNode smtperrmsg = XML.CreateElement("errmsg");
            smtperrmsg.InnerText = "";
            smtp.AppendChild(smtperrmsg);

            mail.AppendChild(smtp);

           
            return XML;
        }


        /// <summary>
        /// 메일 관련하여 xml 문서를 작성합니다.
        /// </summary>
        /// <param name="message">587 포트 메일입니다. google, naver</param>
        /// <param name="mailArgs">메일의 정보입니다. </param>
        /// <param name="xmlD">작성할 XmlDocument 타입의 xml 문서입니다.</param>
        private XmlDocument XmlMailInnerText(System.Net.Mail.MailMessage message, NameValueCollection mailArgs, XmlDocument xmlD)
        {
            string n = mailArgs.GetValues("MailID")[0];
            //XmlNodeList mailchildnodes = xmlD["root"].SelectSingleNode("mail").ChildNodes;//속성 조건으로 노드 하나만 선택하는 코드[@id=" + n + "]
            
            //mailchildnodes[0].InnerText = message.To.ToString();
            //mailchildnodes[1].InnerText = message.From.ToString();
            //mailchildnodes[2].InnerText = message.CC.ToString();
            //mailchildnodes[3].InnerText = message.Bcc.ToString();
            //mailchildnodes[4].InnerText = message.Subject;

            XmlNode mailnode = xmlD["root"].SelectSingleNode("mail");
            mailnode["to"].InnerText = message.To.ToString();
            mailnode["from"].InnerText = message.From.ToString();
            if (message.CC.Count != 0)
            {
                mailnode["cc"].InnerText = message.CC.ToString();
            }
            if (message.Bcc.Count != 0)
            {
                mailnode["bcc"].InnerText = message.Bcc.ToString();
                
            }
            
            mailnode["subject"].InnerText = message.Subject.ToString();
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
            //mailchildnodes[5].InnerText = attachments;
            //mailchildnodes[6].InnerText = message.Body;
            mailnode["attachment"].InnerText = attachments;
            mailnode["body"].InnerText = message.Body;

            string[] mailArgsKeys = mailArgs.AllKeys;
            foreach (string key in mailArgsKeys)
            {
                if (key == "Create")
                {
                    mailnode.Attributes["create"].Value = mailArgs.GetValues(key)[0].ToString();
                }
                else if (key == "errMsg")
                {
                    //mailchildnodes[7].InnerText = mailArgs.GetValues(key)[0];
                    mailnode["errmsg"].InnerText = mailArgs.GetValues(key)[0];
                }
            }
            return xmlD;
        }
        

        /// <summary>
        /// 메일 관련하여 xml 문서를 작성합니다.
        /// </summary>
        /// <param name="message">465 포트 메일입니다. daum, kakao, hanmail</param>
        /// <param name="mailArgs">메일 관련 정보들입니다.</param>
        /// <param name="xmlD">작성할 XmlDocument 타입의 xml 문서입니다.</param>
        //private XmlDocument XmlMailInnerText(System.Web.Mail.MailMessage msg, NameValueCollection mailArgs, XmlDocument xmlD)
        //{
        //    string n = mailArgs.GetValues("MailID")[0];
        //    XmlNode mailnode = xmlD["Mailer"].SelectSingleNode("mail[@id=" + n + "]");
        //    XmlNodeList mailchildnodes = xmlD["Mailer"].SelectSingleNode("mail[@id=" + n + "]").ChildNodes;//속성 조건으로 노드 하나만 선택하는 코드
        //    mailchildnodes[0].InnerText = msg.To;
        //    mailchildnodes[1].InnerText = msg.From;
        //    mailchildnodes[2].InnerText = msg.Subject;
            
        //    string attachments="";
        //    for (int i = 0; i < msg.Attachments.Count; i++)
        //    {
        //        if (i == msg.Attachments.Count - 1)
        //        {
        //            attachments += msg.Attachments[i].ToString();
        //        }
        //        else
        //        {
        //            attachments += msg.Attachments[i].ToString() + ", ";
        //        }
        //    }
        //    mailchildnodes[3].InnerText = attachments;
        //    mailchildnodes[4].InnerText = msg.Body; 
        //    string[] mailArgsKeys = mailArgs.AllKeys;
        //    foreach (string key in mailArgsKeys)
        //    {
        //        if (key == "Create")
        //        {
        //            mailnode.Attributes["create"].Value = mailArgs.GetValues(key)[0].ToString();
        //        }
        //        else if (key == "errMsg")
        //        {
        //            mailchildnodes[5].InnerText = mailArgs.GetValues(key)[0];
        //        }
        //    }
        //    return xmlD;
        //}

        private XmlDocument XmlMailInnerText(System.Web.Mail.MailMessage  msg, NameValueCollection mailArgs, XmlDocument xmlD)
        {
            string n = mailArgs.GetValues("MailID")[0];
            
            //XmlNodeList mailchildnodes = xmlD["root"].SelectSingleNode("mail").ChildNodes;//속성 조건으로 노드 하나만 선택하는 코드
            //mailchildnodes[0].InnerText = wmu.msg.To;
            //mailchildnodes[1].InnerText = wmu.msg.From;
            //mailchildnodes[2].InnerText = wmu.msg.Cc;
            //mailchildnodes[3].InnerText = wmu.msg.Bcc;
            //mailchildnodes[4].InnerText = wmu.msg.Subject;

            XmlNode mailnode = xmlD["root"].SelectSingleNode("mail");
            mailnode["to"].InnerText = msg.To.ToString();
            mailnode["from"].InnerText = msg.From.ToString();
            
            
            if(msg.Cc!=null)
            {
                mailnode["cc"].InnerText = msg.Cc.ToString();
            }
            if (msg.Bcc != null)
            {
                mailnode["bcc"].InnerText = msg.Bcc.ToString();    
            }
            
            mailnode["subject"].InnerText = msg.Subject.ToString();

            string attachments = "";
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
            mailnode["attachment"].InnerText = attachments;
            mailnode["body"].InnerText = msg.Body;
            string[] mailArgsKeys = mailArgs.AllKeys;
            foreach (string key in mailArgsKeys)
            {
                if (key == "Create")
                {
                    mailnode.Attributes["create"].Value = mailArgs.GetValues(key)[0].ToString();
                }
                else if (key == "errMsg")
                {
                    mailnode["errmsg"].InnerText = mailArgs.GetValues(key)[0];
                }
            }
            return xmlD;
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
        private XmlDocument XmlMailInnerText(string sFrom, string sTo, string sSubject, string sBody, NameValueCollection mailArgs, XmlDocument xmlD)
        {


            

            //string n = mailArgs.GetValues("MailID")[0].ToString();
            XmlNode mailnode = xmlD["root"].SelectSingleNode("mail");
            //XmlNodeList mailchildnodes = xmlD["root"].SelectSingleNode("mail").ChildNodes;//속성 조건으로 노드 하나만 선택하는 코드
            ////XmlNodeList s = mailchildnodes.Item;
            //mailchildnodes[0].InnerText = sTo;
            //mailchildnodes[1].InnerText = sFrom;
            //mailchildnodes[2].InnerText = sSubject;
            //mailchildnodes[4].InnerText = sBody;
            mailnode["to"].InnerText = sTo;
            mailnode["from"].InnerText = sFrom;
            mailnode["subject"].InnerText = sSubject;
            mailnode["body"].InnerText = sBody;

         
            

            string[] mailArgsKeys = mailArgs.AllKeys;
            foreach (string key in mailArgsKeys)
            {
                if (key == "Create")
                {
                    mailnode.Attributes["create"].Value = mailArgs.GetValues(key)[0].ToString();
                }
                else if (key == "errMsg")
                {
                    //mailchildnodes[5].InnerText = mailArgs.GetValues(key)[0];
                    mailnode["errmsg"].InnerText = mailArgs.GetValues(key)[0];
                    
                }
            }
            return xmlD;
        }
        private XmlDocument XmlMailInnerText(string sFrom, string sTo, string sCC,string sBCC, string sSubject, string sBody, NameValueCollection mailArgs, XmlDocument xmlD)
        {




            //string n = mailArgs.GetValues("MailID")[0].ToString();
            XmlNode mailnode = xmlD["root"].SelectSingleNode("mail");
            //XmlNodeList mailchildnodes = xmlD["root"].SelectSingleNode("mail").ChildNodes;//속성 조건으로 노드 하나만 선택하는 코드
            ////XmlNodeList s = mailchildnodes.Item;
            //mailchildnodes[0].InnerText = sTo;
            //mailchildnodes[1].InnerText = sFrom;
            //mailchildnodes[2].InnerText = sCC;
            //mailchildnodes[3].InnerText = sBCC;
            //mailchildnodes[4].InnerText = sSubject;
            //mailchildnodes[6].InnerText = sBody;
            mailnode["to"].InnerText = sTo;
            mailnode["from"].InnerText = sFrom;
            mailnode["cc"].InnerText = sCC;
            mailnode["bcc"].InnerText = sBCC;
            mailnode["subject"].InnerText = sSubject;
            mailnode["body"].InnerText = sBody;



            string[] mailArgsKeys = mailArgs.AllKeys;
            foreach (string key in mailArgsKeys)
            {
                if (key == "Create")
                {
                    mailnode.Attributes["create"].Value = mailArgs.GetValues(key)[0].ToString();
                }
                else if (key == "errMsg")
                {
                    //mailchildnodes[7].InnerText = mailArgs.GetValues(key)[0];
                    mailnode["errmsg"].InnerText = mailArgs.GetValues(key)[0];

                }
            }
            return xmlD;
        }
        /// <summary>
        /// 메일의 첨부파일 관련하여 xml 문서를 작성합니다.
        /// </summary>
        /// <param name="msg">465포트를 이용하는 메일입니다.</param>
        /// <param name="mailArgss">메일의 정보입니다.</param>
        /// <param name="xmlD">출력 대상 xml</param>
        private XmlDocument XmlAttachInnerText(System.Web.Mail.MailMessage msg, NameValueCollection mailArgss, XmlDocument xml)
        {
            NameValueCollection mailArgs = mailArgss;
            System.Xml.XmlDocument XML = xml;
            if (mailArgs.GetValues("Create")[0] == "true")
            {
                string n = mailArgs.GetValues("MailID")[0];
                XmlNode mailnode = XML["root"].SelectSingleNode("mail");
                XmlNodeList mailchildnodes = XML["root"].SelectSingleNode("mail").ChildNodes;//속성 조건으로 노드 하나만 선택하는 코드
                if (mailArgs.GetValues("Attach")[0] == "false")
                {
                    //mailchildnodes[3].InnerText = mailArgs.GetValues("AttacherrMsg")[0];
                    mailnode["attachment"].InnerText = mailArgs.GetValues("AttacherrMsg")[0];
                    
                }
                else
                {
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
                    //mailchildnodes[3].InnerText = attachments;
                    mailnode["attachment"].InnerText = attachments;
                }
            }
            return XML;   
        }

        /// <summary>
        /// 메일의 첨부파일 관련하여 xml 문서를 작성합니다.
        /// </summary>
        /// <param name="msg">587포트를 이용하는 메일입니다.</param>
        /// <param name="mailArgss">메일의 정보입니다.</param>
        /// <param name="xmlD">출력 대상 xml</param>
        private XmlDocument XmlAttachInnerText(System.Net.Mail.MailMessage message, NameValueCollection mailArgs, XmlDocument xml)
        {
            string n = mailArgs.GetValues("MailID")[0];
            System.Xml.XmlDocument XML = xml;
            XmlNode mailnode = XML["root"].SelectSingleNode("mail");
            XmlNodeList mailchildnodes = XML["root"].SelectSingleNode("mail").ChildNodes;//속성 조건으로 노드 하나만 선택하는 코드
            AttachmentCollection dm = message.Attachments;
            
            if (mailArgs.GetValues("Attach")[0] == "false")
            {
                //mailchildnodes[3].InnerText = mailArgs.GetValues("AttacherrMsg")[0];
                mailnode["attachment"].InnerText = mailArgs.GetValues("AttacherrMsg")[0];
            }
            else
            {
                string attachments = "";
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
                //mailchildnodes[3].InnerText = attachments;
                mailnode["attachment"].InnerText = attachments;
            }
            return XML;
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
            XmlNode mailnode = xmlD["root"].SelectSingleNode("mail");
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
        //private XmlDocument XmlResult
        //{
        //    get
        //    {
        //        return xmlD;
        //    }
        //}

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
        /// 메일을 생성합니다.
        /// </summary>
        /// <param name="sFrom">보내는 사람의 이메일 입니다.</param>
        /// <param name="sTo">받을 사람의 이메일 입니다.</param>
        /// <param name="sSubject">보내는 메일의 제목 입니다.</param>
        /// <param name="sBody">보내는 메일의 본문 입니다.</param>
        /// <returns></returns>
        private object[] CreateMail(string sFrom, string sTo, string sSubject, string sBody)
        {
            if (sFrom.ToLower().Contains("@gmail") || sFrom.ToLower().Contains("@naver"))
            {
                 return CDOPortMail(sFrom,sTo,sSubject,sBody,true);
            }
            else
            {
                return PortMail(sFrom, sTo, sSubject, sBody, true);
            }
            //return null;
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
        private object[] CDOPortMail(string sFrom, string sTo, string sSubject, string sBody, bool Clear)
        {
            string MailID = "1";
            XmlDocument XML = new XmlDocument();
            XML = CreateMailXML(MailID);
            
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


                            

                            if (MainEmailFormatValidating(sToA) && MainEmailFormatValidating(sFrom))
                            {
                                object[] mailvalid = MailStructureValidating(msg);
                                string err = (string)mailvalid[0];
                                bool v = (bool)mailvalid[1];
                                if (v)
                                {
                                    //CreateMail = true;
                                    MailArgs.Add("MailID", MailID.ToString());
                                    MailArgs.Add("Create", "true");
                                    //WebMailUs wmu = new WebMailUs(msg);
                                    //System.Web.Mail.MailMessage wmu = new WebMailUs(msg).msg;
                                    XML = XmlMailInnerText(msg, MailArgs, XML);
                                }
                                else
                                {
                                    throw new Exception("메일의 기본 구성요소가 누락되었습니다. "+err+"가 누락되었습니다. 메일 생성을 취소합니다.");
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
                
                //CreateMail = false;
                MailArgs.Add("MailID", MailID.ToString());
                MailArgs.Add("Create","false");
                if (e.Message.Contains("개체 참조"))
                {
                    MailArgs.Add("errMsg", "메일의 기본 구성요소가 누락되었습니다. 메일 생성을 취소합니다.");
                  XML=  XmlMailInnerText(sFrom, sTo, sSubject, sBody, MailArgs, XML);
                }
                else
                {
                    MailArgs.Add("errMsg", e.Message.ToString());
                    XML= XmlMailInnerText(sFrom, sTo, sSubject, sBody, MailArgs, XML);
                }

                msg.Fields.Clear();
                msg.Headers.Clear();
                msg.Body = null;
                msg = null;

            }
            object[] param = new object[3];
            param[0]=msg;
            param[1]=MailArgs;
            param[2] = XML;
            return param;
            //원본return msg;
        }

        /// <summary>
        /// 465포트 전용 메일 인스턴스입니다. daum, kakao, paran, hanmail
        /// </summary>
        /// <param name="sFrom">보내는 사람의 이메일 입니다.</param>
        /// <param name="sTo">받을 사람의 이메일 입니다.</param>
        /// <param name="sCC">참조입니다.</param>
        /// <param name="sBCC">숨은 참조입니다.</param>
        /// <param name="sSubject">보내는 메일의 제목 입니다.</param>
        /// <param name="sBody">보내는 메일의 본문 입니다.</param>
        /// <param name="Clear">메일 인스턴스 초기화를 위한 변수입니다. 실행시 다른 변수들은 "0" or null 입력바랍니다.</param>
        /// <returns></returns>
        private object[] CDOPortMail(string sFrom, string sTo, string sCC, string sBCC,string sSubject, string sBody, bool Clear)
        {
            string MailID = "1";
            XmlDocument XML = new XmlDocument();
            XML = CreateMailXML(MailID);

            System.Web.Mail.MailMessage msg = null;
            NameValueCollection MailArgs = new NameValueCollection();

            string[] sToA = CreateRecieverList(sTo);
            //string[] sCCA = CreateRecieverList(sCC);
            //string[] sBCCA = CreateRecieverList(sBCC);
            string[] sCCA = null;
            string[] sBCCA = null;

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
                    //for (int i = 0; i < sCCA.Length; i++)
                    //{
                    //    if (i == sCCA.Length - 1)
                    //    {
                    //        msg.Cc += sCCA[i].Trim();
                    //    }
                    //    else
                    //    {
                    //        msg.Cc += sCCA[i].Trim() + ", ";
                    //    }

                    //} 
                    //for (int i = 0; i < sBCCA.Length; i++)
                    //{
                    //    if (i == sBCCA.Length - 1)
                    //    {
                    //        msg.Bcc += sBCCA[i].Trim();
                    //    }
                    //    else
                    //    {
                    //        msg.Bcc += sBCCA[i].Trim() + ", ";
                    //    }

                    //}
                    if (sCC != "")
                    {
                        sCCA = CreateRecieverList(sCC);
                        for (int i = 0; i < sCCA.Length; i++)
                        {
                            if (i == sCCA.Length - 1)
                            {
                                msg.Cc += sCCA[i].Trim();
                            }
                            else
                            {
                                msg.Cc += sCCA[i].Trim() + ", ";
                            }
                        }
                    }

                    if (sBCC != "")
                    {
                        sBCCA = CreateRecieverList(sBCC);
                        for (int i = 0; i < sBCCA.Length; i++)
                        {
                            if (i == sBCCA.Length - 1)
                            {
                                msg.Bcc += sBCCA[i].Trim();
                            }
                            else
                            {
                                msg.Bcc += sBCCA[i].Trim() + ", ";
                            }
                        }
                    }
                    string h = msg.Headers.ToString();
                    ICollection FK = msg.Fields.Keys;


                    msg.Body = sBody;
                    msg.Subject = sSubject.Trim(); ;




                    if (MainEmailFormatValidating(sToA) && MainEmailFormatValidating(sFrom) && EmailFormatValidating(sCCA) && EmailFormatValidating(sBCCA))
                    {
                        object[] mailvalid = MailStructureValidating(msg);
                        string err = (string)mailvalid[0];
                        bool v = (bool)mailvalid[1];
                        if (v)
                        {
                            //CreateMail = true;
                            MailArgs.Add("MailID", MailID.ToString());
                            MailArgs.Add("Create", "true");
                            
                            XML = XmlMailInnerText(msg, MailArgs, XML);
                        }
                        else
                        {
                            throw new Exception("메일의 기본 구성요소가 누락되었습니다. " + err + "가 누락되었습니다. 메일 생성을 취소합니다.");
                        }
                    }
                    else
                    {
                        throw new Exception("이메일 주소 형식이 올바르지 않습니다. 메일 생성을 취소합니다.");
                    }

                }
            }

            catch (Exception e)
            {

                //CreateMail = false;
                MailArgs.Add("MailID", MailID.ToString());
                MailArgs.Add("Create", "false");
                if (e.Message.Contains("개체 참조"))
                {
                    MailArgs.Add("errMsg", "메일의 기본 구성요소가 누락되었습니다. 메일 생성을 취소합니다.");
                    XML = XmlMailInnerText(sFrom, sTo, sSubject, sBody, MailArgs, XML);
                }
                else
                {
                    MailArgs.Add("errMsg", e.Message.ToString());
                    XML = XmlMailInnerText(sFrom, sTo,sCC,sBCC, sSubject, sBody, MailArgs, XML);
                }

                msg.Fields.Clear();
                msg.Headers.Clear();
                msg.Body = null;
                msg = null;

            }
            object[] param = new object[3];
            param[0] = msg;
            param[1] = MailArgs;
            param[2] = XML;
            return param;
            //원본return msg;
        }
        
       
        /// <summary>
        /// 465 포트 메일 인스턴스를 초기화 합니다.
        /// </summary>
        private void CDOPortMailClear()
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
        private object[] PortMail(string sFrom, string sTo, string sSubject, string sBody, bool Clear)
        {

            string MailID = "1";
            XmlDocument XML = new XmlDocument();
            XML =CreateMailXML(MailID);
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

                        if (MainEmailFormatValidating(sToA) && MainEmailFormatValidating(sFrom))
                        {
                            object[] mailvalid = MailStructureValidating(message);
                             string err=(string)mailvalid[0];
                            bool v = (bool)mailvalid[1];
                            if (v)
                            {
                                //CreateMail = true;
                                MailArgs.Add("MailID", MailID.ToString());
                                MailArgs.Add("Create", "true");
                                //MailArgs.Add("Create Time", DateTime.Now.ToString());
                                XML = XmlMailInnerText(message, MailArgs, XML);

                            }
                            else
                            {
                                throw new Exception("메일의 기본 구성요소가 누락되었습니다."+err+"를 작성해주세요. 메일 생성을 취소합니다.");
                            }
                        }
                        else
                        {
                            if (MainEmailFormatValidating(sFrom)==false)
                            {
                                throw new Exception("보내는 사람의 이메일 형식이 올바르지 않습니다. 메일 생성을 취소합니다.");
                            }
                            else if(MainEmailFormatValidating(sToA)==false)
                            {
                                throw new Exception("받는 사람의 이메일 형식이 올바르지 않습니다. 메일 생성을 취소합니다.");
                            }
                            
                        }
                }
            }
            catch (Exception e)
            {
                
                //CreateMail = false;
                MailArgs.Add("MailID", MailID.ToString());
                MailArgs.Add("Create", "false");
                if (e.Message.Contains("지정한 문자열"))
                {
                    MailArgs.Add("errMsg", "받는 혹은 보내는 이메일 형식이 올바르지 않습니다. 메일 생성을 취소합니다.");
                  XML=  XmlMailInnerText(sFrom, sTo, sSubject, sBody, MailArgs, XML);
                }
                else
                {
                    MailArgs.Add("errMsg", e.Message.ToString());
                    XML=XmlMailInnerText(message, MailArgs, XML);
                }
                
                

            }

            object[] param = new object[3];
            param[0] = message;
            param[1] = MailArgs;
            param[2] = XML;
            return param;
            
            ////////////////////////////////////구글, 네이버 계정 전용 프로토콜 끝////////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// 587 포트 전용 메일 인스턴스입니다. google, naver
        /// </summary>
        /// <param name="sFrom">보내는 사람의 이메일 입니다.</param>
        /// <param name="sTo">받을 사람의 이메일 입니다.</param>
        /// <param name="sCC">참조입니다.</param>
        /// <param name="sBCC">숨은 참조입니다.</param>
        /// <param name="sSubject">보내는 메일의 제목 입니다.</param>
        /// <param name="sBody">보내는 메일의 본문 입니다.</param>
        /// <param name="Clear">메일 인스턴스 초기화를 위한 변수입니다. 실행시 다른 변수들은 "0" or null 입력바랍니다.</param>        
        /// <returns></returns>
        private object[] PortMail(string sFrom, string sTo, string sCC, string sBCC, string sSubject, string sBody, bool Clear)
        {

            string MailID = "1";
            XmlDocument XML = new XmlDocument();
            XML = CreateMailXML(MailID);
            ////////////////////////////////////구글, 네이버 계정 전용 프로토콜 시작////////////////////////////////////////////////////////////////
            System.Net.Mail.MailMessage message = null;
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
                    string[] sCCA = null;
                    string[] sBCCA = null;
                    

                    message = new System.Net.Mail.MailMessage();

                    message.From = new MailAddress(sFrom.Trim());
                    foreach (string to in sToA)
                    {
                        message.To.Add(new MailAddress(to.Trim()));
                    }

                    if (sCC != "")
                    {
                        sCCA = CreateRecieverList(sCC);
                        foreach (string to in sCCA)
                        {
                            message.CC.Add(new MailAddress(to.Trim()));
                        }
                    }
                    
                    if (sBCC != "")
                    {
                        sBCCA = CreateRecieverList(sBCC);
                        foreach (string to in sBCCA)
                        {
                            message.Bcc.Add(new MailAddress(to.Trim()));
                        }
                    }
                    

                    //메일 제목
                    message.Subject = sSubject.Trim();
                    message.SubjectEncoding = System.Text.Encoding.UTF8;

                    //메일 본문
                    message.Body = sBody;
                    message.Body += Environment.NewLine;
                    message.BodyEncoding = System.Text.Encoding.UTF8;

                    if (MainEmailFormatValidating(sToA) && MainEmailFormatValidating(sFrom) && EmailFormatValidating(sCCA) && EmailFormatValidating(sBCCA))
                    {
                        object[] mailvalid = MailStructureValidating(message);
                        string err = (string)mailvalid[0];
                        bool v = (bool)mailvalid[1];
                        if (v)
                        {
                            //CreateMail = true;
                            MailArgs.Add("MailID", MailID.ToString());
                            MailArgs.Add("Create", "true");
                            //MailArgs.Add("Create Time", DateTime.Now.ToString());
                            XML = XmlMailInnerText(message, MailArgs, XML);

                        }
                        else
                        {
                            throw new Exception("메일의 기본 구성요소가 누락되었습니다." + err + "를 작성해주세요. 메일 생성을 취소합니다.");
                        }
                    }
                    else
                    {
                        if (MainEmailFormatValidating(sFrom) == false)
                        {
                            throw new Exception("보내는 사람의 이메일 형식이 올바르지 않습니다. 메일 생성을 취소합니다.");
                        }
                        else if (MainEmailFormatValidating(sToA) == false)
                        {
                            throw new Exception("받는 사람의 이메일 형식이 올바르지 않습니다. 메일 생성을 취소합니다.");
                        }
                        else if (EmailFormatValidating(sCCA) == false)
                        {
                            throw new Exception("참조의 이메일 형식이 올바르지 않습니다. 메일 생성을 취소합니다.");
                        }
                        else if (EmailFormatValidating(sBCCA) == false)
                        {
                            throw new Exception("숨긴 참조의 이메일 형식이 올바르지 않습니다. 메일 생성을 취소합니다.");
                        }

                    }
                }
            }
            catch (Exception e)
            {

                //CreateMail = false;
                MailArgs.Add("MailID", MailID.ToString());
                MailArgs.Add("Create", "false");
                if (e.Message.Contains("지정한 문자열"))
                {
                    MailArgs.Add("errMsg", "입력한 이메일 형식이 올바르지 않습니다. 메일 생성을 취소합니다.");
                    XML = XmlMailInnerText(sFrom, sTo, sCC,sBCC,sSubject, sBody, MailArgs, XML);
                }
                else
                {
                    MailArgs.Add("errMsg", e.Message.ToString());
                    XML = XmlMailInnerText(message, MailArgs, XML);
                }



            }

            object[] param = new object[3];
            param[0] = message;
            param[1] = MailArgs;
            param[2] = XML;
            return param;

            ////////////////////////////////////구글, 네이버 계정 전용 프로토콜 끝////////////////////////////////////////////////////////////////
        }
        
        /// <summary>
        /// 587 포트 메일 인스턴스를 초기화 합니다.
        /// </summary>
        private void PortMailClear()
        {
            PortMail("0","0","0","0", true);
        }
        /// <summary>
        /// 참조, 숨은 참조를 포함한 메일 프로토콜입니다.
        /// </summary>
        /// <param name="ID">아이디입니다.</param>
        /// <param name="PW">비밀번호입니다.</param>
        /// <param name="From">보내는 사람의 이메일입니다.</param>
        /// <param name="To">받는 사람의 이메일입니다.</param>
        /// <param name="CC">참조입니다.</param>
        /// <param name="BCC">숨은 참조입니다.</param>
        /// <param name="Subject">제목입니다.</param>
        /// <param name="Body">본문입니다.</param>
        /// <param name="list">첨부파일 리스트입니다.</param>
        /// <returns></returns>
        public XmlDocument SendMail(string ID, string PW, string From, string To, string CC, string BCC, string Subject, string Body, List<string> list)//string AttachFiles)
        {
            XmlDocument XML = new XmlDocument();
            if (From.ToLower().Contains("@gmail") || From.ToLower().Contains("@naver"))
            {
                object[] msg = new object[2];
                msg = PortMail(From, To,CC,BCC, Subject, Body, false);
                System.Net.Mail.MailMessage mail = (System.Net.Mail.MailMessage)msg[0];
                NameValueCollection mailArgs = (NameValueCollection)msg[1];
                XML = (XmlDocument)msg[2];

                if (list != null)
                {
                    XML = AttachFilesToMail(mail, mailArgs, XML, list);
                }
                XML = SendEmail(ID, PW, mail, mailArgs, XML);
            }
            else
            {
                object[] msg = new object[2];
                msg = CDOPortMail(From, To,CC,BCC, Subject, Body, false);
                System.Web.Mail.MailMessage mail = (System.Web.Mail.MailMessage)msg[0];
                NameValueCollection mailArgs = (NameValueCollection)msg[1];
                XML = (XmlDocument)msg[2];
                if (list != null)
                {
                    XML = AttachFilesToMail(mail, mailArgs, XML, list);
                }
                XML = SendCDOEmail(ID, PW, mail, mailArgs, XML);
            }


            return XML;
        }
        public XmlDocument SendMail(string ID, string PW, string From, string To, string Subject, string Body, List<string> list)//string AttachFiles)
        {
            XmlDocument XML = new XmlDocument();
            if (From.ToLower().Contains("@gmail") || From.ToLower().Contains("@naver"))
            {
                object[] msg = new object[2];
                msg = PortMail(From, To, Subject, Body, false);
                System.Net.Mail.MailMessage mail = (System.Net.Mail.MailMessage)msg[0];
                NameValueCollection mailArgs = (NameValueCollection)msg[1];
                XML = (XmlDocument)msg[2];

                if (list != null)
                {
                    XML = AttachFilesToMail(mail, mailArgs,XML, list);
                }
               XML = SendEmail(ID, PW, mail, mailArgs,XML);
            }
            else
            {
                object[] msg = new object[2];
                msg = CDOPortMail(From, To, Subject, Body, false);
                System.Web.Mail.MailMessage mail = (System.Web.Mail.MailMessage)msg[0];
                NameValueCollection mailArgs = (NameValueCollection)msg[1];
                XML = (XmlDocument)msg[2];
                if (list != null)
                {
                   XML = AttachFilesToMail(mail, mailArgs,XML, list);
                }
                XML = SendCDOEmail(ID, PW, mail, mailArgs,XML);
            }
            

            return XML;
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
        private XmlDocument SendCDOEmail(string sID, string sPW, System.Web.Mail.MailMessage msg, NameValueCollection msgArgs,XmlDocument xml)
        {
            NameValueCollection smtpArgs = new NameValueCollection();
            NameValueCollection mailArgs = msgArgs;
            XmlDocument XML = xml;
            string a = "";
            foreach (string arg in mailArgs.AllKeys)
            {
                a += arg + " ";
            }

            string g = mailArgs.GetValues("MailID")[0];
            XML = CreateSmtpXML(g,XML);
            string sHost="";
            try
            {
                if (msgArgs.GetValues("Create")[0] == "true")
                {
                    if (a.Contains("AttacherrMsg"))
                    {
                        throw new Exception("첨부한 파일에 예외가 발생하여 전송이 취소되었습니다.");
                    }
                    else
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
                        XmlSMTPInnerText(sHost, "465", sID, sPW, smtpArgs, mailArgs, XML);
                    }
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
                if((mailArgs.GetValues("Create")[0] == "true"))
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
                XmlSMTPInnerText(sHost, "465", sID, sPW, smtpArgs, mailArgs, XML);

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
            return XML;

            
        }
        
        /// <summary>
        /// 587 포트 전용 메일을 전송합니다. 전송 결과를 bool형으로 반환 합니다.
        /// </summary>
        /// <param name="sPort">보내는 포트입니다. 587로 고정예정.</param>
        /// <param name="sPW">보내는 계정의 비밀번호입니다.</param>
        /// <param name="message">보내는 메일입니다. PortMail() 메서드를 이용해주세요.</param>
        ///<param name="msgArgs">메일의 정보입니다.</param>
        /// <returns></returns>
        private XmlDocument SendEmail(string sID, string sPW, System.Net.Mail.MailMessage message, NameValueCollection msgArgs, XmlDocument xml)
        {

            //원본 매개변수: string sHost, int sPort, string sID, string sPW, string sFrom, string sTo, string sSubject, string sBody, ArrayList sAttach
            NameValueCollection smtpArgs = new NameValueCollection();
            NameValueCollection mailArgs = msgArgs;
            XmlDocument XML = xml;
            string a = "";
            foreach (string arg in mailArgs.AllKeys)
            {
                a += arg + " ";
            }
            
            
            string g = msgArgs.GetValues("MailID")[0];
            XML =CreateSmtpXML(g,XML);

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
                if (mailArgs.GetValues("Create")[0] == "true")
                {
                    
                        if(a.Contains("AttacherrMsg"))
                        {
                            throw new Exception("첨부한 파일에 예외가 발생하여 전송이 취소되었습니다.");
                        }
                        else
                        {
                             Client.Send(message);
                            SendComplete = true;
                            smtpArgs.Add("connect", "true");
                            smtpArgs.Add("send", "true");
                            XmlSMTPInnerText(sHost, "587", sID, sPW, smtpArgs, mailArgs, XML);
                        }
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
                XmlSMTPInnerText(sHost, "587", sID, sPW, smtpArgs, mailArgs, XML);
            }

            if (SendComplete)
            {
                message.Dispose();
            }

            
        
            ////////////////////////////////////전송 프로토콜 끝////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            return XML;
        }
        
        /// <summary>
        /// 누락되어도 되는 텍스트박스의 이메일 형식 적정성을 검토합니다. TEXTBOX.Validating 이벤트 핸들러에 추가해줘야 합니다.
        /// </summary>
        /// <param name="txt">이메일 형식을 검토할 텍스트박스입니다.</param>
        private bool EmailFormatValidating(System.Windows.Forms.TextBox txtBox)
        {
            if (txtBox.Text != null)
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
            }
            return true;
        }
        private bool MainEmailFormatValidating(System.Windows.Forms.TextBox txtBox)
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
        /// 누락되어도 되는 문자열의 이메일 형식 적정성을 검토합니다.
        /// </summary>
        /// <param name="address">이메일 형식을 검토할 문자열입니다.</param>
        private bool EmailFormatValidating(string address)
        {
            if (address != null)
            {
                if (!ValidEmailAddress(address.Trim()))
                {
                    return false;
                }
            }
            return true;
        }
        private bool MainEmailFormatValidating(string address)
        {
            
                if (!ValidEmailAddress(address.Trim()))
                {
                    return false;
                }
            
            return true;
        }
        /// <summary>
        /// 누락되어도 되는 문자열의 이메일 형식 적정성을 검토합니다. 
        /// </summary>
        /// <param name="address">이메일 형식을 검토할 문자열입니다. CreateRecieverList() 메서드를 이용해주세요.</param>
        private bool EmailFormatValidating(string[] address)
        {
            if(address!=null)
            {
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
            }
            return true;
        }
        private bool MainEmailFormatValidating(string[] address)
        {
            
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
                    //else
                    //{
                    //    return false;
                    //}

                    if (address.Contains(".com") || address.Contains(".net") || address.Contains(".org"))
                    {
                        return true;
                    }
                    //else
                    //{
                    //    return false;
                    //}

                    return false;
                    
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
        
        ///// <summary>
        ///// 첨부파일 들을 arraylist 형식으로 반환합니다.
        ///// </summary>
        ///// <returns></returns>
        //public ArrayList AttachFilesToList()
        //{
        //    ArrayList sATTACH = null;
            
        //        OpenFileDialog openFileDlg = new OpenFileDialog();
        //        openFileDlg.Multiselect = true;
        //        openFileDlg.ShowDialog();
        //        if (openFileDlg.CheckFileExists || openFileDlg.CheckPathExists)
        //        {
        //            if (openFileDlg.FileName.Trim().Contains(".exe"))
        //            {

        //                Console.WriteLine("실행파일은 메일에 첨부 할 수 없습니다.");

        //            }
        //            else
        //            {
        //                foreach (string filename in openFileDlg.FileNames)
        //                {
        //                    sATTACH.Add(filename.Trim());
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (openFileDlg.CheckFileExists)
        //            {
        //                Console.WriteLine("파일이 존재하지 않습니다.");
        //            }
        //            if (openFileDlg.CheckPathExists)
        //            {
        //                Console.WriteLine("경로가 존재하지 않습니다.");
        //            }
        //        }

        //    return sATTACH;
        //}
        
        /// <summary>
        /// 첨부파일들을 메세지에 첨부합니다.
        /// </summary>
        /// <param name="msg">465 포트 메일입니다.</param>
        /// <param name="mailArgss">메일 정보입니다.</param>
        private XmlDocument AttachFilesToMail(System.Web.Mail.MailMessage msg, NameValueCollection mailArgss,XmlDocument xml, List<string> list)
        {
            NameValueCollection AttachArgs = new NameValueCollection();
            NameValueCollection mailArgs = (NameValueCollection)mailArgss;
            XmlDocument XML = xml;
            //string[] files = CreateRecieverList(Files);

            try
            {
               foreach (string file in list)
                {   
                    if(System.IO.File.Exists(file.Trim()))
                    {
                        if (file.Trim().Contains(".exe"))
                         {
                             throw new Exception(file+" 실행파일은 메일에 첨부 할 수 없습니다.");
                            
                         }
                         else
                         {
                             MailAttachment attachment = new MailAttachment(file.Trim());
                            //msg.Attachments.Add(new MailAttachment(file.Trim()));
                             msg.Attachments.Add(attachment);
                            mailArgs.Add("Attach", "true");
                            XML = XmlAttachInnerText(msg, mailArgs, XML);
                         }
                    }
                    else
                    {
                        throw new Exception(file+" 파일이 존재하지 않습니다.");
                    }
                }
            }
            catch(Exception e)
            {
                //AttachComplete = false;
                mailArgs.Add("Attach", "false");
                mailArgs.Add("AttacherrMsg",e.Message.ToString());
              XML=  XmlAttachInnerText(msg, mailArgs, XML);
            }
            return XML;
        }
       
        /// <summary>
        /// 첨부파일들을 메세지에 첨부합니다.
        /// </summary>
        /// <param name="message">587 포트 메일입니다.</param>
        /// <param name="mailArgss">메일 정보입니다.</param>
        private XmlDocument AttachFilesToMail(System.Net.Mail.MailMessage message, NameValueCollection mailArgss, XmlDocument xml,List<string> list)
        {
            NameValueCollection AttachArgs = new NameValueCollection();
            NameValueCollection mailArgs = (NameValueCollection)mailArgss;
            XmlDocument XML = xml;
            //string[] files = CreateRecieverList(Files);

            try
            {
                foreach (string file in list)
                {
                    if (System.IO.File.Exists(file.Trim()))
                    {
                        if (file.Trim().Contains(".exe"))
                        {
                            throw new Exception(file+" 실행파일은 메일에 첨부 할 수 없습니다.");
                        }
                        else
                        {
                            message.Attachments.Add(new Attachment(file.Trim()));
                            mailArgs.Add("Attach", "true");
                            XML = XmlAttachInnerText(message, mailArgs, XML);
                        }
                    }
                    else
                    {
                        throw new Exception(file+" 파일이 존재하지 않습니다.");
                    }
                }
            }
            catch (Exception e)
            {
                mailArgs.Add("Attach", "false");
                mailArgs.Add("AttacherrMsg", e.Message.ToString());
             XML=   XmlAttachInnerText(message, mailArgs, XML);
            }
            return XML;
        }

        /// <summary>
        /// ","로 구별되는 받는 사람들의 이메일 주소들을 리스트로 만들어 줍니다.
        /// </summary>
        /// <param name="str">","로 구별되는 받는 사람들의 이메일 주소들 문자열입니다.</param>
        /// <returns>문자열들을 ","로 나눈 배열로 반환합니다.</returns>
        private string[] CreateRecieverList(string str)
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
                        sHost = "smtp.hanmail.net";
                    }
                }
                else
                {
                    throw new Exception("MailHostSetting()에서 예외가 발생했습니다. 보내는 사람의 이메일 주소를 확인해주세요.");
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
            
            return sPort;
        }
        
        /// <summary>
        /// 메일 구조의 적정성을 검토합니다.
        /// </summary>
        /// <param name="message">587포트를 이용하는 메일입니다.</param>
        private object[] MailStructureValidating(System.Net.Mail.MailMessage message)
        {
            object[] param= new object[2];
            string err = "";
            bool b =true;
            if(message.To.ToString().Trim()=="")
            {
                err += "받는 이메일 ";
                b =false;
            }
            if (message.From.ToString().Trim() == "")
            {
                err += "보내는 이메일 ";
                b = false;
            }
            if (message.Subject.ToString().Trim() == "")
            {
                err += "이메일 제목 ";
                b =false;
            }
            param[0] = err;
            param[1] = b;

             
            return param;
        }
        
        /// <summary>
        /// /// 메일 구조의 적정성을 검토합니다.
        /// </summary>
        /// <param name="msg">465포트를 이용하는 메일입니다.</param>
        private object[] MailStructureValidating(System.Web.Mail.MailMessage msg)
        {
            object[] param = new object[2];
            string err = "";
            bool b = true;
            if (msg.To.ToString().Trim() == "")
            {
                err += "받는 이메일 ";
                b = false;
            }
            if (msg.From.ToString().Trim() == "")
            {
                err += "보내는 이메일 ";
                b = false;
            }
            if (msg.Subject.ToString().Trim() == "")
            {
                err += "이메일 제목 ";
                b = false;
            }
            param[0] = err;
            param[1] = b;


            return param;
        }
      }
}
    


