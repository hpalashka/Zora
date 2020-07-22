using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text.Encodings.Web;

namespace Zora.Web.Helpers
{
    public static class FormGroupHelper
    {

        public static IHtmlContent FormGroupFor<TModel, TResult>(
            this IHtmlHelper<TModel> HtmlHelper,
            Expression<Func<TModel, TResult>> Expression)
        {

            using (var Writer = new StringWriter())
            {

                var Label = HtmlHelper.LabelFor(Expression, new { @class = "control-label" });
                var Editor = HtmlHelper.EditorFor(Expression, new { htmlAttributes = new { @class = "form-control" } });
                var ValidationMessage = HtmlHelper.ValidationMessageFor(Expression, null, new { @class = "text-danger" });

                Writer.Write("<div class=\"form-group\">");
                Label.WriteTo(Writer, HtmlEncoder.Default);
                Editor.WriteTo(Writer, HtmlEncoder.Default);
                ValidationMessage.WriteTo(Writer, HtmlEncoder.Default);
                Writer.Write("</div>");


                return new HtmlString(Writer.ToString());
            }
        }


        public static IHtmlContent FormGroupOptionFor<TModel,TResult>(
           this IHtmlHelper<TModel> HtmlHelper ,
           Expression<Func<TModel, TResult>> Expression,
           IEnumerable<SelectListItem> values)
        {

            using (var Writer = new StringWriter())
            {

                var Label = HtmlHelper.LabelFor(Expression, new { @class = "control-label" });
                var Editor = HtmlHelper.DropDownListFor(Expression, values, "Select a Task", new { @class = "form-control"  });
                var ValidationMessage = HtmlHelper.ValidationMessageFor(Expression, null, new { @class = "text-danger" });

                Writer.Write("<div class=\"form-group\">");
                Label.WriteTo(Writer, HtmlEncoder.Default);
                Editor.WriteTo(Writer, HtmlEncoder.Default);
                ValidationMessage.WriteTo(Writer, HtmlEncoder.Default);
                Writer.Write("</div>");
            //     < div class="form-group">
            //    <label asp-for="TaskId" class="control-label"></label>
            //    <select asp-for="TaskId" asp-items="TasksSelectList" class="form-control"></select>
            //    <span asp-validation-for="TaskId"></span>
            //</div>

                return new HtmlString(Writer.ToString());
            }
        }

    }
}
