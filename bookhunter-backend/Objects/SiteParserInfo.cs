using BookHunter_Backend.Domain.Models;

namespace BookHunter_Backend.Objects
{
    public class SiteParserInfo
    {
        public int Id { get; set; }

        public string SiteName { get; set; } = string.Empty;

        public string NameSelector { get; set; } = string.Empty;

        public string DescriptionSelector { get; set; } = string.Empty;

        public string YearSelector { get; set; } = string.Empty;

        public string ImageSelector { get; set; } = string.Empty;

        public string IsbnSelector { get; set; } = string.Empty;

        public string PageCountSelector { get; set; } = string.Empty;

        public string TagsSelector { get; set; } = string.Empty;

        public string AuthorsSelector { get; set; } = string.Empty;

        public string GenresSelector { get; set; } = string.Empty;

        public string SearchLinkFormat { get; set; } = string.Empty;

        public string BlockInSearchSelector { get; set; } = string.Empty;

        public string LinkInSearchSelector { get; set; } = string.Empty;
        
        public string NameInSearchSelector { get; set; } = string.Empty;

        public string Message { get; set; } = "ok";

        public static SiteParserInfo Success(SiteParser model)
        {
            return new SiteParserInfo()
            {
                Id = model.Id,
                SiteName = model.SiteName,
                NameSelector = model.NameSelector,
                DescriptionSelector = model.DescriptionSelector,
                YearSelector = model.YearSelector,
                ImageSelector = model.ImageSelector,
                IsbnSelector = model.IsbnSelector,
                PageCountSelector = model.PageCountSelector,
                TagsSelector = model.TagsSelector,
                AuthorsSelector = model.AuthorsSelector,
                GenresSelector = model.GenresSelector,
                SearchLinkFormat = model.SearchLinkFormat,
                BlockInSearchSelector = model.BlockInSearchSelector,
                LinkInSearchSelector = model.LinkInSearchSelector,
                NameInSearchSelector = model.NameInSearchSelector
            };
        }
        public static SiteParserInfo Fail(string message)
        {
            return new SiteParserInfo()
            {
                Message = message
            };
        }
    }
}