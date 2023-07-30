using CodingChallengeStories.Web.Services.CacheService;
using CodingChallengeStories.Web.Services.HttpServices;
using CodingChallengeStories.Web.Services.DataProvider;
using CodingChallengeStories.Web.Services.Model;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();
// DI supporting services
builder.Services.AddSingleton(o => new HttpService());
// cache config info
int iCachePages = int.Parse(builder.Configuration.GetSection("AppConfig").GetSection("CachePages").Value);
int iCachePageSize = int.Parse(builder.Configuration.GetSection("AppConfig").GetSection("CachePageSize").Value);
int iCacheExpireHours = int.Parse(builder.Configuration.GetSection("AppConfig").GetSection("CacheExpireHours").Value);
builder.Services.AddSingleton(o => new CachedDataService(iCachePages, iCachePageSize, iCacheExpireHours));
builder.Services.AddSingleton<StoryDataProvider>(provider =>
{
    return new StoryDataProvider(provider.GetRequiredService<ILogger<StoryDataProvider>>(), 
        new HttpService(), 
        new CachedDataService(iCachePages, iCachePageSize, iCacheExpireHours), 
        new CacheInfo(iCachePages, iCachePageSize, iCacheExpireHours));
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
