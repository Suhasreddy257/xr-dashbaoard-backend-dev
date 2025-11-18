
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;


using AP.CHRP.XRDB.AC.DataContract;

namespace AP.CHRP.XRDB.AC.Communication
{
    public class AC_SendEMail
    {
        public String m_FromEmail;
        public List<String> ml_To;
        public List<String> ml_CC;
        public List<String> ml_BCC;

        public String m_EMailSubject;
        public String m_EMailBody;

        public List<AC_DC_NameValuePair> ml_InLineContentAttachmet;
        public List<string> ml_FileAttachmet;

        public String m_LastErrorMessage;

        public AC_SendEMail()
        {
            m_FromEmail = "";
            m_EMailSubject = "";
            m_EMailBody = "";
            m_LastErrorMessage = "";
            ml_To = new List<string>();
            ml_CC = new List<string>();
            ml_BCC = new List<string>();
            ml_InLineContentAttachmet = new List<AC_DC_NameValuePair>();
            ml_FileAttachmet = new List<string>();
        }

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

        private bool IsValidMailID(string strID)
        {
            if (strID.Contains("@") == false)
                return false;
            return true;
        }


        public int SendUsingSMTP(String Host, int Port, String FromAddress, String FromPassword, bool EnableSSL)
        {
            try
            {
                SmtpClient smtp = new SmtpClient(Host, Port);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(FromAddress, FromPassword);
                smtp.EnableSsl = EnableSSL;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;


                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(FromAddress);

                for (int i = 0; i < ml_To.Count; i++)
                {
                    if (IsValidMailID(ml_To[i]) == true)
                    {
                        mail.To.Add(ml_To[i]);
                    }
                }

                for (int i = 0; i < ml_CC.Count; i++)
                {
                    if (IsValidMailID(ml_CC[i]) == true)
                    {
                        mail.CC.Add(ml_CC[i]);
                    }
                }

                for (int i = 0; i < ml_BCC.Count; i++)
                {
                    if (IsValidMailID(ml_BCC[i]) == true)
                    {
                        mail.Bcc.Add(ml_BCC[i]);
                    }
                }

                mail.Subject = m_EMailSubject;
                mail.Body = m_EMailBody;
                mail.IsBodyHtml = true;

                for (int i = 0; i < ml_FileAttachmet.Count; i++)
                {
                    mail.Attachments.Add(new Attachment(ml_FileAttachmet[i]));
                }

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                m_LastErrorMessage = ex.Message + " Inner Exception : " + ex.InnerException.Message;
                return -1;
            }


            return 0;
        }

    }
}




