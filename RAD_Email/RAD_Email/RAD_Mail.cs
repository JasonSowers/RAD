using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RAD_Email
{
    public class RAD_Mail
    {
        public void SendRADMail(string message)
        {
            try
            {
                MailMessage email = new MailMessage(new MailAddress("CASHLINK-do-not-reply@symetra.com"), new MailAddress("jason.sowers@symetra.com"));
                
                //email.To.Add(new MailAddress("email@something.com"));
//                email.To.Add(new MailAddress("de.ders@tr.com"));
                email.CC.Add("1@2.com");
                email.Bcc.Add("email@something.com");
                email.Bcc.Add("1111111111@mms.att.net");
                StringBuilder msg = new StringBuilder("");
                msg.Append("\r\n");
                msg.Append("test");
                email.Body = msg.ToString();

                SmtpClient smtp = new SmtpClient {Host = "smtp.something.com"};
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                //do the stuff and possibly the things
            }
        } 



    }
}
