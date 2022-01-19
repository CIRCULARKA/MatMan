using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MatMan.UI.TagHelpers
{
    public class ShadowedpanelTagHelper : TagHelper
    {
        public static TagBuilder GetOutput()
        {
            var result = new TagBuilder("div");
            result.Attributes.Add("class", "shadow border rounded p-3 bg-light");
            result.Attributes.Add("style", "max-height: 2000px;");

            return result;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var tag = GetOutput();

            output.TagName = tag.TagName;
            output.Attributes.Add("class", tag.Attributes["class"]);
            output.Attributes.Add("style", tag.Attributes["style"]);
        }
    }
}
