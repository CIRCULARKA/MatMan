using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MatMan.UI.TagHelpers;

namespace MatMan.UI.HTMLHelpers
{
    public static class UserMessagingHelper
    {
        public static HtmlString CreateUserMessagesBlock(
            this IHtmlHelper html, string error, string success
        )
        {
            if ((error == null || error == "") && (success == null || success == ""))
                return null;

            var result = new TagBuilder("div");
            result.Attributes.Add("class", "col-12");

            TagBuilder panel = ShadowedpanelTagHelper.GetOutput();

            var errorMessage = new TagBuilder("div");
            errorMessage.Attributes.Add("class", "fs-5 text-danger word-break");
            errorMessage.InnerHtml.Append(error);

            var successMessage = new TagBuilder("div");
            successMessage.Attributes.Add("class", "fs-5 text-success word-break");
            successMessage.InnerHtml.Append(success);

            panel.InnerHtml.AppendHtml(errorMessage);
            panel.InnerHtml.AppendHtml(successMessage);

            result.InnerHtml.AppendHtml(panel);

            return TagBuilderHelper.GetHtmlStringFrom(result);
        }
    }
}
