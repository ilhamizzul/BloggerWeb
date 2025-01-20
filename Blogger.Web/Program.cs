using Blogger.Web.Data;
using Blogger.Web.Repositories;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using dotenv.net;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BloggerDbContext>(options  =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BloggerDbConnectionString")
        //$"Server=${Environment.GetEnvironmentVariable("CONNECTION_DB_SERVER")};Database=${Environment.GetEnvironmentVariable("CONNECTION_DB_NAME")};Trusted_Connection=True;TrustServerCertificate=Yes"
        )
    );

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IBlogPostsRepository, BlogPostsRepository>();
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();

QuestPDF.Settings.License = LicenseType.Community;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
