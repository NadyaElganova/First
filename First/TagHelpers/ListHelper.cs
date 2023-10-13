using Microsoft.AspNetCore.Razor.TagHelpers;

namespace First.TagHelpers
{
    public class ListHelper:TagHelper
    {
        public string[] Items { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            string listItems = "";
            foreach (string item in Items)
            {
                listItems += "<li>" + item + "</li>";
            }
            output.Content.SetHtmlContent(listItems);
        }
    }
}
