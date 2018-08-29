using AutoMapper.Configuration;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Drossey.Admin.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;

        public EmailSender(Microsoft.Extensions.Configuration.IConfiguration config)
        {
            _config = config;

        }

        public async Task<bool> SendEmailAsync(string email, string fullName, string code)
        {
            try
            {
                string toAddress = email;
                string bodyContent;
                string fromAdressTitle;
                string smtpServer;
                string pass;
                int port;
                string fromAddress;
               var subject = GetEmailContentSubject(out bodyContent, code);
                GetEmailSetting(out fromAddress, out fromAdressTitle, out smtpServer, out pass, out port);
                string toAdressTitle = fullName;
                int smtpPortNumber = port;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress
                                        (fromAdressTitle,
                                         fromAddress
                                         ));
                mimeMessage.To.Add(new MailboxAddress
                                         (toAdressTitle,
                                         toAddress
                                         ));
                mimeMessage.Subject = subject; //Subject
                mimeMessage.Body = new TextPart("html")
                {
                    Text = bodyContent
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(smtpServer, smtpPortNumber, false);
                    client.Authenticate(
                        fromAddress,
                        pass
                        );
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                    return true;
                }
            }
            catch (Exception e)
            {
                
                return false;
            }
        }

        private string GetEmailContentSubject(out string bodyContent, string code)
        {
            string subject;

            subject = "Confirm Code";
            var content = "{code}";
            bodyContent = content.Replace("code", code);




            return subject;
        }

        private string GetEmailSetting(out string fromAddress, out string fromAdressTitle, out string smtpServer, out string pass, out int port)
        {
            //string fromAddress = "m.besher.deltasoft@gmail.com";
            //fromAdressTitle = "Drossy Support Team";
            //smtpServer = "smtp.gmail.com";
            //pass = "Pa$$w0rd1123";
            //port = 587;

           


            fromAddress = _config["Emailserver:fromAddress"]; 
            fromAdressTitle = _config["Emailserver:fromAdressTitle"]; 
            smtpServer = _config["Emailserver:smtpServer"]; 
            pass = _config["Emailserver:pass"];
            port = int.Parse( _config["Emailserver:port"]);


            //string fromAddress = "noreply@drossey.com";
            //fromAdressTitle = "Drossy Support Team";
            //smtpServer = "relay-hosting.secureserver.net";
            //pass = "drossey.com";
            //port = 25;
          


            return fromAddress;
        }
    }
}

