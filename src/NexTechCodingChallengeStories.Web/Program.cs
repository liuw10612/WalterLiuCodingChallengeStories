using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CodingChallengeStories.Web.Services.CacheService;
using CodingChallengeStories.Web.Services.HttpServices;
using CodingChallengeStories.Web.Services.DataProvider;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();
// DI supporting services
builder.Services.AddSingleton(o=> new HttpService());
builder.Services.AddSingleton(o => new CachedDataService());
//builder.Services.AddSingleton(o => new StoryDataProvider(builder.GetRequiredService<ILogger<StoryDataProvider>>(), new DataService(), new CachedDataService()));
builder.Services.AddSingleton<StoryDataProvider>(provider =>
{
    return new StoryDataProvider(provider.GetRequiredService<ILogger<StoryDataProvider>>(), new HttpService(), new CachedDataService());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
