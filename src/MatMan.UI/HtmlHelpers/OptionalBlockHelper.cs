using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MatMan.Domain.Models;

namespace MatMan.UI.HTMLHelpers
{
    public static class OptionalBlockHelper
    {
        public static HtmlString CreateOptionalBlock(
            this IHtmlHelper html,
            string aspForValue,
            string aspForUnit,
            IEnumerable<Unit> units
        )
        {
            var result = new TagBuilder("div");

            var optionalText = new TagBuilder("div");
            optionalText.InnerHtml.Append("Опционально");

            result.InnerHtml.AppendHtml(optionalText);

            var inputsGroup = new TagBuilder("div");
            inputsGroup.Attributes.Add("class", "input-group border rounded p-2");

            var valueInput = new TagBuilder("input");
            valueInput.Attributes.Add("class", "form-control d-inline w-auto");
            valueInput.Attributes.Add("name", aspForValue);
            valueInput.Attributes.Add("id", aspForValue);
            valueInput.Attributes.Add("data-val", "true");
            valueInput.Attributes.Add("data-val-required", $"The {aspForValue} field is required.");
            valueInput.Attributes.Add("min", "0");
            valueInput.Attributes.Add("step", ".01");
            valueInput.Attributes.Add("type", "number");
            valueInput.Attributes.Add("placeholder", "Введите величину");

            inputsGroup.InnerHtml.AppendHtml(valueInput);

            var unitSelector = new TagBuilder("select");
            unitSelector.Attributes.Add("class", "form-select d-inline w-auto");
            unitSelector.Attributes.Add("asp-for", aspForUnit);

            foreach (var unit in units)
            {
                var option = new TagBuilder("option");
                option.Attributes.Add("value", $"{unit.ID}");
                option.InnerHtml.Append(unit.ShortName + ".");

                unitSelector.InnerHtml.AppendHtml(option);
            }

            inputsGroup.InnerHtml.AppendHtml(unitSelector);

            result.InnerHtml.AppendHtml(inputsGroup);

            return TagBuilderHelper.GetHtmlStringFrom(result);
        }
    }
}
