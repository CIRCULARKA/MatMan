using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MatMan.UI.TagHelpers
{
    public class OverlayTagHelper : TagHelper
    {
        public string Header { get; set; }

        public string ID { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "div";
            output.Attributes.Add("class", "container");
            output.Attributes.Add("id", ID);
            output.Attributes.Add(
                "style",
                "background: rgba(0, 0, 0, 0.7); display: none;" +
                "position: fixed; height: 100%; width: 100%;" +
                "cursor: pointer; top: 0; left: 0; bottom: 0;"
            );

            var contentDiv = new TagBuilder("div");
            contentDiv.Attributes.Add("class", "col-8 bg-light my-5 p-3 rounded");

            var headerDiv = new TagBuilder("div");
            headerDiv.Attributes.Add("class", "fs-3 w-100 border-bottom");
            headerDiv.InnerHtml.Append(Header);

            contentDiv.InnerHtml.AppendHtml(contentDiv);

            var preDiv = new TagBuilder("div");
            preDiv.Attributes.Add("class", "col-2");

            var rowDiv = new TagBuilder("div");
            rowDiv.Attributes.Add("class", "row");

            output.PreContent.AppendHtml(preDiv);

            var postDiv = new TagBuilder("div");
            postDiv.Attributes.Add("class", "col-2");

            output.PostContent.AppendHtml(postDiv);
        }
    }
}
