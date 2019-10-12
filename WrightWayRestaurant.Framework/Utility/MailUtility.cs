using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace WrightWayRestaurant.Framework.Utility
{
    public class MailUtility
    {
        static string emailAcount = ConfigurationManager.AppSettings.Get("MailServiceAccount");
        static string emailPassword = ConfigurationManager.AppSettings.Get("MailServiceAccountPassword");
        static string smtpService = ConfigurationManager.AppSettings.Get("MailServiceSmtpService");
        static int smtpServicePort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("MailServiceSmtpPort"));

 




        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="strTitle">标题</param>
        /// <param name="strBody">内容</param>
        /// <param name="strToEmails">收件人集合</param>
        /// <returns></returns>
        public static bool SendMail(string strTitle, string strBody, IList<string> strToEmails)
        {
            try
            {

                MailMessage message = new MailMessage();
                //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
                MailAddress fromAddr = new MailAddress(emailAcount);
                message.From = fromAddr;
                //设置收件人,可添加多个
                strToEmails.ToList().ForEach(mailTo =>
                {
                    message.To.Add(mailTo);
                });
                //设置邮件标题
                message.Subject = strTitle;
                //设置邮件内容
                message.Body = strBody;
                message.IsBodyHtml = true;
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                //设置邮件发送服务器,服务器根据你使用的邮箱而不同,可以到相应的 邮箱管理后台查看,下面是QQ的
                SmtpClient client = new SmtpClient(smtpService, smtpServicePort);
                //设置发送人的邮箱账号和密码
                client.Credentials = new NetworkCredential(emailAcount, emailPassword);
                //发送邮件
                client.Send(message);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="strTitle">标题</param>
        /// <param name="strBody">内容</param>
        /// <param name="strToEmails">收件人集合</param>
        /// <returns></returns>
        public static bool SendEmail(
            string strTitle,
            string strBody,
            IList<string> strToEmails,
            IList<string> strCCEmails = null,
            IList<string> strBccEmails = null
            )
        {
            try
            {
                MailMessage message = new MailMessage();
                //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
                MailAddress fromAddr = new MailAddress(emailAcount);
                //message.From.DisplayName = "";//显示名称
                message.From = fromAddr;
                //设置收件人,可添加多个
                strToEmails.ToList().ForEach(mailTo =>
                {
                    message.To.Add(mailTo);
                });
                //message.CC.Add("");//抄送
                //message.Bcc.Add(""); //密送

                //设置邮件标题
                message.Subject = strTitle;
                //设置邮件内容
                message.Body = strBody;
                message.IsBodyHtml = true;
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                //设置邮件发送服务器,服务器根据你使用的邮箱而不同,可以到相应的 邮箱管理后台查看,下面是QQ的
                SmtpClient client = new SmtpClient(smtpService, smtpServicePort);
                client.EnableSsl = false;
                //设置发送人的邮箱账号和密码
                client.Credentials = new NetworkCredential(emailAcount, emailPassword);
                //发送邮件
                client.Send(message);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static bool SendEmail(
            string strTitle,
            string strBody,
            string strToEmails
        )
        {      
            SmtpClient smtp = new SmtpClient(); //实例化一个SmtpClient
         
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network; //将smtp的出站方式设为 Network
            smtp.EnableSsl = true;   //smtp服务器是否启用SSL加密,为设置为false
            smtp.Host = smtpService;// smtpService; //指定 smtp 服务器地址
            smtp.Port = smtpServicePort;// smtpServicePort;             //指定 smtp 服务器的端口，默认是25，如果采用默认端口，可省去
            //如果你的SMTP服务器不需要身份认证，则使用下面的方式，不过，目前基本没有不需要认证的了
            smtp.UseDefaultCredentials = true;
            //如果需要认证，则用下面的方式
            smtp.Credentials = new NetworkCredential(emailAcount,emailPassword); 
         
            MailMessage mm = new MailMessage(); //实例化一个邮件类  
            mm.From = new MailAddress(emailAcount, "Wright Way Restaurant", Encoding.UTF8);
            //收件方看到的邮件来源；
            //第一个参数是发信人邮件地址
            //第二参数是发信人显示的名称
            //第三个参数是 第二个参数所使用的编码
            // mm.ReplyTo = new MailAddress("wangrc1@centaline.com.cn", "BookTouch", Encoding.UTF8);
            //ReplyTo 表示对方回复邮件时默认的接收地址，即：你用一个邮箱发信，但却用另一个来收信
            //上面后两个参数的意义， 同 From 的意义.Encoding.GetEncoding(936)
            mm.To.Add(strToEmails);
            //邮件的接收者
            mm.Subject = strTitle; //邮件标题
            mm.SubjectEncoding = Encoding.UTF8;
            mm.Body = strBody;
            mm.BodyEncoding = Encoding.UTF8;
            mm.IsBodyHtml = true;                       //邮件正文是否是HTML格式
            mm.BodyEncoding = Encoding.UTF8;


            try
            {
                smtp.Send(mm);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }


    }
}
