using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API.Repository.Services
{
    public class Email
    {
        public static string SendEmailResetPassword(string email, string passwordReset, string firstName)
        {
            try
            {
                var fromAddress = new MailAddress("	akunt5634@gmail.com");
                var passwordFrom = "akunt3$t";
                var toAddress = new MailAddress(email);
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, passwordFrom)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Reset Password",
                    Body = $"<h1>Hallo {firstName}</h1> <br>" +
                            $"<h3>Ini Password barunya : {passwordReset}</h3>",
                    IsBodyHtml = true,
                }) smtp.Send(message);
                return "Berhasil Mengirim ke Email, Cek Emailnya ya~";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
