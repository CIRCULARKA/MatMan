using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MatMan.UI.Controllers.Filters;

namespace MatMan.UI.Controllers
{
    [TypeFilter(typeof(AuthorizationFilter))]
    public class ApplicationController : Controller
    {
        [NonAction]
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.AppDescription = "Калькулятор материалов";
            ViewBag.AppVersion = "Версия: 1.2";
            ViewBag.DeveloperContacts = "Связь с разработчиком: ruslan.getmail@gmail.com";
        }

        protected string SuccessMessage
        {
            get => ViewData["successMessage"] as string;
            set {
                ViewData["successMessage"] = value;
                ViewData["errorMessage"] = string.Empty;
            }
        }

        protected string ErrorMessage
        {
            get => ViewData["errorMessage"] as string;
            set {
                ViewData["errorMessage"] = value;
                ViewData["successMessage"] = string.Empty;
            }
        }

        protected string BuildErrorMessage(string startMessage, IEnumerable<string> elements)
        {
            var listedElements = elements.ToList();

            var errorMessageBuilder = new StringBuilder($"{startMessage} (");
            for (int i = 0; i < listedElements.Count; i++)
            {
                errorMessageBuilder.Append($"\"{listedElements[i]}\"");
                if (i < listedElements.Count - 1)
                    errorMessageBuilder.Append(", ");
                else
                    errorMessageBuilder.Append(')');
            }

            return errorMessageBuilder.ToString();
        }

    }
}
