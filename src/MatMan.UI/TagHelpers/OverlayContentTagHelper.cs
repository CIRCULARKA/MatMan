using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MatMan.UI.TagHelpers
{
    public class OverlaycontentTagHelper : TagHelper
    {
        public string Header { get; set; }

        public string ID { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "div";
            output.Attributes.Add("class", "col-8 bg-light my-5 p-3 rounded");

            // var headerDiv = new TagBuilder("div");
            // headerDiv.Attributes.Add("class", "fs-3 w-100 border-bottom");
            // headerDiv.InnerHtml.Append(Header);

            // output.PreElement.AppendHtml(headerDiv);
        }
    }
}
