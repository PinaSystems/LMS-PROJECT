using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Jupiter.Data.Helpers
{
    public class EmailManager
    {
        private string host;
        private int port;
        private bool enableSSL;
        private string userName;
        private string password;
        private string from;
        
        public EmailManager(string host, int port, bool enableSSL, string userName, string password, string from) {
            this.host = host;
            this.port = port;
            this.enableSSL = enableSSL;
            this.userName = userName;
            this.password = password;
            this.from= from;
        }
        public static async Task SendEmailAsync(string email, string subject, string htmlMessage, string url = "")
        {
            string sepath = url;

            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.Bcc.Add("support@pinasystems.com");
            //mail.Bcc.Add("jignesh.doshi@pinasystems.com");
            mail.Bcc.Add("support@pinasystems.com");
            //mail.Bcc.Add("shashik@pinasystems.com");
            // if (CC != "")
            //     mail.CC.Add(CC);
            mail.From = new MailAddress("info@hunterdouglasclub.com");
            LinkedResource logo=null;
            if(!string.IsNullOrEmpty(sepath))
            {
                logo = new LinkedResource(sepath);
                logo.ContentId = "companylogo";
            }
            mail.Subject = subject;
            AlternateView av1 = AlternateView.CreateAlternateViewFromString(htmlMessage, null, MediaTypeNames.Text.Html);
            if(!string.IsNullOrEmpty(sepath))
            {
                av1.LinkedResources.Add(logo);
            }
            mail.AlternateViews.Add(av1);

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.net4india.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = false;

            smtp.Send(mail);
        }

        public static string SendMessage(Batches batch)
        {
            //var values = new Dictionary<string, string>
            //{
            //{ "user",batch.User },
            //{ "password", batch.Password},
            //{ "senderid",batch.SenderId },
            //{ "channel",batch.Channel },
            //{ "DCS",batch.DCS },
            //{ "flashsms",batch.FlashSms },
            // { "number",batch.Number+",9967649789"},
            //  { "text",batch.Text },
            //{ "route",batch.Route }
            //};
            var values = new Dictionary<string, string>
        {
             { "key",batch.Key },
             { "type",batch.Type },
             { "contacts",batch.Number+",9967649789"},
             { "senderid", batch.SenderId},
             { "msg",batch.Text },
             { "routeid",batch.RouteId }
            };
            return ExecuteRest(MethodTypes.GET, "/app/smsapi/index.php", values);
        }
        static string ExecuteRest(MethodTypes methodType, string endpoint, Dictionary<string, string> values)
        {

            var postData = Uri.UnescapeDataString(WebUtility.UrlEncode(string.Join("&",
                        values.Select(kvp =>
                            string.Format("{0}={1}", kvp.Key, kvp.Value)))));
            //var url = "http://sms.global91sms.in" + endpoint + "?" + postData;
            var url = "http://www.global91sms.in" + endpoint + "?" + postData;
            var request = HttpWebRequest.Create(url);
            request.Method = methodType.ToString();
            request.ContentType = "application/x-www-form-urlencoded";
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
    }

    public class Batches : Accounts
        {
            public Accounts Account = new Accounts();
            public string Number { get; set; }
            public string SmsText { get; set; }
            public string Text { get; set; }
            public string Email { get; set; }
            public string EmailMessage { get; set; }
            public string EmailAttachment { get; set; }
        }
        public class Accounts
        {
            private string _key = "25C13AFD109505";
            private string _type = "text";
            private string _SenderId = "HDCLUB";
            //PINAYS
            private string _routeid = "459";

            public string Key { get { return _key; } set { _key = value; } }
            public string Type { get { return _type; } set { _type = value; } }
            public string SenderId { get { return _SenderId; } set { _SenderId = value; } }
            public string RouteId { get { return _routeid; } set { _routeid = value; } }
        }
        public enum MethodTypes
        {
            GET,
            POST
        }
}