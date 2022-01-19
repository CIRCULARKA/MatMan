using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MatMan.UI.HTMLHelpers
{
    public static class ButtonHelper
    {
        public static HtmlString CreateDeleteButton(
            this IHtmlHelper html, string controllerName,
            string actionName, string routeName = null, string routeData = null
        )
        {
            var a = new TagBuilder("a");
            a.Attributes.Add("class", "link-danger far fa-times-circle fa-2x");
            a.Attributes.Add("style", "text-decoration: none;");

            if (routeName != null && routeData != null)
                a.Attributes.Add("href", $"/{controllerName}/{actionName}?{routeName}={routeData}");

            return TagBuilderHelper.GetHtmlStringFrom(a);
        }

        public static HtmlString CreateFoldButton(this IHtmlHelper html)
        {
            var a = new TagBuilder("a");
            a.Attributes.Add("class", "link-secondary far fa-caret-square-down fa-2x");
            a.Attributes.Add("style", "text-decoration: none;");

            return TagBuilderHelper.GetHtmlStringFrom(a);
        }
    }
}
