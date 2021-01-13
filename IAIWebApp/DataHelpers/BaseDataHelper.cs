using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
using System.Web.Security;

namespace IAIWebApp.DataHelpers
{
    public class BaseDataHelper
    {

        public string CS
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["IAIConnectionString"].ConnectionString;
            }
        }


        protected CommandType SP
        {
            get
            {
                return CommandType.StoredProcedure;
            }
        }

        protected SqlParameter GetSqlParameter(string parameterName, SqlDbType sqlDbType, object value)
        {
            SqlParameter par = new SqlParameter(parameterName, sqlDbType);
            par.Value = value;
            return par;
        }

        protected SqlParameter GetSqlParameter(string parameterName, SqlDbType sqlDbType, object value, ParameterDirection direction)
        {
            SqlParameter par = new SqlParameter(parameterName, sqlDbType);
            par.Value = value;
            par.Direction = direction;
            return par;
        }

        protected SqlParameter GetSqlParameter(string parameterName, SqlDbType sqlDbType, object value, ParameterDirection direction, int size)
        {
            SqlParameter par = new SqlParameter(parameterName, sqlDbType);
            par.Size = size;
            par.Direction = direction;
            par.Value = value;
            return par;
        }

        protected SqlParameter GetSqlParameter(string parameterName, SqlDbType sqlDbType)
        {
            return new SqlParameter(parameterName, sqlDbType);
        }

        public static SqlCommand sqlCommand
        {
            get
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection;
                cmd.CommandTimeout = commondTimeOut;
                return cmd;
            }
        }
        static SqlConnection sqlConnection
        {
            get
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConnectionString;
                return conn;
            }
        }
        static int commondTimeOut
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings.Get("commandtimeout").ToString());
            }
        }
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["IAIConnectionString"].ConnectionString;
            }
        }

        public static string SmtpHost
        {
            get
            {
                return ConfigurationManager.AppSettings["smtphost"];
            }
        }
        public static string MailFrom
        {
            get
            {
                return ConfigurationManager.AppSettings["from"];
            }
        }
        //public static string Username
        //{
        //    get
        //    {
        //        return ConfigurationManager.AppSettings["from"];
        //    }
        //}

        public static string PDFFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["PDFFilePath"];
            }
        }

        public static string SmtpPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["password"];
            }
        }


        public static bool SendCreationMail
        {
            get
            {
                return ConfigurationManager.AppSettings["sendcreationmail"] == "1" ? true : false;
            }
        }

        public static string errorto
        {
            get
            {
                return ConfigurationManager.AppSettings["errorto"];
            }
        }

        public void sendErrorMail(Exception ex, string MethodName, string ClassName)
        {
            string Body = "" + ex + " <br/> "
            + "Method Name : " + MethodName + " <br/>"
            + "Class Name : " + ClassName + "";
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress("register@iaminterviewed.com");
            mailMessage.To.Add("praneeth.pn@gmail.com");
            mailMessage.Subject = "Exception";
            mailMessage.CC.Add("ramk@anterntech.com");
            mailMessage.Body = Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpHost;
            smtpClient.Credentials = new System.Net.NetworkCredential(MailFrom, SmtpPassword);
            smtpClient.Send(mailMessage);
        }

        public bool SendMail(string subject, string body, string fromEmail, string toEmails, bool sendEmail)
        {
            try
            {
                if (sendEmail)
                {
                    MailMessage mailMessage = new MailMessage();


                    mailMessage.From = new MailAddress(fromEmail);
                    mailMessage.To.Add(toEmails);
                    mailMessage.Subject = subject;

                    mailMessage.Body = body;

                    mailMessage.IsBodyHtml = true;
                    mailMessage.Priority = MailPriority.Normal;
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = SmtpHost;
                    smtpClient.Credentials = new System.Net.NetworkCredential(MailFrom, SmtpPassword);
                    smtpClient.Send(mailMessage);
                }

                return true;
            }
            catch (Exception exe)
            {
                return false;
            }

        }

        public bool SendMailWithAttachment(string subject, string body, string fromEmail, string toEmails, bool sendEmail)
        {
            try
            {
                if (sendEmail)
                {
                    MailMessage mailMessage = new MailMessage();


                    mailMessage.From = new MailAddress(fromEmail);
                    mailMessage.To.Add(toEmails);
                    mailMessage.Subject = subject;
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(System.Web.HttpContext.Current.Server.MapPath("~/app/test.html"));
                    mailMessage.Attachments.Add(attachment);
                    mailMessage.Body = body;

                    mailMessage.IsBodyHtml = true;
                    mailMessage.Priority = MailPriority.Normal;
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = SmtpHost;
                    smtpClient.Credentials = new System.Net.NetworkCredential(MailFrom, SmtpPassword);
                    smtpClient.Send(mailMessage);
                }

                return true;
            }
            catch (Exception exe)
            {
                return false;
            }

        }
    }
}