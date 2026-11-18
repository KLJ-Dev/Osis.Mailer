using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Web.Mail;
using System.Collections;
using System.IO;





namespace SujiraMailer
{
#pragma warning disable 0612, 0618
    internal class SMTPModel
    {
        SmtpClient Client = new SmtpClient();
       private bool SendComplete = false;
            
        
        //메일 객체 생성
        private System.Web.Mail.MailMessage msg;
        private System.Net.Mail.MailMessage message;

        private Imap Imapclient = new Imap();
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
            /////////////////////2022-06-27/////////////////////////////////////////////////////////////////
            
           string imapH = sHost.Replace("smtp", "imap");
            Imapclient.Login(imapH, 993,sID , sPW, true);
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
        public System.Web.Mail.MailMessage CDOPortMail(string sHost, int sPort, string sID, string sPW, string sFrom, string[] sToA, string sSubject, string sBody, ArrayList sAttach,bool Clear)
        {
            msg = new System.Web.Mail.MailMessage();//방법1
            if (Clear)
            {
                msg = null;
            }
            else
            {
                SmtpMail.SmtpServer = sHost;
                msg.Body = sBody;
                msg.Subject = sSubject.Trim(); ;
                
                for(int i=0; i<sToA.Length;i++)
                {
                    if (i == sToA.Length-1)
                    {
                        msg.To += sToA[i].Trim();
                    }
                    else
                    {
                        msg.To += sToA[i].Trim()+ ", ";
                    }
                    
                }
                //msg.To = gudtkdml@naver.com, gudtkdml@kakao.com 이렇게 해도 들어가네
                
                msg.From = sFrom.Trim();

                string h = msg.Headers.ToString();
                ICollection FK = msg.Fields.Keys;
                //ICollection HeaderCollection =  msg.Headers..Headers.Keys;
                //foreach( in HeaderCollection)
                //{

                //}
                //첨부파일
                ArrayList attachFilesD = sAttach;
                if (attachFilesD != null)
                {
                    foreach (string filepath in attachFilesD)
                    {
                        if (System.IO.File.Exists(filepath))
                        {

                            msg.Attachments.Add(new System.Web.Mail.MailAttachment(filepath));
                            //message.Attachments.Add(new Attachment(filepath));
                            FileInfo FI = new FileInfo(filepath);
                            if (FI.Length > 4294800000)
                            {
                                System.Windows.Forms.MessageBox.Show("첨부파일의 최대 용량은 4GB까지 입니다.");
                            }

                        }
                    }
                }

                int cdoBasic = 1;

                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusingmethod", 2);
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", true);
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 465);
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", cdoBasic);
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", sID);
                msg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", sPW);
            }
            return msg;
        }
        public void CDOPortMailClear()
        {
            CDOPortMail("0",0,"0","0","0",null,"0","0",null, true); ;
        }
        public System.Net.Mail.MailMessage PortMail(string sHost, int sPort, string sID, string sPW, string sFrom, string[] sToA, string sSubject, string sBody, ArrayList sAttach,bool Clear)
        {
            ////////////////////////////////////구글, 네이버 계정 전용 프로토콜 시작////////////////////////////////////////////////////////////////
            //////////////////////메일의 내용을 초기화 편집합니다./////////////////////
            //보내는 사람과 한글깨짐을 방지하기 위해 UTF8로 인코딩한다.
            //MailAddress from = new MailAddress("보내는이의 이메일","address와 연결된 표시 이름",System.Text.Encoding.UTF8);
            if (sFrom != "0" || sToA != null)
            {
                
                //System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(from, to);


                message = new System.Net.Mail.MailMessage();

                message.From =new MailAddress(sFrom.Trim());
                foreach(string to in sToA)
                {
                    message.To.Add(new MailAddress(to.Trim()));
                }
               

                
                //SMTP 서버
            }
            //본 메세지 코딩
            if (Clear)
            {
                message=null;
            }
            else
            {
                
               
                //메일 제목
                message.Subject = sSubject.Trim();
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                //메일 본문
                message.Body = sBody;
                message.Body += Environment.NewLine;
                message.BodyEncoding = System.Text.Encoding.UTF8;



                ArrayList attachFiles = sAttach;
                if (attachFiles != null)
                {
                    foreach (string filepath in attachFiles)
                    {
                        if (System.IO.File.Exists(filepath))
                        {
                            message.Attachments.Add(new Attachment(filepath));
                        }
                    }
                }
            }
            return message;
            ////////////////////////////////////구글, 네이버 계정 전용 프로토콜 끝////////////////////////////////////////////////////////////////
        }
        public void PortMailClear()
        {
            PortMail("0",0,"0","0","0",null,"0","0",null, true);
        }
        public bool SendEmail(string sHost,string sID, string sPW,System.Web.Mail.MailMessage msg)
        {
            try
            {

                SmtpMail.Send(msg);//방법1
                ImapSend(sHost.Trim(), sID.Trim(), sPW.Trim(), msg);
                SendComplete = true;
            }
            catch (Exception e)
            {

                if (e.Message.Contains("0x800704217"))
                {
                    System.Windows.Forms.MessageBox.Show("SMTP 서버에 로그인 할 수 없습니다. 아이디와 비밀번호를 확인해주십시오."+Environment.NewLine+"2차 인증이 걸려있는 경우, 해제해주시기 바랍니다.");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                }
                SendComplete = false;
            }
           

            if (SendComplete)
            {
                System.Windows.Forms.MessageBox.Show("전송을 완료하였습니다.");
                msg = null;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("전송이 취소되었습니다.");
            }
            return SendComplete;

            
        }
        public bool SendEmail(string sHost,int sPort, string sID, string sPW,string sFrom, System.Net.Mail.MailMessage message)
        {

            

            //시스템에 설정된 인증 정보를 사용하지 않는다.
            Client.UseDefaultCredentials = false;
            //SSL을 사용한다.

            Client.EnableSsl = true;
            //Gmail에 인증을 받기 위한 설정
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //해당 호스트에 아이디와 패스워드 입력
            if (sHost == "smtp.gmail.com")
            {
                Client.Credentials = new System.Net.NetworkCredential(sFrom.Trim(), sPW.ToString().Trim());
            }
            else if (sHost == "smtp.naver.com")
            {
                Client.Credentials = new System.Net.NetworkCredential(sID.Trim(), sPW.ToString().Trim());
            }

                try
                {
                     Client.Send(message);
                     SendComplete = true;
                }
                catch (Exception e)
                {
                    if (e.Message.Contains("0x800704217")||e.Message.Contains("SMTP 서버에 보안 연결이 필요하거나 클라이언트가 인증되지 않았습니다. 서버 응답: 5.7.0 Authentication Required. Learn more at"))
                    {
                        System.Windows.Forms.MessageBox.Show("SMTP 서버에 로그인 할 수 없습니다. 아이디와 비밀번호를 확인해주십시오.");
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message.ToString());
                    }
                    SendComplete = false;
                }
                
                if (SendComplete)
                {
                    System.Windows.Forms.MessageBox.Show("전송을 완료하였습니다.");
                    //메세지 전송 후 내용 초기화
                    message.Dispose();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("전송이 취소되었습니다.");
                }

                
            
                ////////////////////////////////////전송 프로토콜 끝////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                return SendComplete;
            }

        }
#pragma warning restore 0612, 0618
    }


