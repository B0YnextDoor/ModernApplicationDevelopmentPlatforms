using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WEB_253502_KRASYOV.UI.TagHelpers
{
	[HtmlTargetElement("Pager")]
	public class Pager : TagHelper
	{
		private readonly LinkGenerator _linkGenerator;
		private readonly HttpContext _httpContext;
		public Pager(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor) { 
			_linkGenerator = linkGenerator;
			_httpContext = httpContextAccessor.HttpContext;
		}
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public string Category { get; set; }
		public bool Admin { get; set; } = false;

		private string GetPageUrl(int pageNumber)
		{
			if (Admin)
				return _linkGenerator.GetPathByPage(_httpContext, page: "/Index",
					values: new { area = "Admin", pageNo = pageNumber });
			
			return _linkGenerator.GetPathByAction(_httpContext, action: "Index",
					controller: "Product", values: new { pageNo = pageNumber, category = Category });
		}

		private TagBuilder ConfigureTag(TagBuilder ulTag, bool condition = true, bool type = false, int i = 0)
		{
			var liTag = new TagBuilder("li");
			liTag.AddCssClass(condition ? "page-item" : "page-item disabled");

			var aTag = new TagBuilder("a");
			aTag.AddCssClass("page-link");

			if (i == 0) aTag.Attributes["aria-label"] = !type ? "Previous" : "Next";
			else if(CurrentPage == i) liTag.AddCssClass("active");

			if(condition)
			{
				var url = GetPageUrl(i > 0 ? i : CurrentPage + (!type ? -1 : 1));
				aTag.Attributes["href"] = url;
				aTag.Attributes["data-ajax-url"] = url;
				aTag.Attributes["data-ajax-method"] = "GET";
			}

			if(i > 0) aTag.InnerHtml.AppendHtml(i.ToString());
			else
			{
				var arrow = new TagBuilder("i");
				arrow.AddCssClass($"fa-solid fa-angles-{(!type ? "left": "right")}");
				aTag.InnerHtml.AppendHtml(arrow);
			}
			liTag.InnerHtml.AppendHtml(aTag);
			ulTag.InnerHtml.AppendHtml(liTag);

			return ulTag;
		}
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "nav";
			output.Attributes.SetAttribute("aria-label", "navigation");

			var ulTag = new TagBuilder("ul");
			ulTag.AddCssClass("pagination");

			var prevAvaibale = CurrentPage > 1;
			var nextAvaibale = CurrentPage < TotalPages;

			ulTag = ConfigureTag(ulTag, condition: prevAvaibale);

			for (int i = 1; i <= TotalPages; i++)
			{
				ulTag = ConfigureTag(ulTag, i: i);
			}

			ulTag = ConfigureTag(ulTag, condition: nextAvaibale, type: true);

			output.Content.AppendHtml(ulTag);
		}
	}
}
