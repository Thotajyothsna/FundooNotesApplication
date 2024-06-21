using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Model
{
     public class Send
    {
        public string SendMail(string ToEmail, string Token)
        {
            string FromEmail = "thotajyothsna2001@gmail.com";
            MailMessage Message = new MailMessage(FromEmail, ToEmail);
            string MailBody = "the token for the reset password: " + Token;
            Message.Subject = "Token Genereted for resetting Password";
            Message.Body = MailBody.ToString();
            Message.BodyEncoding= Encoding.UTF8;
            Message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential credential = new NetworkCredential("thotajyothsna2001@gmail.com", "zyse pevv uujw dkdi");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credential;
            smtpClient.Send(Message);

            return ToEmail;
        }
    }
}
