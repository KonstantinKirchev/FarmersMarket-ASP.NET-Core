namespace FarmersMarket.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public static class HtmlHelpers
    {
        public static HtmlString EmailTextBox(this HtmlHelper helper, string email)
        {
            TagBuilder builder = new TagBuilder("a");
            builder.MergeAttribute("href", $"mailto: {email}");
            //builder.SetInnerText(email);
            return new HtmlString(builder.RenderEndTag().ToString());
        }

        public static HtmlString Image(this HtmlHelper helper, string imgUrl, string alt)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.AddCssClass("img-responsive");
            builder.MergeAttribute("src", imgUrl);
            builder.MergeAttribute("alt", alt);
            return new HtmlString(builder.RenderSelfClosingTag().ToString());
        }

        public static HtmlString CustomImage(this HtmlHelper helper, string imgUrl, string alt, string customClass)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.AddCssClass(customClass);
            builder.MergeAttribute("src", imgUrl);
            builder.MergeAttribute("alt", alt);
            return new HtmlString(builder.RenderSelfClosingTag().ToString());
        }

        public static HtmlString Image(this HtmlHelper helper, string imgUrl, string alt, string style)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.AddCssClass("img-responsive");
            builder.MergeAttribute("src", imgUrl);
            builder.MergeAttribute("alt", alt);
            builder.MergeAttribute("style", style);
            return new HtmlString(builder.RenderSelfClosingTag().ToString());
        }

        public static HtmlString Submit(this HtmlHelper helper, string value)
        {
            TagBuilder builder = new TagBuilder("input");
            builder.AddCssClass("btn btn-primary");
            builder.MergeAttribute("type", "submit");
            builder.MergeAttribute("value", value);
            return new HtmlString(builder.RenderSelfClosingTag().ToString());
        }

        public static HtmlString HyperLink(this HtmlHelper helper, string link, string icon)
        {
            TagBuilder builder = new TagBuilder("a");
            builder.MergeAttribute("href", link);
            builder.MergeAttribute("target", "_blank");
            TagBuilder builderIcon = new TagBuilder("i");
            builderIcon.AddCssClass(icon);
            //builder.SetInnerText(builderIcon.ToString());
            return new HtmlString(builder.ToString());
        }
    }
}
