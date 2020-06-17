using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

namespace SimpleMassMailing.Data
{
    public class OutlookDotComMail
    {
        private Configuration _configuration;

        public OutlookDotComMail(Configuration configuration)
        {
            _configuration = configuration;
        }

        public void SendMail(string targetEMail, string content, Dictionary<string, string> parameters)
        {
            var title = content.InnerText("<title>", "</title>");
            var body = content;

            foreach (var parameter in parameters)
            {
                body = body.Replace("{" + parameter.Key + "}", parameter.Value);
            }

            var client = new SmtpClient(_configuration.Smtp);
            var message = new HtmlMessage()
            {
                Subject = title,
                Body = body
            };

            client.Port = Convert.ToInt32(_configuration.Port);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            var credentials = new System.Net.NetworkCredential(_configuration.Login, _configuration.Password);
            client.EnableSsl = _configuration.Ssl;
            client.Credentials = credentials;

            var from = new MailAddress(_configuration.From, _configuration.FromDisplayName);
            var to = new MailAddress(targetEMail);
            var mail = new MailMessage(from, to);
            mail.IsBodyHtml = true;
            mail.Subject = message.Subject;
            mail.Body = message.Body;

            if (!String.IsNullOrEmpty(_configuration.Attachment) &&
                File.Exists(_configuration.Attachment))
            {
                mail.Attachments.Add(new Attachment(_configuration.Attachment));
            }
            if (!String.IsNullOrEmpty(_configuration.Cc)) mail.CC.Add(_configuration.Cc);
            client.Send(mail);
            client.Dispose();
        }
    }
}
