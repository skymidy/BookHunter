using System.Data.Common;
using System.Net;
using BookHunter_Backend.Domain;
using BookHunter_Backend.Domain.Models;
using BookHunter_Backend.Repository;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "BookHunter", Version = "v1"}); });
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
});
builder.Services.AddRepository();
if (Environment.OSVersion.Platform == PlatformID.Win32NT)
{
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("WindowsConnection")
    ));
}
else
{
    var userSQL = Environment.GetEnvironmentVariable("SQL_USER");
    var paswordSQL = Environment.GetEnvironmentVariable("SQL_PASSWORD");
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    string.Format(builder.Configuration.GetConnectionString("DefaultConnection"), userSQL, paswordSQL)
    ));
}

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while migrating the database: " + ex.Message);
    }
}

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
}

// app.UseHttpsRedirection();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();

app.MapControllers();

app.Run();