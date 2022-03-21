using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Forum.Web.Services
{
    public interface IMailTemplate
    {
        (string Subject, string Body) AccountConfirmationTemplate(string name, string body);
        (string Subject, string Body) ForgotPasswordTemplate(string name, string body);
    }
}
