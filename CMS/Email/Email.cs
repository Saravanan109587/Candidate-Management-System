using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    public class SendEmail
    {



        public string ThreadMail(List<byte[]> Attachment, List<string> AttachmentFileName, string Subject,string body)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {   
                        var Msg = new MailMessage();
                        Msg.Body = "<h3>Hi Saravanan,</h3><br/>";
                        Msg.To.Add("nsaravanan@kmitsolutions.com");
                        Msg.Subject = Subject;
                        if (body != null)
                        {
                            Msg.Body += body;
                        }
                        else
                        { 
                            Msg.Body += "You have new suggesstion request in Candidate management system";

                        }
                       
                        //Msg.Attachments.Add(new Attachment(System.Web.Hosting.HostingEnvironment.MapPath("~/Files/e.pdf")));
                        if (AttachmentFileName.Count>0)
                        {
                           for(var i=0;i<Attachment.Count;i++){
                               Msg.Attachments.Add(new Attachment(new MemoryStream(Attachment[i]), AttachmentFileName[i]));
                            }                           
                        }
                        
                        Msg.IsBodyHtml = true;
                        var smtp = new SmtpClient();
                        Msg.From = new MailAddress("info@kmitsolutions.com");
                        Msg.CC.Add("info@kmitsolutions.com");
  
                    //smtp.Host = Host;
                    //smtp.EnableSsl = EnableSSL;
                    //System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    //NetworkCred.UserName = UserName;
                    //NetworkCred.Password = Password;
                    //smtp.UseDefaultCredentials = true;
                    //smtp.Credentials = NetworkCred;
                    //smtp.Port = Convert.ToInt32(Port);

 
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = "info@kmitsolutions.com";
                    NetworkCred.Password = "kminfo@123";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                     smtp.Send(Msg);
                 
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "Success";
        }
    }
}
