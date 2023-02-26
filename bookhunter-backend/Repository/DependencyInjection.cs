using BookHunter_Backend.Domain.Interfaces;
using BookHunter_Backend.Services;

namespace BookHunter_Backend.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            
            services.AddScoped<BookService>();
            services.AddScoped<HunterService>();
            
            return services;
        }
    }
}