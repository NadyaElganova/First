using First.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace First.TagHelpers
{
    public enum MovieDatailsType
    {
        Full, Modal
    }

    [HtmlTargetElement("a", Attributes = "movie")]
    public class MovieDetailsTagHelper : TagHelper
    {
        public MovieDatailsType MyType { get; set; }
        private readonly IUrlHelperFactory urlHelperFactory;
        public MovieDetailsTagHelper(IUrlHelperFactory urlHelperFactory) 
        {
            this.urlHelperFactory = urlHelperFactory;
        }
        public Movie Movie { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext Context { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(Context);
            output.TagName = "a";
            output.Attributes.Add("class", "btn btn-primary");
            //output.Attributes.Add("href", $"/Home/Movie/{Cinema.imdbID}");

            string url;

            if(MyType == MovieDatailsType.Full)
            {
            url = urlHelper.ActionLink("Movie", "Home", new { id = Movie.imdbID });

            var i = new TagBuilder("i");

            if (Movie.Type == "game")
                i.AddCssClass("fa-solid fa-gamepad");
            else if (Movie.Type == "series")
                i.AddCssClass("fa-solid fa-tv");
            else if (Movie.Type == "movie")
                i.AddCssClass("fa-solid fa-film");

            output.Content.AppendHtml(i);
            output.Content.Append("Details");
            
            }
            else
            {
                url = urlHelper.ActionLink("MovieModal", "Home", new { id = Movie.imdbID });
                var i = new TagBuilder ("i");
                i.AddCssClass("fa-solid fa-eye");
                output.Content.AppendHtml(i);
            }

            output.Attributes.Add("href", url);
        }
    }
}