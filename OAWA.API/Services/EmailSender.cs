using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;
using System.Text;


namespace OAWA.API.Services {
    public class EmailSender : IEmailSender {
         

        // Our private configuration variables
        private string host;
        private int port;
        private bool enableSSL;
        private string userName;
        private string password;
        private string from;
        
        // Get our parameterized configuration
        public EmailSender(string host, int port, bool enableSSL, string userName, string password, string from) {
            this.host = host;
            this.port = port;
            this.enableSSL = enableSSL;
            this.userName = userName;
            this.password = password;
            this.from= from;
        }
        
        // Use our configuration to send the email by using SmtpClient
        public Task SendEmailAsync(string email, string subject, string htmlMessage) {
            var client = new SmtpClient(host, port) {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL
            };
            return client.SendMailAsync(
                new MailMessage(from, email, subject, htmlMessage) { IsBodyHtml = true }
            );
        }
    }
}
