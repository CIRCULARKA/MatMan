using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MatMan.UI.HTMLHelpers
{
    public static class TagBuilderHelper
    {
        public static HtmlString GetHtmlStringFrom(TagBuilder tagBuilder)
        {
            var writer = new StringWriter();
            tagBuilder.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }
    }
}
