// Decompiled with JetBrains decompiler
// Type: wServiceSMS_SGPS.Components.CASPEmail
// Assembly: wServiceSMS_SGPS, Version=1.0.2466.24030, Culture=neutral, PublicKeyToken=null
// MVID: 3BA2DECD-CDC5-43B8-8B15-B5527AC47789
// Assembly location: C:\projectos\WSSMS\WSSMS\bin\wServiceSMS_SGPS.dll

using System.Net.Mail;

namespace wServiceSMS_SGPS.Components
{
    public class CASPEmail
    {
        protected string strSMTPService;

        public string SMTPService
        {
            set => strSMTPService = value;
        }

        public void SendEmailMessage(
            string psTo,
            string psFromName,
            string psFrom,
            string psSubject,
            string psBody)
        {
            using (var mailSender = new SmtpClient())
            {
                if (psTo.Trim().Length <= 0)
                    return;

                mailSender.Host = strSMTPService;
                using (var mm = new MailMessage()
                {
                    From = new MailAddress(psFrom, psFromName),
                    To = { new MailAddress(psTo, psTo) },
                    Subject = psSubject,
                    Body = psBody
                })
                {
                    mailSender.Send(mm);
                }
            }
        }
    }
}