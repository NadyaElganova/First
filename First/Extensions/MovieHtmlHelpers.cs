using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace First.Extensions
{
    public static class MovieHtmlHelpers
    {
        //public static IHtmlContent MovieLink(this IHtmlHelper htmlHelper)
        //{
        //    string link = "<a href=\"https://www.youtube.com\">Youtube</a>";
        //    return new HtmlString(link);
        //}

        //public static IHtmlContent MovieLink(this IHtmlHelper htmlHelper)
        //{
        //    string link = "<script>window.location.href=\"https://www.youtube.com\"</script>";
        //    return new HtmlString(link);
        //}

        //public static IHtmlContent EmailLink(this IHtmlHelper htmlHelper, string mail, string title=null)
        //{
        //    string link = $"<a href=\"mailto:{mail}\">{title ?? mail}</a>";
        //    return new HtmlString(link);
        //}

        public static IHtmlContent EmailLink(this IHtmlHelper htmlHelper, string mail, string title = null)
        {
            var link = new TagBuilder("a");
            link.InnerHtml.Append(title ?? mail);
            link.Attributes.Add("href", $"mailto:{mail}");
            link.AddCssClass( "text-danger" );
            return link;
        }
    }
}
