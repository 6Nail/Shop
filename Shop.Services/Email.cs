using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Shop.Services
{
    class Email
    {
        static void Main(string[] args)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("tnik8080@gmail.com");
            mail.To.Add("toleutaib@gmail.com");
            mail.Subject = "Warning";
            mail.Body = "A hack attempt was noticed";

            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("tnik8080@gmail.com", "DD123123");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);




        }
    }
}
