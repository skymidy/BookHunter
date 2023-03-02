using BookHunter_Backend.Domain.Models;

namespace BookHunter_Backend.Objects
{
    public class SiteParserInput
    {
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

        public void copyToModel(SiteParser model)
        {
            model.SiteName = SiteName;
            model.NameSelector = NameSelector;
            model.DescriptionSelector = DescriptionSelector;
            model.YearSelector = YearSelector;
            model.ImageSelector = ImageSelector;
            model.IsbnSelector = IsbnSelector;
            model.PageCountSelector = PageCountSelector;
            model.TagsSelector = TagsSelector;
            model.AuthorsSelector = AuthorsSelector;
            model.GenresSelector = GenresSelector;
            model.SearchLinkFormat = SearchLinkFormat;
            model.BlockInSearchSelector = BlockInSearchSelector;
            model.LinkInSearchSelector = LinkInSearchSelector;
            model.NameInSearchSelector = NameInSearchSelector;
        }
    }
}