using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Collections.Specialized;

using System.Web;
using System.IO;
using System.Net.Mail;
using System.Configuration;

namespace MYFEELIB.Data
{
    public class Common
    {
        public static bool ValidateConnection(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    return (conn.State == ConnectionState.Open);
                }
                catch (Exception ex)
                {
                    ExceptionLog.ErrorLog(ex);
                    return false;
                }
            }
            return false;
        }

        //public static string USERNAME = "info@khetee.com";
        //public static string API_KEY = "43094f8e-6996-43a7-84a8-bef5df7dd1f0";
        public void SendEmail(string to, string subject, string bodyText, string bodyHtml, string from, string fromName, string attachments, string CCmail)
        {

            //WebClient client = new WebClient();
            //NameValueCollection values = new NameValueCollection();
            //values.Add("username", USERNAME);
            //values.Add("api_key", API_KEY);
            //values.Add("from", from);
            //values.Add("from_name", fromName);
            //values.Add("subject", subject);
            //if (bodyHtml != null)
            //    values.Add("body_html", bodyHtml);
            //if (bodyText != null)
            //    values.Add("body_text", bodyText);
            //values.Add("to", to);
            //byte[] response = client.UploadValues("https://api.elasticemail.com/mailer/send", values);
            //return Encoding.UTF8.GetString(response);

            //Naseer Starting
            string mgDomain = ConfigurationManager.AppSettings["mgDomain"].ToString(); //Text File Path
            string mgDomainPass = ConfigurationManager.AppSettings["mgDomainPass"].ToString(); //Text File Path

            MailMessage mail = new MailMessage(from, to);
            mail.Subject = subject;
            //mail.Body = bodyHtml;
            if (CCmail != string.Empty)
                mail.CC.Add(new MailAddress(CCmail));
            mail.IsBodyHtml = true;
            if (bodyHtml != null)
                mail.Body = bodyHtml;
            if (bodyText != null)
                mail.Body = bodyText;
            mail.Priority = MailPriority.High;
            // Send it!
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(mgDomain, mgDomainPass);
            client.Host = "smtp.mailgun.org";
            client.Send(mail);
            //Naseer Ending 
        }


    }
}
