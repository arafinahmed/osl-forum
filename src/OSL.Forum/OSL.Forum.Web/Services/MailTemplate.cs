using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Forum.Web.Services
{
    public class MailTemplate : IMailTemplate
    {
        public (string Subject, string Body) AccountConfirmationTemplate(string name, string body)
        {
            var Subject = "Confirm your OSL Forum Account";
            var Body = $"Dear <b>{name}</b></br>" +
                $"<p>An OSL Forum Account has been created for you.</p>" +
                $"<p>Please confirm your account by clicking <a href=\"" + body + "\"> here</a></p>" +
                $"<p>If you didn't create an account in OSL Forum, please ignore this message.</p>" +
                $"</br></br>" +
                $"~</br>" +
                $"OSL Forum";
            return (Subject, Body);
        }

        public (string Subject, string Body) ForgotPasswordTemplate(string name, string body)
        {
            var Subject = "Reset password | OSL Forum";
            var Body = $"Dear <b>{name}</b></br>" +
                $"<p>We received a request to change your osl forum account.</p>" +
                $"<p>Please click on the link to set new password. <a href=\"" + body + "\"> Reset Password</a></p>" +
                $"<p>If you didn't make the password reset request in OSL Forum, please ignore this message.</p>" +
                $"</br></br>" +
                $"~</br>" +
                $"OSL Forum";

            return (Subject, Body);
        }
    }
}
