using AngleSharp.Html.Parser;
using BookHunter_Backend.Domain.Models;

namespace BookHunter_Backend.Services
{
    public class HunterService
    {
        private readonly BookService _bookService;

        public HunterService(BookService bookService)
        {
            _bookService = bookService;
        }

        public async Task HuntBooksList()
        {
            var books = new List<Book>();
            const string allBooksUrl = "https://app.dbdesigner.net/designer/schema/604590";
            const string blockSelector = "ul.products li.entry";
            const string nameSelector = "h2 a";

            try
            {
                using (var client = new HttpClient())
                {
                    Console.WriteLine("Start hunt...");
                    using (var res = await client.GetAsync(allBooksUrl))
                    {
                        Console.WriteLine("Got a prey!");
                        using (var content = res.Content)
                        {
                            Console.WriteLine("Start to carve some meat...");
                            var data = await content.ReadAsStringAsync();

                            var parser = new HtmlParser();
                            var document = parser.ParseDocument(data);

                            Console.WriteLine("Start to cut some steaks...");
                            books.AddRange(from element in document.QuerySelectorAll(blockSelector)
                            select element.QuerySelector(nameSelector)?.TextContent ?? string.Empty
                            into name
                            where name != string.Empty
                            select new Book
                            {
                                Title = name
                            });
                        }
                    }
                }

                await _bookService.AddBooks(books);

                Console.WriteLine("Time to eat!");
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception Hit------------");
                Console.WriteLine(exception);
            }

            Console.WriteLine("Hunt ended!");
        }
    }
}