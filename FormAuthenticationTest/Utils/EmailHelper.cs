using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace FormAuthenticationTest.Utils
{
    public class EmailHelper
    {
        private static readonly SmtpClient Client;

        static EmailHelper()
        {
            Client = new SmtpClient(EmailConfig.SmtpServer,EmailConfig.SmtpPort );
            Client.Credentials = new NetworkCredential(EmailConfig.SmtpUser,EmailConfig.SmtpPass);
        }

        public static bool SendMessage(string from, string to, string subject, string body, string attachments)
        {
            MailMessage mm = null;
            //腾讯服务器要求：邮件发送地址必须与授权地址一致
            from = EmailConfig.SmtpUser;
            var isSent = false;
            try
            {
                mm = new MailMessage {From = new MailAddress(@from),Subject = subject,Body = body};
                mm.To.Add(to);
                mm.DeliveryNotificationOptions =DeliveryNotificationOptions.OnFailure;
                //添加附件，直接指定文件名（包含服务器完整的路径）
                if (attachments != "")
                    mm.Attachments.Add(new Attachment(attachments));
                Client.Send(mm);
                isSent = true;
            }
            catch (Exception ex)
            {
                var exMsg = ex.Message;
            }
            finally
            {
                if (mm != null)mm.Dispose();
            }
            return isSent;
        }

        public static bool SendWelcome(string email)
        {
            var body = "Welcome to mytest.com";
            return SendMessage(EmailConfig.AdminEmail, email, "Welcome message", body, "");
        }

        //从Web.config读取Email相关配置信息
        class EmailConfig
        {
            static EmailConfig()
            {
                SmtpServer = ConfigurationManager.AppSettings["smtpServer"];
                SmtpPort = int.Parse(ConfigurationManager.AppSettings["smtpPort"]);
                SmtpUser = ConfigurationManager.AppSettings["smtpUser"];
                SmtpPass = ConfigurationManager.AppSettings["smtpPass"];
                AdminEmail = ConfigurationManager.AppSettings["adminEmail"];
            }
            public static string SmtpServer { get; set; }
            public static int SmtpPort { get; set; }
            public static string SmtpUser { get; set; }
            public static string SmtpPass { get; set; }
            public static string AdminEmail { get; set; }
        }
    }
}