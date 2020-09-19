using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace App.Service
{
    public class Email
    {
        public static bool SenacEmailIsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email.Trim()))
                return false;

            Regex regex = new Regex(@"^[a-z0-9](\.?[a-z0-9]){5,30}@senac.edu.br$");
            return regex.IsMatch(email);
        }

        public static void SendEmailRecoverPassword()
        {
            string bodyMessage = "<!DOCTYPE html><head><meta charset='utf-8'/>" +
               "<style>.style {text-align: center;color: white;font-family:sans-serif}" +
               "div {background-color:blueviolet; width: 400px; height: 180; position: absolute;}" +
               "</style></head><body class='style'><div><h1>foodRecipe</h1><p>Your new password is " +
               "{0}</p><p>Made with &#x2665; by Lucas</p> </div></body></html>";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("", ""),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage()
            {
                From = new MailAddress("alveslucasdev@gmail.com"),
                Subject = "Teste...",
                Body = bodyMessage,
                IsBodyHtml = true,
            };

            mailMessage.To.Add("lucasdeshok@gmail.com");
            smtpClient.Send(mailMessage);                    
        }
    }
}
