using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookHunter_Backend.Domain.Models
{
    public class SiteParser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string SiteName { get; set; }

        public string NameSelector { get; set; }

        public string DescriptionSelector { get; set; }

        public string YearSelector { get; set; }

        public string ImageSelector { get; set; }

        public string IsbnSelector { get; set; }

        public string PageCountSelector { get; set; }

        public string TagsSelector { get; set; }

        public string AuthorsSelector { get; set; }

        public string GenresSelector { get; set; }

        public string SearchLinkFormat { get; set; }

        public string BlockInSearchSelector { get; set; }

        public string LinkInSearchSelector { get; set; }


        public string NameInSearchSelector { get; set; }
    }
}