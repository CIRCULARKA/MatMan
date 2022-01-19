using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MatMan.Domain.Models;

namespace MatMan.UI.HTMLHelpers
{
    public static class TableHelper
    {
        public static HtmlString CreateTableOfComponents<TComponent>(
            this IHtmlHelper html,
            IEnumerable<TComponent> components,
            string deleteComponentActionName,
            string deletionRouteName,
            Func<TComponent, Configuration> getComponentConfiguration
        )
            where TComponent : NamedEntity
        {
            var table = new TagBuilder("table");
            table.Attributes.Add("class", "table");

            var tableBody = new TagBuilder("tbody");

            foreach (var component in components)
            {
                var nameTd = new TagBuilder("td");
                nameTd.Attributes.Add("class", "align-middle w-75");

                var nameTdContent = component.Name;

                if (getComponentConfiguration != null)
                {
                    var config = getComponentConfiguration(component);
                    if (config != null)
                        nameTdContent += $" ({config.Unit.ShortName}.)";
                }

                nameTd.InnerHtml.Append(nameTdContent);

                var deleteButtonTd = new TagBuilder("td");
                deleteButtonTd.Attributes.Add("class", "text-center");
                deleteButtonTd.InnerHtml.AppendHtml(
                    ButtonHelper.CreateDeleteButton(
                        html: html,
                        controllerName: typeof(TComponent).Name + "s",
                        actionName: deleteComponentActionName,
                        routeName: deletionRouteName,
                        routeData: component.ID.ToString()
                    )
                );

                var foldButtonTd = new TagBuilder("td");
                foldButtonTd.Attributes.Add("class", "text-end");
                foldButtonTd.InnerHtml.AppendHtml(
                    ButtonHelper.CreateFoldButton(html)
                );

                var tr = new TagBuilder("tr");
                tr.InnerHtml.AppendHtml(nameTd);
                // tr.InnerHtml.AppendHtml(foldButtonTd);
                tr.InnerHtml.AppendHtml(deleteButtonTd);

                tableBody.InnerHtml.AppendHtml(tr);
            }

            table.InnerHtml.AppendHtml(tableBody);

            return TagBuilderHelper.GetHtmlStringFrom(table);
        }
    }
}
