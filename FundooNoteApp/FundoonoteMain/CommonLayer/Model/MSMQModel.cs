using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Experimental.System.Messaging;

namespace CommonLayer.Model
{
    public class MSMQModel
    {
        MessageQueue messageQueue = new MessageQueue();
        public void sendDatatoQueue(string Token)
        {
            messageQueue.Path = @".\private$\Token";
            if (!MessageQueue.Exists(messageQueue.Path))
            {
                MessageQueue.Create(messageQueue.Path);
            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
            messageQueue.Send(Token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQueue.EndReceive(e.AsyncResult);
            string Token = msg.Body.ToString();
            string url = $"Fundoo Notes Reset Password: <a href=http://localhost:4200/reset/{Token}> Click Here</a>";
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("mohamedriaskhan@gmail.com");
            mail.To.Add("fundoonote199@gmail.com");
            mail.Subject = "subject";

            mail.IsBodyHtml = true;
            string htmlBody;
            mail.Subject = "FundooNote Reset Link";
            mail.Body = "<body><p>Dear Riyas,<br><br>" +
                "We have sent you a link for resetting your password.<br>" +
                "Please copy it and paste in your swagger authorization.</body>" + url;

            /*string Body = "<body><p>Dear Nantha,<br><br>" +
                "We have sent you a link for resetting your password.<br>" +
                "Please copy it and paste in your swagger authorization.</body>" + url;*/
            //string url = "https://localhost:4200/api/User/ResetPassword/";
            //string Body = "Hi Nantha,\nToken Generated To Reset Password\n\n" + "Session Token : " +url +Token;
            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("mohamedriaskhan@gmail.com", "cyyteymcbfkhxena"),
                EnableSsl = true,
            };
            SMTP.Send(mail);
            messageQueue.BeginReceive();

            //msmq
        }

    }


}
