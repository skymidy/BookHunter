using System.Text.RegularExpressions;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using BookHunter_Backend.Domain.Interfaces;
using BookHunter_Backend.Domain.Models;
using BookHunter_Backend.Utility;
using System.Drawing;
using BookHunter_Backend.Objects;

namespace BookHunter_Backend.Services
{
    public class HunterService
    {
        private readonly BookService _bookService;
        private readonly ISiteParserRepository _siteParserRepository;

        public HunterService(BookService bookService, ISiteParserRepository siteParserRepository)
        {
            _bookService = bookService;
            _siteParserRepository = siteParserRepository;
        }

        private async Task<IHtmlDocument> GetDocumentFromLink(HttpClient client, string link)
        {
            using (var res = await client.GetAsync(link))
            {
                var content = res.Content;
                var data = await content.ReadAsStringAsync();
                var parser = new HtmlParser();
                return parser.ParseDocument(data);
            }
        }

        private async Task UpdateBookFromDocument(IHtmlDocument document, Book book, SiteParser site, HttpClient client)
        {
            // if (book.Description == "")
                book.Description = document.QuerySelector(site.DescriptionSelector)?.TextContent.Trim() ?? "";
            // if (book.PagesCount == "")
                book.PagesCount = document.QuerySelector(site.PageCountSelector)?.TextContent.Trim() ?? "";
            // if (book.ISBN == "")
                book.ISBN = document.QuerySelector(site.IsbnSelector)?.TextContent.Trim() ?? "";
            if (book.PublishedDate == DateTime.UnixEpoch || true)
            {
                try
                {
                    book.PublishedDate =
                    DateTime.Parse(document.QuerySelector(site.DescriptionSelector)?.TextContent.Trim() ?? "");
                }
                catch (FormatException e)
                {
                    //pass
                }
            }

            if (book.ImageId == "")
            {
                var url = document.QuerySelector(site.ImageSelector)?.GetAttribute("src") ?? "";
                if (url != "")
                {
                    HttpResponseMessage response =
                    await client.GetAsync(url);
                    byte[] imageBytes;
                    if (response.IsSuccessStatusCode)
                    {
                        imageBytes = await response.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        throw new Exception("Failed to retrieve product image.");
                    }

                    var type = response.Content.Headers.ContentType?.ToString() ?? "image/jpeg";

                    book.ImageId = "data:" + type + ";base64," + Convert.ToBase64String(imageBytes);
                }
            }

            await _bookService.UpdateBook(book);

            //Authors
            if (site.AuthorsSelector != "")
                foreach (var element in document.QuerySelectorAll(site.AuthorsSelector))
                {
                    await _bookService.AddAuthorToBook(book.Id, Regex.Replace(element.TextContent, @"([\,\*])", "").Trim());
                }

            //Genres
            if (site.GenresSelector != "")
                foreach (var element in document.QuerySelectorAll(site.GenresSelector))
                {
                    await _bookService.AddGenreToBook(book.Id, Regex.Replace(element.TextContent, @"([\,\*])", "").Trim());
                }

            //Tags
            if (site.TagsSelector != "")
                foreach (var element in document.QuerySelectorAll(site.TagsSelector))
                {
                    await _bookService.AddTagToBook(book.Id, Regex.Replace(element.TextContent, @"([\,\*])", "").Trim());
                }
        }

        private IEnumerable<SiteParserInfo> ModelToInfoListConverter(IEnumerable<SiteParser> siteParsers)
        {
            var result = new List<SiteParserInfo>();
            foreach (var siteParser in siteParsers)
            {
                result.Add(SiteParserInfo.Success(siteParser));
            }

            return result;
        }

        public void AddNewHuntingSite(SiteParserInput siteParserInput)
        {
            var siteParser = new SiteParser();
            siteParserInput.copyToModel(siteParser);
            _siteParserRepository.Add(siteParser);
        }

        public async Task<SiteParserInfo> GetHuntingSite(int id)
        {
            var model = await _siteParserRepository.GetByIdAsync(id);
            if (model == null)
                return SiteParserInfo.Fail("Database Error");
            return SiteParserInfo.Success(model);
        }

        public async Task<IEnumerable<SiteParserInfo>> GetAllHuntingSitesAsync()
        {
            return ModelToInfoListConverter(await _siteParserRepository.GetAllAsync());

        }

        public async Task<IEnumerable<SiteParserInfo>> GetPaginated(int pageNumber, int pageSize = 10)
        {
            return ModelToInfoListConverter(await _siteParserRepository.GetPaginated(pageNumber, pageSize));
        }

        public async Task<IEnumerable<SiteParserInfo>> GetSitesByName(string title)
        {
            return ModelToInfoListConverter(await _siteParserRepository.GetSitesByName(title));
        }

        #region hunting

        public async Task HuntBookDetails()
        {
            var sites = await _siteParserRepository.GetAllAsync();
            var books = await _bookService.GetAllAsync();
            foreach (var site in sites)
            {
                await HuntBookDetailsTask(site);
            }
        }

        public async Task HuntBookDetailsFromSite(int id)
        {
            var site = await _siteParserRepository.GetByIdAsync(id);
            await HuntBookDetailsTask(site);
        }

        private async Task HuntBookDetailsTask(SiteParser site)
        {
            Console.WriteLine("Check site {0}", site.Id);
            var books = await _bookService.GetAllModelsAsync();
            using (var client = new HttpClient())
            {
                foreach (var book in books)
                {
                    Console.WriteLine("Check book {0}", book.Id);
                    var document =
                    await GetDocumentFromLink(client, string.Format(site.SearchLinkFormat, book.Title));
                    Console.WriteLine(site.SearchLinkFormat, book.Title);
                    foreach (var element in document.QuerySelectorAll(site.BlockInSearchSelector))
                    {
                        Console.WriteLine("Check book search quarry");
                        var name = element.QuerySelector(site.NameInSearchSelector)?.TextContent ??
                                   string.Empty;
                        if (name == string.Empty || StringCompare.CompareStrings(name, book.Title) < 40)
                            continue;
                        var link = element.QuerySelector(site.LinkInSearchSelector)?.GetAttribute("href") ?? "";
                        if (link[0] == '/')
                        {
                            link = "https://" + site.SiteName + link;
                        }
                        else if (link == "")
                        {
                            continue;
                        }
                        
                        Console.WriteLine("Update book {0} by link {1}", book.Id, link);
                        var documentBook = await GetDocumentFromLink(client,
                        link);
                        await UpdateBookFromDocument(documentBook, book, site, client);
                        break;
                    }
                }
            }
        }

        public async Task HuntBooksList()
        {
            const string allBooksUrl = "https://igraslov.store/shop/?products-per-page=all";
            const string authorPattern = @"[\p{L}\-]+(\*?\.?)(\s)(?:\s?[\p{Lu}\p{L}\p{M}*]\.)+(\,?)+(\s?)";
            // const string allBooksUrl = "https://web.archive.org/web/20221004141452/https://igraslov.store/shop/";
            const string blockSelector = "ul.products li.entry";
            const string nameSelector = "h2 a";

            try
            {
                using (var client = new HttpClient())
                {
                    using (var res = await client.GetAsync(allBooksUrl))
                    {
                        var content = res.Content;
                        var data = await content.ReadAsStringAsync();
                        var parser = new HtmlParser();
                        var document = parser.ParseDocument(data);

                        //select books blocks
                        foreach (var element in document.QuerySelectorAll(blockSelector))
                        {
                            var name = element.QuerySelector(nameSelector)?.TextContent ?? string.Empty;

                            //exclude NO-name books
                            if (name.Length == 0) continue;

                            var matches = Regex.Matches(name, authorPattern).ToArray();
                            var tmpName = Regex.Replace(name, authorPattern, "");

                            //Check if the name of the book is just a name of person
                            if (tmpName.Length == 0) matches = Array.Empty<Match>();
                            else name = tmpName.Trim();

                            //Add book to DB
                            var book = new Book {Title = name};
                            var bookEntry = _bookService.AddBook(book);

                            // Add book authors if there is any
                            foreach (var author in matches)
                            {
                                var authorString = Regex.Replace(author.Value, @"([\,\*])", "");
                                await _bookService.AddAuthorToBook(bookEntry.Id, authorString.Trim());
                            }
                        }

                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception Hit at HunterService.HuntBooksList------------");
                Console.WriteLine(exception);
            }
        }

        #endregion
    }
}