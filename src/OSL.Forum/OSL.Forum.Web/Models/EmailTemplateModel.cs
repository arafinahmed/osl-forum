using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSL.Forum.Web.Models
{
    public class EmailTemplateModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }

        public void AccoutnConfirmationTemplate(string name, string body)
        {
            Subject = "Confirm your OSL Forum Account";
            Body = $"Dear <b>{name}</b></br>" +
                $"<p>An OSL Forum Account has been created for you.</p>" +
                $"<p>Please confirm your account by clicking <a href=\"" + body + "\"> here</a></p>" +
                $"<p>If you didn't create an account in OSL Forum, please ignore this message.</p>" +
                $"</br></br>" +
                $"~</br>" +
                $"OSL Forum";
        }
    }
}