using Microsoft.AspNetCore.Razor.TagHelpers;

namespace First.TagHelpers
{
    public class EmailLinkTagHelper: TagHelper
    {
        public string Email { get; set; }
        public string MyTitle { get; set; }
        //    public override void Process(TagHelperContext context, TagHelperOutput output)
        //    {            
        //        output.TagName = "button";
        //        output.Content.Append("Nadya");
        //    }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            //output.Content.Append("Youtube");
            //output.Attributes.Add("href", "https://www.youtube.com");

            output.Content.Append(MyTitle ?? Email);
            output.Attributes.Add("href", $"mailto:{Email}");
        }
    }
}
