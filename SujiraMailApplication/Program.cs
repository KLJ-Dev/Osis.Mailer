using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SujiraMailer
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SujiraMailer se = new SujiraMailer();
            //SujiraMail.SJRMail s = new SujiraMail.SJRMail();
           // List<string> attach = new List<string>();
            //attach.Add(@"C:\Users\김이정\Desktop\SujiraMailApplication Projects\테스트자료\예.txt");

            //System.Xml.XmlDocument sssss = s.SendMail("sujirasoft", "smc105503", "sujirasoft@hanmail.net", "gudtkdml@kakao.com","gudtkdml@naver.com,gudtkdml@kakao.com","gudtkdml0830@gmail.com", "g", "g", attach);
            //System.Xml.XmlDocument ss = s.SendMail("gudtkdml0830@gmail.com", "zocqlnwlhrsyprhq", "gudtkdml0830@gmail.com", "gudtkdml@kakao.com", "gudtkdml@kakao.com", "gudtkdml@naver.com", "gg", "gg", attach);
            Application.Run(se);
        }
    }
}