using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
//Authatnication kullanýcýyý giriþ yaparken tanýmlamak için kullanýlýr.
//Authorization ise kullanýcýnýn eriþim yetkisini belirler.
namespace BlogApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<BlogContext>(options =>
            {
                var config = builder.Configuration;
                var connectionString = config.GetConnectionString("sql_connection");
                options.UseSqlite(connectionString);
            });
             
            builder.Services.AddScoped<IPostRepository,EfPostRepository>();
            builder.Services.AddScoped<ITagRepository,EfTagRepository>();
            builder.Services.AddScoped<ICommentRepository,EfCommentRepository>();
            builder.Services.AddScoped<IUserRepository,EfUserRepository>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Users/Login";
            });

            var app = builder.Build();
            SeedData.TestVerileriniDoldur(app);

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "post_details",
                pattern: "posts/details/{url}",
                defaults: new { controller = "Posts", action = "Details" });

            app.MapControllerRoute(
                name: "posts_by_tag",
                pattern: "posts/tag/{tagName}",
                defaults: new { controller = "Posts", action = "Index" });

            app.MapControllerRoute(
                name: "user_profile",
                pattern: "profile/{username}",
                defaults: new { controller = "Users", action = "Profile" }
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Posts}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
